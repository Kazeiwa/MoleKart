using UnityEngine;

public class TrafficLightBehaviour : MonoBehaviour
{
	private GameObject cachedRedLight;

	private GameObject cachedGreenLight;

	public GameObject RedLight
	{
		get
		{
			if (null == cachedRedLight)
			{
				cachedRedLight = base.transform.FindChild("red_mov").gameObject;
			}
			return cachedRedLight;
		}
	}

	public GameObject GreenLight
	{
		get
		{
			if (null == cachedGreenLight)
			{
				cachedGreenLight = base.transform.FindChild("blue_mov").gameObject;
			}
			return cachedGreenLight;
		}
	}

	public void BecomeRed()
	{
		RedLight.active = true;
		GreenLight.active = false;
	}

	public void BecomeGreen()
	{
		RedLight.active = false;
		GreenLight.active = true;
	}
}
