using System.Collections.Generic;
using UnityEngine;

internal abstract class RaceModeBase : IRaceMode
{
	protected bool m_Init;

	public ERaceState m_currentRaceState;

	protected float m_LastGameTime;

	protected HeroCar m_HeroCar;

	protected GameObject m_CarEffect;

	protected GameObject m_MainCamera;

	protected SceneRace m_SceneRaceComp;

	protected bool m_RaceOver;

	protected bool m_BgSoundPlayed;

	protected float m_iSpendTime;

	protected List<Vector3> m_lstStartPoints;

	protected Quaternion m_kStartRotation;

	protected RacingType iRacingType;

	protected CarModeType iCar;

	protected RoleType iRole;

	protected float m_fTimer;

	protected bool m_InitAccelerate;

	public Quaternion StartRotation
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int StartPositionCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float LastGameTime
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public bool Inited
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual void Init(SceneRace scene)
	{
	}

	protected virtual void CreatePlayerCar()
	{
	}

	protected void CreateNPCCars()
	{
	}

	private void InitCollidersData()
	{
	}

	private void InitTriggers()
	{
	}

	private void InstallEffects()
	{
	}

	private void InitStartPosition()
	{
	}

	public Vector3 StartPosition(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void Uninit()
	{
	}

	public virtual void UpdateMode()
	{
	}

	public float TotalTime()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public int TotalLap()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public virtual void RaceOver(bool over)
	{
	}

	public virtual void ReStartGame()
	{
	}

	protected void ResetTimer()
	{
	}

	public virtual void UpdateUI()
	{
	}

	public virtual void BeginCountTime()
	{
	}

	public virtual void FinishRace()
	{
	}

	protected void AccelerateCarWhenGo()
	{
	}
}
