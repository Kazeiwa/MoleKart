using System.Collections;
using UnityEngine;

public class CullingGroup_Manual : MonoBehaviour
{
	public string cullingGroupMasterName = string.Empty;

	private bool isVisible = true;

	private Renderer[] myRenderers;

	private void Start()
	{
		SetupVars();
	}

	public void SetupVars()
	{
		Component[] componentsInChildren = base.transform.GetComponentsInChildren<Renderer>();
		if (myRenderers == null || componentsInChildren.Length != myRenderers.Length)
		{
			myRenderers = new Renderer[componentsInChildren.Length];
			int num = 0;
			Component[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Renderer renderer = (Renderer)array[i];
				myRenderers[num] = renderer;
				num++;
			}
			if (num >= 1)
			{
				isVisible = myRenderers[0].enabled;
			}
			else
			{
				isVisible = true;
			}
		}
	}

	public IEnumerator StartCulling()
	{
		if (myRenderers == null)
		{
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				SetupVars();
			}
			yield return 0;
		}
		if (isVisible)
		{
			isVisible = !isVisible;
			Renderer[] array = myRenderers;
			foreach (Renderer myRenderer in array)
			{
				myRenderer.enabled = false;
			}
		}
	}

	public IEnumerator StopCulling()
	{
		if (myRenderers == null)
		{
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			{
				SetupVars();
			}
			yield return 0;
		}
		if (!isVisible)
		{
			isVisible = !isVisible;
			Renderer[] array = myRenderers;
			foreach (Renderer myRenderer in array)
			{
				myRenderer.enabled = true;
			}
		}
	}
}
