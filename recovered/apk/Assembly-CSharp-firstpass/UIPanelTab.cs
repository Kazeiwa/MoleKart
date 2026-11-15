using UnityEngine;

[AddComponentMenu("EZ GUI/Controls/Panel Tab")]
public class UIPanelTab : UIRadioBtn
{
	public bool toggle;

	public UIPanelManager panelManager;

	public UIPanelBase panel;

	public bool panelShowingAtStart = true;

	protected bool panelIsShowing = true;

	public override void Start()
	{
		if (!m_started)
		{
			base.Start();
			panelIsShowing = panelShowingAtStart;
			if (panelManager == null && panel != null && panel.Container != null)
			{
				panelManager = (UIPanelManager)panel.Container;
			}
			Value = panelIsShowing;
			if (managed && m_hidden)
			{
				Hide(tf: true);
			}
		}
	}

	public override void OnInput(ref POINTER_INFO ptr)
	{
		if (deleted)
		{
			return;
		}
		base.OnInput(ref ptr);
		if (!m_controlIsEnabled || IsHidden() || panel == null || ptr.evt != whenToInvoke)
		{
			return;
		}
		if (toggle)
		{
			if (panelManager != null)
			{
				if (panelManager.CurrentPanel == panel)
				{
					panelManager.Dismiss(UIPanelManager.MENU_DIRECTION.Forwards);
					panelIsShowing = false;
				}
				else
				{
					panelManager.BringIn(panel);
					panelIsShowing = true;
				}
			}
			else
			{
				if (panelIsShowing)
				{
					panel.StartTransition(UIPanelManager.SHOW_MODE.DismissForward);
				}
				else
				{
					panel.StartTransition(UIPanelManager.SHOW_MODE.BringInForward);
				}
				panelIsShowing = !panelIsShowing;
			}
			Value = panelIsShowing;
		}
		else if (panelManager != null)
		{
			panelManager.BringIn(panel, UIPanelManager.MENU_DIRECTION.Forwards);
		}
		else
		{
			panel.StartTransition(UIPanelManager.SHOW_MODE.BringInForward);
		}
	}

	public override void Copy(SpriteRoot s)
	{
		Copy(s, ControlCopyFlags.All);
	}

	public override void Copy(SpriteRoot s, ControlCopyFlags flags)
	{
		base.Copy(s, flags);
		if (s is UIPanelTab)
		{
			UIPanelTab uIPanelTab = (UIPanelTab)s;
			if ((flags & ControlCopyFlags.Settings) == ControlCopyFlags.Settings)
			{
				toggle = uIPanelTab.toggle;
				panelManager = uIPanelTab.panelManager;
				panel = uIPanelTab.panel;
				panelShowingAtStart = uIPanelTab.panelShowingAtStart;
			}
			if ((flags & ControlCopyFlags.State) == ControlCopyFlags.State)
			{
				panelIsShowing = uIPanelTab.panelIsShowing;
			}
		}
	}

	public new static UIPanelTab Create(string name, Vector3 pos)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		return (UIPanelTab)gameObject.AddComponent(typeof(UIPanelTab));
	}

	public new static UIPanelTab Create(string name, Vector3 pos, Quaternion rotation)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = pos;
		gameObject.transform.rotation = rotation;
		return (UIPanelTab)gameObject.AddComponent(typeof(UIPanelTab));
	}
}
