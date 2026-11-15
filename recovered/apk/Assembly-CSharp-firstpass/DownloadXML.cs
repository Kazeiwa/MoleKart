using System.Xml;
using UnityEngine;

public class DownloadXML
{
	public static XmlDocument Load(string xmlPath)
	{
		xmlPath = GetRomoteFolder() + xmlPath;
		WWW wWW = new WWW(xmlPath);
		if (wWW != null && wWW.error == null)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(wWW.text);
			return xmlDocument;
		}
		Debug.Log("ERROR: " + wWW.error);
		return null;
	}

	private static string GetRomoteFolder()
	{
		if (Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			return Application.dataPath + "/assetbundles/";
		}
		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
		{
			return "file://" + Application.dataPath + "/../AssetBundles/";
		}
		return string.Empty;
	}
}
