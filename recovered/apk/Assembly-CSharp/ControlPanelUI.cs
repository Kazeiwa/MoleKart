using UnityEngine;

public class ControlPanelUI : ISceneUI
{
	private bool canShowSlider = true;

	private bool canShowDrift = true;

	private GameObject slider;

	private GameObject background;

	private GameObject handbreak;

	private GameObject drift;

	public bool CanShowSlider
	{
		set
		{
			canShowSlider = value;
		}
	}

	public bool CanShowDrift
	{
		set
		{
			canShowDrift = value;
		}
	}

	public void InitUI(Transform transform)
	{
		handbreak = transform.FindChild("ControlPanel/handbreak").gameObject;
		background = transform.FindChild("ControlPanel/background").gameObject;
		slider = transform.FindChild("ControlPanel/background/slider").gameObject;
		drift = transform.FindChild("ControlPanel/drift").gameObject;
		Rigidbody rigidbody = background.AddComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
		rigidbody = slider.AddComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
		Debug.Log("ControlPanelUI:init break control rectangle");
		float num = (float)Screen.width / 1024f * 90f;
		EZScreenPlacement component = handbreak.GetComponent<EZScreenPlacement>();
		Rect rect = new Rect((1f + component.screenPos.x) * (float)Screen.width - num / 2f, (1f - component.screenPos.y) * (float)Screen.height - num / 2f, num, num);
		Debug.Log("Handbreak Retangle = " + rect);
		num = (float)Screen.width / 1024f * 95f;
		EZScreenPlacement component2 = drift.GetComponent<EZScreenPlacement>();
		Rect rect2 = new Rect((1f + component2.screenPos.x) * (float)Screen.width - num / 2f, (1f - component2.screenPos.y) * (float)Screen.height - num / 2f, num, num);
		Debug.Log("Shift Rectangle = " + rect2);
		DriveManager.Instance.InitBreakRectangle(rect, rect2);
		InputManager.Instance.InitBreakRectangle(rect, rect2);
	}

	private void ShowDrift()
	{
		CONTROL_TYPE cONTROL_TYPE = InputManager.Instance.LoadControlType();
		if (cONTROL_TYPE == CONTROL_TYPE.CONTROLTYPE_BABY || cONTROL_TYPE == CONTROL_TYPE.CONTROLTYPE_DAXIA)
		{
			drift.active = false;
		}
		else
		{
			drift.active = true && canShowDrift;
		}
	}

	public void ShowSlider(bool show)
	{
		bool flag = show;
		handbreak.active = show;
		if (!canShowSlider)
		{
			flag = false;
		}
		ShowDrift();
		background.active = flag;
		slider.active = flag;
		if (flag)
		{
			Vector3 localPosition = new Vector3(0f, 0f, -10f);
			localPosition.x += InputManager.Instance.CurrentSliding * 60f;
			slider.transform.localPosition = localPosition;
		}
	}

	public void UpdateUI()
	{
	}
}
