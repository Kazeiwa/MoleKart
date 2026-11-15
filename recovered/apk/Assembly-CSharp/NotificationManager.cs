using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	public enum MESSAGE_TYPE
	{
		MESSAGE_TYPE_UPDATE = 0,
		MESSAGE_TYPE_FREE = 2,
		MESSAGE_TYPE_Discount = 3
	}

	private const int SPECIAL_TRACKS_NUMBER = 4;

	private const int SPECIAL_CARS_NUMBER = 4;

	private Dictionary<int, MESSAGE_TYPE> trackSpecialDic = new Dictionary<int, MESSAGE_TYPE>();

	private Dictionary<CarModeType, MESSAGE_TYPE> characterSpecialDic = new Dictionary<CarModeType, MESSAGE_TYPE>();

	private static NotificationManager instance;

	public Dictionary<int, MESSAGE_TYPE> TrackSpecialDic => trackSpecialDic;

	public Dictionary<CarModeType, MESSAGE_TYPE> CharacterSpecialDic => characterSpecialDic;

	public static NotificationManager Instance => instance;

	private void Awake()
	{
		instance = GetComponent<NotificationManager>();
		Init();
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	private void Init()
	{
	}

	private void SetUUID(string uuid)
	{
		Debug.Log("UUID from ios:" + uuid);
		SystemConfig.Instance.UUID = uuid;
		Debug.Log("UUID from ios after:" + SystemConfig.Instance.UUID);
	}

	public void SetCanShowNotification(bool canShow)
	{
	}

	private void ReceiveCharacterId(string msg)
	{
		Debug.Log("ReceiveCharacterId" + msg);
		string[] array = msg.Split("|".ToCharArray(), 2);
		if (characterSpecialDic.ContainsKey((CarModeType)Convert.ToInt32(array[0])))
		{
			characterSpecialDic.Remove((CarModeType)Convert.ToInt32(array[0]));
		}
		characterSpecialDic.Add((CarModeType)Convert.ToInt32(array[0]), (MESSAGE_TYPE)Convert.ToInt32(array[1]));
	}

	private void ReceiveTrackMessage(string msg)
	{
		Debug.Log("ReceiveTrackMessage:" + msg);
		string[] array = msg.Split("|".ToCharArray(), 2);
		if (trackSpecialDic.ContainsKey(Convert.ToInt32(array[0])))
		{
			trackSpecialDic.Remove(Convert.ToInt32(array[0]));
		}
		trackSpecialDic.Add(Convert.ToInt32(array[0]), (MESSAGE_TYPE)Convert.ToInt32(array[1]));
	}
}
