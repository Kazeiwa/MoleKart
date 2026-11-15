using System.Collections;
using UnityEngine;

public class SkillManager
{
	private const float RETARD_TIME = 3f;

	private const float BOOST_DURATION = 3f;

	private const float BOOST_RATIO = 0.3f;

	private const float SHIELD_DURATION = 5f;

	private static SkillManager instance;

	private ArrayList instanceList;

	private GameObject m_MainCamera;

	public static SkillManager Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private SkillManager()
	{
	}

	public void Init()
	{
	}

	public bool HasProp(Car car)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UseProp(Car car, bool throwForward)
	{
	}

	public void FireSphere(Vector3 fromPos, Vector3 destPos, float speed, float time)
	{
	}

	private bool GetThrowAtPos(Transform transform, ref Vector3 pos, ref Vector3 dir, float delta)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void DropProp(Car car, ePropType type)
	{
	}

	private void Discharge(ePropType type, Car car, Vector3 vDir, Vector3 vSpeed)
	{
	}

	private void LaunchMissile(Car car)
	{
	}

	private void DropBanana(Car car, Vector3 pos, Vector3 vDir, float speed)
	{
	}

	private bool ThrowBanana(Car car, Vector3 vDir, Vector3 vSpeed, bool fromCarBack)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DropTortoise(Car car, ePropType type, Vector3 pos, Vector3 dir, float speed)
	{
	}

	private bool ThrowTortoise(Car car, Vector3 vDir, Vector3 vSpeed, ePropType type, bool fromCarSurround)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DropMushroom(Car car, Vector3 pos, Vector3 dir, float speed)
	{
	}

	private void UseMushroom(Car car, bool golden)
	{
	}

	private void UseLuckyStar(Car car)
	{
	}

	private void UseBomb(Car car, Vector3 direction, Vector3 speed)
	{
	}

	private void UseThunderCloud(Car car)
	{
	}

	private void UseLightning(Car car)
	{
	}

	private void UseEarthQuake(Car car)
	{
	}

	private void UseBigMushroom(Car car)
	{
	}

	private void UseCuttleFish(Car car)
	{
	}

	public void LightningSelf(Car car)
	{
	}

	private void UseCheatBox(Car car, Vector3 direction, Vector3 speed)
	{
	}

	private void DropCheatBox(Car car, Vector3 pos, Vector3 dir, float speed)
	{
	}

	private void UseBlueTortoise(Car car, Vector3 vDir, Vector3 vSpeed)
	{
	}
}
