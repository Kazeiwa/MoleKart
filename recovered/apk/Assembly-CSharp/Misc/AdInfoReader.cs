using System;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Misc;

public class AdInfoReader
{
	public AdInfo Info { get; set; }

	public AdInfoReader(AdInfo info)
	{
		Info = info;
	}

	public bool Read(byte[] bytes)
	{
		XmlDocument xmlDocument = new XmlDocument();
		MemoryStream inStream = new MemoryStream(bytes);
		try
		{
			xmlDocument.Load(inStream);
		}
		catch (XmlException ex)
		{
			Debug.LogWarning("AdInfo parse error: " + ex);
			return false;
		}
		XmlElement documentElement = xmlDocument.DocumentElement;
		if (documentElement == null || documentElement.Name != "taomee_ad")
		{
			return false;
		}
		AdInfo info = Info;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "id")
			{
				info.ID = childNode.InnerText;
			}
			else if (childNode.Name == "down_url")
			{
				info.DownloadUrl = childNode.InnerText;
			}
			else if (childNode.Name == "pic_url")
			{
				info.ImageUrl = childNode.InnerText;
			}
			else if (childNode.Name == "words")
			{
				info.Words = childNode.InnerText;
			}
		}
		return info.DownloadUrl != null && info.ImageUrl != null;
	}

	private static bool ReadInteger(XmlNode root, out int value)
	{
		try
		{
			int num = int.Parse(root.InnerText);
			value = num;
		}
		catch (Exception)
		{
			value = 0;
			return false;
		}
		return true;
	}

	private static bool ReadFloat(XmlNode root, out float value)
	{
		try
		{
			float num = float.Parse(root.InnerText);
			value = num;
		}
		catch (Exception)
		{
			value = 0f;
			return false;
		}
		return true;
	}
}
