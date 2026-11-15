using System.Collections.Generic;
using UnityEngine;

public class CarManagerSelected : TSingleton<CarManagerSelected>
{
	protected Object m_CarModel;

	private Dictionary<int, GameObject> mySelectedCars = new Dictionary<int, GameObject>();

	public GameObject CreateCar(int id, string name, Vector3 pos, Quaternion rotate, Vector3 scale, CarModeType iType)
	{
		if (!CarConfigData.m_CarMap.ContainsKey((int)iType))
		{
			return null;
		}
		string high_mode = CarConfigData.m_CarMap[(int)iType].high_mode;
		m_CarModel = Resources.Load(high_mode);
		if (null == m_CarModel)
		{
			return null;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(m_CarModel);
		if (null != gameObject)
		{
			if (id != 2002)
			{
				gameObject.transform.localPosition = pos;
				gameObject.transform.localRotation = rotate;
				gameObject.transform.localScale = scale;
			}
			gameObject.name = name;
		}
		return gameObject;
	}

	public GameObject GetCarAndCreateifNotExist(int id, string name, Vector3 pos, Quaternion rotate, Vector3 scale, CarModeType iType)
	{
		if (!CarConfigData.m_CarMap.ContainsKey((int)iType))
		{
			return null;
		}
		if (mySelectedCars.ContainsKey((int)iType))
		{
			return mySelectedCars[(int)iType];
		}
		string high_mode = CarConfigData.m_CarMap[(int)iType].high_mode;
		m_CarModel = Resources.Load(high_mode);
		if (null == m_CarModel)
		{
			return null;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(m_CarModel);
		if (null != gameObject)
		{
			if (id != 2002)
			{
				gameObject.transform.localPosition = pos;
				gameObject.transform.localRotation = rotate;
				gameObject.transform.localScale = scale;
			}
			gameObject.name = name;
		}
		mySelectedCars[(int)iType] = gameObject;
		return gameObject;
	}

	public void ClearCachedSelectedCar()
	{
		foreach (GameObject value in mySelectedCars.Values)
		{
			Object.DestroyImmediate(value, allowDestroyingAssets: true);
		}
		mySelectedCars.Clear();
		m_CarModel = null;
		Resources.UnloadUnusedAssets();
	}
}
