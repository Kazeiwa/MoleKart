using UnityEngine;

public class LoadingUI : ISceneUI
{
	private Transform m_Transform;

	public void InitUI(Transform transform)
	{
		if (!(null == transform))
		{
			m_Transform = transform;
		}
	}

	public void UpdateUI()
	{
	}

	public void DisableLoadingUI()
	{
		GameObject gameObject = m_Transform.FindChild("Loading").gameObject;
		if (null != gameObject)
		{
			gameObject.SetActiveRecursively(state: false);
		}
	}
}
