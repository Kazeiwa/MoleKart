using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMapUI : ISceneUI
{
	private Dictionary<int, GameObject> m_PlayerMap = new Dictionary<int, GameObject>();

	private Dictionary<int, GameObject> m_NpcMap = new Dictionary<int, GameObject>();

	private ArrayList m_NpcList = new ArrayList();

	private Dictionary<int, Texture> m_PlayerMapTex = new Dictionary<int, Texture>();

	private Dictionary<int, Texture> m_NpcMapTex = new Dictionary<int, Texture>();

	private GameObject m_Direction;

	private bool m_HideSmallMap;

	private Transform m_Map;

	private float m_LastRotateAngle;

	private Transform m_Transform;

	public void InitUI(Transform transform)
	{
		Debug.Log("InitUI" + transform.name);
		if (null == transform)
		{
			return;
		}
		m_Transform = transform;
		GameObject gameObject = transform.FindChild("SmallMap/Logos/Npc_Bear").gameObject;
		m_NpcMapTex.Add(3101, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Momo").gameObject;
		m_NpcMapTex.Add(3001, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Rk").gameObject;
		m_NpcMapTex.Add(3002, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Mmt").gameObject;
		m_NpcMapTex.Add(3201, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Molele").gameObject;
		m_NpcMapTex.Add(3003, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Kula").gameObject;
		m_NpcMapTex.Add(3004, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_Puti").gameObject;
		m_NpcMapTex.Add(3005, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Npc_yali").gameObject;
		m_NpcMapTex.Add(3006, gameObject.GetComponent<MeshRenderer>().material.GetTexture("_MainTex"));
		m_NpcList.Add(gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Bear").gameObject;
		m_PlayerMap.Add(3101, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Momo").gameObject;
		m_PlayerMap.Add(3001, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Rk").gameObject;
		m_PlayerMap.Add(3002, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Mmt").gameObject;
		m_PlayerMap.Add(3201, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Molele").gameObject;
		m_PlayerMap.Add(3003, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_Kula").gameObject;
		m_PlayerMap.Add(3004, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_puti").gameObject;
		m_PlayerMap.Add(3005, gameObject);
		gameObject = transform.FindChild("SmallMap/Logos/Player_yali").gameObject;
		m_PlayerMap.Add(3006, gameObject);
		m_Direction = transform.FindChild("SmallMap/Logos/Direction").gameObject;
		m_Direction.active = true;
		for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (null != carByIndex)
			{
				int roleID = carByIndex.GetRole(0).RoleID;
				GameObject gameObject2 = (GameObject)m_NpcList[i];
				if (null != gameObject2)
				{
					gameObject2.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_NpcMapTex[roleID]);
				}
				m_NpcMap.Add(carByIndex.ID, (GameObject)m_NpcList[i]);
			}
		}
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			Car carByIndex2 = TSingleton<CarManager>.Instance.GetCarByIndex(j);
			if (null != carByIndex2)
			{
				int roleID2 = carByIndex2.GetRole(0).RoleID;
				if (carByIndex2.isHero && m_PlayerMap.ContainsKey(roleID2))
				{
					m_PlayerMap[roleID2].active = true;
				}
				else if (m_NpcMap.ContainsKey(carByIndex2.ID))
				{
					m_NpcMap[carByIndex2.ID].active = true;
				}
			}
		}
		m_Map = transform.FindChild("SmallMap/Maps/" + SceneManager.Instance.CurGameSceneId);
		if (null != m_Map)
		{
			m_Map.gameObject.active = true;
		}
		for (int k = 1; k <= 9; k++)
		{
			if (1100 + k != SceneManager.Instance.CurGameSceneId)
			{
				Transform transform2 = transform.FindChild("SmallMap/Maps/" + (1100 + k));
				if (null != transform2)
				{
					Object.Destroy(transform2.gameObject);
				}
			}
		}
		ShowSmallMap();
	}

	public void ShowSmallMap()
	{
		if (m_HideSmallMap)
		{
			if (null != m_Direction)
			{
				m_Direction.active = true;
			}
			if (null != m_Map)
			{
				m_Map.gameObject.active = true;
			}
			for (int i = 0; i < TSingleton<CarManager>.Instance.GetCarCount(); i++)
			{
				Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
				if (null != carByIndex)
				{
					int roleID = carByIndex.GetRole(0).RoleID;
					if (carByIndex.isHero && m_PlayerMap.ContainsKey(roleID))
					{
						Quaternion identity = Quaternion.identity;
						identity.eulerAngles = new Vector3(0f, 180f, 0f);
						m_PlayerMap[roleID].transform.rotation = identity;
						m_PlayerMap[roleID].active = true;
					}
					else if (m_NpcMap.ContainsKey(carByIndex.ID))
					{
						Quaternion identity2 = Quaternion.identity;
						identity2.eulerAngles = new Vector3(0f, 180f, 0f);
						m_NpcMap[carByIndex.ID].transform.rotation = identity2;
						m_NpcMap[carByIndex.ID].active = true;
					}
				}
			}
			m_HideSmallMap = false;
		}
		for (int j = 0; j < TSingleton<CarManager>.Instance.GetCarCount(); j++)
		{
			Car carByIndex2 = TSingleton<CarManager>.Instance.GetCarByIndex(j);
			if (null == carByIndex2)
			{
				continue;
			}
			int roleID2 = carByIndex2.GetRole(0).RoleID;
			if (!m_PlayerMap.ContainsKey(roleID2) || !m_NpcMap.ContainsKey(carByIndex2.ID))
			{
				continue;
			}
			GameObject gameObject = null;
			gameObject = ((!carByIndex2.isHero) ? m_NpcMap[carByIndex2.ID] : m_PlayerMap[roleID2]);
			if (!(null == gameObject))
			{
				float x = (0f - carByIndex2.transform.position.x) * m_Map.localScale.z;
				float y = (0f - carByIndex2.transform.position.z) * m_Map.localScale.z;
				gameObject.transform.localPosition = new Vector3(x, y, gameObject.transform.localPosition.z);
				if (carByIndex2.isHero)
				{
					m_Direction.transform.localPosition = gameObject.transform.localPosition + new Vector3(0f, 0f, 5f);
					float num = Mathf.Atan2(carByIndex2.transform.forward.z, carByIndex2.transform.forward.x);
					m_Direction.transform.RotateAround(Vector3.forward, num - m_LastRotateAngle);
					m_LastRotateAngle = num;
				}
			}
		}
	}

	public void HideSmallMap()
	{
		if (!(null == m_Transform))
		{
			GameObject gameObject = m_Transform.FindChild("SmallMap").gameObject;
			if (null != gameObject)
			{
				gameObject.SetActiveRecursively(state: false);
				m_HideSmallMap = true;
			}
		}
	}

	private bool Rotate(Car car)
	{
		if (null == car)
		{
			return false;
		}
		if ((car.CarState & 0x10) != 0 || (car.CarState & 0x200) != 0 || (car.CarState & 8) != 0 || (car.CarState & 0x1000) != 0 || (car.CarState & 0x2000) != 0 || (car.CarState & 8) != 0)
		{
			return true;
		}
		return false;
	}

	public void RotateCar(Car car)
	{
		if (null == car)
		{
			return;
		}
		int roleID = car.GetRole(0).RoleID;
		if (m_PlayerMap.ContainsKey(roleID) && m_NpcMap.ContainsKey(car.ID))
		{
			GameObject gameObject = null;
			gameObject = ((!car.isHero) ? m_NpcMap[car.ID] : m_PlayerMap[roleID]);
			if (!(null == gameObject) && null != gameObject.animation && !gameObject.animation.isPlaying)
			{
				gameObject.animation.Play();
			}
		}
	}

	public void UpdateUI()
	{
	}
}
