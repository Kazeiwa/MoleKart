using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Misc;
using UnityEngine;

public class PropManager : TSingleton<PropManager>
{
	private static Dictionary<ePropType, GameObject> m_PropMap;

	private ArrayList m_PropArray = new ArrayList();

	private ArrayList m_MissileArray = new ArrayList();

	private ArrayList m_BombEffectArray = new ArrayList();

	private ArrayList m_ThunderCloud = new ArrayList();

	private ArrayList m_FlyingProps = new ArrayList();

	private ArrayList m_FireSphere = new ArrayList();

	private Dictionary<string, ArrayList> m_FollowBananas = new Dictionary<string, ArrayList>();

	private Dictionary<string, ArrayList> m_RotateTortoise = new Dictionary<string, ArrayList>();

	private ArrayList m_PropBoxArray = new ArrayList();

	private static string xml_path = "Config/xmlConfig/PropConfig";

	private float[] m_Cos = new float[3] { 2f, -1f, -1f };

	private float[] m_Sin = new float[3] { 0f, 1.732f, -1.732f };

	public ArrayList PropBoxes => m_PropBoxArray;

	public ArrayList Porps => m_PropArray;

	private bool Parse()
	{
		if (m_PropMap == null || m_PropMap.Count <= 0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			UnityEngine.Object obj = Resources.Load(xml_path);
			if (null != obj)
			{
				TextAsset textAsset = (TextAsset)obj;
				xmlDocument.LoadXml(textAsset.text);
				return ParseXml(xmlDocument);
			}
			return false;
		}
		return true;
	}

	private bool ParseXml(XmlDocument xml)
	{
		if (xml == null)
		{
			return false;
		}
		XmlNode xmlNode = xml.SelectSingleNode("Root");
		if (xmlNode != null)
		{
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Prop");
			if (xmlNodeList != null)
			{
				foreach (XmlNode item in xmlNodeList)
				{
					if (item != null)
					{
						XmlNode xmlNode3 = item.SelectSingleNode("Path");
						XmlNode xmlNode4 = item.SelectSingleNode("ID");
						if (xmlNode4 == null || xmlNode3 == null)
						{
							Debug.LogError("PropConfig is error, please correct");
							return false;
						}
						UnityEngine.Object obj = Resources.Load(xmlNode3.InnerText);
						if (null != obj)
						{
							m_PropMap.Add((ePropType)int.Parse(xmlNode4.InnerText), (GameObject)obj);
						}
						else
						{
							Debug.LogError(xmlNode3.InnerText + " not exist");
						}
					}
				}
				Resources.UnloadUnusedAssets();
				return true;
			}
			Debug.LogError("Have no Prop node in PropConfig.xml file");
			return false;
		}
		Debug.LogError("Root node is null in PropConfig.xml file");
		return false;
	}

	public void Init()
	{
		PropIDManager.Instance.ResetID();
		Debug.Log("PropManager:Init()");
		m_PropMap = new Dictionary<ePropType, GameObject>();
		if (!Parse())
		{
			Debug.LogError("PropManager.Init is error, please check");
		}
	}

	public void UnInit()
	{
		Debug.Log("PropManager:UnInit()");
		m_PropMap.Clear();
		m_PropMap = null;
		Resources.UnloadUnusedAssets();
	}

	public void DestrotyPropBox()
	{
		for (int i = 0; i < m_PropBoxArray.Count; i++)
		{
			if (m_PropBoxArray[i] != null)
			{
				UnityEngine.Object.DestroyImmediate((m_PropBoxArray[i] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_PropBoxArray.Clear();
	}

	public void SetScenePropBoxes(string path, GameObject obj)
	{
		if (null == obj || string.IsNullOrEmpty(path))
		{
			return;
		}
		ArrayList pointsDatas = GetPointsDatas(path);
		if (pointsDatas != null && pointsDatas.Count > 0)
		{
			for (int i = 0; i < pointsDatas.Count; i++)
			{
				SetBox(obj.transform, (Vector3)pointsDatas[i]);
			}
		}
	}

	private ArrayList GetPointsDatas(string name)
	{
		ArrayList arrayList = new ArrayList();
		TextAsset textAsset = Resources.Load(name) as TextAsset;
		if (null == textAsset)
		{
			return null;
		}
		Stream stream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(stream);
		if (streamReader != null)
		{
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				if (!string.IsNullOrEmpty(text))
				{
					char[] separator = new char[1] { '\t' };
					string[] array = text.Split(separator);
					if (array != null)
					{
						Vector3 vector = new Vector3(Convert.ToSingle(array[0].Replace(" ", string.Empty)), Convert.ToSingle(array[1].Replace(" ", string.Empty)), Convert.ToSingle(array[2].Replace(" ", string.Empty)));
						arrayList.Add(vector);
					}
				}
			}
		}
		return arrayList;
	}

	public void SetBox(Transform parent, Vector3 pos)
	{
		if (!m_PropMap.ContainsKey(ePropType.BOX) || !(null != m_PropMap[ePropType.BOX]))
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(m_PropMap[ePropType.BOX]) as GameObject;
		if (null != gameObject)
		{
			Prop component = gameObject.GetComponent<Prop>();
			if (component == null)
			{
				component = gameObject.AddComponent<Prop>();
				m_PropBoxArray.Add(component);
			}
			gameObject.transform.parent = parent;
			gameObject.transform.position = pos;
		}
	}

	public IEnumerable<Prop> PropBoxesWeakNear(Vector2 position, float weakDistance)
	{
		foreach (Prop box in PropBoxes)
		{
			if (MathfEx.WeakDistance(box.Pos, position) <= weakDistance)
			{
				yield return box;
			}
		}
	}

	public void FireSphere(out Prop kcomponent)
	{
		kcomponent = null;
		if (!m_PropMap.ContainsKey(ePropType.SPHERE) || !(null != m_PropMap[ePropType.SPHERE]))
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(m_PropMap[ePropType.SPHERE]) as GameObject;
		if (null != gameObject)
		{
			FSphere fSphere = gameObject.GetComponent<FSphere>();
			if (fSphere == null)
			{
				fSphere = gameObject.AddComponent<FSphere>();
			}
			AddFireSphere(fSphere);
			fSphere.IncrementUuid();
			gameObject.name = "Sphere" + PropIDManager.Instance.GetID(null);
			kcomponent = fSphere;
		}
	}

	public GameObject UseProp(Car car, ePropType eType, out Prop kcomponent)
	{
		kcomponent = null;
		if (m_PropMap.ContainsKey(eType) && null != m_PropMap[eType])
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_PropMap[eType]) as GameObject;
			switch (eType)
			{
			case ePropType.BANANA:
			{
				Banana banana = gameObject.GetComponent<Banana>();
				if (banana == null)
				{
					banana = gameObject.AddComponent<Banana>();
				}
				banana.IncrementUuid();
				gameObject.name = "banana" + PropIDManager.Instance.GetID(car);
				kcomponent = banana;
				AddBanana(banana);
				break;
			}
			case ePropType.TORTOISE_RED:
			{
				Tortoise tortoise3 = gameObject.GetComponent<TortoiseRed>();
				if (null == tortoise3)
				{
					tortoise3 = gameObject.AddComponent<TortoiseRed>();
				}
				tortoise3.IncrementUuid();
				tortoise3.name = "tortoise" + PropIDManager.Instance.GetID(car);
				kcomponent = tortoise3;
				AddTortoise(tortoise3);
				AddFlyingProp(gameObject);
				break;
			}
			case ePropType.TORTOISE_GREEN:
			{
				Tortoise tortoise2 = gameObject.GetComponent<TortoiseGreen>();
				if (null == tortoise2)
				{
					tortoise2 = gameObject.AddComponent<TortoiseGreen>();
				}
				tortoise2.IncrementUuid();
				tortoise2.name = "tortoise" + PropIDManager.Instance.GetID(car);
				kcomponent = tortoise2;
				AddTortoise(tortoise2);
				AddFlyingProp(gameObject);
				break;
			}
			case ePropType.TORTOISE_BLUE:
			{
				Tortoise tortoise = gameObject.GetComponent<TortoiseBlue>();
				if (null == tortoise)
				{
					tortoise = gameObject.AddComponent<TortoiseBlue>();
				}
				tortoise.IncrementUuid();
				tortoise.name = "tortoise" + PropIDManager.Instance.GetID(car);
				kcomponent = tortoise;
				AddTortoise(tortoise);
				AddFlyingProp(gameObject);
				break;
			}
			case ePropType.MISSILE:
			{
				Missile missile = gameObject.GetComponent<Missile>();
				if (null == missile)
				{
					missile = gameObject.AddComponent<Missile>();
				}
				missile.IncrementUuid();
				missile.name = "missile" + PropIDManager.Instance.GetID(car);
				kcomponent = missile;
				AddMissile(missile);
				AddFlyingProp(gameObject);
				break;
			}
			case ePropType.BOMB:
			{
				Bomb bomb = gameObject.GetComponent<Bomb>();
				if (null == bomb)
				{
					bomb = gameObject.AddComponent<Bomb>();
				}
				bomb.IncrementUuid();
				bomb.name = "bomb" + PropIDManager.Instance.GetID(car);
				kcomponent = bomb;
				AddBomb(bomb);
				break;
			}
			case ePropType.BOMB_EFFECT:
			{
				BombEffect bombEffect = gameObject.GetComponent<BombEffect>();
				if (null == bombEffect)
				{
					bombEffect = gameObject.AddComponent<BombEffect>();
				}
				bombEffect.IncrementUuid();
				bombEffect.name = "bombEffect" + PropIDManager.Instance.GetID(car);
				kcomponent = bombEffect;
				AddBombEffect(bombEffect);
				break;
			}
			case ePropType.BLUE_TORTOISE_BOMB_EFFECT:
			{
				BombEffect bombEffect2 = gameObject.GetComponent<BombEffect>();
				if (null == bombEffect2)
				{
					bombEffect2 = gameObject.AddComponent<BombEffect>();
				}
				bombEffect2.IncrementUuid();
				bombEffect2.name = "bombEffect" + PropIDManager.Instance.GetID(car);
				kcomponent = bombEffect2;
				AddBombEffect(bombEffect2);
				break;
			}
			case ePropType.CHEAT_BOX:
			{
				CheatBox cheatBox = gameObject.GetComponent<CheatBox>();
				if (null == cheatBox)
				{
					cheatBox = gameObject.AddComponent<CheatBox>();
				}
				cheatBox.IncrementUuid();
				cheatBox.name = "CheatBox" + PropIDManager.Instance.GetID(car);
				kcomponent = cheatBox;
				AddCheatBox(cheatBox);
				break;
			}
			case ePropType.THUNDER_CLOUD:
			{
				ThunderCloud thunderCloud = gameObject.GetComponent<ThunderCloud>();
				if (null == thunderCloud)
				{
					thunderCloud = gameObject.AddComponent<ThunderCloud>();
				}
				thunderCloud.IncrementUuid();
				thunderCloud.name = "thunder" + PropIDManager.Instance.GetID(car);
				kcomponent = thunderCloud;
				AddThunderCloud(kcomponent as ThunderCloud);
				break;
			}
			case ePropType.MUSHROOM_ONE:
			{
				Mushroom mushroom = gameObject.GetComponent<Mushroom>();
				if (null == mushroom)
				{
					mushroom = gameObject.AddComponent<Mushroom>();
				}
				mushroom.IncrementUuid();
				mushroom.name = "mushroom" + PropIDManager.Instance.GetID(car);
				kcomponent = mushroom;
				break;
			}
			}
			return gameObject;
		}
		return null;
	}

	public void AddFlyingProp(GameObject obj)
	{
		m_FlyingProps.Add(obj);
	}

	public void RemoveFlyingProp(GameObject obj)
	{
		m_FlyingProps.Remove(obj);
	}

	public void EquipBanana(Car followCar, int nums)
	{
		if (!(null != followCar) || Equiped(followCar.name) > ePropType.MISSILE)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < nums; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_PropMap[ePropType.BANANA]) as GameObject;
			if (null != gameObject)
			{
				Banana banana = gameObject.GetComponent<Banana>();
				if (null == banana)
				{
					banana = gameObject.AddComponent<Banana>();
					banana.IncrementUuid();
				}
				banana.m_FollowCar = followCar;
				banana.m_FollowDis = 1.5f + (float)i * 0.5f;
				banana.FollowOrder = i;
				AddBanana(banana);
				arrayList.Add(banana);
				if (followCar.isHero)
				{
					gameObject.name = "banana" + PropIDManager.Instance.GetID(followCar).ToString() + "_" + i;
				}
				else
				{
					gameObject.name = "banana" + PropIDManager.Instance.m_OtherCarUsePropID.ToString() + "_" + i;
				}
			}
		}
		if (m_FollowBananas.ContainsKey(followCar.name))
		{
			m_FollowBananas[followCar.name] = arrayList;
		}
		else
		{
			m_FollowBananas.Add(followCar.name, arrayList);
		}
	}

	public void EquipTortoise(Car surroundCar, int nums, ePropType flag)
	{
		if (!(null != surroundCar) || Equiped(surroundCar.name) > ePropType.MISSILE)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < nums; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_PropMap[flag]) as GameObject;
			if (null != gameObject)
			{
				Tortoise tortoise = gameObject.GetComponent<Tortoise>();
				if (null == tortoise)
				{
					tortoise = ((flag != ePropType.TORTOISE_RED) ? ((Tortoise)gameObject.AddComponent<TortoiseGreen>()) : ((Tortoise)gameObject.AddComponent<TortoiseRed>()));
					tortoise.IncrementUuid();
				}
				tortoise.SurroundCar = surroundCar;
				tortoise.gameObject.transform.localPosition = new Vector3(m_Cos[i], 0.5f, m_Sin[i]);
				AddTortoise(tortoise);
				arrayList.Add(tortoise);
				if (surroundCar.isHero)
				{
					gameObject.name = "tortoise" + PropIDManager.Instance.GetID(surroundCar).ToString() + "_" + i;
				}
				else
				{
					gameObject.name = "tortoise" + PropIDManager.Instance.m_OtherCarUsePropID.ToString() + "_" + i;
				}
			}
		}
		if (m_RotateTortoise.ContainsKey(surroundCar.name))
		{
			m_RotateTortoise[surroundCar.name] = arrayList;
		}
		else
		{
			m_RotateTortoise.Add(surroundCar.name, arrayList);
		}
		surroundCar.PlaySound(51301, 1);
	}

	public Banana ThrowBanana(string name)
	{
		if (m_FollowBananas != null && m_FollowBananas.ContainsKey(name))
		{
			int count = m_FollowBananas[name].Count;
			if (count <= 0)
			{
				return null;
			}
			Banana banana = (Banana)m_FollowBananas[name][count - 1];
			if (null != banana)
			{
				banana.m_FollowCar = null;
			}
			m_FollowBananas[name].Remove(banana);
			return banana;
		}
		return null;
	}

	public Tortoise ThrowTortoise(Car car, string name)
	{
		if (m_RotateTortoise != null && m_RotateTortoise.ContainsKey(name))
		{
			int count = m_RotateTortoise[name].Count;
			if (count <= 0)
			{
				return null;
			}
			Tortoise tortoise = (Tortoise)m_RotateTortoise[name][count - 1];
			if (null != tortoise)
			{
				tortoise.SurroundCar = null;
				AddFlyingProp(tortoise.gameObject);
			}
			m_RotateTortoise[name].Remove(tortoise);
			if (null == tortoise)
			{
				return null;
			}
			if (m_RotateTortoise[name].Count <= 0)
			{
				car.StopSound(51301);
			}
			return tortoise;
		}
		return null;
	}

	public ePropType Equiped(string name)
	{
		if (m_FollowBananas.ContainsKey(name) && m_FollowBananas[name].Count > 0)
		{
			return ePropType.BANANA;
		}
		if (m_RotateTortoise.ContainsKey(name) && m_RotateTortoise[name].Count > 0)
		{
			return (m_RotateTortoise[name][0] as Tortoise).m_Type;
		}
		return ePropType.NONE;
	}

	public ArrayList GetFlyingProps()
	{
		return m_FlyingProps;
	}

	public void Release(ePropType eType, Prop kcomponent)
	{
		switch (eType)
		{
		case ePropType.BANANA:
			RemoveEquipedBanana(kcomponent);
			break;
		case ePropType.TORTOISE:
			RemoveEquipedTortoise(kcomponent);
			break;
		case ePropType.MISSILE:
			RemoveMissile(kcomponent as Missile);
			break;
		case ePropType.BOMB:
			RemoveBomb(kcomponent);
			break;
		case ePropType.BOMB_EFFECT:
			RemoveBombEffect(kcomponent as BombEffect);
			break;
		case ePropType.CHEAT_BOX:
			RemoveCheatBox(kcomponent);
			break;
		case ePropType.THUNDER_CLOUD:
			RemoveThunderCloud(kcomponent as ThunderCloud);
			break;
		case ePropType.SPHERE:
			RemoveFireSphere(kcomponent as FSphere);
			break;
		}
	}

	private void AddBanana(Prop kBanana)
	{
		if (!m_PropArray.Contains(kBanana))
		{
			m_PropArray.Add(kBanana);
		}
	}

	private void AddTortoise(Prop tortoise)
	{
		if (!m_PropArray.Contains(tortoise))
		{
			m_PropArray.Add(tortoise);
		}
	}

	private void AddMissile(Missile missile)
	{
		if (!m_MissileArray.Contains(missile))
		{
			m_MissileArray.Add(missile);
		}
	}

	private void AddBomb(Prop bomb)
	{
		if (!m_PropArray.Contains(bomb))
		{
			m_PropArray.Add(bomb);
		}
	}

	private void AddBombEffect(BombEffect effect)
	{
		if (!m_BombEffectArray.Contains(effect))
		{
			m_BombEffectArray.Add(effect);
		}
	}

	private void AddCheatBox(Prop box)
	{
		if (!m_PropArray.Contains(box))
		{
			m_PropArray.Add(box);
		}
	}

	private void AddThunderCloud(ThunderCloud thunder)
	{
		if (!m_ThunderCloud.Contains(thunder))
		{
			m_ThunderCloud.Add(thunder);
		}
	}

	private void AddFireSphere(FSphere sphere)
	{
		if (!m_FireSphere.Contains(sphere))
		{
			m_FireSphere.Add(sphere);
		}
	}

	private void RemoveEquipedBanana(Prop kBanana)
	{
		m_PropArray.Remove(kBanana);
		if (null != kBanana && null != (kBanana as Banana).m_FollowCar && m_FollowBananas.ContainsKey((kBanana as Banana).m_FollowCar.name))
		{
			m_FollowBananas[(kBanana as Banana).m_FollowCar.name].Remove(kBanana as Banana);
		}
	}

	private void RemoveEquipedTortoise(Prop tortoise)
	{
		m_PropArray.Remove(tortoise);
		if (null != (tortoise as Tortoise).SurroundCar && m_RotateTortoise.ContainsKey((tortoise as Tortoise).SurroundCar.name))
		{
			m_RotateTortoise[(tortoise as Tortoise).SurroundCar.name].Remove(tortoise as Tortoise);
		}
	}

	private void RemoveMissile(Missile missile)
	{
		m_MissileArray.Remove(missile);
	}

	private void RemoveBomb(Prop bomb)
	{
		m_PropArray.Remove(bomb);
	}

	private void RemoveBombEffect(BombEffect effect)
	{
		m_BombEffectArray.Remove(effect);
	}

	private void RemoveCheatBox(Prop box)
	{
		m_PropArray.Remove(box);
	}

	private void RemoveThunderCloud(ThunderCloud thunder)
	{
		m_ThunderCloud.Remove(thunder);
	}

	private void RemoveFireSphere(FSphere sphere)
	{
		m_FireSphere.Remove(sphere);
	}

	public void LoseBanana(Car car, string name)
	{
		if (m_FollowBananas == null || !m_FollowBananas.ContainsKey(name))
		{
			return;
		}
		for (int num = 0; num < m_FollowBananas[name].Count; num = 0)
		{
			for (int i = 0; i < m_PropArray.Count; i++)
			{
				if (m_PropArray[i] == m_FollowBananas[name][num])
				{
					m_PropArray.RemoveAt(i);
					break;
				}
			}
			((Banana)m_FollowBananas[name][num]).SetHitTarget(bHit: true);
			m_FollowBananas[name].RemoveAt(num);
		}
	}

	public void LoseTortoise(Car car, string name)
	{
		if (m_RotateTortoise == null || !m_RotateTortoise.ContainsKey(name))
		{
			return;
		}
		for (int num = 0; num < m_RotateTortoise[name].Count; num = 0)
		{
			for (int i = 0; i < m_PropArray.Count; i++)
			{
				if (m_PropArray[i] == m_RotateTortoise[name][num])
				{
					m_PropArray.RemoveAt(i);
					break;
				}
			}
			((Tortoise)m_RotateTortoise[name][num]).SetHitTarget(bHit: true);
			m_RotateTortoise[name].RemoveAt(num);
		}
		car.StopSound(51301);
	}

	public ArrayList Tortoises(string name)
	{
		if (m_RotateTortoise.ContainsKey(name))
		{
			return m_RotateTortoise[name];
		}
		return null;
	}

	public ArrayList Bananas(string name)
	{
		if (m_FollowBananas.ContainsKey(name))
		{
			return m_FollowBananas[name];
		}
		return null;
	}

	public void Clear()
	{
		for (int i = 0; i < m_PropBoxArray.Count; i++)
		{
			if (m_PropBoxArray[i] != null)
			{
				UnityEngine.Object.DestroyImmediate((m_PropBoxArray[i] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_PropBoxArray.Clear();
		for (int j = 0; j < m_RotateTortoise.Count; j++)
		{
			foreach (KeyValuePair<string, ArrayList> item in m_RotateTortoise)
			{
				if (item.Value == null)
				{
					continue;
				}
				for (int k = 0; k < item.Value.Count; k++)
				{
					if (item.Value[k] != null && null != item.Value[k] as Prop)
					{
						UnityEngine.Object.DestroyImmediate((item.Value[k] as Prop).gameObject, allowDestroyingAssets: true);
					}
				}
			}
		}
		m_RotateTortoise.Clear();
		for (int l = 0; l < m_FollowBananas.Count; l++)
		{
			foreach (KeyValuePair<string, ArrayList> followBanana in m_FollowBananas)
			{
				if (followBanana.Value == null)
				{
					continue;
				}
				for (int m = 0; m < followBanana.Value.Count; m++)
				{
					if (followBanana.Value[m] != null && null != followBanana.Value[m] as Prop)
					{
						UnityEngine.Object.DestroyImmediate((followBanana.Value[m] as Prop).gameObject, allowDestroyingAssets: true);
					}
				}
			}
		}
		m_FollowBananas.Clear();
		for (int n = 0; n < m_FlyingProps.Count; n++)
		{
			if (m_FlyingProps[n] != null && null != m_FlyingProps[n] as Prop)
			{
				UnityEngine.Object.DestroyImmediate((m_FlyingProps[n] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_FlyingProps.Clear();
		for (int num = 0; num < m_PropArray.Count; num++)
		{
			if (m_PropArray[num] != null && null != m_PropArray[num] as Prop)
			{
				UnityEngine.Object.DestroyImmediate((m_PropArray[num] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_PropArray.Clear();
		for (int num2 = 0; num2 < m_MissileArray.Count; num2++)
		{
			if (m_MissileArray[num2] != null && null != m_MissileArray[num2] as Prop)
			{
				UnityEngine.Object.DestroyImmediate((m_MissileArray[num2] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_MissileArray.Clear();
		for (int num3 = 0; num3 < m_BombEffectArray.Count; num3++)
		{
			if (m_BombEffectArray[num3] != null && null != m_BombEffectArray[num3] as Prop)
			{
				UnityEngine.Object.DestroyImmediate((m_BombEffectArray[num3] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_BombEffectArray.Clear();
		for (int num4 = 0; num4 < m_ThunderCloud.Count; num4++)
		{
			if (m_ThunderCloud[num4] != null && null != m_ThunderCloud[num4] as Prop)
			{
				UnityEngine.Object.DestroyImmediate((m_ThunderCloud[num4] as Prop).gameObject, allowDestroyingAssets: true);
			}
		}
		m_ThunderCloud.Clear();
	}

	public void DestroyProp(ePropType type, string guid)
	{
		if (m_PropArray == null)
		{
			return;
		}
		for (int i = 0; i < m_PropArray.Count; i++)
		{
			Prop prop = m_PropArray[i] as Prop;
			if (null != prop && null != prop.gameObject && prop.gameObject.name.Equals(guid))
			{
				prop.SetHitTarget(bHit: true);
			}
		}
	}
}
