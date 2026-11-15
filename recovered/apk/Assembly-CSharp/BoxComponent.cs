using UnityEngine;

public class BoxComponent : CCollisionBody
{
	protected class BoxData
	{
		public Vector3 center;

		public Vector3 size;

		public float scale;

		public Vector3 vFR;

		public Vector3 vFL;

		public Vector3 vBR;

		public Vector3 vBL;

		public Vector3 vForwardDir;

		public Vector3 vUp;
	}

	private Transform m_tsCollider;

	public EnmCollisionSide m_state;

	public Vector3 CollisionPos;

	public Vector3 GroundNormal;

	public float DistanceToGround;

	protected BoxData m_BoxData;

	public Vector3 vFR => m_tsCollider.TransformPoint(m_BoxData.vFR);

	public Vector3 vFL => m_tsCollider.TransformPoint(m_BoxData.vFL);

	public Vector3 vBR => m_tsCollider.TransformPoint(m_BoxData.vBR);

	public Vector3 vBL => m_tsCollider.TransformPoint(m_BoxData.vBL);

	public Vector3 vBoxCenter => m_tsCollider.TransformPoint(m_BoxData.center);

	public Vector3 vForwardDir => m_tsCollider.TransformDirection(m_BoxData.vForwardDir);

	public Vector3 size => m_BoxData.size;

	public float scale => m_BoxData.scale;

	public Vector3 up => m_tsCollider.TransformDirection(m_BoxData.vUp);

	public BoxComponent(GameObject gameobject, EnmCollisionLayer clLayer)
		: base(gameobject, clLayer)
	{
		m_type = CollisionBodyType.CBT_DYNAMIC;
		if (clLayer == EnmCollisionLayer.CL_PLAYER || clLayer == EnmCollisionLayer.CL_NPC)
		{
			m_tsCollider = m_Transform;
			BoxCollider kBoxCollider = m_Collider as BoxCollider;
			InitBoxData(kBoxCollider);
		}
	}

	public BoxComponent(GameObject gameobject, float fOffsetDistance, EnmCollisionLayer clLayer)
		: base(gameobject, clLayer)
	{
		Debug.Log("BoxComponent: Init = " + gameobject.name);
		m_type = CollisionBodyType.CBT_DYNAMIC;
		if (clLayer == EnmCollisionLayer.CL_PLAYER || clLayer == EnmCollisionLayer.CL_NPC)
		{
			BoxCollider boxCollider = m_Collider as BoxCollider;
			Vector3 vector = boxCollider.size;
			vector.y -= fOffsetDistance;
			if (vector.y < 0.1f)
			{
				vector.y = 0.1f;
			}
			boxCollider.size = vector;
			m_tsCollider = Utility.FindChildTranform(m_Transform, "collision");
			InitBoxData(boxCollider);
		}
	}

	public virtual void SetCurState(EnmCollisionSide curState)
	{
		m_state = curState;
	}

	private void InitBoxData(BoxCollider kBoxCollider)
	{
		m_BoxData = new BoxData();
		m_BoxData.center = kBoxCollider.center;
		m_BoxData.size = kBoxCollider.size;
		m_BoxData.scale = Mathf.Sqrt(m_BoxData.size.x * m_BoxData.size.x + m_BoxData.size.z * m_BoxData.size.z) / 2f;
		m_BoxData.vFR = new Vector3(m_BoxData.size.x / 2f, 0f, m_BoxData.size.z / 2f);
		m_BoxData.vFR = m_BoxData.center + m_BoxData.vFR;
		m_BoxData.vFL = new Vector3((0f - m_BoxData.size.x) / 2f, 0f, m_BoxData.size.z / 2f);
		m_BoxData.vFL = m_BoxData.center + m_BoxData.vFL;
		m_BoxData.vBR = new Vector3(m_BoxData.size.x / 2f, 0f, (0f - m_BoxData.size.z) / 2f);
		m_BoxData.vBR = m_BoxData.center + m_BoxData.vBR;
		m_BoxData.vBL = new Vector3((0f - m_BoxData.size.x) / 2f, 0f, (0f - m_BoxData.size.z) / 2f);
		m_BoxData.vBL = m_BoxData.center + m_BoxData.vBL;
		m_BoxData.vForwardDir = new Vector3(0f, 0f, 1f);
		m_BoxData.vUp = new Vector3(0f, 1f, 0f);
	}

	public void MoveToGround()
	{
		if (m_BoxData != null)
		{
			Debug.Log("MoveToGround()" + vBoxCenter);
			Vector3 vector = vBoxCenter;
			if (CollisionLayer.Instance.VerticalIntersectHitPosition(vector, out var vGroundNormal, out var vHitPoint))
			{
				vGroundNormal.Normalize();
				float num = m_BoxData.size.z / 2f;
				Vector3 translation = vHitPoint + vGroundNormal * num;
				translation -= vector;
				m_GameObject.transform.Translate(translation);
			}
		}
	}
}
