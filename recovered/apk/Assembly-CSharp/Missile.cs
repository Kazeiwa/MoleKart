using System.Collections;
using UnityEngine;

public class Missile : Prop
{
	private Car m_PlayerCar;

	private int m_CurrentWaypoint;

	private int m_Branch = -1;

	private float m_FlyingTime;

	private static GameObject m_MainCamera;

	public float MISSILE_LIFE_TIME = 10f;

	private float m_FlySpeed = 40f;

	protected ArrayList m_WayPoints;

	private bool m_PlayDisappearEft;

	private ArrayList pts = new ArrayList();

	private float m_PathLength;

	private void Start()
	{
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.enabled = true;
			componentInChildren.castShadows = false;
			componentInChildren.receiveShadows = false;
		}
		PlaySound(52101, 1);
	}

	private void FixedUpdate()
	{
		if (null == m_PlayerCar)
		{
			return;
		}
		FollowWaypoint();
		if (!m_PlayDisappearEft && m_FlyingTime >= MISSILE_LIFE_TIME - 0.3f)
		{
			m_PlayDisappearEft = true;
			HideMissile();
		}
		if (m_FlyingTime >= MISSILE_LIFE_TIME)
		{
			if (null != m_PlayerCar)
			{
				ReCoverCarConfig();
				if (m_PlayerCar.isHero && null != m_MainCamera)
				{
					SmoothCameraWithBumper component = m_MainCamera.GetComponent<SmoothCameraWithBumper>();
					if (null != component)
					{
						component.m_CurCameraData.m_Target = m_PlayerCar.gameObject.transform;
					}
				}
				m_PlayerCar.RecoverCarFromMissile();
			}
			if (null != m_Audio)
			{
				m_Audio.Stop();
				SoundManager.Instance.RemoveAudio(m_Audio);
			}
			TSingleton<PropManager>.Instance.Release(ePropType.MISSILE, this);
			Object.Destroy(base.gameObject);
		}
		CheckCollidWithMonster(3f);
	}

	private void ReCoverCarConfig()
	{
		m_PlayerCar.m_RecoverPos = base.transform.position;
		m_PlayerCar.m_RecoverQuat = base.transform.rotation;
		m_PlayerCar.m_RecoverVel = base.transform.forward * 20f;
		m_PlayerCar.SetPosition(m_PlayerCar.m_RecoverPos);
		m_PlayerCar.SetRotation(m_PlayerCar.m_RecoverQuat);
		m_PlayerCar.ResetPathFollowing();
	}

	private void HideMissile()
	{
		EmitController componentInChildren = base.transform.GetComponentInChildren<EmitController>();
		if (null != componentInChildren)
		{
			componentInChildren.doReset();
		}
		SkinnedMeshRenderer[] componentsInChildren = base.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
		int num = 0;
		while (componentsInChildren != null && num < componentsInChildren.Length)
		{
			componentsInChildren[num].enabled = false;
			num++;
		}
		MeshRenderer[] componentsInChildren2 = base.transform.GetComponentsInChildren<MeshRenderer>();
		int num2 = 0;
		while (componentsInChildren2 != null && num2 < componentsInChildren2.Length)
		{
			componentsInChildren2[num2].enabled = false;
			num2++;
		}
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

	private void FollowWaypoint()
	{
		m_FlyingTime += Time.deltaTime;
		if (!(m_FlyingTime >= MISSILE_LIFE_TIME))
		{
			base.transform.position = Interp(pts, m_FlyingTime / MISSILE_LIFE_TIME, m_PathLength);
			base.transform.LookAt(Interp(pts, m_FlyingTime / MISSILE_LIFE_TIME + 0.05f, m_PathLength));
			m_PlayerCar.SetPosition(base.transform.position);
			m_PlayerCar.SetRotation(base.transform.rotation);
		}
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && null != m_PlayerCar && m_PlayerCar.gameObject != other.transform.parent.parent.gameObject)
		{
			bool right = false;
			if (Vector3.Dot(other.transform.parent.parent.transform.position - base.transform.position, base.transform.right) > 0f)
			{
				right = true;
			}
			car.PlaySound(40002, 1);
			car.HitByMissile(right);
		}
	}

	public void SetCar(Car car, GameObject mainCamera)
	{
		if (!(null != car))
		{
			return;
		}
		m_PlayerCar = car;
		base.transform.position = car.transform.position + car.transform.forward + car.transform.up * 0.5f;
		base.transform.right = car.transform.right;
		base.transform.up = car.transform.up;
		base.transform.forward = car.transform.forward;
		m_CurrentWaypoint = FindNearByWaypoint(step: true);
		car.BecomeMissile();
		if (null != mainCamera)
		{
			m_MainCamera = mainCamera;
		}
		if (car.isHero && null != m_MainCamera)
		{
			SmoothCameraWithBumper component = m_MainCamera.GetComponent<SmoothCameraWithBumper>();
			if (null != component)
			{
				component.m_CurCameraData.m_Target = base.transform;
			}
		}
		CreatePath();
	}

	private bool CreatePath()
	{
		float num = m_FlySpeed * MISSILE_LIFE_TIME;
		pts.Clear();
		pts.Add(base.transform.position);
		pts.Add(base.transform.position);
		pts.Add((Vector3)m_WayPoints[m_CurrentWaypoint]);
		m_PathLength = ((Vector3)pts[2] - (Vector3)pts[1]).magnitude;
		Vector3 vector = base.transform.position;
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

	protected ArrayList GetBranchWaypoints()
	{
		if (m_Branch < 0)
		{
			m_Branch = WayPoint.Instance.GetBranchNum(base.transform.position);
		}
		m_WayPoints = WayPoint.Instance.GetBranchWayPoints(m_Branch);
		return m_WayPoints;
	}

	private int FindNearByWaypoint(bool step)
	{
		if (GetBranchWaypoints() == null)
		{
			return m_CurrentWaypoint;
		}
		float num = float.MaxValue;
		int result = 0;
		int num2 = 0;
		while (m_WayPoints != null && num2 < m_WayPoints.Count)
		{
			Vector3 vector = (Vector3)m_WayPoints[num2];
			float magnitude = (vector - base.transform.position).magnitude;
			vector -= base.transform.position;
			if (magnitude < num)
			{
				num = magnitude;
				result = num2;
			}
			num2++;
		}
		return result;
	}
}
