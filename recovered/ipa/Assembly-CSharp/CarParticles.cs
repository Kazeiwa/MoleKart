using UnityEngine;

public class CarParticles
{
	private Car playerCar;

	private Effect jetEffectL;

	private Effect jetEffectR;

	private Effect jetEffectLBoo;

	private Effect jetEffectRBoo;

	private Effect jetEffectLBack;

	private Effect jetEffectRBack;

	private float m_fTime;

	private eEffectType m_eftJet;

	private eEffectType m_eftBoost;

	private eEffectType m_eftBack;

	private bool m_DisableState;

	private int m_iPerformance;

	private RUNNING_MDOE m_ePreRunningMode;

	private bool m_bLastBoost;

	private bool m_bLastBoosttest;

	private bool m_ForceJet;

	private bool backEffect;

	public CarParticles(Car car, eEffectType eftJet, eEffectType eftBoost, eEffectType eftBack)
	{
	}

	public void SetupParticles(Transform carTransform)
	{
	}

	public void EnableJet(bool bBoost)
	{
	}

	public void EnableJet(bool bBoost, bool bPlayRoleSound)
	{
	}

	public void DisableAllJetState(bool bBoost)
	{
	}

	public void EnableBackEffect(bool bBack)
	{
	}
}
