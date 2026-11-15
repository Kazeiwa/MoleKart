using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
		}
	}

	public class TrickInfo
	{
		private bool shouldCheckToFinalRush;

		public Car Car
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public float ExpectedSpeed
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public bool IsVisible
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public float FinishedDistance
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public float DistanceInLapFromGoal
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public int Laps
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public int Rank
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public float NormalSpeed
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public bool ShouldCheckToFinalRush
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			set
			{
			}
		}

		public void SetExpectedSpeed(float speed)
		{
		}
	}

	private int lapCount;

	private CrudeElapsedTimer timer;

	private IDictionary<Car, TrickInfo> npcCarTrickInfos;

	private Phase phase;

	private Car heroCar;

	private TrickInfo heroTrickInfo;

	public static FunMaker Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Active
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public int TrackDifficulty
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public float MinSpeedVisiable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MaxSpeedVisiable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MinSpeedInvisiable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float MaxSpeedInvisiable
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private FunMaker()
	{
	}

	public void StartState()
	{
	}

	public void UpdateState()
	{
	}

	public void UninitState()
	{
	}

	private void UpdatePhaseA()
	{
	}

	private void UpdatePhaseB()
	{
	}

	private void UpdatePhaseC()
	{
	}

	private void Reset()
	{
	}

	private void SetClampedExpectedSpeedDelta(TrickInfo info, float speedDelta)
	{
	}

	private float ClampSpeed(TrickInfo info, float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float MinSpeed(bool isVisile)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float MaxSpeed(bool isVisile)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool SpeedIsPermitted(bool isVisile, float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float ClampSpeedVisible(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float ClampSpeedVisible(float speed, float deltaSpeed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float ClampSpeedInvisible(float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float ClampSpeedInvisible(float speed, float deltaSpeed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool HeroCarHasArrivedAtCheckPoint()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void OnHeroCarArrivesCheckPoint()
	{
	}

	private float RandomSpeed()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RefreshTrickInfo()
	{
	}

	private void SetClampedExpectedSpeedOfNpcCar(TrickInfo info, float speed)
	{
	}

	private void ControlSpeedNpcCarInPhaseA(TrickInfo info)
	{
	}

	private void SlowDownNpcCar(TrickInfo info)
	{
	}

	private void SlowDownNpcCar(TrickInfo info, float offset)
	{
	}

	private void SpeedUpNpcCar(TrickInfo info)
	{
	}

	private void SpeedUpNpcCar(TrickInfo info, float offset)
	{
	}

	private void UseNormalSpeed(TrickInfo info)
	{
	}
}
