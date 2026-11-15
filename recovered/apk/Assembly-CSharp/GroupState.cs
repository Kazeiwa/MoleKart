using System.Collections;

public class GroupState
{
	private ArrayList m_UpdatingStates = new ArrayList();

	private ArrayList m_EnterStates = new ArrayList();

	private FAbstractState m_NormalState;

	private Car m_Car;

	public bool Init(FAbstractState normalState)
	{
		m_Car = normalState.PlayerCar;
		m_NormalState = normalState;
		PushState(normalState);
		return true;
	}

	public void PushState(FAbstractState state)
	{
		if (state != null && !m_EnterStates.Contains(state))
		{
			m_EnterStates.Add(state);
		}
	}

	public bool CanDrive()
	{
		if ((m_Car.CarState & 8) != 0 || (m_Car.CarState & 0x2000) != 0 || (m_Car.CarState & 0x1000) != 0 || (m_Car.CarState & 0x10) != 0 || (m_Car.CarState & 0x200) != 0 || (m_Car.CarState & 0x20) != 0 || (m_Car.CarState & 0x8000) != 0)
		{
			return false;
		}
		return true;
	}

	public void Update()
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i >= 0 && i < m_EnterStates.Count; i++)
		{
			if (!(m_EnterStates[i] as FAbstractState).Reject())
			{
				arrayList.Add(m_EnterStates[i]);
			}
		}
		foreach (FAbstractState updatingState in m_UpdatingStates)
		{
			if (updatingState.m_StateFlag == StateFlag.OVER)
			{
				updatingState.Exited();
				if (m_Car.isHero)
				{
					CameraManager.instance.EnterCamera();
				}
			}
		}
		foreach (FAbstractState item in arrayList)
		{
			if (item.m_StateFlag == StateFlag.STOP || item.CanRepeat)
			{
				item.Entered();
				if (m_Car.isHero)
				{
					CameraManager.instance.EnterCamera();
				}
			}
			if (!m_UpdatingStates.Contains(item))
			{
				m_UpdatingStates.Add(item);
			}
		}
		m_EnterStates.Clear();
		foreach (FAbstractState updatingState2 in m_UpdatingStates)
		{
			if (updatingState2.m_StateFlag == StateFlag.UPDATING)
			{
				updatingState2.Update();
			}
		}
	}
}
