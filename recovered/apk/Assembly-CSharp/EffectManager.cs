using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EffectManager
{
	private static EffectManager instance;

	private static Dictionary<eEffectType, GameObject> m_EftMap;

	public static FXMgrShadow fxMgrShadow;

	public static Camera sceneCamera;

	private static string xml_path = "Config/xmlConfig/EffectConfig";

	public static EffectManager Instance
	{
		get
		{
			if (instance == null)
			{
				new EffectManager();
			}
			return instance;
		}
	}

	public EffectManager()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private bool Parse()
	{
		if (m_EftMap == null || m_EftMap.Count <= 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				xmlDocument.LoadXml(textAsset.text);
				return ParseXml(xmlDocument);
			}
			return false;
		}
		return true;
	}

	private bool ParseXml(XmlDocument xml)
	{
		if (xml == null)
		{
			return false;
		}
		XmlNode xmlNode = xml.SelectSingleNode("Root");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Eft");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					if (item != null)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("Path");
						XmlNode xmlNode4 = item.SelectSingleNode("ID");
						if (xmlNode4 == null || xmlNode3 == null)
						{
							Debug.LogError("EffectConfig is error, please correct");
							return false;
						}
						Object obj = Resources.Load(xmlNode3.InnerText);
						if (null != obj)
						{
							m_EftMap.Add((eEffectType)int.Parse(xmlNode4.InnerText), (GameObject)obj);
						}
						else
						{
							Debug.LogError(xmlNode3.InnerText + " not exist");
						}
					}
				}
				return true;
			}
			Debug.LogError("Have no Eft node in EffectConfig.xml file");
			return false;
		}
		Debug.LogError("Root node is null in EffectConfig.xml file");
		return false;
	}

	public void Init()
	{
		if (m_EftMap == null)
		{
			m_EftMap = new Dictionary<eEffectType, GameObject>();
		}
		if (!Parse())
		{
			Debug.LogError("EffectManager.Init is error, please check");
			Debug.Break();
		}
	}

	private GameObject InstantiateEffect(eEffectType type)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(m_EftMap[type]);
		gameObject.name = type.ToString();
		gameObject.AddComponent<Effect>();
		return gameObject;
	}

	public GameObject CreateEffect(eEffectType type, bool isPause, bool isOneShot)
	{
		GameObject gameObject = InstantiateEffect(type);
		Effect component = gameObject.GetComponent<Effect>();
		component.SetOneShot(isOneShot);
		if (isPause)
		{
			component.Pause();
		}
		return gameObject;
	}

	public GameObject CreateEffect(eEffectType type, bool isPause, bool isOneShot, ref Effect eft)
	{
		GameObject gameObject = InstantiateEffect(type);
		Effect component = gameObject.GetComponent<Effect>();
		component.SetOneShot(isOneShot);
		eft = component;
		if (isPause)
		{
			component.Pause();
		}
		return gameObject;
	}

	public void RemoveEffect(GameObject effect)
	{
		Object.Destroy(effect);
	}
}
