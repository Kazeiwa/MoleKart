using UnityEngine;

internal class BuddyGizmos : MonoBehaviour
{
	private SphereBuddy m_sphere;

	public void SetSphereBuddy(SphereBuddy sphere)
	{
		m_sphere = sphere;
	}

	private void OnDrawGizmos()
	{
		if (m_sphere != null)
		{
			Vector3 position = base.transform.position;
			Vector3 vLastForce = m_sphere.m_vLastForce;
			Gizmos.color = Color.red;
			Gizmos.DrawRay(position, vLastForce);
		}
	}
}
