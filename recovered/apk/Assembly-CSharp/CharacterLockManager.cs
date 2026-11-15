using System.Xml;
using UnityEngine;

public class CharacterLockManager
{
	private XmlDocument m_XmlDoc;

	private static CharacterLockManager instance;

	private string xml_path = "Config/xmlConfig/ReleaseCharacterConfig";

	public static CharacterLockManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new CharacterLockManager();
			}
			return instance;
		}
	}

	private CharacterLockManager()
	{
	}

	public int ReleaseCharacter(int type)
	{
		int num = LocalUserConfig.Instance.ReleaseCharacter(type);
		if (num == 1)
		{
			return 0;
		}
		if (m_XmlDoc == null)
		{
			m_XmlDoc = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				m_XmlDoc.LoadXml(textAsset.text);
			}
		}
		if (m_XmlDoc != null)
		{
			XmlNode xmlNode = m_XmlDoc.SelectSingleNode("Characters");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ID_" + type);
				if (xmlNode2 != null)
				{
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("Charge");
					if (xmlNode3 != null && int.Parse(xmlNode3.InnerText) == 1)
					{
						return 1;
					}
					XmlNode xmlNode4 = xmlNode2.SelectSingleNode("TotalMedal");
					if (xmlNode4 != null)
					{
						int num2 = int.Parse(xmlNode4.InnerText);
						if (num2 > 0)
						{
							if (LocalUserConfig.Instance.TotalMedals() >= num2)
							{
								LocalUserConfig.Instance.SaveReleaseCharacter(type);
								return 0;
							}
							return 2;
						}
					}
					for (int i = 1; i <= 9; i++)
					{
						XmlNode xmlNode5 = xmlNode2.SelectSingleNode("ID_110" + i);
						if (xmlNode5 != null)
						{
							int num3 = int.Parse(xmlNode5.InnerText);
							if (num3 > 0 && num3 > LocalUserConfig.Instance.Medals(1100 + i))
							{
								return 3;
							}
						}
					}
				}
			}
		}
		LocalUserConfig.Instance.SaveReleaseCharacter(type);
		string text = null;
		switch (type)
		{
		case 2001:
			text = "com.taomee.molekart.MOMO";
			break;
		case 2004:
			text = "com.taomee.molekart.Kula";
			break;
		}
		if (text != null)
		{
			FlurryBinding.ReportBuyCharacter(text);
		}
		return 0;
	}

	public int NeedTotalMedals(int sceneID)
	{
		if (m_XmlDoc == null)
		{
			m_XmlDoc = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				m_XmlDoc.LoadXml(textAsset.text);
			}
		}
		if (m_XmlDoc != null)
		{
			XmlNode xmlNode = m_XmlDoc.SelectSingleNode("Characters");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ID_" + sceneID);
				if (xmlNode2 != null)
				{
					XmlNode xmlNode3 = xmlNode2.SelectSingleNode("TotalMedal");
					if (xmlNode3 != null)
					{
						return int.Parse(xmlNode3.InnerText);
					}
				}
			}
		}
		return 0;
	}

	public int GetSpecificMedals(int sceneID)
	{
		if (m_XmlDoc == null)
		{
			m_XmlDoc = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				m_XmlDoc.LoadXml(textAsset.text);
			}
		}
		if (m_XmlDoc != null)
		{
			XmlNode xmlNode = m_XmlDoc.SelectSingleNode("Characters");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ID_" + sceneID);
				if (xmlNode2 != null)
				{
					int num = 0;
					for (int i = 1; i <= 9; i++)
					{
						XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_110" + i);
						if (xmlNode3 != null)
						{
							int num2 = int.Parse(xmlNode3.InnerText);
							if (num2 > 0)
							{
								num += LocalUserConfig.Instance.Medals(1100 + i);
							}
						}
					}
					return num;
				}
			}
		}
		return 0;
	}

	public int NeedSpecificMedals(int sceneID)
	{
		if (m_XmlDoc == null)
		{
			m_XmlDoc = new XmlDocument();
			Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				m_XmlDoc.LoadXml(textAsset.text);
			}
		}
		if (m_XmlDoc != null)
		{
			XmlNode xmlNode = m_XmlDoc.SelectSingleNode("Characters");
			if (xmlNode != null)
			{
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ID_" + sceneID);
				if (xmlNode2 != null)
				{
					int num = 0;
					for (int i = 1; i <= 9; i++)
					{
						XmlNode xmlNode3 = xmlNode2.SelectSingleNode("ID_110" + i);
						if (xmlNode3 != null)
						{
							num += int.Parse(xmlNode3.InnerText);
						}
					}
					return num;
				}
			}
		}
		return 0;
	}

	public int Medal(int sceneID, int easy)
	{
		return LocalUserConfig.Instance.Medal(sceneID, easy);
	}

	public int Medals(int sceneID)
	{
		return LocalUserConfig.Instance.Medals(sceneID);
	}

	public int TotalMedals()
	{
		return LocalUserConfig.Instance.TotalMedals();
	}

	public void SaveReleaseCharacter(int type)
	{
		LocalUserConfig.Instance.SaveReleaseCharacter(type);
	}

	public int CheckDiffculty(int trackID)
	{
		if (Medal(trackID, 1) > 0)
		{
			return 2;
		}
		if (Medal(trackID, 0) > 0)
		{
			return 1;
		}
		return 0;
	}
}
