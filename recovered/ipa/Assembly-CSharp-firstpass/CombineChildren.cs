using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Mesh/Combine Children")]
public class CombineChildren : MonoBehaviour
{
	public bool generateTriangleStrips;

	public List<GameObject> currentMeshes;

	public List<Renderer> allRenderers;

	public bool combineAtStart;

	public bool destroyOnDisable;

	[HideInInspector]
	public bool areaCombine;

	public bool removeColliders;

	private void Start()
	{
	}

	public void DisableCombine()
	{
	}

	public void Combine()
	{
	}
}
