using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Mesh/Combine Children")]
public class CombineChildren : MonoBehaviour
{
	public bool generateTriangleStrips = true;

	public List<GameObject> currentMeshes;

	public List<Renderer> allRenderers;

	public bool combineAtStart = true;

	public bool destroyOnDisable;

	[HideInInspector]
	public bool areaCombine;

	public bool removeColliders;

	private void Start()
	{
		if (combineAtStart)
		{
			Combine();
		}
	}

	public void DisableCombine()
	{
		if (currentMeshes != null)
		{
			for (int i = 0; i < currentMeshes.Count; i++)
			{
				if (currentMeshes[i] != null)
				{
					if (Application.isEditor)
					{
						UnityEngine.Object.DestroyImmediate(currentMeshes[i]);
					}
					else
					{
						UnityEngine.Object.Destroy(currentMeshes[i]);
					}
				}
			}
			currentMeshes.Clear();
		}
		if (allRenderers != null)
		{
			for (int j = 0; j < allRenderers.Count; j++)
			{
				if (allRenderers[j] != null)
				{
					allRenderers[j].enabled = true;
				}
			}
			allRenderers.Clear();
		}
		if (GetComponent(typeof(MeshFilter)) != null)
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(GetComponent(typeof(MeshFilter)));
			}
			else
			{
				UnityEngine.Object.Destroy(GetComponent(typeof(MeshFilter)));
			}
		}
		if ((bool)GetComponent("MeshRenderer"))
		{
			if (Application.isEditor)
			{
				UnityEngine.Object.DestroyImmediate(GetComponent(typeof(MeshRenderer)));
			}
			else
			{
				UnityEngine.Object.Destroy(GetComponent("MeshRenderer"));
			}
		}
	}

	public void Combine()
	{
		if (!Application.isEditor)
		{
			DisableCombine();
		}
		currentMeshes = new List<GameObject>();
		allRenderers = new List<Renderer>();
		Component[] componentsInChildren = GetComponentsInChildren(typeof(MeshFilter));
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		Hashtable hashtable = new Hashtable();
		MeshFilter component = GetComponent<MeshFilter>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshFilter meshFilter = (MeshFilter)componentsInChildren[i];
			if (component != null && component == meshFilter)
			{
				continue;
			}
			if (removeColliders && meshFilter.collider != null)
			{
				if (Application.isEditor)
				{
					UnityEngine.Object.DestroyImmediate(meshFilter.collider);
				}
				else
				{
					UnityEngine.Object.Destroy(meshFilter.collider);
				}
			}
			Renderer renderer = componentsInChildren[i].renderer;
			MeshCombineUtility.MeshInstance meshInstance = new MeshCombineUtility.MeshInstance
			{
				mesh = meshFilter.sharedMesh
			};
			if (!(renderer != null) || !renderer.enabled || !(meshInstance.mesh != null))
			{
				continue;
			}
			meshInstance.transform = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
			Material[] sharedMaterials = renderer.sharedMaterials;
			for (int j = 0; j < sharedMaterials.Length; j++)
			{
				meshInstance.subMeshIndex = Math.Min(j, meshInstance.mesh.subMeshCount - 1);
				ArrayList arrayList = (ArrayList)hashtable[sharedMaterials[j]];
				if (arrayList != null)
				{
					arrayList.Add(meshInstance);
					continue;
				}
				arrayList = new ArrayList();
				arrayList.Add(meshInstance);
				hashtable.Add(sharedMaterials[j], arrayList);
			}
			allRenderers.Add(renderer);
			renderer.enabled = false;
		}
		foreach (DictionaryEntry item in hashtable)
		{
			ArrayList arrayList2 = (ArrayList)item.Value;
			MeshCombineUtility.MeshInstance[] combines = (MeshCombineUtility.MeshInstance[])arrayList2.ToArray(typeof(MeshCombineUtility.MeshInstance));
			if (hashtable.Count == 1)
			{
				if (GetComponent(typeof(MeshFilter)) == null)
				{
					base.gameObject.AddComponent(typeof(MeshFilter));
				}
				if (!GetComponent("MeshRenderer"))
				{
					base.gameObject.AddComponent("MeshRenderer");
				}
				MeshFilter meshFilter2 = (MeshFilter)GetComponent(typeof(MeshFilter));
				meshFilter2.mesh = MeshCombineUtility.Combine(combines, generateTriangleStrips);
				base.renderer.material = (Material)item.Key;
				base.renderer.enabled = true;
			}
			else
			{
				GameObject gameObject = new GameObject("Combined mesh");
				currentMeshes.Add(gameObject);
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.AddComponent(typeof(MeshFilter));
				gameObject.AddComponent("MeshRenderer");
				gameObject.renderer.material = (Material)item.Key;
				MeshFilter meshFilter3 = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
				meshFilter3.mesh = MeshCombineUtility.Combine(combines, generateTriangleStrips);
			}
		}
	}
}
