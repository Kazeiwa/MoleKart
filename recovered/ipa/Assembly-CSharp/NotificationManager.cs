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

	private Dictionary<int, MESSAGE_TYPE> trackSpecialDic;

	private Dictionary<CarModeType, MESSAGE_TYPE> characterSpecialDic;

	private static NotificationManager instance;

	public Dictionary<int, MESSAGE_TYPE> TrackSpecialDic
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Dictionary<CarModeType, MESSAGE_TYPE> CharacterSpecialDic
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static NotificationManager Instance
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

	private void SetUUID(string uuid)
	{
	}

	public void SetCanShowNotification(bool canShow)
	{
	}

	private void ReceiveCharacterId(string msg)
	{
	}

	private void ReceiveTrackMessage(string msg)
	{
	}
}
