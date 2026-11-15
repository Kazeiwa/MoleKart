using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class VersionUpdateChecker : MonoBehaviour
{
	public string updateMessage;

	private static string versionUrl = "http://wlad.61.com/icar/icar_android_version.xml";

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		StartCoroutine("CheckVersion");
	}

	private IEnumerator CheckVersion()
	{
		WWW infoWww = new WWW(versionUrl);
		yield return infoWww;
		if (infoWww.error != null || infoWww.bytes.Length <= 0)
		{
			yield break;
		}
		string lastUpdateVersion = LocalUserConfig.Instance.LoadLastRequiredUpdateVersion();
		string newVersion = GetNewVersion(infoWww.text, AppMain.Instance.channel);
		string currVersion = AppMain.Instance.currVersion;
		if (newVersion.Length > 0 && lastUpdateVersion.Length > 0 && currVersion.Length > 0)
		{
			int ret1 = CompareVersions(lastUpdateVersion, newVersion);
			int ret2 = CompareVersions(currVersion, newVersion);
			if (ret1 == -1 && ret2 == -1)
			{
				UtilBinding.ShowToast(updateMessage);
				LocalUserConfig.Instance.SaveLastRequiredUpdateVersion(newVersion);
			}
		}
	}

	private string GetNewVersion(string versionInfo, string channelName)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(versionInfo);
		string result = string.Empty;
		XmlNode xmlNode = xmlDocument.SelectSingleNode("ICarAndroidVersion");
		if (xmlNode == null)
		{
			return result;
		}
		foreach (XmlNode childNode in xmlNode.ChildNodes)
		{
			if (channelName.Equals(childNode.Attributes["channel"].Value))
			{
				result = childNode.InnerText;
				break;
			}
		}
		return result;
	}

	public int CompareVersions(string strA, string strB)
	{
		char[] separator = new char[2] { '.', ',' };
		string[] array = strA.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		string[] array2 = strB.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		int[] array3 = new int[3];
		int[] array4 = new int[3];
		for (int i = 0; i < 3; i++)
		{
			array3[i] = Convert.ToInt32(array[i]);
			array4[i] = Convert.ToInt32(array2[i]);
		}
		return RecursiveCompareArrays(array3, array4, 0);
	}

	private int RecursiveCompareArrays(int[] versionA, int[] versionB, int idx)
	{
		if (versionA[idx] < versionB[idx])
		{
			return -1;
		}
		if (versionA[idx] > versionB[idx])
		{
			return 1;
		}
		if (idx == versionA.Length - 1)
		{
			return 0;
		}
		return RecursiveCompareArrays(versionA, versionB, idx + 1);
	}
}
