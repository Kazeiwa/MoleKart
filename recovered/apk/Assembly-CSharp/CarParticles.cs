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

	private float m_fTime = 1f;

	private eEffectType m_eftJet;

	private eEffectType m_eftBoost;

	private eEffectType m_eftBack;

	private bool m_DisableState;

	private int m_iPerformance = 1;

	private RUNNING_MDOE m_ePreRunningMode;

	private bool m_bLastBoost = true;

	private bool m_bLastBoosttest;

	private bool m_ForceJet;

	public CarParticles(Car car, eEffectType eftJet, eEffectType eftBoost, eEffectType eftBack)
	{
		string currentPlatform = AppMain.Instance.GetCurrentPlatform();
		if (currentPlatform == "iphone" || iPhoneSettings.generation == iPhoneGeneration.iPad1Gen)
		{
			m_iPerformance = 0;
		}
		m_eftJet = eftJet;
		m_eftBoost = eftBoost;
		m_eftBack = eftBack;
		playerCar = car;
		SetupParticles(car.gameObject.transform);
	}

	public void SetupParticles(Transform carTransform)
	{
		Transform parentTrans = Utility.FindChildTranform(carTransform, "tailL");
		Transform parentTrans2 = Utility.FindChildTranform(carTransform, "tailR");
		if (m_iPerformance > 0)
		{
			jetEffectL = EffectManager.Instance.CreateEffect(m_eftJet, isPause: true, isOneShot: false).GetComponent<Effect>();
			jetEffectR = EffectManager.Instance.CreateEffect(m_eftJet, isPause: true, isOneShot: false).GetComponent<Effect>();
			jetEffectL.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
			jetEffectR.AttachTo(parentTrans2, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
			jetEffectL.Disable();
			jetEffectR.Disable();
		}
		jetEffectLBoo = EffectManager.Instance.CreateEffect(m_eftBoost, isPause: true, isOneShot: false).GetComponent<Effect>();
		jetEffectRBoo = EffectManager.Instance.CreateEffect(m_eftBoost, isPause: true, isOneShot: false).GetComponent<Effect>();
		jetEffectLBoo.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
		jetEffectRBoo.AttachTo(parentTrans2, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
		jetEffectLBoo.Disable();
		jetEffectRBoo.Disable();
		jetEffectLBack = EffectManager.Instance.CreateEffect(m_eftBack, isPause: true, isOneShot: false).GetComponent<Effect>();
		jetEffectRBack = EffectManager.Instance.CreateEffect(m_eftBack, isPause: true, isOneShot: false).GetComponent<Effect>();
		jetEffectLBack.AttachTo(parentTrans, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
		jetEffectRBack.AttachTo(parentTrans2, new Vector3(0f, 0f, 0f), new Vector3(90f, 0f, 0f));
		jetEffectLBack.Disable();
		jetEffectRBack.Disable();
		EnableBackEffect(bBack: false);
		EnableJet(bBoost: false);
		m_ePreRunningMode = playerCar.vehicle.RunningMode;
	}

	public void EnableJet(bool bBoost)
	{
		if (!bBoost)
		{
			if ((bool)jetEffectL)
			{
				jetEffectL.Enable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Enable();
			}
		}
		else
		{
			if ((bool)jetEffectL)
			{
				jetEffectL.Disable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Disable();
			}
		}
		if (bBoost)
		{
			if (playerCar.isHero)
			{
				playerCar.PlaySound(51701, 1);
			}
			int num = (int)Time.time;
			switch (num % 3)
			{
			case 0:
				if (playerCar.isHero)
				{
					playerCar.GetRole(0).PlaySound(Role.HAPPY_THREE);
				}
				break;
			case 1:
				if (playerCar.isHero)
				{
					playerCar.GetRole(0).PlaySound(Role.HAPPY_TWO);
				}
				break;
			default:
				if (playerCar.isHero)
				{
					playerCar.GetRole(0).PlaySound(Role.HAPPY_ONE);
				}
				break;
			}
			if ((bool)jetEffectLBoo)
			{
				jetEffectLBoo.Enable();
			}
			if ((bool)jetEffectRBoo)
			{
				jetEffectRBoo.Enable();
			}
		}
		else
		{
			if ((bool)jetEffectLBoo)
			{
				jetEffectLBoo.Disable();
			}
			if ((bool)jetEffectRBoo)
			{
				jetEffectRBoo.Disable();
			}
		}
	}

	public void DisableAllJetState(bool bBoost)
	{
		m_DisableState = bBoost;
		if (bBoost)
		{
			if ((bool)jetEffectL)
			{
				jetEffectL.Disable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Disable();
			}
			if ((bool)jetEffectLBoo)
			{
				jetEffectLBoo.Disable();
			}
			if ((bool)jetEffectRBoo)
			{
				jetEffectRBoo.Disable();
			}
		}
		else
		{
			if ((bool)jetEffectL)
			{
				jetEffectL.Enable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Enable();
			}
		}
	}

	public void EnableBackEffect(bool bBack)
	{
		if (bBack)
		{
			jetEffectLBack.Enable();
			jetEffectRBack.Enable();
			if ((bool)jetEffectL)
			{
				jetEffectL.Disable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Disable();
			}
			if ((bool)jetEffectLBoo)
			{
				jetEffectLBoo.Disable();
			}
			if ((bool)jetEffectRBoo)
			{
				jetEffectRBoo.Disable();
			}
		}
		else
		{
			jetEffectLBack.Disable();
			jetEffectRBack.Disable();
			if ((bool)jetEffectL)
			{
				jetEffectL.Enable();
			}
			if ((bool)jetEffectR)
			{
				jetEffectR.Enable();
			}
		}
	}
}
