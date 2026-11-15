using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EffectManager
{
	private static EffectManager instance;

	private static Dictionary<eEffectType, GameObject> m_EftMap;

	public static FXMgrShadow fxMgrShadow;

	public static Camera sceneCamera;

	private static string xml_path;

	public static EffectManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private bool Parse()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseXml(XmlDocument xml)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Init()
	{
	}

	private GameObject InstantiateEffect(eEffectType type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public GameObject CreateEffect(eEffectType type, bool isPause, bool isOneShot)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public GameObject CreateEffect(eEffectType type, bool isPause, bool isOneShot, ref Effect eft)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveEffect(GameObject effect)
	{
	}
}
