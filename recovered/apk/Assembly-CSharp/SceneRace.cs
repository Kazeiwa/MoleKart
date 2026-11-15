using UnityEngine;

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

	public RacingType m_eGameMode = RacingType.INVAID_MODE;

	private IRaceMode m_iRaceMode;

	public string s_DebugMessage;

	public int iSceneID;

	public bool m_bFixUpdate;

	private GUIStyle style = new GUIStyle();

	public SceneRace(RacingType eType, int iSceneID)
	{
		m_eGameMode = eType;
		this.iSceneID = iSceneID;
		InitRaceMode();
	}

	private void InitRaceMode()
	{
		if (m_eGameMode != RacingType.INVAID_MODE)
		{
			if (m_iRaceMode == null)
			{
				m_iRaceMode = RaceModeFactory.CreateCurrentGameMode(m_eGameMode);
			}
			if (m_iRaceMode != null)
			{
				m_iRaceMode.Init(this);
				if (null != SceneUIManager.Instance.m_Loading)
				{
					SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
				}
			}
		}
		else
		{
			Debug.Log("Error--InVaild Game Mode!!");
		}
	}

	public void Uninit()
	{
		if (m_iRaceMode != null)
		{
			m_iRaceMode.Uninit();
		}
	}

	public void OnGUI()
	{
		if (HeightMapManager.Instance.Inited)
		{
			if (m_iRaceMode != null)
			{
				m_iRaceMode.UpdateUI();
			}
			if (NiLog.strMessage.Length > 0)
			{
				GUILayout.BeginArea(new Rect(10f, 400f, 410f, 600f));
				float height = 18 * (NiLog.GetLinesNumber() + NiLog.GetLinesNumberX());
				style.active.textColor = Color.blue;
				GUI.Label(new Rect(0f, 0f, 600f, height), NiLog.strMessage, style);
				GUILayout.EndArea();
			}
		}
	}

	public void Update()
	{
		if (HeightMapManager.Instance.Inited)
		{
			NiLog.ClearX();
			Utility.CPU_Profile_Begin_X();
			if (m_iRaceMode != null)
			{
				m_iRaceMode.UpdateMode();
				m_iRaceMode.UpdateUI();
			}
			if (SmoothCameraWithBumper.Instacne != null)
			{
				SmoothCameraWithBumper.Instacne.UpdateEx();
			}
			Utility.CPU_Profile_End_X("m_iRaceMode.UpdateMode = ");
		}
	}

	public void FixedUpdate()
	{
		if (HeightMapManager.Instance.Inited)
		{
			NiLog.Clear();
			Utility.CPU_Profile_Begin();
			if (m_bFixUpdate && m_iRaceMode is RaceModeBase { m_currentRaceState: ERaceState.START_RACE_STATE })
			{
				Engine.UpdatePhysx();
			}
			Utility.CPU_Profile_End("Engine:UpdatePhysx = ");
			if (SmoothCameraWithBumper.Instacne != null)
			{
				SmoothCameraWithBumper.Instacne.FixedUpdateEx();
			}
			Utility.CPU_Profile_End("SmoothCameraWithBumper:UpdatePhysx = ");
			if (m_bFixUpdate && m_iRaceMode != null)
			{
				TSingleton<CarManager>.Instance.FixedUpdate();
			}
			Utility.CPU_Profile_End("CarManager:FixedUpdate= ");
		}
	}

	public IRaceMode GetIRaceMode()
	{
		return m_iRaceMode;
	}
}
