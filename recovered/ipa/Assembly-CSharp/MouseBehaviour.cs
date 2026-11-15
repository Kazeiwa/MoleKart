using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class MouseBehaviour : SimpleMonsterBehaviour
{
	public enum StateID
	{
		None,
		Init,
		Lurk,
		Jump,
		KnockedOut
	}

	public enum CommandID
	{
		CarHit
	}

	public class LurkState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public LurkState(StateID stateID, MouseBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}
	}

	public class JumpState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public JumpState(StateID stateID, MouseBehaviour behaviour)
		{
		}
	}

	public class KnockedOutState : SuperFSM<StateID, MouseBehaviour>.State
	{
		public KnockedOutState(StateID stateID, MouseBehaviour behaviour)
		{
		}

		public override void EntryAction()
		{
		}

		public override void ExitAction()
		{
		}
	}

	private const float LurkTimeLength = 3f;

	private const float KnockedOutTimeLength = 3f;

	public const float CarTimerLimit = 2f;

	private SuperFSM<StateID, MouseBehaviour> fsm;

	private Transform cachedRootTransform;

	public override Transform GrabTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override GameObject GrabGameObject
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Transform RootTransform
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector3 Home
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

	public override bool ShouldSendMovementInfo
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void SendCommand(CommandID commandId)
	{
	}

	public override void ClientOnReceiveStateInfo(int stateId)
	{
	}

	public override void ClientOnReceiveCommand(int commandId)
	{
	}

	public override void StartState()
	{
	}

	public override void UpdateState()
	{
	}

	protected void OnTriggerEnter(Collider other)
	{
	}

	protected void OnDrawGizmos()
	{
	}

	protected void MakeFSM()
	{
	}

	private void OnStateInfo(StateID stateId)
	{
	}
}
