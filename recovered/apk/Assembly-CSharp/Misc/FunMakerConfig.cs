using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class FunMakerConfig
{
	public enum SpeedTreatment
	{
		None,
		Normal,
		Up,
		Down
	}

	public struct Interval
	{
		public float Min;

		public float Max;

		public Interval(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public bool HitTest(float x)
		{
			return x < Max && x > Min;
		}

		public override string ToString()
		{
			return "[" + Min + ", " + Max + "]";
		}
	}

	public IList<IList<SpeedTreatment>> speedTreatmentMatrix = new List<IList<SpeedTreatment>>();

	public float RefreshTimeLength { get; set; }

	public Interval VisibleInterval { get; set; }

	public float CheckPointRatio { get; set; }

	public float FinalRushDistance { get; set; }

	public float FinalRushSpeedDelta { get; set; }

	public float SlowDownThreshold1 { get; set; }

	public float SlowDownThreshold2 { get; set; }

	public float SlowDownSpeedDelta1 { get; set; }

	public float SlowDownSpeedDelta2 { get; set; }

	public float SlowDownSpeedDelta3 { get; set; }

	public float SpeedUpThreshold1 { get; set; }

	public float SpeedUpThreshold2 { get; set; }

	public float SpeedUpSpeedDelta1 { get; set; }

	public float SpeedUpSpeedDelta2 { get; set; }

	public float SpeedUpSpeedDelta3 { get; set; }

	public Interval[] VisibleSpeedIntervals { get; set; }

	public Interval[] InvisibleSpeedIntervals { get; set; }

	public FunMakerConfig()
	{
		VisibleSpeedIntervals = new Interval[3];
		InvisibleSpeedIntervals = new Interval[3];
	}

	public SpeedTreatment GetSpeedTreatment(int heroCarRank, int npcCarRank)
	{
		return speedTreatmentMatrix[heroCarRank - 1][npcCarRank - 1];
	}

	public void LogMatrix()
	{
		Debug.Log("LogMatrix ...");
		int num = 1;
		foreach (IList<SpeedTreatment> item in speedTreatmentMatrix)
		{
			string text = "  " + num + ": ";
			foreach (SpeedTreatment item2 in item)
			{
				text = text + SpeedTreatment2String(item2) + " ";
			}
			Debug.Log(text);
			num++;
		}
	}

	public void LogVisibleSpeedIntervals()
	{
		Debug.Log("VisibleSpeedIntervals");
		LogSpeedIntervals(VisibleSpeedIntervals);
	}

	public void LogInvisibleSpeedIntervals()
	{
		Debug.Log("InvisibleSpeedIntervals");
		LogSpeedIntervals(InvisibleSpeedIntervals);
	}

	private static void LogSpeedIntervals(Interval[] intervals)
	{
		int num = 0;
		foreach (Interval interval in intervals)
		{
			Debug.Log("  " + num + " :" + interval);
			num++;
		}
	}

	private static string SpeedTreatment2String(SpeedTreatment treatment)
	{
		return treatment switch
		{
			SpeedTreatment.Down => "d", 
			SpeedTreatment.Up => "u", 
			SpeedTreatment.Normal => "n", 
			SpeedTreatment.None => "z", 
			_ => " ", 
		};
	}
}
