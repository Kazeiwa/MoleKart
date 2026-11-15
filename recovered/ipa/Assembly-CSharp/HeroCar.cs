using UnityEngine;

[MessageClass(typeof(HeroCar))]
internal class HeroCar : Car
{
	private bool m_bLockMovement;

	private InputManager inputMg;

	private Transform m_effect_leaf_2;

	private GameObject m_gameObjectFxLeafFortest;

	private Transform m_kProjShadow;

	private Projector m_kProjector;

	private float m_fBaseProjScale;

	private Effect m_eftGrassL;

	private Effect m_eftGrassR;

	private Effect m_eftWaterbubbleL;

	private Effect m_eftWaterbubbleR;

	private Effect m_eftWaterflower;

	private GameObject m_goWaterflower;

	private Effect m_eftSandL;

	private Effect m_eftSandR;

	private Effect m_eftSnowL;

	private Effect m_eftSnowR;

	private Effect m_eftStrawL;

	private Effect m_eftStrawR;

	private GameObject m_objFallInWater;

	private Effect m_eftFallInWater;

	private GameObject m_objFlyBurst;

	private Effect m_eftFlyBurst;

	private GameObject m_objWinBreakRecord;

	private Effect m_eftWinBreakRecord;

	private Effect m_eftShift1_BlueDotL;

	private Effect m_eftShift1_BlueDotR;

	private Effect m_eftShift2_BlueFireL;

	private Effect m_eftShift2_BlueFireR;

	private Effect m_eftShift3_GoldDotL;

	private Effect m_eftShift3_GoldDotR;

	private Effect m_eftShift4_GoldFireL;

	private Effect m_eftShift4_GoldFireR;

	private bool bInit;

	public int m_iPerformaceLevel;

	private bool hasGrass;

	private bool hasSand;

	private bool hasStraw;

	private bool hasSnow;

	private Transform shadowLookAtTrans;

	private bool enableFlyBurst;

	protected override void CreateVehicle(Transform lfp, Transform rfp, Transform lbp, Transform rbp)
	{
	}

	private void CheckEffect()
	{
	}

	public override bool Init(int id, GameObject carEffectRoot)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Uninit()
	{
	}

	[MessageFun(typeof(HeroCar))]
	public new void processMsg(int msg, ref ABMessageData data)
	{
	}

	public override void UpdateCar()
	{
	}

	public override void UpdatePhysics()
	{
	}

	private void UpdateSound()
	{
	}

	private void UpdateEffect()
	{
	}

	private void UpdateProjector()
	{
	}

	public void LockMovement(bool bLock)
	{
	}

	private void DisableAllShiftEffect()
	{
	}

	private void PlayShiftEffect(int lvl)
	{
	}

	private void DrawGroundEffect()
	{
	}

	public override void EnableFlyBurst(bool enable)
	{
	}

	public void PlayWinBreakRecord()
	{
	}

	public override bool IsInShiftMode()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override float GetShiftPower()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override int GetBoostTriggerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
