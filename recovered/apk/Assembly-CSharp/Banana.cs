using RaceTrack;
using UnityEngine;

public class Banana : Prop
{
	private bool m_Throwing;

	private Vector3 m_ThrowDir = Vector3.one;

	public Car m_FollowCar;

	public float m_FollowDis = 2f;

	public float BANANA_LIFE_TIME = 60f;

	private float m_ThrowSpeed = 150f;

	private float m_AnimateTime = 1f;

	private bool m_AnimPlayed;

	private Transform m_Fx_Banana;

	private Transform m_Pro01;

	public int FollowOrder = 1;

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
			return m_Throwing;
		}
		set
		{
		}
	}

	private void Start()
	{
		m_HitTarget = false;
		Renderer componentInChildren = base.gameObject.GetComponentInChildren<Renderer>();
		if (null != componentInChildren)
		{
			componentInChildren.enabled = true;
			componentInChildren.castShadows = false;
			componentInChildren.receiveShadows = false;
		}
		m_Fx_Banana = base.transform.FindChild("fx_banana");
		if (null != m_Fx_Banana)
		{
			m_Fx_Banana.gameObject.SetActiveRecursively(state: false);
		}
		InitDistanceToCamera();
	}

	protected override void OnTriggerEnter(Collider other)
	{
		Car car = null;
		if (null != other.transform.parent && null != other.transform.parent.parent && null != (car = other.transform.parent.parent.gameObject.GetComponent<Car>()) && m_FollowCar != car && !m_HitTarget && !m_Throwing)
		{
			car.HitByBanana();
			m_HitTarget = true;
			SendMessageHandler.Instance.BroadPropDestroy(ePropType.BANANA, base.name);
		}
	}

	private void Update()
	{
		UpdateDistanceToCamera();
		UpdateVisible();
		if (m_HitTarget)
		{
			PrepareDeleteObject();
			return;
		}
		if (m_Throwing)
		{
			if (!CheckTouchGround())
			{
				if (CheckCollide(base.transform.position, m_ThrowDir, 2f, out var rebounceDir))
				{
					m_ThrowDir = rebounceDir;
				}
				float num = 0f;
				while (num < Time.deltaTime)
				{
					float num2 = ((!(DELTA_TIME < Time.deltaTime - num)) ? (Time.deltaTime - num) : DELTA_TIME);
					num += num2;
					m_ThrowDir.x -= m_ThrowDir.x * num2 * 0.5f;
					m_ThrowDir.z -= m_ThrowDir.z * num2 * 0.5f;
					m_ThrowDir.y -= Mathf.Abs(m_ThrowDir.y) * num2 * 25f;
					m_ThrowDir.y += -9.8f * num2;
					base.gameObject.transform.position += m_ThrowDir * num2;
				}
				return;
			}
			m_Throwing = false;
			EmitController componentInChildren = base.transform.GetComponentInChildren<EmitController>();
			if (null != componentInChildren)
			{
				componentInChildren.doReset();
			}
			if (base.transform.position.y < HeightMap.m_InvalideHeight)
			{
				m_HitTarget = true;
			}
		}
		if (null != m_FollowCar)
		{
			if (null == m_Pro01)
			{
				m_Pro01 = m_FollowCar.transform.Find("car/root/pro01");
			}
			if (null != m_Pro01)
			{
				Vector3 vector = new Vector3(m_FollowCar.transform.position.x, m_Pro01.position.y, m_FollowCar.transform.position.z);
				base.transform.position = vector - m_FollowCar.transform.forward * ((float)FollowOrder * 0.5f + m_FollowCar.m_FollowDis);
			}
		}
		else
		{
			m_CreatedTime += Time.deltaTime;
			if (m_CreatedTime > BANANA_LIFE_TIME)
			{
				RaceWorld.Instance.RemoveProp(base.name);
				TSingleton<PropManager>.Instance.Release(ePropType.BANANA, this);
				SoundManager.Instance.RemoveAudio(m_Audio);
				Object.Destroy(base.gameObject);
			}
		}
		CheckCollidWithMonster(3f);
		if (null == m_FollowCar)
		{
			CheckCollideWithOtherProps(2f);
		}
	}

	protected void PrepareDeleteObject()
	{
		m_AnimateTime -= Time.deltaTime;
		if (!m_AnimPlayed)
		{
			RaceWorld.Instance.RemoveProp(base.name);
			TSingleton<PropManager>.Instance.Release(ePropType.BANANA, this);
			if (m_Fx_Banana != null)
			{
				Object.Destroy(m_Fx_Banana.gameObject);
			}
			Animation componentInChildren = base.transform.GetComponentInChildren<Animation>();
			if (null != componentInChildren)
			{
				componentInChildren.Play();
			}
			PlaySound(51002, 1);
			m_AnimPlayed = true;
		}
		if (m_AnimateTime <= 0f)
		{
			m_AnimateTime = 0f;
			SoundManager.Instance.RemoveAudio(m_Audio);
			Object.Destroy(base.gameObject);
		}
	}

	private bool CheckTouchGround()
	{
		float num = HeightMapManager.Instance.Height(base.transform.position.x, base.transform.position.z);
		if (base.transform.position.y - num > 0.2f)
		{
			if (m_DeltaRight < 0.7f)
			{
				m_DeltaRight += 0.05f;
				base.transform.position -= 0.05f * m_InitRight;
				base.transform.localScale += new Vector3(0.07f, 0.07f, 0.07f);
			}
			return false;
		}
		base.transform.position = new Vector3(base.transform.position.x, num, base.transform.position.z);
		return true;
	}

	public void ThrowFromPos(Vector3 vPos, Vector3 vDir, Vector3 right, float fRelationspeed, bool forward)
	{
		m_ThrowDir = vDir;
		base.transform.position = vPos;
		m_ThrowDir.y = 2.5f;
		m_ThrowDir.Normalize();
		if (forward)
		{
			m_ThrowDir *= m_ThrowSpeed + fRelationspeed * 5f;
		}
		else
		{
			m_ThrowDir *= 20f;
		}
		m_InitRight = right;
		m_Throwing = true;
		if (null == m_Fx_Banana)
		{
			m_Fx_Banana = base.transform.FindChild("fx_banana");
		}
		if (null != m_Fx_Banana)
		{
			m_Fx_Banana.gameObject.SetActiveRecursively(state: true);
		}
		RaceWorld.Instance.AddProp(base.name, this);
	}

	public void DropAtPos(Vector3 pos, Vector3 vDir, float speed)
	{
		base.transform.position = pos;
		m_ThrowDir = vDir;
		m_ThrowDir.y = 1.5f;
		m_ThrowDir = m_ThrowDir.normalized * speed;
		m_Throwing = true;
		RaceWorld.Instance.AddProp(base.name, this);
	}
}
