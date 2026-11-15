using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ParseResourceXml : IXmlParserListen
{
	public static string xmlDefaultPath = "resource.xml";

	private XmlDocument xmldoc;

	private OnParsedObject listener;

	private ResourceCollect resourceCollect = new ResourceCollect();

	public ParseResourceXml(XmlDocument xmldoc)
	{
		this.xmldoc = xmldoc;
	}

	public ParseResourceXml()
	{
	}

	public void Parse(string xmlPath, OnParsedObject listener, int level, int scene)
	{
		if (xmldoc == null)
		{
			xmldoc = DownloadXML.Load(xmlPath);
		}
		this.listener = listener;
		if (xmldoc == null || listener == null)
		{
			return;
		}
		string text = "level" + level;
		string text2 = "scene" + scene;
		string xpath = text + "/" + text2;
		XmlNodeList xmlNodeList = xmldoc.SelectNodes(xpath);
		if (xmlNodeList == null || xmlNodeList.Count <= 0)
		{
			return;
		}
		foreach (XmlNode childNode in xmlNodeList[0].ChildNodes)
		{
			resourceCollect.RemoveAllResources();
			if (childNode.NodeType == XmlNodeType.Element && childNode.Name.Equals("sharedgroup"))
			{
				ParseSharedNode(childNode);
				this.listener(resourceCollect, shared: true);
			}
			else if (childNode.NodeType == XmlNodeType.Element && childNode.Name.Equals("objects"))
			{
				foreach (XmlNode item in childNode.SelectNodes("object"))
				{
					ParseObjectNode(item, shared: false);
				}
				this.listener(resourceCollect, shared: false);
			}
			else if (childNode.NodeType == XmlNodeType.Element && childNode.Name.Equals("players"))
			{
				foreach (XmlNode item2 in childNode.SelectNodes("player"))
				{
					ParseObjectNode(item2, shared: false);
				}
				this.listener(resourceCollect, shared: false);
			}
			else
			{
				Debug.LogError("Cauch unknown xml node : " + childNode.Name);
			}
		}
	}

	private void ParseSharedNode(XmlNode nodeShared)
	{
		foreach (XmlNode item in nodeShared.SelectNodes("sharedobjects/object"))
		{
			ParseObjectNode(item, shared: true);
		}
		foreach (XmlNode item2 in nodeShared.SelectNodes("objects/object"))
		{
			ParseObjectNode(item2, shared: true);
		}
	}

	private void ParseObjectNode(XmlNode objectsNode, bool shared)
	{
		if (objectsNode == null)
		{
			return;
		}
		ResourceItem resourceItem = new ResourceItem();
		resourceItem.name = objectsNode.Attributes.GetNamedItem("name").Value;
		resourceItem.type = PrefabStringToInt(objectsNode.SelectSingleNode("type").InnerText);
		if (resourceItem.type == ResourceType.ResourcePlayer)
		{
			int num = 0;
			foreach (XmlNode item in objectsNode.SelectNodes("localfolder"))
			{
				resourceItem.localFolders[num] = item.InnerText;
				num++;
				if (num >= ResourceItem.MAX_FOLDERS)
				{
					break;
				}
			}
		}
		else
		{
			resourceItem.localFolder = objectsNode.SelectSingleNode("localfolder").InnerText;
		}
		resourceItem.remoteFolder = objectsNode.SelectSingleNode("remotefolder").InnerText;
		resourceCollect.AddResource(resourceItem, shared);
	}

	private ResourceType PrefabStringToInt(string typeString)
	{
		return typeString switch
		{
			"Prefab" => ResourceType.ResourcePrefab, 
			"Texture" => ResourceType.ResourceTexture, 
			"Player" => ResourceType.ResourcePlayer, 
			_ => ResourceType.ResourcePrefab, 
		};
	}

	public void GetAllObjectsName(Dictionary<string, string> nameMap)
	{
		if (xmldoc == null)
		{
			Debug.LogError("Can not run here");
			xmldoc = DownloadXML.Load(xmlDefaultPath);
		}
		if (xmldoc == null)
		{
			return;
		}
		foreach (XmlNode childNode in xmldoc.ChildNodes)
		{
			if (childNode.NodeType != XmlNodeType.Element || !childNode.Name.Contains("level"))
			{
				continue;
			}
			foreach (XmlNode childNode2 in childNode.ChildNodes)
			{
				if (childNode2.NodeType != XmlNodeType.Element || !childNode2.Name.Contains("scene"))
				{
					continue;
				}
				foreach (XmlNode item in childNode2.SelectNodes("sharedgroup"))
				{
					ParseSharedNodeEx(item, nameMap);
				}
				foreach (XmlNode item2 in childNode2.SelectNodes("objects/object"))
				{
					ParseObjectNodeEx(item2, shared: false, nameMap);
				}
			}
		}
	}

	private void ParseSharedNodeEx(XmlNode nodeShared, Dictionary<string, string> nameMap)
	{
		foreach (XmlNode item in nodeShared.SelectNodes("sharedobjects/object"))
		{
			ParseObjectNodeEx(item, shared: true, nameMap);
		}
		foreach (XmlNode item2 in nodeShared.SelectNodes("objects/object"))
		{
			ParseObjectNodeEx(item2, shared: true, nameMap);
		}
	}

	private void ParseObjectNodeEx(XmlNode objectNode, bool shared, Dictionary<string, string> nameMap)
	{
		if (objectNode != null)
		{
			string value = objectNode.Attributes.GetNamedItem("name").Value;
			string innerText = objectNode.SelectSingleNode("remotefolder").InnerText;
			nameMap.Add(value, innerText);
		}
	}
}
