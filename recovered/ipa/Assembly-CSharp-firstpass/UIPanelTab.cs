using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Panel Tab")]
public class UIPanelTab : UIRadioBtn
{
	public bool toggle;

	public UIPanelManager panelManager;

	public UIPanelBase panel;

	public bool panelShowingAtStart;

	protected bool panelIsShowing;

	public override void Start()
	{
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public new static UIPanelTab Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIPanelTab Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
