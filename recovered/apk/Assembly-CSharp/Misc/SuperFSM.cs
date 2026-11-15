using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class SuperFSM<StateIDType, T> where StateIDType : IComparable
{
	public class State
	{
		public const int WrapUpdateCount = 5;

		protected CrudeElapsedTimer timer;

		public T Behaviour { get; set; }

		public List<Transition> Transitions { get; set; }

		public StateIDType ID { get; set; }

		public CrudeElapsedTimer Timer => timer;

		public bool HasTimer => null != timer;

		public int UpdateCount { get; set; }

		public float DeltaTime { get; set; }

		public State(StateIDType stateID, T behaviour)
		{
			ID = stateID;
			Transitions = new List<Transition>();
			Behaviour = behaviour;
			UpdateCount = 0;
			DeltaTime = 0f;
		}

		public State AddTransition(Transition trans)
		{
			Transitions.Add(trans);
			return this;
		}

		public State CreateTransition(StateIDType destStateID)
		{
			TrueTransition trans = new TrueTransition(destStateID);
			AddTransition(trans);
			return this;
		}

		public State CreateTransition(StateIDType destStateID, Func<T, bool> trigger)
		{
			DelegateTransition trans = new DelegateTransition(destStateID, Behaviour, trigger);
			AddTransition(trans);
			return this;
		}

		public State CreateTransition(StateIDType destStateID, Func<bool> trigger)
		{
			SimpleDelegateTransition trans = new SimpleDelegateTransition(destStateID, trigger);
			AddTransition(trans);
			return this;
		}

		public State CreateTransition(StateIDType destStateID, Func<T, bool> trigger, Action<T> action)
		{
			DelegateTransition2 trans = new DelegateTransition2(destStateID, Behaviour, trigger, action);
			AddTransition(trans);
			return this;
		}

		public State CreateTimedTransition(float limit, StateIDType targetStateID)
		{
			if (timer != null)
			{
				Debug.LogError("State.CreateTimedTransition: timer exists");
			}
			timer = new CrudeElapsedTimer(limit);
			Transition trans = new TimedTransition(targetStateID, Behaviour, timer);
			AddTransition(trans);
			return this;
		}

		public void ResetTimer()
		{
			if (timer == null)
			{
				Debug.LogError("TimedState.ResetTimer: no timer");
			}
			timer.Reset();
		}

		public void UpdateTimer(float delta)
		{
			if (timer == null)
			{
				Debug.LogError("TimedState.UpdateTimer: no timer");
			}
			timer.Advance(delta);
		}

		public State CreateTimer(float limit)
		{
			if (timer != null)
			{
				Debug.LogError("TimedState.CreateTimer: timer exists");
			}
			timer = new CrudeElapsedTimer(limit);
			return this;
		}

		public State CreateTimedTransition(StateIDType targetStateID, Func<T, CrudeElapsedTimer, bool> trigger)
		{
			if (timer == null)
			{
				Debug.LogError("TimedState.CreateTimedTransition: timer does not exist");
			}
			Transition trans = new DelegateTimedTransition(targetStateID, Behaviour, timer, trigger);
			AddTransition(trans);
			return this;
		}

		public State CreateTimedTransition(StateIDType targetStateID, Func<T, CrudeElapsedTimer, bool> trigger, Action<T, CrudeElapsedTimer> action)
		{
			if (timer == null)
			{
				Debug.LogError("TimedState.CreateTimedTransition: timer does not exist");
			}
			Transition trans = new DelegateTimedTransition2(targetStateID, Behaviour, timer, trigger, action);
			AddTransition(trans);
			return this;
		}

		public void IncUpdateCount()
		{
			if (UpdateCount == 0)
			{
				DeltaTime = 0f;
			}
			UpdateCount = (UpdateCount + 1) % 5;
			DeltaTime += Time.deltaTime;
		}

		public void ResetUpdateCount()
		{
			UpdateCount = 0;
			DeltaTime = 0f;
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
		public T Behaviour { get; set; }

		public StateIDType DestStateID { get; set; }

		public Transition(StateIDType destStateID, T behaviour)
		{
			DestStateID = destStateID;
			Behaviour = behaviour;
		}

		public Transition(StateIDType destStateID)
		{
			DestStateID = destStateID;
			Behaviour = default(T);
		}

		public abstract bool IsTriggered();

		public virtual void Action()
		{
		}
	}

	public class TrueTransition : Transition
	{
		public TrueTransition(StateIDType destStateID)
			: base(destStateID)
		{
		}

		public override bool IsTriggered()
		{
			return true;
		}
	}

	public class TimedTransition : Transition
	{
		private CrudeElapsedTimer timer;

		public TimedTransition(StateIDType destStateID, T behaviour, CrudeElapsedTimer timer)
			: base(destStateID, behaviour)
		{
			this.timer = timer;
		}

		public override bool IsTriggered()
		{
			return timer.TimeOutCount > 0;
		}
	}

	public class SimpleDelegateTransition : Transition
	{
		private Func<bool> Trigger { get; set; }

		public SimpleDelegateTransition(StateIDType destStateID, Func<bool> trigger)
			: base(destStateID)
		{
			Trigger = trigger;
		}

		public override bool IsTriggered()
		{
			return Trigger();
		}
	}

	public class DelegateTransition : Transition
	{
		private Func<T, bool> Trigger { get; set; }

		public DelegateTransition(StateIDType destStateID, T behaviour, Func<T, bool> trigger)
			: base(destStateID, behaviour)
		{
			Trigger = trigger;
		}

		public override bool IsTriggered()
		{
			if (base.Behaviour == null)
			{
				Debug.LogError("DelegateTransition: Null trigger error.");
			}
			return Trigger(base.Behaviour);
		}
	}

	public class DelegateTransition2 : DelegateTransition
	{
		private Action<T> ActionDel { get; set; }

		public DelegateTransition2(StateIDType destStateID, T behaviour, Func<T, bool> trigger, Action<T> action)
			: base(destStateID, behaviour, trigger)
		{
			ActionDel = action;
		}

		public override void Action()
		{
			if (ActionDel == null)
			{
				Debug.LogError("DelegateTransition2: Null action error.");
			}
			ActionDel(base.Behaviour);
		}
	}

	public class DelegateTimedTransition : Transition
	{
		public CrudeElapsedTimer Timer { get; set; }

		private Func<T, CrudeElapsedTimer, bool> Trigger { get; set; }

		public DelegateTimedTransition(StateIDType destStateID, T behaviour, CrudeElapsedTimer timer, Func<T, CrudeElapsedTimer, bool> trigger)
			: base(destStateID, behaviour)
		{
			Timer = timer;
			Trigger = trigger;
		}

		public override bool IsTriggered()
		{
			if (base.Behaviour == null)
			{
				Debug.LogError("DelegateTimedTransition: Null timer error.");
			}
			if (Timer == null)
			{
				Debug.LogError("DelegateTimedTransition: Null timer error.");
			}
			return Trigger(base.Behaviour, Timer);
		}
	}

	public class DelegateTimedTransition2 : DelegateTimedTransition
	{
		private Action<T, CrudeElapsedTimer> ActionDel { get; set; }

		public DelegateTimedTransition2(StateIDType destStateID, T behaviour, CrudeElapsedTimer timer, Func<T, CrudeElapsedTimer, bool> trigger, Action<T, CrudeElapsedTimer> action)
			: base(destStateID, behaviour, timer, trigger)
		{
			ActionDel = action;
		}

		public override void Action()
		{
			if (ActionDel == null)
			{
				Debug.LogError("DelegateTimedTransition2: Null action error.");
			}
			ActionDel(base.Behaviour, base.Timer);
		}
	}

	public delegate void OnTransitionHandlerFunc(StateIDType stateID);

	private IDictionary<StateIDType, State> states = new Dictionary<StateIDType, State>();

	public State CurrentState { get; protected set; }

	public StateIDType CurrentStateID => CurrentState.ID;

	public T Behaviour { get; set; }

	public OnTransitionHandlerFunc OnTransition { get; set; }

	public SuperFSM(T behaviour)
	{
		Behaviour = behaviour;
		OnTransition = null;
	}

	public void AddState(State state)
	{
		if (state == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
		}
		if (states.Count == 0)
		{
			CurrentState = state;
		}
		states.Add(state.ID, state);
	}

	public State GetStateByID(StateIDType stateID)
	{
		return states[stateID];
	}

	public void Update(bool doStateActionIfNotTriggered)
	{
		Transition transition = null;
		foreach (Transition transition2 in CurrentState.Transitions)
		{
			if (transition2.IsTriggered())
			{
				transition = transition2;
				break;
			}
		}
		if (transition != null)
		{
			DoTransition(transition);
		}
		else if (doStateActionIfNotTriggered)
		{
			if (CurrentState.HasTimer)
			{
				CurrentState.UpdateTimer(Time.deltaTime);
			}
			CurrentState.Action();
			CurrentState.IncUpdateCount();
		}
	}

	public StateType CreateState<StateType>(StateIDType stateID) where StateType : State
	{
		StateType val = (StateType)Activator.CreateInstance(typeof(StateType), stateID, Behaviour);
		AddState(val);
		return val;
	}

	public StateType CreateTimedState<StateType>(StateIDType stateID) where StateType : State
	{
		return CreateState<StateType>(stateID);
	}

	public State CreateState(StateIDType stateID)
	{
		State state = new State(stateID, Behaviour);
		AddState(state);
		return state;
	}

	public void GotoState(StateIDType destStateID)
	{
		CurrentState.ExitAction();
		if (OnTransition != null)
		{
			OnTransition(CurrentStateID);
		}
		CurrentState = GetStateByID(destStateID);
		CurrentState.ResetUpdateCount();
		CurrentState.EntryAction();
	}

	protected void DoTransition(Transition trans)
	{
		CurrentState.ExitAction();
		if (CurrentState.HasTimer)
		{
			CurrentState.ResetTimer();
		}
		CurrentState = GetStateByID(trans.DestStateID);
		CurrentState.ResetUpdateCount();
		trans.Action();
		if (OnTransition != null)
		{
			OnTransition(CurrentStateID);
		}
		CurrentState.EntryAction();
	}
}
