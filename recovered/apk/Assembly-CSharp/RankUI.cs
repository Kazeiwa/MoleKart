using UnityEngine;

public class RankUI : MonoBehaviour
{
	public TextAsset fontTextAsset;

	public Texture fontTexture;

	public GameObject finish;

	public GameObject unFinish;

	public GameObject breakRec;

	public GameObject medal;

	public GameObject words;

	public GameObject easyWord;

	public GameObject normalWord;

	public GameObject masterWord;

	public GameObject background;

	public GameObject congra;

	public GameObject aero;

	private void Awake()
	{
		if (!AppMain.Instance.IsOnlineMode)
		{
			Debug.Log("Current Rank UI is RankUIBase");
			RankUIBase rankUIBase = base.gameObject.AddComponent<RankUIBase>();
			rankUIBase.InitRankRoot(this);
		}
		else if (AppMain.Instance.IsServer)
		{
			Debug.Log("Current Rank UI is HostRankUIBase");
			OnlineRankUIBase onlineRankUIBase = base.gameObject.AddComponent<HostRankUI>();
			onlineRankUIBase.InitRankRoot(this);
		}
		else if (AppMain.Instance.IsClient)
		{
			Debug.Log("Current Rank UI is ClientRankUIBase");
			OnlineRankUIBase onlineRankUIBase2 = base.gameObject.AddComponent<ClientRankUI>();
			onlineRankUIBase2.InitRankRoot(this);
		}
		else
		{
			Debug.Log("Known mode when show rank.");
		}
	}
}
