using System.Collections.Generic;
using UnityEngine;

public class CarManagerSelected : TSingleton<CarManagerSelected>
{
	protected Object m_CarModel;

	private Dictionary<int, GameObject> mySelectedCars;

	public GameObject CreateCar(int id, string name, Vector3 pos, Quaternion rotate, Vector3 scale, CarModeType iType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public GameObject GetCarAndCreateifNotExist(int id, string name, Vector3 pos, Quaternion rotate, Vector3 scale, CarModeType iType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ClearCachedSelectedCar()
	{
	}
}
