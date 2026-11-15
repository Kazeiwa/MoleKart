using System.Collections.Generic;
using UnityEngine;

public class SmallMapUI : ISceneUI
{
	private Dictionary<int, GameObject> m_PlayerMap;

	private Dictionary<int, GameObject> m_NpcMap;

	private List<GameObject> m_NpcList;

	private Dictionary<int, Texture> m_NpcMapTex;

	private GameObject m_Direction;

	private bool m_HideSmallMap;

	private Transform m_Map;

	private float m_LastRotateAngle;

	private Transform m_Transform;

	private float m_OffsetX;

	private float m_OffsetZ;

	public void InitUI(Transform transform)
	{
	}

	public void ShowSmallMap()
	{
	}

	public void HideSmallMap()
	{
	}

	public void RotateCar(Car car)
	{
	}

	public void UpdateUI()
	{
	}
}
