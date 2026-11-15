public class PropIDManager
{
	private static PropIDManager instance;

	private int m_ID;

	public int m_OtherCarUsePropID;

	public static PropIDManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new PropIDManager();
			}
			return instance;
		}
	}

	private PropIDManager()
	{
	}

	public int GetID(Car car)
	{
		if (AppMain.Instance.IsServer || !AppMain.Instance.IsOnlineMode)
		{
			m_ID++;
		}
		if (AppMain.Instance.IsOnlineMode && null != car && !car.isHero)
		{
			return m_OtherCarUsePropID;
		}
		return m_ID;
	}

	public void ResetID()
	{
		m_ID = 0;
		m_OtherCarUsePropID = 0;
	}

	public void RequestID()
	{
		SendMessageHandler.Instance.RequestID();
	}

	public void SetID(int id)
	{
		if (AppMain.Instance.IsClient)
		{
			m_ID = id;
		}
	}
}
