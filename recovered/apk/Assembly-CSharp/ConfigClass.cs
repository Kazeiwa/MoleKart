using System.Xml;

public class ConfigClass
{
	public string strFileName;

	private XmlDocument m_xmlDoc;

	public ConfigClass(XmlDocument xmlDoc)
	{
		m_xmlDoc = xmlDoc;
	}

	public string ReadConfig(string configKey)
	{
		string result = string.Empty;
		XmlNode xmlNode = m_xmlDoc.ChildNodes.Item(0);
		if (xmlNode == null)
		{
			return result;
		}
		foreach (XmlNode childNode in xmlNode.ChildNodes)
		{
			if (childNode.Name.Equals(configKey))
			{
				result = childNode.InnerText;
				break;
			}
		}
		return result;
	}

	public bool ReadBool(string key)
	{
		string floatString = ReadConfig(key);
		return (int)Utility.StringToFloat(floatString) > 0;
	}

	public void SaveConfig(string configKey, string configValue)
	{
		XmlDocument xmlDocument = EncryptXMLDocument.Load(strFileName);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("Config");
		if (xmlNode != null)
		{
			foreach (XmlNode childNode in xmlNode.ChildNodes)
			{
				if (childNode.Name == configKey)
				{
					childNode.InnerXml = configValue;
					break;
				}
			}
		}
		EncryptXMLDocument.SaveToEncryptXML(xmlDocument, strFileName);
	}
}
