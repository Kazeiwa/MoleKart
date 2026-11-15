using System.Collections.Generic;
using System.Xml;

public class ParseResourceXml : IXmlParserListen
{
	public static string xmlDefaultPath;

	private XmlDocument xmldoc;

	private OnParsedObject listener;

	private ResourceCollect resourceCollect;

	public ParseResourceXml(XmlDocument xmldoc)
	{
	}

	public ParseResourceXml()
	{
	}

	public void Parse(string xmlPath, OnParsedObject listener, int level, int scene)
	{
	}

	private void ParseSharedNode(XmlNode nodeShared)
	{
	}

	private void ParseObjectNode(XmlNode objectsNode, bool shared)
	{
	}

	private ResourceType PrefabStringToInt(string typeString)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void GetAllObjectsName(Dictionary<string, string> nameMap)
	{
	}

	private void ParseSharedNodeEx(XmlNode nodeShared, Dictionary<string, string> nameMap)
	{
	}

	private void ParseObjectNodeEx(XmlNode objectNode, bool shared, Dictionary<string, string> nameMap)
	{
	}
}
