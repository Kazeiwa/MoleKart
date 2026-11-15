using UnityEngine;

public class RealtimeRankUIController : ISceneUI
{
	private GameObject[] numbers = new GameObject[6];

	private int curRank = -1;

	private int lastRank = -1;

	private bool canShowUI;

	private float deltaTime1 = -1f;

	private float deltaTime2 = -1f;

	public bool CanShowUI
	{
		set
		{
			canShowUI = value;
		}
	}

	public void InitUI(Transform transform)
	{
		numbers[0] = transform.FindChild("RealtimeRank/1").gameObject;
		Object.Destroy(numbers[0].GetComponent<UIPanelTab>());
		numbers[1] = transform.FindChild("RealtimeRank/2").gameObject;
		Object.Destroy(numbers[1].GetComponent<UIPanelTab>());
		numbers[2] = transform.FindChild("RealtimeRank/3").gameObject;
		Object.Destroy(numbers[2].GetComponent<UIPanelTab>());
		numbers[3] = transform.FindChild("RealtimeRank/4").gameObject;
		Object.Destroy(numbers[3].GetComponent<UIPanelTab>());
		numbers[4] = transform.FindChild("RealtimeRank/5").gameObject;
		Object.Destroy(numbers[4].GetComponent<UIPanelTab>());
		numbers[5] = transform.FindChild("RealtimeRank/6").gameObject;
		Object.Destroy(numbers[5].GetComponent<UIPanelTab>());
	}

	private bool CheckRankChanged()
	{
		Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
		if (null != heroCar)
		{
			PlayerData data = new PlayerData();
			if (RaceSceneData.GetPlayerData((ulong)heroCar.ID, ref data))
			{
				if (data.Ranking <= 0)
				{
					return false;
				}
				lastRank = curRank;
				curRank = data.Ranking - 1;
				if (lastRank != curRank)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void PlayRankSound(int rank)
	{
		switch (rank)
		{
		case 1:
			SoundManager.Instance.PlaySound(60007);
			break;
		case 2:
			SoundManager.Instance.PlaySound(60008);
			break;
		case 3:
			SoundManager.Instance.PlaySound(60009);
			break;
		case 4:
			SoundManager.Instance.PlaySound(60010);
			break;
		case 5:
			SoundManager.Instance.PlaySound(60011);
			break;
		case 6:
			SoundManager.Instance.PlaySound(60012);
			break;
		}
	}

	public void showUI(bool show)
	{
		canShowUI = show;
		if (!show)
		{
			HideAll();
		}
	}

	public void HideAll()
	{
		for (int i = 0; i < 6; i++)
		{
			if (numbers[i] != null)
			{
				numbers[i].active = false;
			}
		}
	}

	public void UpdateUI()
	{
		if (!canShowUI)
		{
			return;
		}
		CheckRankChanged();
		for (int i = 0; i < 6; i++)
		{
			if (numbers[i] != null)
			{
				if (i == curRank)
				{
					numbers[i].active = true;
				}
				else
				{
					numbers[i].active = false;
				}
			}
		}
		if (deltaTime1 < 0f && deltaTime2 < 0f)
		{
			deltaTime1 = 0.5f;
			deltaTime2 = 0.3f;
		}
	}
}
