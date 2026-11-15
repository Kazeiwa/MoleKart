using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Change Panel Button")]
public class UIBtnChangePanel : UIButton
{
	public enum ChangeType
	{
		BringIn,
		BringInForward,
		BringInBack,
		Dismiss,
		DismissCurrent,
		Toggle,
		Forward,
		Back,
		BringInImmediate,
		DismissImmediate
	}

	public UIPanelManager panelManager;

	public ChangeType changeType;

	public string panel;

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public new static UIBtnChangePanel Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIBtnChangePanel Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
