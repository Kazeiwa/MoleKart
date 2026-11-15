using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Misc;

public class RankMaker
{
	private class Nested
	{
		internal static readonly RankMaker Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
		}
	}

	private const int LapCount = 3;

	private float distanceBiasMax;

	private IList<NPCCar> cars;

	private IDictionary<NPCCar, float> walkPositions;

	private CrudeElapsedTimer timer;

	private float lapTimeLength;

	private float timeLength;

	private float lapLength;

	private float totalLength;

	public static RankMaker Instance
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

	public float LapTimeLength
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float TimeLength
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float DistanceBiasMax
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float LapLength
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public float TotalLength
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private RankMaker()
	{
	}

	public void StartState(float[] lapTimeLengthArray, float[] distanceArray)
	{
	}

	public void UpdateState()
	{
	}

	public void UninitState()
	{
	}

	private void Reset()
	{
	}

	private float NpcCarIsTooFastOrSlow(Car car)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SlowDown(NPCCar car)
	{
	}

	private void SpeedUp(NPCCar car)
	{
	}

	private void RestoreNormal(NPCCar car)
	{
	}

	private void UpdateRandomPosition()
	{
	}

	private float RandomWalk(float position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
