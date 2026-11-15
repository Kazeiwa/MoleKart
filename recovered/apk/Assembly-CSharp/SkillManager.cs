using System.Collections;
using RaceTrack;
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
			if (instance == null)
			{
				new SkillManager();
			}
			return instance;
		}
	}

	private SkillManager()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void Init()
	{
		instanceList = new ArrayList();
	}

	public bool HasProp(Car car)
	{
		if (null == car)
		{
			return false;
		}
		if (TSingleton<PropManager>.Instance.Equiped(car.name) > ePropType.MISSILE)
		{
			return true;
		}
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData((ulong)car.ID, ref data);
		if (data.selProp != ePropType.NONE)
		{
			return true;
		}
		return false;
	}

	public void UseProp(Car car, bool throwForward)
	{
		if (null == car)
		{
			return;
		}
		Vector3 vDir;
		Vector3 vecGlobal;
		if (throwForward)
		{
			vDir = car.gameObject.transform.forward;
			vecGlobal = car.GetVecGlobal();
		}
		else
		{
			vDir = -car.gameObject.transform.forward;
			vecGlobal = car.GetVecGlobal();
		}
		PlayerData data = new PlayerData();
		RaceSceneData.GetPlayerData((ulong)car.ID, ref data);
		if (data.selProp != ePropType.THUNDER_CLOUD)
		{
			if (ThrowBanana(car, vDir, vecGlobal, fromCarBack: true))
			{
				if (car.isHero)
				{
					SendMessageHandler.Instance.BroadcastUseProp(ePropType.BANANA, throwForward, car.ID, -1);
				}
				return;
			}
			if (ThrowTortoise(car, vDir, vecGlobal, ePropType.TORTOISE, fromCarSurround: true))
			{
				if (car.isHero)
				{
					SendMessageHandler.Instance.BroadcastUseProp(ePropType.TORTOISE, throwForward, car.ID, -1);
				}
				return;
			}
		}
		if (data.selProp == ePropType.NONE)
		{
			return;
		}
		if (car.isHero)
		{
			if (LocalUserConfig.Instance.CanShowTutorial && data.selProp != ePropType.THUNDER_CLOUD)
			{
				LocalUserConfig.Instance.SaveCanUseProp();
			}
			SendMessageHandler.Instance.BroadcastUseProp(data.selProp, throwForward, car.ID, PropIDManager.Instance.GetID(car));
		}
		if (data.selProp == ePropType.BANANA_THREE)
		{
			TSingleton<PropManager>.Instance.EquipBanana(car, 3);
		}
		else if (data.selProp == ePropType.TORTOISE_RED_THREE)
		{
			TSingleton<PropManager>.Instance.EquipTortoise(car, 3, ePropType.TORTOISE_RED);
		}
		else if (data.selProp == ePropType.TORTOISE_GREEN_THREE)
		{
			TSingleton<PropManager>.Instance.EquipTortoise(car, 3, ePropType.TORTOISE_GREEN);
		}
		else
		{
			Discharge(data.selProp, car, vDir, vecGlobal);
		}
		if (data.selProp == ePropType.MUSHROOM_TWO)
		{
			data.altProp = (data.selProp = ePropType.MUSHROOM_ONE);
		}
		else if (data.selProp == ePropType.MUSHROOM_THREE)
		{
			data.altProp = (data.selProp = ePropType.MUSHROOM_TWO);
			if (car.isHero)
			{
				SceneUIManager.Instance.ShowProp(ePropType.NONE);
			}
		}
		else if (data.selProp == ePropType.MUSHROOM_GOLDERN)
		{
			data.altProp = (data.selProp = ePropType.MUSHROOM_GOLDERN);
		}
		else if (data.selProp == ePropType.MISSILE || data.selProp == ePropType.THUNDER_CLOUD)
		{
			data.altProp = data.selProp;
			data.selProp = ePropType.NONE;
		}
		else
		{
			data.selProp = ePropType.NONE;
			data.altProp = ePropType.NONE;
		}
		RaceSceneData.SetPlayerData((ulong)car.ID, data);
		if (car.isHero)
		{
			SceneUIManager.Instance.ShowProp(data.altProp);
		}
	}

	public void FireSphere(Vector3 fromPos, Vector3 destPos, float speed, float time)
	{
		Prop kcomponent = null;
		TSingleton<PropManager>.Instance.FireSphere(out kcomponent);
		if (null != kcomponent)
		{
			(kcomponent as FSphere).ThrowFromPos(fromPos, destPos, speed, time);
		}
		if (AppMain.Instance.IsServer)
		{
			SendMessageHandler.Instance.BroadCastFireSphere(fromPos, destPos, speed, time);
		}
	}

	private bool GetThrowAtPos(Transform transform, ref Vector3 pos, ref Vector3 dir, float delta)
	{
		int num = Random.Range(0, 900);
		int num2 = ((num >= 300) ? 1 : (-1));
		num2 = ((num > 600) ? 1 : 0);
		int num3 = Random.Range(0, 900);
		int num4 = ((num3 >= 300) ? 1 : (-1));
		num4 = ((num3 >= 600) ? 1 : 0);
		dir = (num4 * transform.forward + num2 * transform.right).normalized;
		Vector3 vector = Mathf.Cos(delta * 90f) * Vector3.one;
		pos = transform.position + transform.up + dir;
		pos.y = HeightMapManager.Instance.Height(pos.x, pos.z) + 2f + delta;
		pos += vector;
		return true;
	}

	public void DropProp(Car car, ePropType type)
	{
		float speed = 30f;
		Vector3 pos = default(Vector3);
		Vector3 dir = default(Vector3);
		switch (type)
		{
		case ePropType.CHEAT_BOX:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropCheatBox(car, pos, dir, speed);
			break;
		case ePropType.BANANA:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropBanana(car, pos, dir, speed);
			break;
		case ePropType.BANANA_THREE:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropBanana(car, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0.5f);
			DropBanana(car, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropBanana(car, pos, dir, speed);
			break;
		case ePropType.TORTOISE_RED:
		case ePropType.TORTOISE_GREEN:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropTortoise(car, type, pos, dir, speed);
			break;
		case ePropType.TORTOISE_GREEN_THREE:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropTortoise(car, ePropType.TORTOISE_GREEN, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0.5f);
			DropTortoise(car, ePropType.TORTOISE_GREEN, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropTortoise(car, ePropType.TORTOISE_GREEN, pos, dir, speed);
			break;
		case ePropType.TORTOISE_RED_THREE:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropTortoise(car, ePropType.TORTOISE_RED, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0.5f);
			DropTortoise(car, ePropType.TORTOISE_RED, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropTortoise(car, ePropType.TORTOISE_RED, pos, dir, speed);
			break;
		case ePropType.MUSHROOM_ONE:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropMushroom(car, pos, dir, speed);
			break;
		case ePropType.MUSHROOM_TWO:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropMushroom(car, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0.5f);
			DropMushroom(car, pos, dir, speed);
			break;
		case ePropType.MUSHROOM_THREE:
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropMushroom(car, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0.5f);
			DropMushroom(car, pos, dir, speed);
			GetThrowAtPos(car.transform, ref pos, ref dir, 0f);
			DropMushroom(car, pos, dir, speed);
			break;
		case ePropType.TORTOISE:
		case ePropType.MUSHROOM_GOLDERN:
		case ePropType.LUCKY_STAR:
		case ePropType.TORTOISE_BLUE:
		case ePropType.BOMB:
		case ePropType.BOMB_EFFECT:
		case ePropType.LIGHTNING:
			break;
		}
	}

	private void Discharge(ePropType type, Car car, Vector3 vDir, Vector3 vSpeed)
	{
		switch (type)
		{
		case ePropType.MISSILE:
			LaunchMissile(car);
			break;
		case ePropType.BANANA:
			ThrowBanana(car, vDir, vSpeed, fromCarBack: false);
			break;
		case ePropType.TORTOISE_RED:
		case ePropType.TORTOISE_GREEN:
			ThrowTortoise(car, vDir, vSpeed, type, fromCarSurround: false);
			break;
		case ePropType.TORTOISE_BLUE:
			UseBlueTortoise(car, vDir, vSpeed);
			break;
		case ePropType.MUSHROOM_ONE:
		case ePropType.MUSHROOM_TWO:
		case ePropType.MUSHROOM_THREE:
			UseMushroom(car, golden: false);
			break;
		case ePropType.MUSHROOM_GOLDERN:
			UseMushroom(car, golden: true);
			break;
		case ePropType.LUCKY_STAR:
			UseLuckyStar(car);
			break;
		case ePropType.BOMB:
			UseBomb(car, vDir, vSpeed);
			break;
		case ePropType.LIGHTNING:
			UseLightning(car);
			break;
		case ePropType.CUTTLE_FISH:
			UseCuttleFish(car);
			break;
		case ePropType.MUSHROOM_BIG:
			UseBigMushroom(car);
			break;
		case ePropType.CHEAT_BOX:
			UseCheatBox(car, vDir, vSpeed);
			break;
		case ePropType.THUNDER_CLOUD:
			UseThunderCloud(car);
			break;
		case ePropType.QUAKE:
			UseEarthQuake(car);
			break;
		case ePropType.BANANA_THREE:
		case ePropType.TORTOISE:
		case ePropType.TORTOISE_RED_THREE:
		case ePropType.TORTOISE_GREEN_THREE:
		case ePropType.BOMB_EFFECT:
			break;
		}
	}

	private void LaunchMissile(Car car)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.MISSILE, out kcomponent);
		if (null != gameObject)
		{
			if (null == m_MainCamera)
			{
				m_MainCamera = GameObject.Find("MainCamera");
			}
			((Missile)kcomponent).SetCar(car, m_MainCamera);
		}
	}

	private void DropBanana(Car car, Vector3 pos, Vector3 vDir, float speed)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.BANANA, out kcomponent);
		if (null != gameObject)
		{
			((Banana)kcomponent).DropAtPos(pos, vDir, speed);
		}
	}

	private bool ThrowBanana(Car car, Vector3 vDir, Vector3 vSpeed, bool fromCarBack)
	{
		if (fromCarBack)
		{
			Banana banana = TSingleton<PropManager>.Instance.ThrowBanana(car.name);
			if (null != banana)
			{
				if (car.transform.forward == vDir)
				{
					car.PlayAni("thrf");
					Vector3 vPos = car.transform.position + car.transform.right + 0.5f * car.transform.up;
					banana.ThrowFromPos(vPos, vDir, car.transform.right, vSpeed.magnitude, forward: true);
				}
				else
				{
					car.PlayAni("thrb");
					Vector3 vPos2 = car.transform.position + car.transform.right + car.transform.up;
					banana.ThrowFromPos(vPos2, vDir, car.transform.right, vSpeed.magnitude, forward: false);
				}
				car.PlaySound(51001, 1);
				if (car.isHero && car.GetRole(0) != null)
				{
					car.GetRole(0).PlaySound(Role.THROW_PROP);
				}
				return true;
			}
			return false;
		}
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.BANANA, out kcomponent);
		if (gameObject != null)
		{
			if (car.transform.forward == vDir)
			{
				car.PlayAni("thrf");
				Vector3 vPos3 = car.transform.position + car.transform.right + 1f * car.transform.up;
				((Banana)kcomponent).ThrowFromPos(vPos3, vDir, car.transform.right, vSpeed.magnitude, forward: true);
			}
			else
			{
				car.PlayAni("thrb");
				Vector3 vPos4 = car.transform.position + car.transform.right + car.transform.up;
				((Banana)kcomponent).ThrowFromPos(vPos4, vDir, car.transform.right, vSpeed.magnitude, forward: false);
			}
			car.PlaySound(51001, 1);
			if (car.isHero && car.GetRole(0) != null)
			{
				car.GetRole(0).PlaySound(Role.THROW_PROP);
			}
		}
		return true;
	}

	private void DropTortoise(Car car, ePropType type, Vector3 pos, Vector3 dir, float speed)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, type, out kcomponent);
		if (null != gameObject)
		{
			((Tortoise)kcomponent).DropAtPos(pos, dir, speed);
		}
	}

	private bool ThrowTortoise(Car car, Vector3 vDir, Vector3 vSpeed, ePropType type, bool fromCarSurround)
	{
		if (fromCarSurround)
		{
			Tortoise tortoise = TSingleton<PropManager>.Instance.ThrowTortoise(car, car.name);
			if (null != tortoise)
			{
				if (car.transform.forward == vDir)
				{
					car.PlayAni("thrf");
					Vector3 vPos = car.transform.position + car.transform.right + 1.5f * car.transform.up;
					tortoise.ThrowFromPos(car.gameObject, vPos, vDir, car.transform.right, vSpeed.magnitude, forward: true);
				}
				else
				{
					car.PlayAni("thrb");
					Vector3 vPos2 = car.transform.position + car.transform.right + car.transform.up;
					tortoise.ThrowFromPos(car.gameObject, vPos2, vDir, car.transform.right, vSpeed.magnitude, forward: false);
				}
				car.PlaySound(51201, 1);
				if (car.isHero && car.GetRole(0) != null)
				{
					car.GetRole(0).PlaySound(Role.THROW_PROP);
				}
				return true;
			}
			return false;
		}
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, type, out kcomponent);
		if (null != gameObject)
		{
			if (car.transform.forward == vDir)
			{
				car.PlayAni("thrf");
				Vector3 vPos3 = car.transform.position + car.transform.right + 1.5f * car.transform.up;
				((Tortoise)kcomponent).ThrowFromPos(car.gameObject, vPos3, vDir, car.transform.right, vSpeed.magnitude, forward: true);
			}
			else
			{
				car.PlayAni("thrb");
				Vector3 vPos4 = car.transform.position + car.transform.right + car.transform.up;
				((Tortoise)kcomponent).ThrowFromPos(car.gameObject, vPos4, vDir, car.transform.right, vSpeed.magnitude, forward: false);
			}
			car.PlaySound(51201, 1);
			if (car.isHero && car.GetRole(0) != null)
			{
				car.GetRole(0).PlaySound(Role.THROW_PROP);
			}
		}
		return true;
	}

	private void DropMushroom(Car car, Vector3 pos, Vector3 dir, float speed)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.MUSHROOM_ONE, out kcomponent);
		if (gameObject != null)
		{
			(kcomponent as Mushroom).DropAtPos(pos, dir, speed);
		}
	}

	private void UseMushroom(Car car, bool golden)
	{
		if (null != car)
		{
			car.AccelerateCar(2f);
			if (golden && car.gameObject.GetComponent<MushroomGolden>() == null)
			{
				car.gameObject.AddComponent<MushroomGolden>();
			}
		}
	}

	private void UseLuckyStar(Car car)
	{
		car.InvincibleCar();
	}

	private void UseBomb(Car car, Vector3 direction, Vector3 speed)
	{
		if (car.isHero && car.GetRole(0) != null)
		{
			car.GetRole(0).PlaySound(Role.THROW_PROP);
		}
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.BOMB, out kcomponent);
		Vector3 vPos = car.transform.position + car.transform.right + car.transform.up;
		if (gameObject != null)
		{
			car.PlaySound(51001, 1);
			if (car.transform.forward == direction)
			{
				car.PlayAni("thrf");
				(kcomponent as Bomb).ThrowFromPos(vPos, direction, car.transform.right, speed.magnitude, forward: true);
			}
			else
			{
				car.PlayAni("thrb");
				(kcomponent as Bomb).ThrowFromPos(vPos, direction, car.transform.right, speed.magnitude, forward: false);
			}
		}
	}

	private void UseThunderCloud(Car car)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.THUNDER_CLOUD, out kcomponent);
		if (gameObject != null)
		{
			(kcomponent as ThunderCloud).CreateThunderCloud(car);
		}
	}

	private void UseLightning(Car car)
	{
		if (null == car)
		{
			return;
		}
		foreach (IMonster monster in RaceWorld.Instance.Monsters)
		{
			monster.KnockedOut = true;
		}
		for (int i = 0; i < TSingleton<CarManager>.Instance.Count; i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (!(null == carByIndex))
			{
				PlayerData data = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)car.ID, ref data);
				PlayerData data2 = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data2);
				if (data.Ranking > data2.Ranking)
				{
					Lightning.Instance.Flashing(carByIndex);
				}
			}
		}
	}

	private void UseEarthQuake(Car car)
	{
		if (null == car)
		{
			return;
		}
		for (int i = 0; i < TSingleton<CarManager>.Instance.Count; i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (!(null == carByIndex))
			{
				PlayerData data = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)car.ID, ref data);
				PlayerData data2 = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data2);
				if (data.Ranking > data2.Ranking)
				{
					EarthQuake.Instance.Quake(carByIndex);
				}
			}
		}
	}

	private void UseBigMushroom(Car car)
	{
		if (!(null == car))
		{
			BigMushroom.Instance.UseBigMushroom(car);
		}
	}

	private void UseCuttleFish(Car car)
	{
		if (null == car)
		{
			return;
		}
		for (int i = 0; i < TSingleton<CarManager>.Instance.Count; i++)
		{
			Car carByIndex = TSingleton<CarManager>.Instance.GetCarByIndex(i);
			if (!(null == carByIndex) && carByIndex.isHero)
			{
				PlayerData data = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)car.ID, ref data);
				PlayerData data2 = new PlayerData();
				RaceSceneData.GetPlayerData((ulong)carByIndex.ID, ref data2);
				if (data.Ranking > data2.Ranking)
				{
					CuttleFish.Instance.HitByCuttleFish(carByIndex);
				}
				break;
			}
		}
	}

	public void LightningSelf(Car car)
	{
		if (!(null == car))
		{
			Lightning.Instance.Flashing(car);
		}
	}

	private void UseCheatBox(Car car, Vector3 direction, Vector3 speed)
	{
		if (car.isHero && car.GetRole(0) != null)
		{
			car.GetRole(0).PlaySound(Role.THROW_PROP);
		}
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.CHEAT_BOX, out kcomponent);
		Vector3 vPos = car.transform.position + car.transform.right * 0.5f + car.transform.up * 2f;
		if (gameObject != null)
		{
			car.PlaySound(51001, 1);
			if (car.transform.forward == direction)
			{
				car.PlayAni("thrf");
				(kcomponent as CheatBox).ThrowFromPos(vPos, direction, car.transform.right, speed.magnitude, forward: true);
			}
			else
			{
				car.PlayAni("thrb");
				(kcomponent as CheatBox).ThrowFromPos(vPos, direction, car.transform.right, speed.magnitude, forward: false);
			}
		}
	}

	private void DropCheatBox(Car car, Vector3 pos, Vector3 dir, float speed)
	{
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.CHEAT_BOX, out kcomponent);
		if (gameObject != null)
		{
			(kcomponent as CheatBox).DropAtPos(pos, dir, speed);
		}
	}

	private void UseBlueTortoise(Car car, Vector3 vDir, Vector3 vSpeed)
	{
		Vector3 vPos = car.transform.position + car.transform.right + car.transform.up;
		Prop kcomponent;
		GameObject gameObject = TSingleton<PropManager>.Instance.UseProp(car, ePropType.TORTOISE_BLUE, out kcomponent);
		if (null != gameObject)
		{
			((Tortoise)kcomponent).ThrowFromPos(car.gameObject, vPos, vDir, car.transform.right, vSpeed.magnitude, forward: true);
		}
	}
}
