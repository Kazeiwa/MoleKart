using System;
using System.Collections;
using UnityEngine;

public class MovableObject
{
	protected RUNNING_MDOE m_eRunnigMode;

	public Vector3 m_vVecGlobal;

	public Rigidbody m_kRigidBody;

	public Transform m_CenterofMassTransform;

	public RUNNINGSTATES m_state;

	protected float m_fYaw;

	protected float m_fPitch;

	protected float m_fBank;

	protected float m_fAngularVelocity;

	protected Vector3 m_vWecGlobal;

	protected ENMLOCKMOTION m_eLockMotion;

	protected ArrayList arrForcePool = new ArrayList();

	protected ArrayList arrTwistPool = new ArrayList();

	protected Vector3 m_vTotalForce = Vector3.zero;

	protected Vector3 m_vTotalTwist = Vector3.zero;

	private int[] aIndicesOrg = new int[8];

	public RUNNING_MDOE RunningMode => m_eRunnigMode;

	public ENMLOCKMOTION LockMotion => m_eLockMotion;

	public Vector3 vTotalForce => m_vTotalForce;

	public Vector3 vTotalTwist => m_vTotalTwist;

	public MovableObject()
	{
		m_state = RUNNINGSTATES.RS_ONEARTH;
		m_eRunnigMode = RUNNING_MDOE.RM_GROUNDRUNING;
		m_vVecGlobal = Vector3.zero;
	}

	public void SetPosition(Vector3 pos)
	{
		m_CenterofMassTransform.position = pos;
	}

	public Vector3 GetPosition()
	{
		if (null != m_CenterofMassTransform)
		{
			return m_CenterofMassTransform.position;
		}
		return Vector3.zero;
	}

	public void SetRotation(Quaternion rotation)
	{
		m_CenterofMassTransform.rotation = rotation;
	}

	public Quaternion GetRotation()
	{
		return m_CenterofMassTransform.rotation;
	}

	public Vector3 GetVecGlobal()
	{
		if (!m_kRigidBody.isKinematic)
		{
			return m_kRigidBody.velocity;
		}
		return m_vVecGlobal;
	}

	public void SetVecGlobal(Vector3 vec)
	{
		m_vVecGlobal = vec;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.velocity = m_vVecGlobal;
		}
	}

	public void SetAngularVec(Vector3 avec)
	{
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.angularVelocity = avec;
		}
	}

	public float GetVelocity()
	{
		if (!m_kRigidBody.isKinematic)
		{
			m_vVecGlobal = m_kRigidBody.velocity;
		}
		return m_vVecGlobal.magnitude;
	}

	public float GetVecXGlobal()
	{
		return m_vVecGlobal.x;
	}

	public void SetVecXGlobal(float value)
	{
		m_vVecGlobal.x = value;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.velocity = m_vVecGlobal;
		}
	}

	public float GetVecYGlobal()
	{
		return m_vVecGlobal.y;
	}

	public void SetVecYGlobal(float value)
	{
		m_vVecGlobal.y = value;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.velocity = m_vVecGlobal;
		}
	}

	public float GetVecZGlobal()
	{
		return m_vVecGlobal.z;
	}

	public void SetVecZGlobal(float value)
	{
		m_vVecGlobal.z = value;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.velocity = m_vVecGlobal;
		}
	}

	public Vector3 GetVecLocal()
	{
		return m_CenterofMassTransform.InverseTransformDirection(m_vVecGlobal);
	}

	public Vector3 GetEulerAngle()
	{
		m_fYaw = m_CenterofMassTransform.rotation.eulerAngles.y;
		m_fPitch = m_CenterofMassTransform.rotation.eulerAngles.x;
		m_fBank = m_CenterofMassTransform.rotation.eulerAngles.z;
		return new Vector3(m_fPitch, m_fYaw, m_fBank);
	}

	public bool SetEulerAngle(Vector3 vEuler)
	{
		m_fYaw = vEuler.y;
		m_fPitch = vEuler.x;
		m_fBank = vEuler.z;
		m_CenterofMassTransform.localRotation = Quaternion.Euler(m_fPitch, m_fYaw, m_fBank);
		return true;
	}

	public void SetEulerAngleX(float fAngle)
	{
		m_fPitch = fAngle;
		m_CenterofMassTransform.localRotation = Quaternion.Euler(m_fPitch, m_fYaw, m_fBank);
	}

	public void SetEulerAngleY(float fAngle)
	{
		m_fYaw = fAngle;
		m_CenterofMassTransform.localRotation = Quaternion.Euler(m_fPitch, m_fYaw, m_fBank);
	}

	public float GetEulerAngleX()
	{
		return m_fPitch;
	}

	public float GetEulerAngleY()
	{
		return m_fYaw;
	}

	public float GetEulerAngleZ()
	{
		return m_fBank;
	}

	public void SetWecGlobal(Vector3 vWec)
	{
		m_vWecGlobal = vWec;
		m_kRigidBody.angularVelocity = m_vWecGlobal;
	}

	public Vector3 GetWecGlobal()
	{
		m_vWecGlobal = m_kRigidBody.angularVelocity;
		return m_vWecGlobal;
	}

	public void SetWecLocal(Vector3 vWec)
	{
		m_vWecGlobal = m_CenterofMassTransform.localToWorldMatrix * vWec;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.angularVelocity = m_vWecGlobal;
		}
	}

	public Vector3 GetWecLocal()
	{
		m_vWecGlobal = m_kRigidBody.angularVelocity;
		return m_CenterofMassTransform.worldToLocalMatrix * m_vWecGlobal;
	}

	public void LockRotation(bool bLockX, bool bLockY, bool bLockZ)
	{
		RigidbodyConstraints rigidbodyConstraints = RigidbodyConstraints.None;
		if (bLockX)
		{
			rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationX;
		}
		if (bLockY)
		{
			rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationY;
		}
		if (bLockZ)
		{
			rigidbodyConstraints |= RigidbodyConstraints.FreezeRotationZ;
		}
		m_kRigidBody.constraints = rigidbodyConstraints;
	}

	public void AddForceGlobal(Vector3 vForce, ENMFORCETYPE eType, float fTime)
	{
		FORCE fORCE = new FORCE
		{
			vForce = vForce,
			dwTime = fTime,
			eType = eType
		};
		arrForcePool.Add(fORCE);
	}

	public void AddForceAtPointGlobal(Vector3 vForce, Vector3 vPosition, float fTime)
	{
		FORCE fORCE = new FORCE
		{
			vForce = vForce,
			dwTime = fTime,
			vPosition = vPosition,
			eType = ENMFORCETYPE.EFT_POSITION
		};
		arrForcePool.Add(fORCE);
	}

	public void TriggerForceTwistPool(float fTicktime)
	{
		m_vTotalForce = Vector3.zero;
		m_vTotalTwist = Vector3.zero;
		bool flag = true;
		Vector3 zero = Vector3.zero;
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < arrForcePool.Count; i++)
		{
			FORCE fORCE = (FORCE)arrForcePool[i];
			if (fORCE.eType == ENMFORCETYPE.EFT_ONLYONE)
			{
				if (flag)
				{
					m_vTotalForce += fORCE.vForce;
					flag = false;
				}
			}
			else if (fORCE.eType == ENMFORCETYPE.EFT_POSITION)
			{
				if (!m_kRigidBody.isKinematic)
				{
					m_kRigidBody.AddForceAtPosition(fORCE.vForce, fORCE.vPosition);
				}
				zero += fORCE.vForce;
			}
			else if (fORCE.eType == ENMFORCETYPE.EFT_VELOCITY_CHANGE)
			{
				if (!m_kRigidBody.isKinematic)
				{
					m_kRigidBody.AddForce(fORCE.vForce * fTicktime, ForceMode.VelocityChange);
				}
			}
			else
			{
				m_vTotalForce += fORCE.vForce;
			}
			fORCE.dwTime -= fTicktime;
			if (fORCE.dwTime > 0f)
			{
				arrayList.Add(fORCE);
			}
		}
		ArrayList arrayList2 = new ArrayList();
		for (int j = 0; j < arrTwistPool.Count; j++)
		{
			TWIST tWIST = (TWIST)arrTwistPool[j];
			m_vTotalTwist += tWIST.vTwist;
			tWIST.dwTime -= fTicktime;
			if (tWIST.dwTime > 0f)
			{
				arrayList2.Add(tWIST);
			}
		}
		arrTwistPool = arrayList2;
		arrForcePool = arrayList;
		if (!m_kRigidBody.isKinematic)
		{
			m_kRigidBody.AddForce(m_vTotalForce);
			m_kRigidBody.AddTorque(m_vTotalTwist);
		}
		m_vTotalForce += zero;
	}

	public void SortDec<T>(T[] aV, int[] aIndices, int num) where T : IComparable<T>
	{
		for (int i = 0; i < num; i++)
		{
			aIndicesOrg[i] = i;
		}
		for (int num2 = num; num2 > 0; num2--)
		{
			for (int j = 0; j < num2 - 1; j++)
			{
				ref T reference = ref aV[aIndicesOrg[j]];
				T other = aV[aIndicesOrg[j + 1]];
				if (reference.CompareTo(other) < 0)
				{
					int num3 = aIndicesOrg[j];
					aIndicesOrg[j] = aIndicesOrg[j + 1];
					aIndicesOrg[j + 1] = num3;
				}
			}
		}
		for (int k = 0; k < num; k++)
		{
			aIndices[k] = aIndicesOrg[k];
		}
	}
}
