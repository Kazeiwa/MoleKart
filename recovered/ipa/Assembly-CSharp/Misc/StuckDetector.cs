using System.Runtime.CompilerServices;
using UnityEngine;

namespace Misc;

public class StuckDetector
{
	private const int MaxSampleCount = 5;

	private const float MaxSampleTime = 3f;

	private CircularBuffer<Vector2> samplePositions;

	private CrudeElapsedTimer timer;

	public string Name
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

	public bool DectectStuck(Vector2 position, float deltaTime)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Reset()
	{
	}

	private bool HasNoMove()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private string SamplePositionsText()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
