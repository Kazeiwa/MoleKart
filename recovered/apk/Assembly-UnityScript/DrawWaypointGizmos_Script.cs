using System;
using UnityEngine;

[Serializable]
public class DrawWaypointGizmos_Script : MonoBehaviour
{
	public virtual void OnDrawGizmos()
	{
		Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeof(Transform));
		int i = 0;
		Component[] array = componentsInChildren;
		for (int length = array.Length; i < length; i = checked(i + 1))
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(((Transform)array[i]).position, 0.5f);
		}
	}

	public virtual void Main()
	{
	}
}
