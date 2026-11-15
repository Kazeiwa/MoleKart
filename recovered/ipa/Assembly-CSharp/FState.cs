public class FState : FAbstractState
{
	public enum ChildMode
	{
		ExclusiveStates,
		ParallelStates
	}

	private ChildMode childStateMode;

	public ChildMode ChildSateMode
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public FState(ChildMode childMode, FState parent)
	{
	}

	public void CheckAndBreakShift()
	{
	}

	public override void Entered()
	{
	}

	public override void Exited()
	{
	}

	public override void Update()
	{
	}

	public override void UpdatePhysics()
	{
	}

	public override bool Reject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
