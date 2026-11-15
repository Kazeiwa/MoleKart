using System.Collections;
using UnityEngine;

public class PropUI : ISceneUI
{
	private ArrayList m_PropUI;

	private Transform m_PreShowProp;

	private int m_StartIndex;

	private int m_EndIndex;

	private int m_UpIndex;

	private int m_DownIndex;

	private int m_SelectIndex;

	private GameObject m_UpProp;

	private GameObject m_DownProp;

	private Vector3 m_UpScale;

	private Vector3 m_DownScale;

	private Vector3 m_UpPos;

	private Vector3 m_DownPos;

	public float m_ScaleFactor;

	private bool m_StopRoll;

	private float m_RollTime;

	private ePropType m_PickedType;

	private Transform m_Transform;

	private bool m_HideRollUI;

	private Vector3 tempVector3;

	private readonly string[] typesStrings;

	private float m_DelayPlaySoundTime;

	public void InitUI(Transform transform)
	{
	}

	public void ShowProp()
	{
	}

	public void HideProp()
	{
	}

	public void RollPropUI(ePropType type)
	{
	}

	public void ShowProp(ePropType type)
	{
	}

	public void UpdateUI()
	{
	}
}
