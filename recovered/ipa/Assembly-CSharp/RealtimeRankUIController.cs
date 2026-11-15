using UnityEngine;

public class RealtimeRankUIController : ISceneUI
{
	private GameObject[] numbers;

	private int curRank;

	private int lastRank;

	private bool canShowUI;

	private float deltaTime1;

	private float deltaTime2;

	public bool CanShowUI
	{
		set
		{
		}
	}

	public void InitUI(Transform transform)
	{
	}

	private bool CheckRankChanged()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void PlayRankSound(int rank)
	{
	}

	public void showUI(bool show)
	{
	}

	public void HideAll()
	{
	}

	public void UpdateUI()
	{
	}
}
