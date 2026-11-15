using System.Collections.Generic;
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

	public Dictionary<int, Car> myCars;

	protected static Dictionary<CarModeType, GameObject> m_CarPrefabs;

	private List<Car> m_NpcCars;

	private Car m_HeroCar;

	public int Count
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Dictionary<int, Car>.ValueCollection Cars
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Car LocalCar
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool Init()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UnInit()
	{
	}

	public GameObject CreateCar(int id, CarLogicType eRoleType, string name, Vector3 pos, Quaternion rotate, CarModeType iType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DestroyCar(GameObject car)
	{
	}

	public void DestroyCar()
	{
	}

	public Car GetCar(int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Car GetHeroCar()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Car GetCarByIndex(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveCar(int id)
	{
	}

	public int GetCarCount()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Dictionary<int, Car> GetCarsDictionary()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Update()
	{
	}

	public void FixedUpdate()
	{
	}

	public GameObject FindNearestCar(GameObject ownerCar, GameObject obj, float radius)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Car FindFirstCar()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public VehicleData GetVehicleData(CarModeType iType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
