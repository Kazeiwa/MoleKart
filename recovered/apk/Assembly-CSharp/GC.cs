using System.Collections;
using UnityEngine;

public class GC : MonoBehaviour
{
	public static GC Create()
	{
		return new GameObject("GC").AddComponent<GC>();
	}

	public void CollectGarbage()
	{
		Debug.Log("Collect Gargabe!");
		StartCoroutine("UnLoadAssets");
	}

	private IEnumerator UnLoadAssets()
	{
		yield return 0;
		yield return 0;
	}
}
