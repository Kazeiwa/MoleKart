using UnityEngine;

public class TurorialUI : ISceneUI
{
	public enum TurorialType
	{
		TT_PROP,
		TT_COUNT
	}

	private Transform transformRoot;

	private GameObject tutorial;

	public void InitUI(Transform root)
	{
	}

	public void CacheResourceToMemory(TurorialType eType)
	{
	}

	public void ShowPropUsageTutorial(bool show)
	{
	}

	public void DestroyPropUsageTutorial()
	{
	}

	public void UpdateUI()
	{
	}
}
