using System;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
	private float m_LoadingTime;

	private SpriteText m_RandomText;

	private void Start()
	{
		m_LoadingTime = 0f;
		m_RandomText = GameObject.Find("LoadingUI/Camera/Loading/randomtext").GetComponent<SpriteText>();
	}

	private void Update()
	{
		if (HeightMapManager.Instance.Inited && (RaceModeFactory.CurrentRaceMode as RaceModeBase).Inited)
		{
			if (AppMain.Instance.GetCurrentPlatform().Equals("iphone"))
			{
				m_LoadingTime += Time.deltaTime;
				base.gameObject.SetActiveRecursively(state: false);
				m_LoadingTime = 0f;
				UtilBinding.ShowLoadingIndicator(bShow: false);
				m_RandomText.Text = string.Empty;
			}
			else
			{
				base.gameObject.SetActiveRecursively(state: false);
				UtilBinding.ShowLoadingIndicator(bShow: false);
				m_RandomText.Text = string.Empty;
			}
		}
	}

	public void ShowLoadingIndicator()
	{
		System.Random random = new System.Random((int)Time.time);
		int num = random.Next() % 8;
		if (AppMain.Instance.GetCurrentPlatform() == "iphone")
		{
			UtilBinding.ShowLoadingIndicator(bShow: true);
			if (SceneUIManager.Instance.m_CurStrings != null && SceneUIManager.Instance.m_CurStrings.Length > num)
			{
				m_RandomText.Text = SceneUIManager.Instance.m_CurStrings[num];
			}
		}
		else
		{
			UtilBinding.ShowLoadingIndicator(bShow: true);
			if (SceneUIManager.Instance.m_CurStrings != null && SceneUIManager.Instance.m_CurStrings.Length > num)
			{
				m_RandomText.Text = SceneUIManager.Instance.m_CurStrings[num];
			}
		}
	}
}
