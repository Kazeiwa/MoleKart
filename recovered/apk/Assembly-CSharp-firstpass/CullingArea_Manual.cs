using System.Collections.Generic;
using UnityEngine;

public class CullingArea_Manual : MonoBehaviour
{
	[SerializeField]
	public List<CullingAreaGroupSettings> groupsList;

	public Color gizmoColor;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawCube(base.transform.position, base.transform.localScale);
	}
}
