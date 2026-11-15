using System.Collections;
using RaceTrack;
using UnityEngine;

public class Tortoise : Prop
{
	protected float m_EftTime;

	protected float m_LifeTime = 1f;

	protected bool m_Throwing;

	protected Vector3 m_ThrowDir = Vector3.one;

	protected Car m_SurroundCar;

	protected bool m_Forward;

	protected int m_CurrentWaypoint;

	public float TORTOISE_LIFE_TIME = 50f;

	protected float m_FlyingSpeed = 35f;

	protected float m_ThrowSpeed = 60f;

	public float TORTOISE_ROTATE_RADIUS = 1.9f;

	public float TORTOISE_ROTATE_SPEED = 10f;

	protected TrailRenderer m_TrailRender;

	protected int m_Branch = -1;

	protected ArrayList m_WayPoints;

	protected Vector3 m_InitForward = Vector3.one;

	protected GameObject m_OwnerCar;

	protected bool m_Droped;

	protected ArrayList pts = new ArrayList();

	protected float m_PathLength;

	public ePropType m_Type = ePropType.TORTOISE_GREEN;

	public Car SurroundCar
	{
		get
		{
			return m_SurroundCar;
		}
		set
		{
			m_SurroundCar = value;
			if (null == m_SurroundCar)
			{
				base.transform.parent = null;
			}
			else
			{
				base.transform.parent = m_SurroundCar.transform.Find("effect");
			}
		}
	}

	public override bool IsPickable
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public override bool IsDangerous
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public override bool IsMoving
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	protected void RotateSurroundCar()
	{
		TORTOISE_ROTATE_RADIUS = m_SurroundCar.m_CarRadius;
		if (!(null == base.transform.parent))
		{
			Vector3 localPosition = base.transform.localPosition;
			Vector3 vector = Vector3.Cross(Vector3.up, localPosition);
			Vector3 localPosition2 = base.transform.localPosition + vector.normalized * TORTOISE_ROTATE_SPEED * Time.deltaTime;
			localPosition2.y = 0.5f;
			if (localPosition2.sqrMagnitude > TORTOISE_ROTATE_RADIUS * TORTOISE_ROTATE_RADIUS)
			{
				localPosition2 = localPosition2.normalized * TORTOISE_ROTATE_RADIUS;
			}
			base.transform.localPosition = localPosition2;
		}
	}

	private void RotatePositionAdjust()
	{
		ArrayList arrayList = TSingleton<PropManager>.Instance.Tortoises(m_SurroundCar.name);
		Tortoise tortoise = null;
		if (arrayList != null)
		{
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (this == arrayList[i] as Tortoise)
				{
					tortoise = arrayList[(i + 1) % arrayList.Count] as Tortoise;
					break;
				}
			}
		}
		if (null != tortoise && !tortoise.m_HitTarget && this != tortoise)
		{
			Vector3 vector = base.transform.position - m_SurroundCar.transform.position;
			Vector3 vector2 = tortoise.transform.position - m_SurroundCar.transform.position;
			vector.y = (vector2.y = 0f);
			if (Vector3.Dot(vector.normalized, vector2.normalized) >= 0f)
			{
				Vector3 vector3 = Quaternion.AngleAxis(60f, Vector3.up) * vector;
				base.transform.position = m_SurroundCar.transform.position + vector3.normalized * TORTOISE_ROTATE_RADIUS;
			}
		}
	}

	protected void PrepareDeleteObject(float time)
	{
		RaceWorld.Instance.RemoveProp(base.name);
		if (m_EftTime > time)
		{
			if (null != m_Audio)
			{
				m_Audio.Stop();
				SoundManager.Instance.RemoveAudio(m_Audio);
			}
			Object.Destroy(base.gameObject);
		}
		m_EftTime += Time.deltaTime;
	}

	protected ArrayList GetBranchWaypoints()
	{
		if (m_Branch < 0)
		{
			m_Branch = WayPoint.Instance.GetBranchNum(base.transform.position);
		}
		m_WayPoints = WayPoint.Instance.GetBranchWayPoints(m_Branch);
		return m_WayPoints;
	}

	private Vector3 Interp(ArrayList pts, float t, float pathLength)
	{
		float num = pathLength * t;
		int num2 = 0;
		float num3 = 0f;
		float num4 = 0f;
		while (num3 <= num && num < pathLength)
		{
			Vector3 vector = (Vector3)pts[num2];
			Vector3 vector2 = (Vector3)pts[num2 + 1];
			num4 = (vector2 - vector).magnitude;
			num3 += num4;
			num2++;
		}
		if (num2 < 2)
		{
			return (Vector3)pts[pts.Count - 1];
		}
		num2--;
		float num5 = (num - (num3 - num4)) / num4;
		Vector3 vector3 = (Vector3)pts[num2 - 1];
		Vector3 vector4 = (Vector3)pts[num2];
		Vector3 vector5 = (Vector3)pts[num2 + 1];
		Vector3 vector6 = (Vector3)pts[num2 + 2];
		return 0.5f * ((-vector3 + 3f * vector4 - 3f * vector5 + vector6) * (num5 * num5 * num5) + (2f * vector3 - 5f * vector4 + 4f * vector5 - vector6) * (num5 * num5) + (-vector3 + vector5) * num5 + 2f * vector4);
	}

	protected bool CreatePath()
	{
		m_CurrentWaypoint = FindNearByWaypoint();
		float num = m_FlyingSpeed * TORTOISE_LIFE_TIME;
		pts.Clear();
		pts.Add(base.transform.position);
		pts.Add(base.transform.position);
		pts.Add((Vector3)m_WayPoints[m_CurrentWaypoint]);
		m_PathLength = ((Vector3)pts[2] - (Vector3)pts[1]).magnitude;
		Vector3 vector = (Vector3)m_WayPoints[m_CurrentWaypoint];
		int num2 = 1;
		while (m_WayPoints != null && m_PathLength < num)
		{
			if (m_Branch > 0 && m_CurrentWaypoint + num2 >= m_WayPoints.Count)
			{
				m_CurrentWaypoint = WayPoint.Instance.GetNearWaypointNum(0, (Vector3)m_WayPoints[m_WayPoints.Count - 1]);
				num2 = 0;
				m_Branch = 0;
				GetBranchWaypoints();
			}
			else
			{
				vector = (Vector3)m_WayPoints[(m_CurrentWaypoint + num2) % m_WayPoints.Count];
				m_PathLength += (vector - (Vector3)pts[pts.Count - 1]).magnitude;
				pts.Add(vector);
			}
			num2++;
		}
		pts.Add(vector);
		m_PathLength = num;
		return true;
	}

	protected void FollowWaypoint()
	{
		if (!(m_CreatedTime >= TORTOISE_LIFE_TIME))
		{
			base.transform.position = Interp(pts, m_CreatedTime / TORTOISE_LIFE_TIME, m_PathLength);
		}
	}

	protected int FindNearByWaypoint()
	{
		if (GetBranchWaypoints() == null)
		{
			return m_CurrentWaypoint;
		}
		float num = float.MaxValue;
		int num2 = -1;
		int num3 = 0;
		while (m_WayPoints != null && num3 < m_WayPoints.Count)
		{
			Vector3 vector = (Vector3)m_WayPoints[num3];
			float magnitude = (vector - base.transform.position).magnitude;
			vector -= base.transform.position;
			if (magnitude < num && Vector3.Dot(base.transform.forward, vector) > 0f)
			{
				float magnitude2 = (base.transform.position - (Vector3)m_WayPoints[num3]).magnitude;
				if (!CollisionLayer.Instance.RayCastHitToGuardbar(base.transform.position, base.transform.forward, magnitude2, out var _, out var _))
				{
					num = magnitude;
					num2 = num3;
				}
			}
			num3++;
		}
		if (num2 < 0)
		{
			for (int i = 0; i < m_WayPoints.Count; i++)
			{
				Vector3 vector2 = (Vector3)m_WayPoints[i];
				float magnitude3 = (vector2 - base.transform.position).magnitude;
				if (magnitude3 < num)
				{
					num = magnitude3;
					num2 = i;
				}
			}
			if (Vector3.Dot(WayPoint.Instance.GetPoint(m_Branch, num2) - base.transform.position, base.transform.forward) < 0f)
			{
				num2++;
				if (num2 >= m_WayPoints.Count)
				{
					num2 = WayPoint.Instance.GetNearWaypointNum(0, base.transform.position);
					num2++;
					m_Branch = 0;
					num2 %= WayPoint.Instance.GetBranchWayPoints(0).Count;
					m_WayPoints = WayPoint.Instance.GetBranchWayPoints(0);
				}
			}
		}
		return num2;
	}

	protected bool CheckTouchGround()
	{
		float num = HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z);
		if (base.transform.position.y - num > 0.2f)
		{
			if (m_DeltaRight < 0.7f)
			{
				m_DeltaRight += 0.05f;
				base.transform.position -= 0.05f * m_InitRight;
			}
			return false;
		}
		base.transform.position = new Vector3(base.transform.position.x, num + 0.5f, base.transform.position.z);
		if (!m_Droped)
		{
			PlaySound(51202, 1);
		}
		return true;
	}

	public void ThrowFromPos(GameObject ownerCar, Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
		m_OwnerCar = ownerCar;
		base.transform.localPosition = vPos;
		m_ThrowDir = vDir;
		m_InitForward = vDir;
		m_ThrowDir.y = 0.8f;
		m_ThrowDir.Normalize();
		if (forward)
		{
			m_ThrowDir *= m_ThrowSpeed + fRelationspeed * 4f;
		}
		else
		{
			m_ThrowDir *= 20f;
		}
		m_InitRight = right;
		m_Throwing = true;
		m_Forward = forward;
		base.transform.forward = vDir;
		RaceWorld.Instance.AddProp(base.name, this);
	}

	public void DropAtPos(Vector3 pos, Vector3 dir, float speed)
	{
		base.transform.localPosition = pos;
		m_ThrowDir = dir;
		m_ThrowDir.y = 0.5f;
		m_ThrowDir = m_ThrowDir.normalized * speed;
		m_Throwing = true;
		m_Droped = true;
		RaceWorld.Instance.AddProp(base.name, this);
	}

	protected void PlayExplodeEft(float time, eEffectType type)
	{
		GameObject gameObject = EffectManager.Instance.CreateEffect(type, isPause: true, isOneShot: false);
		if (null != gameObject)
		{
			Effect component = gameObject.GetComponent<Effect>();
			if (null != component)
			{
				component.AttachTo(base.transform, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
				component.SetLifeTime(time);
				component.doReset();
			}
		}
	}

	protected void StopSurroundSound()
	{
		if (null != m_SurroundCar && TSingleton<PropManager>.Instance.Tortoises(m_SurroundCar.name) != null && TSingleton<PropManager>.Instance.Tortoises(m_SurroundCar.name).Count <= 1)
		{
			m_SurroundCar.StopSound(51301);
		}
	}
}
