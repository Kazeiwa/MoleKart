using System.Collections;
using UnityEngine;

public class NameController : MonoBehaviour
{
	public UITextField playerField;

	public SpriteText playerName;

	public GameObject firstTip;

	private void Start()
	{
		if (LocalUserConfig.Instance.LoadShowFirstTip())
		{
			firstTip.SetActiveRecursively(state: true);
			LocalUserConfig.Instance.SaveShowFirstTip(bShow: false);
		}
		playerName.Text = LocalUserConfig.Instance.userName;
		playerField.Text = LocalUserConfig.Instance.userName;
		playerField.SetCommitDelegate(CommitPlayerName);
	}

	private void CommitPlayerName(IKeyFocusable control)
	{
		firstTip.SetActiveRecursively(state: false);
		if (playerName.text.Length > 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("&");
			arrayList.Add("<");
			arrayList.Add(">");
			arrayList.Add(" ");
			string text = playerName.text;
			for (int i = 0; i < arrayList.Count; i++)
			{
				text = text.Replace((string)arrayList[i], string.Empty);
			}
			if (text.Length > 8)
			{
				text = text.Substring(0, 8);
			}
			if (text.Length > 0)
			{
				playerName.Text = text;
				playerField.Text = text;
				if (null != playerName)
				{
					LocalUserConfig.Instance.userName = playerName.Text;
					LocalUserConfig.Instance.SaveDataToDisk();
				}
			}
			else
			{
				playerName.Text = LocalUserConfig.Instance.userName;
				playerField.Text = LocalUserConfig.Instance.userName;
			}
		}
		else
		{
			playerName.Text = LocalUserConfig.Instance.userName;
			playerField.Text = LocalUserConfig.Instance.userName;
		}
	}
}
