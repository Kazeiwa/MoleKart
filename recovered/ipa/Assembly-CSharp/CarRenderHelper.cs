using UnityEngine;

public class CarRenderHelper
{
	private bool flagCarModelEnable;

	private int carModelLodLevel;

	private GameObject carRoot;

	private GameObject carLodNode;

	private GameObject carRoleNode;

	private bool subNormalCarModel;

	private bool subNormalCarRole;

	private bool subLodCarModel;

	private MeshRenderer[] normalCarModel;

	private MeshRenderer[] normalCarRole;

	private MeshRenderer[] lodCarModel;

	private bool flagCarShadowEnable;

	private bool neverShowShadow;

	private GameObject carShadow;

	private bool flagCarCollisionEnable;

	private GameObject carCollision;

	private bool flagCarWheelsEnable;

	private GameObject carWheels;

	private bool flagEffectEnable;

	private GameObject carEffect;

	private GameObject carTailR;

	private GameObject carTailL;

	private GameObject carRightBack;

	private GameObject carLeftBack;

	private ParticleRenderer[] allParticle;

	public void Init(GameObject root)
	{
	}

	public void ResetMesh()
	{
	}

	public void SetLodLevel(int lod)
	{
	}

	public int GetLodLevel()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetCarMesh(bool enable)
	{
	}

	public void SetCanShowShadow(bool canShow)
	{
	}

	public void SetShadow(bool enable)
	{
	}

	public void SetParticle(bool enable)
	{
	}

	public void SetVisibleAll(bool enable)
	{
	}
}
