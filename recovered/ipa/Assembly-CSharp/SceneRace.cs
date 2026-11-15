public class SceneRace
{
	public enum SceneType
	{
		ST_UNDEFINE = -1,
		ST_LOGIN,
		ST_SELECT,
		ST_RACING,
		ST_COMMUNITY
	}

	public enum EndLineAxis
	{
		X,
		Z
	}

	public RacingType m_eGameMode;

	private IRaceMode m_iRaceMode;

	public string s_DebugMessage;

	public int iSceneID;

	public bool m_bFixUpdate;

	public SceneRace(RacingType eType, int iSceneID)
	{
	}

	private void InitRaceMode()
	{
	}

	public void Uninit()
	{
	}

	public void Update()
	{
	}

	public void FixedUpdate()
	{
	}

	public IRaceMode GetIRaceMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
