using UnityEngine;

public class CarRenderHelper
{
	private bool flagCarModelEnable = true;

	private int carModelLodLevel = 1;

	private GameObject carRoot;

	private GameObject carLodNode;

	private bool subNormalCarModel = true;

	private bool subNormalCarRole = true;

	private bool subLodCarModel = true;

	private MeshRenderer[] normalCarModel;

	private SkinnedMeshRenderer[] normalCarRole;

	private MeshRenderer[] lodCarModel;

	private bool flagCarShadowEnable = true;

	private bool neverShowShadow = true;

	private GameObject carShadow;

	private bool flagCarCollisionEnable = true;

	private GameObject carCollision;

	private bool flagCarWheelsEnable = true;

	private GameObject carWheels;

	private bool flagEffectEnable = true;

	private GameObject carEffect;

	private GameObject carTailR;

	private GameObject carTailL;

	private GameObject carRightBack;

	private GameObject carLeftBack;

	private ParticleRenderer[] allParticle;

	public void Init(GameObject root)
	{
		carRoot = root.transform.FindChild("car").gameObject;
		carLodNode = root.transform.FindChild("car/root/bodyLod").gameObject;
		carCollision = root.transform.FindChild("collision").gameObject;
		carWheels = root.transform.FindChild("WheelColliders").gameObject;
		carEffect = root.transform.FindChild("effect").gameObject;
		carTailR = root.transform.FindChild("car/root/tailR").gameObject;
		carTailL = root.transform.FindChild("car/root/tailL").gameObject;
		carRightBack = root.transform.FindChild("car/root/rightback").gameObject;
		carLeftBack = root.transform.FindChild("car/root/leftback").gameObject;
		MeshRenderer[] componentsInChildren = carRoot.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		lodCarModel = carLodNode.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		if (componentsInChildren.Length - lodCarModel.Length > 0)
		{
			normalCarModel = new MeshRenderer[componentsInChildren.Length - lodCarModel.Length];
			int i = 0;
			int num = 0;
			for (; i < componentsInChildren.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < lodCarModel.Length; j++)
				{
					if (componentsInChildren[i] == lodCarModel[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					normalCarModel[num] = componentsInChildren[i];
					num++;
				}
			}
		}
		normalCarRole = carRoot.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);
		allParticle = root.GetComponentsInChildren<ParticleRenderer>(includeInactive: true);
		SetLodLevel(1);
		SetCanShowShadow(canShow: false);
	}

	public void ResetMesh()
	{
		normalCarRole = carRoot.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);
	}

	public void SetLodLevel(int lod)
	{
		carModelLodLevel = lod;
		if (flagCarModelEnable)
		{
			if (carModelLodLevel == 0)
			{
				if (subNormalCarModel && normalCarModel != null)
				{
					for (int i = 0; i < normalCarModel.Length; i++)
					{
						normalCarModel[i].enabled = false;
					}
					subNormalCarModel = false;
				}
				if (subNormalCarRole && normalCarRole != null)
				{
					for (int j = 0; j < normalCarRole.Length; j++)
					{
						normalCarRole[j].enabled = false;
					}
					subNormalCarRole = false;
				}
				if (!subLodCarModel && lodCarModel != null)
				{
					for (int k = 0; k < lodCarModel.Length; k++)
					{
						lodCarModel[k].enabled = true;
					}
					subLodCarModel = true;
				}
			}
			else
			{
				if (carModelLodLevel <= 0)
				{
					return;
				}
				if (!subNormalCarModel && normalCarModel != null)
				{
					for (int l = 0; l < normalCarModel.Length; l++)
					{
						normalCarModel[l].enabled = true;
					}
					subNormalCarModel = true;
				}
				if (!subNormalCarRole && normalCarRole != null)
				{
					for (int m = 0; m < normalCarRole.Length; m++)
					{
						normalCarRole[m].enabled = true;
					}
					subNormalCarRole = true;
				}
				if (subLodCarModel && lodCarModel != null)
				{
					for (int n = 0; n < lodCarModel.Length; n++)
					{
						lodCarModel[n].enabled = false;
					}
					subLodCarModel = false;
				}
			}
			return;
		}
		if (subNormalCarModel && normalCarModel != null)
		{
			for (int num = 0; num < normalCarModel.Length; num++)
			{
				normalCarModel[num].enabled = false;
			}
			subNormalCarModel = false;
		}
		if (subNormalCarRole && normalCarRole != null)
		{
			for (int num2 = 0; num2 < normalCarRole.Length; num2++)
			{
				normalCarRole[num2].enabled = false;
			}
			subNormalCarRole = false;
		}
		if (subLodCarModel && lodCarModel != null)
		{
			for (int num3 = 0; num3 < lodCarModel.Length; num3++)
			{
				lodCarModel[num3].enabled = false;
			}
			subLodCarModel = false;
		}
	}

	public void SetCarMesh(bool enable)
	{
		flagCarModelEnable = enable;
		SetLodLevel(carModelLodLevel);
	}

	public void SetCanShowShadow(bool canShow)
	{
		neverShowShadow = !canShow;
		SetShadow(flagCarShadowEnable);
	}

	public void SetShadow(bool enable)
	{
		flagCarShadowEnable = enable;
		if (carShadow != null)
		{
			if (neverShowShadow)
			{
				carShadow.active = false;
			}
			else
			{
				carShadow.active = enable;
			}
		}
	}

	public void SetParticle(bool enable)
	{
		flagEffectEnable = enable;
		if (flagEffectEnable && allParticle != null)
		{
			for (int i = 0; i < allParticle.Length; i++)
			{
				allParticle[i].enabled = false;
			}
		}
	}

	public void SetVisibleAll(bool enable)
	{
		SetCarMesh(enable);
		SetShadow(enable);
		SetParticle(enable);
	}
}
