using System.Collections.Generic;
using Leaderboard;
using UnityEngine;

public class rankController : MonoBehaviour
{
	public static rankController instance;

	public GameObject rankList_no1;

	public GameObject rankList_no2;

	public GameObject rankList_no3;

	public GameObject rankList_normal;

	public GameObject moreDownBtn;

	public GameObject playerScoreInfo_rank;

	public GameObject playerScoreInfo_no;

	public GameObject playerScoreInfo_name;

	public GameObject playerScoreInfo_time;

	private int curPage;

	private bool bHasDownBtn;

	private int MAX_LINES = 200;

	public ExtendScrollList rankList;

	private void Start()
	{
		instance = this;
	}

	public void insertDataToRankList(List<MKScore> list)
	{
		if (rankList.Count >= 1 && bHasDownBtn)
		{
			bHasDownBtn = false;
			rankList.RemoveItem(rankList.Count - 1, destroy: true);
		}
		int num = rankList.Count;
		List<string> list2 = new List<string>();
		for (int i = 0; i < list.Count; i++)
		{
			if (rankList.Count >= MAX_LINES)
			{
				return;
			}
			MKScore mKScore = list[i];
			string item = string.Empty + mKScore.getRank();
			string item2 = string.Empty + mKScore.getMimihao();
			string item3 = mKScore.getName();
			string score = mKScore.getScore();
			if (num <= 2)
			{
				list2.Clear();
				list2.Add(item2);
				list2.Add(item3);
				list2.Add(score);
				switch (num)
				{
				case 0:
					rankList.CreateItem(rankList_no1, num, list2);
					break;
				case 1:
					rankList.CreateItem(rankList_no2, num, list2);
					break;
				case 2:
					rankList.CreateItem(rankList_no3, num, list2);
					break;
				}
			}
			else
			{
				list2.Clear();
				list2.Add(item);
				list2.Add(item2);
				list2.Add(item3);
				list2.Add(score);
				rankList.CreateItem(rankList_normal, num + rankList.Count, list2);
			}
			num++;
		}
		if (list.Count == LeaderboardController.instance.getMaxNumInOnePage())
		{
			rankList.CreateItem(moreDownBtn);
			bHasDownBtn = true;
		}
	}

	public void destroyListItems()
	{
		curPage = 0;
		bHasDownBtn = false;
		while (rankList.Count != 0)
		{
			rankList.RemoveItem(rankList.Count - 1, destroy: true);
		}
		SpriteText spriteText = null;
		if ((bool)playerScoreInfo_rank)
		{
			spriteText = (SpriteText)playerScoreInfo_rank.gameObject.GetComponentInChildren(typeof(SpriteText));
			spriteText.Text = string.Empty;
		}
		if ((bool)playerScoreInfo_no)
		{
			spriteText = (SpriteText)playerScoreInfo_no.gameObject.GetComponentInChildren(typeof(SpriteText));
			spriteText.Text = string.Empty;
		}
		if ((bool)playerScoreInfo_name)
		{
			spriteText = (SpriteText)playerScoreInfo_name.gameObject.GetComponentInChildren(typeof(SpriteText));
			spriteText.Text = string.Empty;
		}
		if ((bool)playerScoreInfo_time)
		{
			spriteText = (SpriteText)playerScoreInfo_time.gameObject.GetComponentInChildren(typeof(SpriteText));
			spriteText.Text = string.Empty;
		}
	}

	public void clickMoreDownBtn()
	{
		if (bHasDownBtn && rankList.SelectedItem.Index == rankList.Count - 1)
		{
			curPage++;
			LeaderboardController.instance.GetNewData(string.Empty + curPage);
		}
	}

	private List<string> splitStringWithChar(string txt, char c)
	{
		string[] array = txt.Split(c);
		List<string> list = new List<string>();
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim();
			if (!(text == string.Empty))
			{
				list.Add(text);
			}
		}
		return list;
	}

	public void displayPlayerScoreInfo(string txt)
	{
		if (txt == string.Empty)
		{
			return;
		}
		List<string> list = splitStringWithChar(txt, ' ');
		if (list.Count == 5)
		{
			if ((bool)playerScoreInfo_rank)
			{
				SpriteText spriteText = (SpriteText)playerScoreInfo_rank.gameObject.GetComponentInChildren(typeof(SpriteText));
				spriteText.Text = list[0] + "(" + list[4] + ")";
			}
			if ((bool)playerScoreInfo_no)
			{
				SpriteText spriteText2 = (SpriteText)playerScoreInfo_no.gameObject.GetComponentInChildren(typeof(SpriteText));
				spriteText2.Text = list[1];
			}
			if ((bool)playerScoreInfo_name)
			{
				SpriteText spriteText3 = (SpriteText)playerScoreInfo_name.gameObject.GetComponentInChildren(typeof(SpriteText));
				spriteText3.Text = list[2];
			}
			if ((bool)playerScoreInfo_time)
			{
				SpriteText spriteText4 = (SpriteText)playerScoreInfo_time.gameObject.GetComponentInChildren(typeof(SpriteText));
				spriteText4.Text = list[3];
			}
		}
	}
}
