using System.Collections.Generic;
using UnityEngine;

public class iTweenPath : MonoBehaviour
{
	public string pathName;

	public Color pathColor;

	public List<Vector3> nodes;

	public int nodeCount;

	public static Dictionary<string, iTweenPath> paths;

	public bool initialized;

	public string initialName;

	private void OnEnable()
	{
	}

	private void OnDrawGizmosSelected()
	{
	}

	public static Vector3[] GetPath(string requestedName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void Clear()
	{
	}
}
