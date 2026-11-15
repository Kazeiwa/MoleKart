using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Misc;

public class SuperFSM<TStateID, TBehaviour> where TStateID : IComparable
{
	public class State
	{
		public const int WrapUpdateCount = 5;

		protected CrudeElapsedTimer timer;

		public TBehaviour Behaviour
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

		public List<Transition> Transitions
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

		public TStateID ID
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

		public CrudeElapsedTimer Timer
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public bool HasTimer
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public int UpdateCount
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

		public float DeltaTime
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

		public State(TStateID stateID, TBehaviour behaviour)
		{
		}

		public State AddTransition(Transition trans)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTransition(TStateID destStateID)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTransition(TStateID destStateID, Func<TBehaviour, bool> trigger)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTransition(TStateID destStateID, Func<bool> trigger)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTransition(TStateID destStateID, Func<TBehaviour, bool> trigger, Action<TBehaviour> action)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTimedTransition(float limit, TStateID targetStateID)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public void ResetTimer()
		{
		}

		public void UpdateTimer(float delta)
		{
		}

		public State CreateTimer(float limit)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTimedTransition(TStateID targetStateID, Func<TBehaviour, CrudeElapsedTimer, bool> trigger)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public State CreateTimedTransition(TStateID targetStateID, Func<TBehaviour, CrudeElapsedTimer, bool> trigger, Action<TBehaviour, CrudeElapsedTimer> action)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public void IncUpdateCount()
		{
		}

		public void ResetUpdateCount()
		{
		}

		public virtual void EntryAction()
		{
		}

		public virtual void ExitAction()
		{
		}

		public virtual void Action()
		{
		}
	}

	public abstract class Transition
	{
		public TBehaviour Behaviour
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

		public TStateID DestStateID
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

		public Transition(TStateID destStateID, TBehaviour behaviour)
		{
		}

		public Transition(TStateID destStateID)
		{
		}

		public abstract bool IsTriggered();

		public virtual void Action()
		{
		}
	}

	public class TrueTransition : Transition
	{
		public TrueTransition(TStateID destStateID)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class TimedTransition : Transition
	{
		private CrudeElapsedTimer timer;

		public TimedTransition(TStateID destStateID, TBehaviour behaviour, CrudeElapsedTimer timer)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class SimpleDelegateTransition : Transition
	{
		private Func<bool> Trigger
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

		public SimpleDelegateTransition(TStateID destStateID, Func<bool> trigger)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class DelegateTransition : Transition
	{
		private Func<TBehaviour, bool> Trigger
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

		public DelegateTransition(TStateID destStateID, TBehaviour behaviour, Func<TBehaviour, bool> trigger)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class DelegateTransition2 : DelegateTransition
	{
		private Action<TBehaviour> ActionDel
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

		public DelegateTransition2(TStateID destStateID, TBehaviour behaviour, Func<TBehaviour, bool> trigger, Action<TBehaviour> action)
		{
		}

		public override void Action()
		{
		}
	}

	public class DelegateTimedTransition : Transition
	{
		public CrudeElapsedTimer Timer
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

		private Func<TBehaviour, CrudeElapsedTimer, bool> Trigger
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

		public DelegateTimedTransition(TStateID destStateID, TBehaviour behaviour, CrudeElapsedTimer timer, Func<TBehaviour, CrudeElapsedTimer, bool> trigger)
		{
		}

		public override bool IsTriggered()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public class DelegateTimedTransition2 : DelegateTimedTransition
	{
		private Action<TBehaviour, CrudeElapsedTimer> ActionDel
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

		public DelegateTimedTransition2(TStateID destStateID, TBehaviour behaviour, CrudeElapsedTimer timer, Func<TBehaviour, CrudeElapsedTimer, bool> trigger, Action<TBehaviour, CrudeElapsedTimer> action)
		{
		}

		public override void Action()
		{
		}
	}

	public delegate void OnTransitionHandlerFunc(TStateID stateID);

	private IDictionary<TStateID, State> states;

	public State CurrentState
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		protected set
		{
		}
	}

	public TStateID CurrentStateID
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public TBehaviour Behaviour
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

	public OnTransitionHandlerFunc OnTransition
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

	public SuperFSM(TBehaviour behaviour)
	{
	}

	public void AddState(State state)
	{
	}

	public State GetStateByID(TStateID stateID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Update(bool doStateActionIfNotTriggered)
	{
	}

	public void Update(bool doStateActionIfNotTriggered, bool onlyUpdateTimer)
	{
	}

	public StateType CreateState<StateType>(TStateID stateID) where StateType : State
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public State CreateState(TStateID stateID)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void GotoState(TStateID destStateID)
	{
	}

	protected void DoTransition(Transition trans)
	{
	}
}
