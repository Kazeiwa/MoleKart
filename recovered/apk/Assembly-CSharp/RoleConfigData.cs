using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RoleConfigData : TSingleton<RoleConfigData>
{
	public static Dictionary<int, RoleInf> m_RoleMap = new Dictionary<int, RoleInf>();

	private string xml_path = "Config/xmlConfig/RoleConfig";

	private bool m_bInit;

	public bool bInited => m_bInit;

	public bool Init()
	{
		if (m_RoleMap == null || m_RoleMap.Count <= 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				xmlDocument.LoadXml(textAsset.text);
				m_bInit = true;
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
		XmlNode xmlNode = xml.SelectSingleNode("Roles");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Role");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					if (item != null)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("Low_Mode");
						XmlNode xmlNode4 = item.SelectSingleNode("High_Mode");
						XmlNode xmlNode5 = item.SelectSingleNode("ID");
						XmlNode xmlNode6 = item.SelectSingleNode("type");
						XmlNode xmlNode7 = item.SelectSingleNode("Npc_Mode");
						if (xmlNode3 == null || xmlNode5 == null || xmlNode6 == null || xmlNode4 == null || xmlNode7 == null)
						{
							Debug.LogError("SceneConfig is error, please correct");
							return false;
						}
						if (!m_RoleMap.ContainsKey(int.Parse(xmlNode5.InnerText)))
						{
							RoleInf value = new RoleInf
							{
								low_mode = xmlNode3.InnerText,
								type = int.Parse(xmlNode6.InnerText),
								high_mode = xmlNode4.InnerText,
								npc_mode = xmlNode7.InnerText,
								ID = int.Parse(xmlNode5.InnerText)
							};
							m_RoleMap.Add(int.Parse(xmlNode5.InnerText), value);
						}
					}
				}
				return true;
			}
			Debug.LogWarning("Have no Role node in RoleConfig.xml file");
			return false;
		}
		Debug.LogWarning("Roles node is null in RoleConfig.xml file");
		return false;
	}
}
