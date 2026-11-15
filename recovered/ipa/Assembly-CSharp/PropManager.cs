using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using UnityEngine;

public class PropManager : TSingleton<PropManager>
{
	private static Dictionary<ePropType, GameObject> m_PropMap;

	private static string xml_path;

	private List<Prop> m_PropArray;

	private List<Prop> m_MissileArray;

	private List<Prop> m_BombEffectArray;

	private List<Prop> m_ThunderCloud;

	private List<Prop> m_FlyingProps;

	private List<Prop> m_FireSphere;

	private Dictionary<string, List<Prop>> m_FollowBananas;

	private Dictionary<string, List<Prop>> m_RotateTortoise;

	private List<Prop> m_PropBoxArray;

	private float[] m_Cos;

	private float[] m_Sin;

	private Dictionary<ePropType, PropInstanceList> m_CachedProps;

	public IEnumerable<Prop> PropBoxes
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<Prop> Props
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Init()
	{
	}

	public void UnInit()
	{
	}

	public void DestrotyPropBox()
	{
	}

	public void SetScenePropBoxes(string path, GameObject obj)
	{
	}

	public void SetBox(Transform parent, Vector3 pos)
	{
	}

	[DebuggerHidden]
	public IEnumerable<Prop> PropBoxesWeakNear(Vector2 position, float weakDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<Prop> GetPropBoxes()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void FireSphere(out Prop kcomponent)
	{
	}

	public GameObject GetPropInstance(ePropType eType)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DestroyPropInstance(ePropType eType, GameObject obj)
	{
	}

	public GameObject UseProp(Car car, ePropType eType, out Prop kcomponent)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void EquipBanana(Car followCar, int nums)
	{
	}

	public void EquipTortoise(Car surroundCar, int nums, ePropType flag)
	{
	}

	public Banana ThrowBanana(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Tortoise ThrowTortoise(Car car, string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public ePropType Equiped(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IEnumerable<Prop> GetFlyingProps()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Release(ePropType eType, Prop kcomponent)
	{
	}

	public void LoseBanana(Car car, string name)
	{
	}

	public void LoseTortoise(Car car, string name)
	{
	}

	public IList<Prop> Tortoises(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IEnumerable<Prop> Bananas(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Clear()
	{
	}

	public void DestroyProp(ePropType type, string guid)
	{
	}

	private void AddBanana(Prop banana)
	{
	}

	private void AddTortoise(Prop tortoise)
	{
	}

	private void AddMissile(Missile missile)
	{
	}

	private void AddBomb(Prop bomb)
	{
	}

	private void AddBombEffect(Prop effect)
	{
	}

	private void AddCheatBox(Prop box)
	{
	}

	private void AddThunderCloud(Prop cloud)
	{
	}

	private void AddFireSphere(FSphere sphere)
	{
	}

	private void RemoveEquipedBanana(Prop banana)
	{
	}

	private void RemoveEquipedTortoise(Prop tortoise)
	{
	}

	private void RemoveMissile(Prop missile)
	{
	}

	private void RemoveBomb(Prop bomb)
	{
	}

	private void RemoveBombEffect(Prop effect)
	{
	}

	private void RemoveCheatBox(Prop box)
	{
	}

	private void RemoveThunderCloud(ThunderCloud thunder)
	{
	}

	private void RemoveFireSphere(FSphere sphere)
	{
	}

	private void AddFlyingProp(Prop prop)
	{
	}

	private void RemoveFlyingProp(Prop prop)
	{
	}

	private bool Parse()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseXml(XmlDocument xml)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private ArrayList GetPointsDatas(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
