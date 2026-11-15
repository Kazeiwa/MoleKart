using System;

public class ResourceItem
{
	public static int MAX_FOLDERS = 10;

	public string name;

	public ResourceType type;

	public string localFolder;

	public string[] localFolders = new string[MAX_FOLDERS];

	public string remoteFolder;

	public override string ToString()
	{
		string text = "name: " + name;
		text = text + "\r\nType: " + Convert.ToInt16(type);
		text = text + "\r\nLocalFolder: " + localFolder;
		return text + "\r\nRemoteFolder: " + remoteFolder;
	}
}
