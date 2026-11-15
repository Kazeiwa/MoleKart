using System.Collections.Generic;
using GameNetwork;
using Misc;
using UnityEngine;

public class CarManager : TSingleton<CarManager>
{
	public enum EDummy_Index
	{
		MOLE_DUMMY,
		SW_DUMMY,
		LOOKAT_DUMMY,
		BRAND_DUMMY
	}

	public Dictionary<int, Car> myCars = new Dictionary<int, Car>();

	protected static Dictionary<CarModeType, GameObject> m_CarPrefabs = new Dictionary<CarModeType, GameObject>();

	private List<Car> m_NpcCars = new List<Car>();

	private Car m_HeroCar;

	public int Count => myCars.Count;

	public Dictionary<int, Car>.ValueCollection Cars => myCars.Values;

	public Car LocalCar => m_HeroCar;

	public bool Init()
	{
		Debug.Log("CarManager:Init");
		foreach (CarInf value in CarConfigData.m_CarMap.Values)
		{
			GameObject gameObject = Resources.Load(value.low_mode) as GameObject;
			if (null != gameObject && !m_CarPrefabs.ContainsKey((CarModeType)value.ID))
			{
				m_CarPrefabs.Add((CarModeType)value.ID, gameObject);
			}
		}
		return true;
	}

	public void UnInit()
	{
		Debug.Log("CarManager:UnInit()");
		m_CarPrefabs.Clear();
		Resources.UnloadUnusedAssets();
	}

	public GameObject CreateCar(int id, CarLogicType eRoleType, string name, Vector3 pos, Quaternion rotate, CarModeType iType)
	{
		GameObject gameObject = ((!m_CarPrefabs.ContainsKey(iType)) ? null : m_CarPrefabs[iType]);
		if (null == gameObject && CarConfigData.m_CarMap.ContainsKey((int)iType))
		{
			string low_mode = CarConfigData.m_CarMap[(int)iType].low_mode;
			gameObject = Resources.Load(low_mode) as GameObject;
			if (null == gameObject)
			{
				return null;
			}
			m_CarPrefabs.Add(iType, gameObject);
		}
		if (myCars.ContainsKey(id))
		{
			Debug.Log("CreateCar" + id + "Already exist!");
			return null;
		}
		if (null == gameObject)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (null != gameObject2)
		{
			gameObject2.transform.localPosition = pos;
			gameObject2.transform.localRotation = rotate;
			gameObject2.name = name;
			if (null == gameObject2.GetComponent<Rigidbody>())
			{
				gameObject2.AddComponent<Rigidbody>();
			}
			Car car = null;
			switch (eRoleType)
			{
			case CarLogicType.RT_HERO:
				car = (m_HeroCar = gameObject2.AddComponent<HeroCar>());
				myCars.Add(id, car);
				break;
			case CarLogicType.RT_NPC:
				car = gameObject2.AddComponent<NPCCar>();
				myCars.Add(id, car);
				m_NpcCars.Add(car);
				break;
			case CarLogicType.RT_PLAYER:
				car = gameObject2.AddComponent<PlayerCar>();
				myCars.Add(id, car);
				break;
			case CarLogicType.RT_PHYSPLAYER:
				car = gameObject2.AddComponent<PhysxPlayerCar>();
				myCars.Add(id, car);
				break;
			case CarLogicType.RT_HOST_PHYSPLAYER:
				car = gameObject2.AddComponent<HostPhysxPlayerCar>();
				myCars.Add(id, car);
				break;
			}
			if (null != car)
			{
				car.m_eCarModeType = iType;
			}
		}
		return gameObject2;
	}

	public void DestroyCar(GameObject car)
	{
		if ((bool)car)
		{
			Car component = car.GetComponent<Car>();
			if ((bool)component && myCars.ContainsKey(component.ID))
			{
				component.Uninit();
				m_NpcCars.Remove(component);
				myCars.Remove(component.ID);
			}
			Object.DestroyImmediate(car);
		}
	}

	public void DestroyCar()
	{
		foreach (KeyValuePair<int, Car> myCar in myCars)
		{
			GameObject gameObject = myCar.Value.gameObject;
			Object.DestroyImmediate(gameObject);
		}
		myCars.Clear();
		m_NpcCars.Clear();
	}

	public Car GetCar(int id)
	{
		if (myCars.ContainsKey(id))
		{
			return myCars[id];
		}
		return null;
	}

	public Car GetHeroCar()
	{
		return m_HeroCar;
	}

	public Car GetCarByIndex(int idx)
	{
		if (idx >= 0 && idx < GetCarCount())
		{
			Dictionary<int, Car>.Enumerator enumerator = myCars.GetEnumerator();
			enumerator.MoveNext();
			while (idx > 0)
			{
				enumerator.MoveNext();
				idx--;
			}
			return enumerator.Current.Value;
		}
		return null;
	}

	public void RemoveCar(int id)
	{
		if (myCars.ContainsKey(id))
		{
			m_NpcCars.Remove(myCars[id]);
			myCars.Remove(id);
		}
	}

	public int GetCarCount()
	{
		return (myCars != null) ? Count : 0;
	}

	public Dictionary<int, Car> GetCarsDictionary()
	{
		return myCars;
	}

	public void Update()
	{
		if (NetworkManager.Instance.IsStandAlone)
		{
			FunMaker.Instance.UpdateState();
		}
		foreach (Car value in myCars.Values)
		{
			if (!value.isHero)
			{
				value.UpdateCar();
				value.UpdateAISteering();
			}
			else
			{
				value.UpdateCar();
			}
		}
	}

	public void FixedUpdate()
	{
		foreach (Car value in myCars.Values)
		{
			value.UpdatePhysics();
		}
	}

	public GameObject FindNearestCar(GameObject ownerCar, GameObject obj, float radius)
	{
		GameObject result = null;
		float num = radius;
		foreach (Car value in myCars.Values)
		{
			GameObject gameObject = value.gameObject;
			if (null != gameObject && gameObject != ownerCar && !value.CarCanNotByHit)
			{
				Vector3 forward = obj.transform.forward;
				Vector3 rhs = gameObject.transform.position - obj.transform.position;
				if (Vector3.Dot(forward, rhs) > 0f && rhs.magnitude < num)
				{
					result = gameObject;
					num = rhs.magnitude;
				}
			}
		}
		return result;
	}

	public Car FindFirstCar()
	{
		foreach (Car value in myCars.Values)
		{
			PlayerData data = new PlayerData();
			RaceSceneData.GetPlayerData((ulong)value.ID, ref data);
			if (data.Ranking == 1)
			{
				return value;
			}
		}
		return null;
	}

	public VehicleData GetVehicleData(CarModeType iType)
	{
		if (CarConfigData.m_CarMap.ContainsKey((int)iType))
		{
			return CarConfigData.m_CarMap[(int)iType].kPhysData;
		}
		return null;
	}
}
