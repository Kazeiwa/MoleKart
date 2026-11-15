using System.Collections.Generic;
using UnityEngine;

public class Role
{
	private Dictionary<string, int> m_SoundMap = new Dictionary<string, int>();

	private string m_PreAnimClipName = string.Empty;

	public GameObject m_RoleModel;

	private SkinnedMeshRenderer[] m_SkinMeshRenders;

	private MeshRenderer[] m_MehsRenders;

	public Transform m_Parent;

	public bool m_CurrentFlag;

	public string m_CurrentName;

	private RoleType m_RoleType;

	public static string THROW_PROP = "throw_prop";

	public static string SCREAM_LOW = "scream_low";

	public static string SCREAM_HIGHT = "scream_high";

	public static string SUCCESS = "success";

	public static string FAIL = "fail";

	public static string SELECT = "select";

	public static string HAPPY_ONE = "happy_one";

	public static string HAPPY_TWO = "happy_two";

	public static string HAPPY_THREE = "happy_three";

	public static string SAY_HI = "say_hi";

	private Dictionary<int, AudioSource> m_Audios = new Dictionary<int, AudioSource>();

	private static float m_LastPlaySound;

	public Car m_Car;

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

	public int RoleID => (int)m_RoleType;

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
			if (null != m_RoleModel)
			{
				if (null != m_RoleModel.GetComponent<RoleLogic>())
				{
					m_RoleModel.GetComponent<RoleLogic>().Parent = value;
				}
				else if (null != m_RoleModel.GetComponent<NpcLogic>())
				{
					m_RoleModel.GetComponent<NpcLogic>().Parent = value;
				}
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
			if (null != m_RoleModel)
			{
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
			}
			if (!(m_RoleModel.transform.FindChild("Bip root") != null))
			{
				return;
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

	public Role(string name, Vector3 pos, Quaternion rotation, Object role, RoleType type, bool isNpc)
	{
		if (null != role)
		{
			m_RoleType = type;
			m_RoleModel = (GameObject)Object.Instantiate(role, pos, rotation);
			m_RoleModel.name = name;
			if (isNpc)
			{
				m_RoleModel.AddComponent<NpcLogic>();
			}
			else
			{
				m_RoleModel.AddComponent<RoleLogic>();
			}
			m_SkinMeshRenders = m_RoleModel.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
			m_MehsRenders = m_RoleModel.transform.GetComponentsInChildren<MeshRenderer>();
			InitSoundMap(type);
		}
	}

	public Role(string name, Vector3 pos, Quaternion rotation, Object skeleton, Object skin, RoleType type, bool isNpc)
	{
		if (null != skeleton)
		{
			m_RoleType = type;
			m_RoleModel = (GameObject)Object.Instantiate(skeleton, pos, rotation);
			m_RoleModel.name = name;
			ReplaceRolePart(skin);
			if (isNpc)
			{
				m_RoleModel.AddComponent<NpcLogic>();
			}
			else
			{
				m_RoleModel.AddComponent<RoleLogic>();
			}
			if (null != m_RoleModel.animation)
			{
				m_RoleModel.animation.Stop(m_RoleModel.animation.clip.name);
			}
			m_SkinMeshRenders = m_RoleModel.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
			m_MehsRenders = m_RoleModel.transform.GetComponentsInChildren<MeshRenderer>();
			InitSoundMap(type);
		}
	}

	private void InitSoundMap(RoleType type)
	{
		switch (type)
		{
		case RoleType.EBear:
			m_SoundMap.Add(SELECT, 30301);
			m_SoundMap.Add(SCREAM_LOW, 30307);
			m_SoundMap.Add(SCREAM_HIGHT, 30302);
			m_SoundMap.Add(SUCCESS, 30304);
			m_SoundMap.Add(FAIL, 30310);
			m_SoundMap.Add(HAPPY_ONE, 30306);
			m_SoundMap.Add(HAPPY_TWO, 30308);
			m_SoundMap.Add(HAPPY_THREE, 30308);
			m_SoundMap.Add(THROW_PROP, 30309);
			m_SoundMap.Add(SAY_HI, 30308);
			break;
		case RoleType.EKuLa:
			m_SoundMap.Add(SELECT, 30601);
			m_SoundMap.Add(SCREAM_LOW, 30602);
			m_SoundMap.Add(SCREAM_HIGHT, 30605);
			m_SoundMap.Add(SUCCESS, 30610);
			m_SoundMap.Add(FAIL, 30603);
			m_SoundMap.Add(HAPPY_ONE, 30606);
			m_SoundMap.Add(HAPPY_TWO, 30607);
			m_SoundMap.Add(HAPPY_THREE, 30608);
			m_SoundMap.Add(THROW_PROP, 30609);
			m_SoundMap.Add(SAY_HI, 30604);
			break;
		case RoleType.EMmt:
			m_SoundMap.Add(SELECT, 30405);
			m_SoundMap.Add(SCREAM_LOW, 30402);
			m_SoundMap.Add(SCREAM_HIGHT, 30403);
			m_SoundMap.Add(SUCCESS, 30404);
			m_SoundMap.Add(FAIL, 30410);
			m_SoundMap.Add(HAPPY_ONE, 30406);
			m_SoundMap.Add(HAPPY_TWO, 30407);
			m_SoundMap.Add(HAPPY_THREE, 30408);
			m_SoundMap.Add(THROW_PROP, 30409);
			m_SoundMap.Add(SAY_HI, 30407);
			break;
		case RoleType.EMolele:
			m_SoundMap.Add(SELECT, 30501);
			m_SoundMap.Add(SCREAM_LOW, 30502);
			m_SoundMap.Add(SCREAM_HIGHT, 30503);
			m_SoundMap.Add(SUCCESS, 30504);
			m_SoundMap.Add(FAIL, 30505);
			m_SoundMap.Add(HAPPY_ONE, 30506);
			m_SoundMap.Add(HAPPY_TWO, 30507);
			m_SoundMap.Add(HAPPY_THREE, 30508);
			m_SoundMap.Add(THROW_PROP, 30509);
			m_SoundMap.Add(SAY_HI, 30510);
			break;
		case RoleType.EMomo:
			m_SoundMap.Add(SELECT, 30101);
			m_SoundMap.Add(SCREAM_LOW, 30102);
			m_SoundMap.Add(SCREAM_HIGHT, 30103);
			m_SoundMap.Add(SUCCESS, 30107);
			m_SoundMap.Add(FAIL, 30105);
			m_SoundMap.Add(HAPPY_ONE, 30106);
			m_SoundMap.Add(HAPPY_TWO, 30106);
			m_SoundMap.Add(HAPPY_THREE, 30104);
			m_SoundMap.Add(THROW_PROP, 30109);
			m_SoundMap.Add(SAY_HI, 30104);
			break;
		case RoleType.ERk:
			m_SoundMap.Add(SELECT, 30201);
			m_SoundMap.Add(SCREAM_LOW, 30202);
			m_SoundMap.Add(SCREAM_HIGHT, 30205);
			m_SoundMap.Add(SUCCESS, 30204);
			m_SoundMap.Add(FAIL, 30205);
			m_SoundMap.Add(HAPPY_ONE, 30206);
			m_SoundMap.Add(HAPPY_TWO, 30207);
			m_SoundMap.Add(HAPPY_THREE, 30208);
			m_SoundMap.Add(THROW_PROP, 30209);
			m_SoundMap.Add(SAY_HI, 30210);
			break;
		case RoleType.EPuti:
			m_SoundMap.Add(SELECT, 30701);
			m_SoundMap.Add(SCREAM_LOW, 30702);
			m_SoundMap.Add(SCREAM_HIGHT, 30703);
			m_SoundMap.Add(SUCCESS, 30704);
			m_SoundMap.Add(FAIL, 30705);
			m_SoundMap.Add(HAPPY_ONE, 30706);
			m_SoundMap.Add(HAPPY_TWO, 30707);
			m_SoundMap.Add(HAPPY_THREE, 30708);
			m_SoundMap.Add(THROW_PROP, 30709);
			m_SoundMap.Add(SAY_HI, 30710);
			break;
		case RoleType.EYali:
			m_SoundMap.Add(SELECT, 30801);
			m_SoundMap.Add(SCREAM_LOW, 30802);
			m_SoundMap.Add(SCREAM_HIGHT, 30803);
			m_SoundMap.Add(SUCCESS, 30804);
			m_SoundMap.Add(FAIL, 30805);
			m_SoundMap.Add(HAPPY_ONE, 30806);
			m_SoundMap.Add(HAPPY_TWO, 30806);
			m_SoundMap.Add(HAPPY_THREE, 30807);
			m_SoundMap.Add(THROW_PROP, 30809);
			m_SoundMap.Add(SAY_HI, 30810);
			break;
		default:
			m_SoundMap.Add(SELECT, 30201);
			m_SoundMap.Add(SCREAM_LOW, 30202);
			m_SoundMap.Add(SCREAM_HIGHT, 30205);
			m_SoundMap.Add(SUCCESS, 30204);
			m_SoundMap.Add(FAIL, 30205);
			m_SoundMap.Add(HAPPY_ONE, 30206);
			m_SoundMap.Add(HAPPY_TWO, 30207);
			m_SoundMap.Add(HAPPY_THREE, 30208);
			m_SoundMap.Add(THROW_PROP, 30209);
			m_SoundMap.Add(SAY_HI, 30210);
			break;
		}
	}

	private void ReplaceRolePart(Object obj)
	{
		if (!(null != obj))
		{
			return;
		}
		GameObject gameObject = new GameObject(obj.name);
		gameObject.transform.parent = m_RoleModel.transform;
		gameObject.AddComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer component = gameObject.GetComponent<SkinnedMeshRenderer>();
		GameObject gameObject2 = (GameObject)Object.Instantiate(obj);
		SkinnedMeshRenderer componentInChildren = gameObject2.GetComponentInChildren<SkinnedMeshRenderer>();
		component.sharedMesh = componentInChildren.sharedMesh;
		component.sharedMaterial = componentInChildren.sharedMaterial;
		Transform[] bones = componentInChildren.bones;
		Transform[] array = new Transform[bones.GetLength(0)];
		Transform[] componentsInChildren = m_RoleModel.GetComponentsInChildren<Transform>();
		for (int i = 0; i < bones.GetLength(0); i++)
		{
			Transform[] array2 = componentsInChildren;
			foreach (Transform transform in array2)
			{
				if (transform.name.Equals(bones[i].name))
				{
					array[i] = transform;
					break;
				}
			}
		}
		component.bones = array;
		Object.Destroy(gameObject2);
	}

	public void SetAniState(string name)
	{
		if (null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(name))
		{
			AnimationState animationState = m_RoleModel.animation[name];
			if (animationState != null)
			{
				animationState.wrapMode = WrapMode.Loop;
			}
		}
	}

	public void Play(string name)
	{
		if (null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(name) && !name.Equals(m_PreAnimClipName))
		{
			m_RoleModel.animation.CrossFade(name, 0.4f);
			m_PreAnimClipName = name;
		}
	}

	public void PlayOther(string name)
	{
		if (null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(name))
		{
			m_RoleModel.animation.CrossFadeQueued(name, 0.3f, QueueMode.PlayNow);
		}
	}

	public void PlayAni(string name, bool flag)
	{
		m_CurrentName = name;
		m_CurrentFlag = flag;
	}

	public void PlayOnly(string name)
	{
		if (null != m_RoleModel && null != m_RoleModel.animation && null != m_RoleModel.animation.GetClip(name))
		{
			AnimationState animationState = m_RoleModel.animation[name];
			if (animationState != null)
			{
				animationState.wrapMode = WrapMode.Loop;
			}
			m_RoleModel.animation.Play(name);
		}
	}

	public void StopAni()
	{
		if (m_CurrentName != null && m_RoleModel != null && m_RoleModel.animation != null)
		{
			m_RoleModel.animation.Stop(m_CurrentName);
		}
		m_CurrentName = null;
		m_CurrentFlag = false;
	}

	public void PlayNext(string name)
	{
		if (m_RoleModel != null && m_RoleModel.animation != null && m_RoleModel.animation.GetClip(name) != null)
		{
			m_RoleModel.animation.Play(name);
		}
	}

	public bool PlaySound(string name)
	{
		if (m_SoundMap.ContainsKey(name))
		{
			return PlaySound(m_SoundMap[name], 1);
		}
		return false;
	}

	public bool PlaySound(int id, int priority)
	{
		if (Time.time - m_LastPlaySound < 2f && null != m_Car && !m_Car.isHero && iPhoneSettings.generation < iPhoneGeneration.iPad2Gen)
		{
			return true;
		}
		m_LastPlaySound = Time.time;
		if (!m_Audios.ContainsKey(id))
		{
			AudioSource audioSource = SoundManager.Instance.AddAudio(m_RoleModel, id);
			if (null != audioSource)
			{
				audioSource.loop = false;
				m_Audios.Add(id, audioSource);
			}
		}
		if (m_Audios.ContainsKey(id) && null != m_Audios[id])
		{
			if (m_Audios[id].audio.isPlaying)
			{
				m_Audios[id].audio.Stop();
			}
			SoundManager.Instance.PlayAudio(m_RoleModel, m_Audios[id], 1);
			return true;
		}
		return false;
	}

	public bool StopSound(int id)
	{
		if (m_Audios.ContainsKey(id))
		{
			SoundManager.Instance.StopAudio(m_Audios[id]);
			return true;
		}
		return false;
	}

	public void SetPreAniName(string name)
	{
		m_PreAnimClipName = name;
	}
}
