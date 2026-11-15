using UnityEngine;

public class ControlPanelUI : ISceneUI
{
	private bool canShowSlider;

	private bool canShowDrift;

	private bool canShowControlBtn;

	private GameObject slider;

	private GameObject background;

	private GameObject handbreak;

	private GameObject drift;

	private GameObject leftBtn;

	private GameObject rightBtn;

	public bool CanShowSlider
	{
		set
		{
		}
	}

	public bool CanShowDrift
	{
		set
		{
		}
	}

	public bool CanShowControlBtn
	{
		set
		{
		}
	}

	public void InitUI(Transform transform)
	{
	}

	private void ShowDrift()
	{
	}

	public void ShowSlider(bool show)
	{
	}

	public void UpdateControlBtns()
	{
	}

	public void UpdateUI()
	{
	}
}
