using System.Runtime.CompilerServices;
using Misc;

namespace RaceTrack;

public class PathLookAheadBase
{
	protected const int Capacity = 5;

	private RacingLine racingLine;

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

	public bool Available
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public CircularBuffer<RichEdge> RichEdgeQueue
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

	protected RacingLine RacingLine
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Reset()
	{
	}

	protected void AddEdge(RichEdge edge)
	{
	}

	protected void Advance()
	{
	}
}
