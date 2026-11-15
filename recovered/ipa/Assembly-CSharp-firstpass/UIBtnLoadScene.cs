using System.Collections;
using System.Diagnostics;
using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Load Scene Button")]
public class UIBtnLoadScene : UIButton
{
	public string scene;

	public UIPanelBase loadingPanel;

	public void LoadSceneDelegate(UIPanelBase panel, EZTransition trans)
	{
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
	}

	protected void DoLoadScene()
	{
	}

	[DebuggerHidden]
	protected IEnumerator LoadScene()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void Copy(SpriteRoot s)
	{
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
	}

	public new static UIBtnLoadScene Create(string name, Vector3 pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public new static UIBtnLoadScene Create(string name, Vector3 pos, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
