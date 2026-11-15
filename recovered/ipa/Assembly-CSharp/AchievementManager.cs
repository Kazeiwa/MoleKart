using System;
using System.Collections.Generic;
using Achievement;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
	private const string configPath = "Config/xmlConfig/ArchievementConfig.xml";

	private string userAchievementPath;

	private static AchievementManager instance;

	private Dictionary<ACHIEVEMENT, bool> achievementsDict;

	private Dictionary<ACHIEVEMENT, bool> showAchievement;

	private int masterGolds;

	private int anyGolds;

	private DateTime totalTime;

	private DateTime beginTime;

	private int onlinePlayed;

	private int version;

	public static AchievementManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private void Awake()
	{
	}

	private void Init()
	{
	}

	public void AddPlayOnlineGame()
	{
	}

	public void AddMasterGold()
	{
	}

	public void AddAnyGold()
	{
	}

	public void LoadAchievement()
	{
	}

	public void SaveAchievement()
	{
	}

	public bool CheckAchievement(ACHIEVEMENT achievement)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CreateUserFile()
	{
	}

	public void ShowCommentAlert()
	{
	}

	private void CheckAllAchievements()
	{
	}

	private bool CheckComment()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void NeverShowComment()
	{
	}

	private void ClearCommentData()
	{
	}

	private void ShowCommentLater()
	{
	}
}
