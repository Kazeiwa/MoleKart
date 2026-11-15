using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RaceTrack;
using UnityEngine;

namespace Misc;

public class FunMaker : FunMakerConfig
{
	private enum Phase
	{
		A,
		B,
		C
	}

	private class Nested
	{
		internal static readonly FunMaker Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new FunMaker();
		}
	}

	public class TrickInfo
	{
		private bool shouldCheckToFinalRush;

		public Car Car { get; set; }

		public float ExpectedSpeed { get; set; }

		public bool IsVisible { get; set; }

		public float FinishedDistance { get; set; }

		public float DistanceInLapFromGoal { get; set; }

		public int Laps { get; set; }

		public int Rank { get; set; }

		public float NormalSpeed { get; set; }

		public bool ShouldCheckToFinalRush
		{
			get
			{
				return shouldCheckToFinalRush;
			}
			set
			{
				shouldCheckToFinalRush = value;
			}
		}

		public void SetExpectedSpeed(float speed)
		{
			ExpectedSpeed = speed;
			Car.MaxSpeed = speed;
			Car.LimitSpeed(speed);
			if (ExpectedSpeed > Car.GetConfigMaxSpeed)
			{
				Car.MotorForceScale = 1.5f;
			}
			else
			{
				Car.MotorForceScale = 1f;
			}
			if (ExpectedSpeed > Car.GetConfigMaxSpeed)
			{
				Car.MaxTurnScale = 3.5f;
			}
			else
			{
				Car.MaxTurnScale = 1f;
			}
		}
	}

	private int lapCount;

	private CrudeElapsedTimer timer;

	private IDictionary<Car, TrickInfo> npcCarTrickInfos = new Dictionary<Car, TrickInfo>();

	private Phase phase;

	private Car heroCar;

	private TrickInfo heroTrickInfo;

	public static FunMaker Instance => Nested.Instance;

	public bool Active { get; set; }

	public int TrackDifficulty { get; set; }

	public float MinSpeedVisiable => base.VisibleSpeedIntervals[TrackDifficulty].Min;

	public float MaxSpeedVisiable => base.VisibleSpeedIntervals[TrackDifficulty].Max;

	public float MinSpeedInvisiable => base.InvisibleSpeedIntervals[TrackDifficulty].Min;

	public float MaxSpeedInvisiable => base.InvisibleSpeedIntervals[TrackDifficulty].Max;

	private FunMaker()
	{
		Active = false;
		TrackDifficulty = 1;
		FunMakerConfigTextAssetReader funMakerConfigTextAssetReader = new FunMakerConfigTextAssetReader(this, "Config/FunMakerConfig");
		funMakerConfigTextAssetReader.Read();
		timer = new CrudeElapsedTimer(base.RefreshTimeLength);
	}

	public void StartState()
	{
		Debug.Log("FunMaker Start");
		Reset();
		lapCount = RaceModeFactory.CurrentRaceMode.TotalLap();
		phase = Phase.A;
		TrackDifficulty = LocalUserInfo.Instance.TrackLevel;
		foreach (NPCCar npcCar in RaceWorld.Instance.NpcCars)
		{
			TrickInfo trickInfo = new TrickInfo();
			trickInfo.Car = npcCar;
			trickInfo.IsVisible = true;
			trickInfo.ExpectedSpeed = npcCar.MaxSpeed;
			trickInfo.NormalSpeed = npcCar.MaxSpeed;
			trickInfo.Rank = npcCar.ID;
			npcCarTrickInfos.Add(npcCar, trickInfo);
		}
		heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
		heroTrickInfo = new TrickInfo();
		heroTrickInfo.Car = heroCar;
		heroTrickInfo.Rank = heroCar.ID;
	}

	public void UpdateState()
	{
		if (!Active)
		{
			return;
		}
		timer.Advance(Time.deltaTime);
		if (timer.TimeOutCount > 0)
		{
			RefreshTrickInfo();
			if (phase == Phase.A)
			{
				UpdatePhaseA();
			}
			else if (phase == Phase.B)
			{
				UpdatePhaseB();
			}
			else if (phase == Phase.C)
			{
				UpdatePhaseC();
			}
			timer.ResetTimeOutCount();
		}
	}

	public void UninitState()
	{
		Debug.Log("FunMaker Uninit");
		Reset();
	}

	private void UpdatePhaseA()
	{
		if (HeroCarHasArrivedAtCheckPoint())
		{
			OnHeroCarArrivesCheckPoint();
			phase = Phase.B;
			return;
		}
		foreach (TrickInfo value in npcCarTrickInfos.Values)
		{
			ControlSpeedNpcCarInPhaseA(value);
		}
	}

	private void UpdatePhaseB()
	{
		foreach (TrickInfo value in npcCarTrickInfos.Values)
		{
			if (value.ShouldCheckToFinalRush && value.DistanceInLapFromGoal < base.FinalRushDistance)
			{
				float num = MaxSpeed(value.IsVisible);
				SetClampedExpectedSpeedOfNpcCar(value, num + base.FinalRushSpeedDelta);
			}
			else if (!SpeedIsPermitted(value.IsVisible, value.ExpectedSpeed))
			{
				SetClampedExpectedSpeedOfNpcCar(value, value.ExpectedSpeed);
			}
		}
	}

	private void UpdatePhaseC()
	{
		foreach (TrickInfo value in npcCarTrickInfos.Values)
		{
			if (!SpeedIsPermitted(value.IsVisible, value.ExpectedSpeed))
			{
				SetClampedExpectedSpeedOfNpcCar(value, value.ExpectedSpeed);
			}
		}
	}

	private void Reset()
	{
		Active = false;
		npcCarTrickInfos.Clear();
		timer.Reset();
		phase = Phase.A;
	}

	private void SetClampedExpectedSpeedDelta(TrickInfo info, float speedDelta)
	{
		if (info.IsVisible)
		{
			info.SetExpectedSpeed(ClampSpeedVisible(info.ExpectedSpeed, speedDelta));
		}
		else
		{
			info.SetExpectedSpeed(ClampSpeedInvisible(info.ExpectedSpeed, speedDelta));
		}
	}

	private float ClampSpeed(TrickInfo info, float speed)
	{
		return (!info.IsVisible) ? ClampSpeedInvisible(speed) : ClampSpeedVisible(speed);
	}

	private float MinSpeed(bool isVisile)
	{
		return (!isVisile) ? MinSpeedInvisiable : MinSpeedVisiable;
	}

	private float MaxSpeed(bool isVisile)
	{
		return (!isVisile) ? MaxSpeedInvisiable : MaxSpeedVisiable;
	}

	private bool SpeedIsPermitted(bool isVisile, float speed)
	{
		return speed < MinSpeed(isVisile) || speed > MaxSpeed(isVisile);
	}

	private float ClampSpeedVisible(float speed)
	{
		return Mathf.Clamp(speed, MinSpeedVisiable, MaxSpeedVisiable);
	}

	private float ClampSpeedVisible(float speed, float deltaSpeed)
	{
		return ClampSpeedVisible(speed + deltaSpeed);
	}

	private float ClampSpeedInvisible(float speed)
	{
		return Mathf.Clamp(speed, MinSpeedInvisiable, MaxSpeedInvisiable);
	}

	private float ClampSpeedInvisible(float speed, float deltaSpeed)
	{
		return ClampSpeedInvisible(speed + deltaSpeed);
	}

	private bool HeroCarHasArrivedAtCheckPoint()
	{
		return lapCount == heroTrickInfo.Laps && heroTrickInfo.DistanceInLapFromGoal < base.CheckPointRatio * RaceWorld.Instance.RacingLine.LapLength;
	}

	private void OnHeroCarArrivesCheckPoint()
	{
		if (heroTrickInfo.Rank == 1)
		{
			phase = Phase.B;
			{
				foreach (TrickInfo value in npcCarTrickInfos.Values)
				{
					SpeedUpNpcCar(value);
					value.ShouldCheckToFinalRush = true;
				}
				return;
			}
		}
		phase = Phase.C;
		if (heroTrickInfo.Rank <= 1)
		{
			return;
		}
		foreach (TrickInfo value2 in npcCarTrickInfos.Values)
		{
			if (value2.Rank < heroTrickInfo.Rank)
			{
				SlowDownNpcCar(value2);
			}
		}
	}

	private float RandomSpeed()
	{
		return Random.Range(MinSpeedVisiable, MaxSpeedVisiable);
	}

	private void RefreshTrickInfo()
	{
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData((ulong)heroCar.ID, ref data);
		TrickInfo trickInfo = heroTrickInfo;
		trickInfo.FinishedDistance = data.distanceFromBeginning;
		trickInfo.DistanceInLapFromGoal = data.distanceInLapFromGoal;
		trickInfo.Rank = data.Ranking;
		trickInfo.Laps = data.CurLaps;
		foreach (TrickInfo value in npcCarTrickInfos.Values)
		{
			RaceSceneData.GetPlayerData((ulong)value.Car.ID, ref data);
			value.FinishedDistance = data.distanceFromBeginning;
			value.DistanceInLapFromGoal = data.distanceInLapFromGoal;
			value.Rank = data.Ranking;
			value.Laps = data.CurLaps;
			float x = value.FinishedDistance - heroTrickInfo.FinishedDistance;
			value.IsVisible = base.VisibleInterval.HitTest(x);
		}
	}

	private void SetClampedExpectedSpeedOfNpcCar(TrickInfo info, float speed)
	{
		if (info.IsVisible)
		{
			info.SetExpectedSpeed(ClampSpeedVisible(speed));
		}
		else
		{
			info.SetExpectedSpeed(ClampSpeedInvisible(speed));
		}
	}

	private void ControlSpeedNpcCarInPhaseA(TrickInfo info)
	{
		float offset = info.FinishedDistance - heroTrickInfo.FinishedDistance;
		switch (GetSpeedTreatment(heroTrickInfo.Rank, info.Rank))
		{
		case SpeedTreatment.Down:
			SlowDownNpcCar(info, offset);
			break;
		case SpeedTreatment.Up:
			SpeedUpNpcCar(info, offset);
			break;
		case SpeedTreatment.Normal:
			UseNormalSpeed(info);
			break;
		}
	}

	private void SlowDownNpcCar(TrickInfo info)
	{
		float offset = info.FinishedDistance - heroTrickInfo.FinishedDistance;
		SlowDownNpcCar(info, offset);
	}

	private void SlowDownNpcCar(TrickInfo info, float offset)
	{
		if (offset > 0f)
		{
			float speedDelta = ((offset < base.SlowDownThreshold1) ? base.SlowDownSpeedDelta1 : ((!(offset < base.SlowDownThreshold2)) ? base.SlowDownSpeedDelta3 : base.SlowDownSpeedDelta2));
			SetClampedExpectedSpeedDelta(info, speedDelta);
		}
	}

	private void SpeedUpNpcCar(TrickInfo info)
	{
		float offset = info.FinishedDistance - heroTrickInfo.FinishedDistance;
		SpeedUpNpcCar(info, offset);
	}

	private void SpeedUpNpcCar(TrickInfo info, float offset)
	{
		if (offset < 0f)
		{
			float speedDelta = ((offset > base.SpeedUpThreshold1) ? base.SpeedUpSpeedDelta1 : ((!(offset > base.SpeedUpThreshold2)) ? base.SpeedUpSpeedDelta3 : base.SpeedUpSpeedDelta2));
			SetClampedExpectedSpeedDelta(info, speedDelta);
		}
	}

	private void UseNormalSpeed(TrickInfo info)
	{
		float expectedSpeed = ClampSpeedVisible(info.NormalSpeed);
		info.SetExpectedSpeed(expectedSpeed);
	}
}
