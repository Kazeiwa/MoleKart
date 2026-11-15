using System.Collections.Generic;
using RaceTrack;
using UnityEngine;

public class RaceSceneData
{
	private static Dictionary<ulong, PlayerData> _playerDataDictionary = new Dictionary<ulong, PlayerData>();

	private static Dictionary<int, float> tempDic = new Dictionary<int, float>();

	private static Dictionary<int, Car> tempCarDic = new Dictionary<int, Car>();

	private static PlayerData data = new PlayerData();

	private static PlayerData rankData = new PlayerData();

	public static bool GetPlayerData(ulong playerId, ref PlayerData data)
	{
		if (_playerDataDictionary.ContainsKey(playerId))
		{
			data = _playerDataDictionary[playerId];
			return true;
		}
		return false;
	}

	public static bool SetPlayerData(ulong playerId, PlayerData data)
	{
		bool flag = false;
		if (_playerDataDictionary.ContainsKey(playerId))
		{
			_playerDataDictionary[playerId] = data;
			return true;
		}
		_playerDataDictionary.Add(playerId, data);
		return true;
	}

	public static void RemovePlayerData(ulong playerId)
	{
		if (_playerDataDictionary.ContainsKey(playerId))
		{
			_playerDataDictionary.Remove(playerId);
		}
	}

	public static void UpdateRaceData(IRaceMode raceMode)
	{
		if (raceMode == null)
		{
			return;
		}
		tempCarDic.Clear();
		tempDic.Clear();
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (!(null != carByIndex) || !GetPlayerData((ulong)carByIndex.ID, ref data) || data.IsFinish)
			{
				continue;
			}
			data.SpendTime += Time.deltaTime;
			Vector2 pos = carByIndex.Pos;
			int lapCount = data.CurLaps;
			RaceWorld.Instance.RacingLine.CrossOverEdge.UpdateLapCountAndAreaIndex(pos, ref lapCount, ref data.areaIndex);
			data.CurLaps = ((lapCount > 0) ? lapCount : 0);
			data.ShowLaps = ((data.ShowLaps <= data.CurLaps) ? data.CurLaps : data.ShowLaps);
			data.ShowLaps = ((data.ShowLaps > raceMode.TotalLap()) ? raceMode.TotalLap() : data.ShowLaps);
			if (data.CurLaps > raceMode.TotalLap())
			{
				data.IsFinish = true;
				data.NormalFinish = true;
				SetPlayerData((ulong)carByIndex.ID, data);
				if (carByIndex.isHero)
				{
					raceMode.RaceOver(over: true);
				}
				continue;
			}
			float distanceFromBeginning = RaceWorld.Instance.RacingLine.GetDistanceFromBeginning(pos, data.CurLaps, data.areaIndex, out data.distanceInLapFromGoal);
			data.distanceFromBeginning = distanceFromBeginning;
			data.m_Distance = distanceFromBeginning;
			tempDic.Add(carByIndex.ID, distanceFromBeginning);
			if (!tempCarDic.ContainsKey(data.Ranking))
			{
				tempCarDic.Add(data.Ranking, carByIndex);
			}
			SetPlayerData((ulong)carByIndex.ID, data);
		}
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			Car carByIndex2 = TSingleton<CarManager>.Instance.GetCarByIndex(j);
			if (!(null != carByIndex2))
			{
				continue;
			}
			GetPlayerData((ulong)carByIndex2.ID, ref rankData);
			int ranking = rankData.Ranking;
			rankData.Ranking = TSingleton<CarManager>.Instance.GetCarCount();
			for (int k = 0; k < TSingleton<CarManager>.Instance.GetCarCount(); k++)
			{
				Car carByIndex3 = TSingleton<CarManager>.Instance.GetCarByIndex(k);
				if (carByIndex3.ID != carByIndex2.ID && null != carByIndex3)
				{
					GetPlayerData((ulong)carByIndex3.ID, ref data);
					if (rankData.IsFinish && !data.IsFinish)
					{
						rankData.Ranking--;
					}
					else if (rankData.IsFinish && data.IsFinish && rankData.SpendTime < data.SpendTime)
					{
						rankData.Ranking--;
					}
					else if (rankData.IsFinish && data.IsFinish && rankData.SpendTime == data.SpendTime && j < k)
					{
						rankData.Ranking--;
					}
					else if (!data.IsFinish && rankData.CurLaps > data.CurLaps)
					{
						rankData.Ranking--;
					}
					else if (!data.IsFinish && rankData.CurLaps == data.CurLaps && tempDic[carByIndex2.ID] > tempDic[carByIndex3.ID])
					{
						rankData.Ranking--;
					}
				}
			}
			SetPlayerData((ulong)carByIndex2.ID, rankData);
			if (ranking <= rankData.Ranking || !tempCarDic.ContainsKey(rankData.Ranking))
			{
				continue;
			}
			Car car = tempCarDic[rankData.Ranking];
			if (null != car)
			{
				Vector3 lhs = car.transform.position - carByIndex2.transform.position;
				Vector3 right = carByIndex2.transform.right;
				lhs.y = (right.y = 0f);
				if (Vector3.Dot(lhs, right) > 0f)
				{
					carByIndex2.PlayAni("beyr");
				}
				else
				{
					carByIndex2.PlayAni("beyl");
				}
			}
		}
	}

	public static int Rank(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.Ranking;
		}
		return -1;
	}

	public static float SpendTime(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.SpendTime;
		}
		return 0f;
	}

	public static bool IsFinish(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.NormalFinish;
		}
		return false;
	}

	public static float CarDisance(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.m_Distance;
		}
		return 0f;
	}

	public static int CarCurrentLap(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.CurLaps;
		}
		return 0;
	}

	public static bool CarIsFinish(int carID)
	{
		PlayerData playerData = new PlayerData();
		if (GetPlayerData((ulong)carID, ref playerData))
		{
			return playerData.IsFinish;
		}
		return false;
	}
}
