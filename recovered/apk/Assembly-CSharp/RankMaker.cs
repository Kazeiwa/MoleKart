using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Misc;
using RaceTrack;
using UnityEngine;

public class RankMaker
{
	private class Nested
	{
		internal static readonly RankMaker Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new RankMaker();
		}
	}

	private const int LapCount = 3;

	private float distanceBiasMax = 15f;

	private IList<NPCCar> cars = new List<NPCCar>();

	private IDictionary<NPCCar, float> walkPositions = new Dictionary<NPCCar, float>();

	private CrudeElapsedTimer timer = new CrudeElapsedTimer(2f);

	private float lapTimeLength;

	private float timeLength;

	private float lapLength;

	private float totalLength;

	public static RankMaker Instance => Nested.Instance;

	public bool Active { get; set; }

	public int TrackLevel { get; set; }

	public float LapTimeLength
	{
		get
		{
			return lapTimeLength;
		}
		set
		{
			lapTimeLength = value;
			timeLength = 3f * value;
		}
	}

	public float TimeLength => timeLength;

	public float DistanceBiasMax
	{
		get
		{
			return distanceBiasMax;
		}
		set
		{
			distanceBiasMax = value;
		}
	}

	public float LapLength
	{
		get
		{
			return lapLength;
		}
		set
		{
			lapLength = value;
			totalLength = 3f * value;
		}
	}

	public float TotalLength => totalLength;

	private RankMaker()
	{
		Active = false;
		TrackLevel = 1;
	}

	public void StartState(float[] lapTimeLengthArray, float[] distanceArray)
	{
		Debug.Log("RankMaker Start");
		Reset();
		TrackLevel = LocalUserInfo.Instance.TrackLevel;
		LapTimeLength = lapTimeLengthArray[TrackLevel];
		DistanceBiasMax = distanceArray[TrackLevel];
		LapLength = RaceWorld.Instance.RacingLine.LapLength;
		foreach (NPCCar npcCar in RaceWorld.Instance.NpcCars)
		{
			cars.Add(npcCar);
			walkPositions.Add(npcCar, Random.Range(0f, DistanceBiasMax));
		}
	}

	public void UpdateState()
	{
		if (!Active)
		{
			return;
		}
		timer.Advance(Time.deltaTime);
		if (timer.TimeOutCount <= 0)
		{
			return;
		}
		timer.ResetTimeOutCount();
		foreach (Car vehicle in RaceWorld.Instance.Vehicles)
		{
			if (RaceSceneData.CarIsFinish(vehicle.ID))
			{
				continue;
			}
			float num = NpcCarIsTooFastOrSlow(vehicle);
			if (vehicle is NPCCar)
			{
				if (num > 0f)
				{
					SlowDown((NPCCar)vehicle);
				}
				else if (num < 0f)
				{
					SpeedUp((NPCCar)vehicle);
				}
				else
				{
					RestoreNormal((NPCCar)vehicle);
				}
				UpdateRandomPosition();
			}
		}
	}

	public void UninitState()
	{
		Debug.Log("RankMaker Uninit");
		Reset();
	}

	private void Reset()
	{
		Active = false;
		cars.Clear();
		walkPositions.Clear();
		timer.Reset();
	}

	private float NpcCarIsTooFastOrSlow(Car car)
	{
		float num = ((!(car is NPCCar)) ? 0f : walkPositions[(NPCCar)car]);
		int num2 = RaceSceneData.CarCurrentLap(car.ID);
		float num3 = RaceSceneData.CarDisance(car.ID);
		float num4 = ((num2 > 0) ? ((float)num2 * LapLength - num3) : 0f);
		float num5 = timer.ElapsedTime / TimeLength;
		float num6 = num5 * TotalLength - num;
		return num4 - num6;
	}

	private void SlowDown(NPCCar car)
	{
		car.MaxTurnScale = 1f;
		car.MotorForceScale = 0.5f;
	}

	private void SpeedUp(NPCCar car)
	{
		car.MaxTurnScale = 2f;
		car.MotorForceScale = 3f;
	}

	private void RestoreNormal(NPCCar car)
	{
		car.MaxTurnScale = 1.5f;
		car.MotorForceScale = 1f;
	}

	private void UpdateRandomPosition()
	{
		float num = DistanceBiasMax;
		foreach (NPCCar car in cars)
		{
			float num2 = RandomWalk(walkPositions[car]);
			if (num2 < num)
			{
				num = num2;
			}
			walkPositions[car] = num2;
		}
		foreach (NPCCar car2 in cars)
		{
			IDictionary<NPCCar, float> dictionary2;
			IDictionary<NPCCar, float> dictionary = (dictionary2 = walkPositions);
			NPCCar key2;
			NPCCar key = (key2 = car2);
			float num3 = dictionary2[key2];
			dictionary[key] = num3 - num;
		}
	}

	private float RandomWalk(float position)
	{
		float num = DistanceBiasMax / 15f;
		float num2 = (float)Random.Range(-1, 2) * num;
		return Mathf.Clamp(position + num2, 0f, DistanceBiasMax);
	}
}
