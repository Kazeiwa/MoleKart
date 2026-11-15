using UnityEngine;

public class SelectedRole
{
	private string m_PreAnimClipName = string.Empty;

	public GameObject m_RoleModel;

	private SkinnedMeshRenderer[] m_SkinMeshRenders;

	private MeshRenderer[] m_MehsRenders;

	public Transform m_Parent;

	public Transform transform
	{
		get
		{
			if (null != m_RoleModel)
			{
				return m_RoleModel.transform;
			}
			return null;
		}
	}

	public Vector3 Position
	{
		get
		{
			if (null != m_RoleModel)
			{
				return m_RoleModel.transform.position;
			}
			return default(Vector3);
		}
		set
		{
			if (null != m_RoleModel)
			{
				m_RoleModel.transform.position = value;
			}
		}
	}

	public Quaternion Rotation
	{
		get
		{
			if (null != m_RoleModel)
			{
				return m_RoleModel.transform.rotation;
			}
			return default(Quaternion);
		}
		set
		{
			if (null != m_RoleModel)
			{
				m_RoleModel.transform.rotation = value;
			}
		}
	}

	public Transform Parent
	{
		get
		{
			return m_Parent;
		}
		set
		{
			m_Parent = value;
			if (null != m_RoleModel && null != m_RoleModel.GetComponent<RoleLogic>())
			{
				m_RoleModel.GetComponent<RoleLogic>().Parent = value;
			}
		}
	}

	public Animation animation
	{
		get
		{
			if (null != m_RoleModel)
			{
				return m_RoleModel.animation;
			}
			return null;
		}
	}

	public bool Enabled
	{
		set
		{
			if (!(null != m_RoleModel))
			{
				return;
			}
			if (m_SkinMeshRenders != null)
			{
				SkinnedMeshRenderer[] skinMeshRenders = m_SkinMeshRenders;
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinMeshRenders)
				{
					skinnedMeshRenderer.enabled = value;
				}
			}
			if (m_MehsRenders != null)
			{
				MeshRenderer[] mehsRenders = m_MehsRenders;
				foreach (MeshRenderer meshRenderer in mehsRenders)
				{
					meshRenderer.enabled = value;
				}
			}
			MeshRenderer[] componentsInChildren = m_RoleModel.transform.FindChild("Bip root").GetComponentsInChildren<MeshRenderer>();
			if (componentsInChildren != null)
			{
				for (int k = 0; k < componentsInChildren.Length; k++)
				{
					componentsInChildren[k].enabled = false;
				}
			}
		}
	}

	public SelectedRole(string name, Vector3 pos, Quaternion rotation, Vector3 scale, Object role)
	{
		if (!(null != role))
		{
			return;
		}
		m_RoleModel = (GameObject)Object.Instantiate(role, pos, rotation);
		m_RoleModel.name = name;
		m_RoleModel.transform.localScale = scale;
		m_RoleModel.AddComponent<RoleLogic>();
		m_SkinMeshRenders = m_RoleModel.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
		m_MehsRenders = m_RoleModel.transform.GetComponentsInChildren<MeshRenderer>();
		if (!(m_RoleModel.transform.FindChild("Bip root") != null))
		{
			return;
		}
		MeshRenderer[] componentsInChildren = m_RoleModel.transform.FindChild("Bip root").GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren != null)
		{
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = false;
			}
		}
	}

	public void Uninit()
	{
		Object.DestroyImmediate(m_RoleModel, allowDestroyingAssets: true);
		m_RoleModel = null;
		m_Parent = null;
		m_MehsRenders = null;
		m_SkinMeshRenders = null;
	}

	public void Play(string name)
	{
		if (null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(name) && !name.Equals(m_PreAnimClipName))
		{
			AnimationState animationState = m_RoleModel.animation[name];
			if (null != animationState)
			{
				animationState.wrapMode = WrapMode.Once;
				animationState.speed = 1f;
			}
			m_RoleModel.animation.Play(name);
			m_PreAnimClipName = name;
		}
	}

	public void PlayRevert()
	{
		if (!string.IsNullOrEmpty(m_PreAnimClipName) && null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(m_PreAnimClipName))
		{
			AnimationState animationState = m_RoleModel.animation[m_PreAnimClipName];
			if (null != animationState)
			{
				animationState.wrapMode = WrapMode.Once;
				animationState.speed = -1f;
				animationState.time = animationState.length;
			}
			m_RoleModel.animation.Play(m_PreAnimClipName);
			m_PreAnimClipName = string.Empty;
		}
	}
}
