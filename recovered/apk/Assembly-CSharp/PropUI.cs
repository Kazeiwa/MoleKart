using System.Collections;
using UnityEngine;

public class PropUI : ISceneUI
{
	private ArrayList m_PropUI = new ArrayList();

	private Transform m_PreShowProp;

	private int m_StartIndex;

	private int m_EndIndex;

	private int m_UpIndex;

	private int m_DownIndex;

	private int m_SelectIndex;

	private GameObject m_UpProp;

	private GameObject m_DownProp;

	private Vector3 m_UpScale;

	private Vector3 m_DownScale;

	private Vector3 m_UpPos;

	private Vector3 m_DownPos;

	public float m_ScaleFactor = 0.2f;

	private bool m_StopRoll = true;

	private float m_RollTime;

	private ePropType m_PickedType;

	private Transform m_Transform;

	private bool m_HideRollUI;

	private PlayerData playerData = new PlayerData();

	private Vector3 tempVector3 = default(Vector3);

	private readonly string[] typesStrings = new string[25]
	{
		"MISSILE", "BANANA", "BANANA_THREE", "TORTOISE", "TORTOISE_RED", "TORTOISE_RED_THREE", "TORTOISE_GREEN", "TORTOISE_GREEN_THREE", "MUSHROOM_ONE", "MUSHROOM_TWO",
		"MUSHROOM_THREE", "MUSHROOM_GOLDERN", "LUCKY_STAR", "TORTOISE_BLUE", "BOMB", "BOMB_EFFECT", "LIGHTNING", "CHEAT_BOX", "CUTTLE_FISH", "MUSHROOM_BIG",
		"QUAKE", "THUNDER_CLOUD", "BLUE_TORTOISE_BOMB_EFFECT", "BOX", "SPHERE"
	};

	private float m_DelayPlaySoundTime = -2f;

	public void InitUI(Transform transform)
	{
		if (null == transform)
		{
			return;
		}
		m_Transform = transform;
		GameObject gameObject = transform.FindChild("RollProp/BANANA").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component = gameObject.GetComponent<UIPanelTab>();
			if (null != component)
			{
				Object.Destroy(component);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/BANANA_THREE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component2 = gameObject.GetComponent<UIPanelTab>();
			if (null != component2)
			{
				Object.Destroy(component2);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/BOMB").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component3 = gameObject.GetComponent<UIPanelTab>();
			if (null != component3)
			{
				Object.Destroy(component3);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/CHEAT_BOX").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component4 = gameObject.GetComponent<UIPanelTab>();
			if (null != component4)
			{
				Object.Destroy(component4);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/CUTTLE_FISH").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component5 = gameObject.GetComponent<UIPanelTab>();
			if (null != component5)
			{
				Object.Destroy(component5);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/LIGHTNING").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component6 = gameObject.GetComponent<UIPanelTab>();
			if (null != component6)
			{
				Object.Destroy(component6);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/LUCKY_STAR").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component7 = gameObject.GetComponent<UIPanelTab>();
			if (null != component7)
			{
				Object.Destroy(component7);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MISSILE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component8 = gameObject.GetComponent<UIPanelTab>();
			if (null != component8)
			{
				Object.Destroy(component8);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MUSHROOM_BIG").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component9 = gameObject.GetComponent<UIPanelTab>();
			if (null != component9)
			{
				Object.Destroy(component9);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MUSHROOM_GOLDERN").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component10 = gameObject.GetComponent<UIPanelTab>();
			if (null != component10)
			{
				Object.Destroy(component10);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MUSHROOM_ONE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component11 = gameObject.GetComponent<UIPanelTab>();
			if (null != component11)
			{
				Object.Destroy(component11);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MUSHROOM_THREE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component12 = gameObject.GetComponent<UIPanelTab>();
			if (null != component12)
			{
				Object.Destroy(component12);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/MUSHROOM_TWO").gameObject;
		if (null != gameObject)
		{
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/QUAKE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component13 = gameObject.GetComponent<UIPanelTab>();
			if (null != component13)
			{
				Object.Destroy(component13);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/THUNDER_CLOUD").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component14 = gameObject.GetComponent<UIPanelTab>();
			if (null != component14)
			{
				Object.Destroy(component14);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/TORTOISE_BLUE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component15 = gameObject.GetComponent<UIPanelTab>();
			if (null != component15)
			{
				Object.Destroy(component15);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/TORTOISE_GREEN").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component16 = gameObject.GetComponent<UIPanelTab>();
			if (null != component16)
			{
				Object.Destroy(component16);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/TORTOISE_GREEN_THREE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component17 = gameObject.GetComponent<UIPanelTab>();
			if (null != component17)
			{
				Object.Destroy(component17);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/TORTOISE_RED").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component18 = gameObject.GetComponent<UIPanelTab>();
			if (null != component18)
			{
				Object.Destroy(component18);
			}
			m_PropUI.Add(gameObject);
		}
		gameObject = transform.FindChild("RollProp/TORTOISE_RED_THREE").gameObject;
		if (null != gameObject)
		{
			UIPanelTab component19 = gameObject.GetComponent<UIPanelTab>();
			if (null != component19)
			{
				Object.Destroy(component19);
			}
			m_PropUI.Add(gameObject);
		}
	}

	public void ShowProp()
	{
		m_HideRollUI = false;
		if (null != m_UpProp)
		{
			m_UpProp.active = true;
		}
		if (null != m_DownProp)
		{
			m_DownProp.active = true;
		}
	}

	public void HideProp()
	{
		m_HideRollUI = true;
		if (null != m_UpProp)
		{
			m_UpProp.active = false;
		}
		if (null != m_DownProp)
		{
			m_DownProp.active = false;
		}
	}

	public void RollPropUI(ePropType type)
	{
		RaceSceneData.GetPlayerData((ulong)TSingleton<CarManager>.Instance.GetHeroCar().ID, ref playerData);
		playerData.altProp = type;
		RaceSceneData.SetPlayerData((ulong)TSingleton<CarManager>.Instance.GetHeroCar().ID, playerData);
		m_PickedType = type;
		if (m_PropUI == null || m_PropUI.Count <= 0)
		{
			return;
		}
		m_UpIndex = (m_DownIndex = -100);
		for (int i = 0; i < m_PropUI.Count; i++)
		{
			if (m_PropUI[i] != null && ((GameObject)m_PropUI[i]).name.Equals(typesStrings[(int)type]))
			{
				m_StartIndex = i - 5;
				m_EndIndex = i + 4;
				m_DownIndex = i;
				m_SelectIndex = i;
				m_UpIndex = m_DownIndex - 1;
				break;
			}
		}
		if (m_DownIndex != -100)
		{
			m_UpProp = (GameObject)m_PropUI[(m_UpIndex + m_PropUI.Count) % m_PropUI.Count];
			m_UpScale.x = 1f;
			m_UpScale.y = 0f;
			m_UpScale.z = 1f;
			m_UpProp.transform.localScale = m_UpScale;
			tempVector3 = m_UpProp.transform.localPosition;
			tempVector3.y = 75f;
			m_UpProp.transform.localPosition = tempVector3;
			m_UpPos = m_UpProp.transform.localPosition;
			m_DownProp = (GameObject)m_PropUI[(m_DownIndex + m_PropUI.Count) % m_PropUI.Count];
			m_DownScale.x = 1f;
			m_DownScale.y = 1f;
			m_DownScale.z = 1f;
			m_DownProp.transform.localScale = m_DownScale;
			tempVector3 = m_DownProp.transform.localPosition;
			tempVector3.y = 0f;
			m_DownProp.transform.localPosition = tempVector3;
			m_DownPos = m_DownProp.transform.localPosition;
			m_DownProp.active = true;
			m_UpProp.active = true;
			m_StopRoll = false;
			m_RollTime = 0f;
			m_DelayPlaySoundTime = 0f;
			PropIDManager.Instance.RequestID();
		}
	}

	public void ShowProp(ePropType type)
	{
		if (type == ePropType.NONE)
		{
			SoundManager.Instance.StopSound(50002);
			SceneUIManager.Instance.ShowTutorialUI(show: false);
			m_DelayPlaySoundTime = -2f;
		}
		if (null != m_UpProp)
		{
			m_UpProp.active = false;
			m_UpProp = null;
		}
		if (null != m_DownProp)
		{
			m_DownProp.active = false;
			m_DownProp = null;
		}
		if (null != m_PreShowProp)
		{
			m_PreShowProp.gameObject.active = false;
		}
		m_PreShowProp = m_Transform.FindChild("RollProp/" + type);
		if (null != m_PreShowProp)
		{
			m_PreShowProp.gameObject.active = true;
			tempVector3.x = 1f;
			tempVector3.y = 1f;
			tempVector3.z = 1f;
			m_PreShowProp.localScale = tempVector3;
			tempVector3.x = 0f;
			tempVector3.y = 0f;
			tempVector3.z = 0f;
			m_PreShowProp.localPosition = tempVector3;
		}
		m_StopRoll = true;
	}

	public void UpdateUI()
	{
		if (m_DelayPlaySoundTime > -1f)
		{
			m_DelayPlaySoundTime += Time.deltaTime;
		}
		if (m_DelayPlaySoundTime > 0.5f)
		{
			SoundManager.Instance.PlaySoundEx(50002);
			m_DelayPlaySoundTime = -2f;
		}
		if (m_HideRollUI || m_StopRoll || !(null != m_UpProp) || !(null != m_DownProp))
		{
			return;
		}
		m_RollTime += Time.deltaTime;
		if (m_UpProp.transform.localScale.y >= 1f || m_DownProp.transform.localScale.y <= 0f)
		{
			m_DownProp.active = false;
			m_DownIndex = m_UpIndex;
			m_UpIndex--;
			if (m_UpIndex < m_StartIndex)
			{
				m_UpIndex = m_EndIndex;
			}
			if (m_RollTime > 4f && m_UpProp.name.Equals(m_PickedType.ToString()))
			{
				m_PreShowProp = m_UpProp.transform;
				m_StopRoll = true;
				Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
				if (null != heroCar)
				{
					heroCar.PickProp(m_PickedType);
					if (m_PickedType != ePropType.THUNDER_CLOUD)
					{
						if (LocalUserConfig.Instance.CanShowTutorial)
						{
							SceneUIManager.Instance.ShowTutorialUI(show: true);
						}
						else
						{
							SceneUIManager.Instance.DestroyTutorialUI();
						}
					}
				}
				if (m_PickedType == ePropType.THUNDER_CLOUD)
				{
					SkillManager.Instance.UseProp(heroCar, throwForward: false);
				}
			}
			else
			{
				Vector3 vector;
				if (m_RollTime > 3.5f)
				{
					m_UpProp = (GameObject)m_PropUI[m_SelectIndex];
					tempVector3.x = 1f;
					tempVector3.y = 0f;
					tempVector3.z = 1f;
					vector = tempVector3;
					m_UpProp.transform.localScale = vector;
					m_UpScale = vector;
					tempVector3 = m_UpProp.transform.localPosition;
					tempVector3.y = 75f;
					vector = tempVector3;
					m_UpProp.transform.localPosition = vector;
					m_UpPos = vector;
					m_UpProp.active = true;
				}
				else
				{
					m_UpProp = (GameObject)m_PropUI[(m_UpIndex + m_PropUI.Count) % m_PropUI.Count];
					tempVector3.x = 1f;
					tempVector3.y = 0f;
					tempVector3.z = 1f;
					vector = tempVector3;
					m_UpProp.transform.localScale = vector;
					m_UpScale = vector;
					tempVector3 = m_UpProp.transform.localPosition;
					tempVector3.y = 75f;
					vector = tempVector3;
					m_UpProp.transform.localPosition = vector;
					m_UpPos = vector;
					m_UpProp.active = true;
				}
				m_DownProp = (GameObject)m_PropUI[(m_DownIndex + m_PropUI.Count) % m_PropUI.Count];
				tempVector3.x = 1f;
				tempVector3.y = 1f;
				tempVector3.z = 1f;
				vector = tempVector3;
				m_DownProp.transform.localScale = vector;
				m_DownScale = vector;
				tempVector3 = m_DownProp.transform.localPosition;
				tempVector3.y = 0f;
				vector = tempVector3;
				m_DownProp.transform.localPosition = vector;
				m_DownPos = vector;
				m_DownProp.active = true;
			}
		}
		if (!m_StopRoll)
		{
			if (m_RollTime > 4f)
			{
				m_RollTime = 4f;
			}
			float num = (float)(5 - (int)m_RollTime) * Time.deltaTime;
			m_UpScale.y += num;
			m_UpScale.y = ((!(m_UpScale.y > 1f)) ? m_UpScale.y : 1f);
			m_UpScale.y = ((!(m_UpScale.y < 0f)) ? m_UpScale.y : 0f);
			m_UpPos.y -= num * 75f;
			m_UpProp.transform.localPosition = m_UpPos;
			m_UpProp.transform.localScale = m_UpScale;
			m_DownScale.y -= num;
			m_DownScale.y = ((!(m_DownScale.y < 0f)) ? m_DownScale.y : 0f);
			m_DownScale.y = ((!(m_DownScale.y > 1f)) ? m_DownScale.y : 1f);
			m_DownPos.y -= num * 75f;
			m_DownProp.transform.localScale = m_DownScale;
			m_DownProp.transform.localPosition = m_DownPos;
		}
	}
}
