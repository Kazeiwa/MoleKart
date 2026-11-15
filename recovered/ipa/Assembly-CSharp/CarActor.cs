using System.Runtime.CompilerServices;
using RaceTrack;

public class CarActor
{
	private Car m_PlayerCar;

	private PlayerData m_PlayerData;

	private FStateMachine machine;

	private PlayerState m_PlayerState;

	private Role m_Role;

	private string m_OtherClipName;

	private float m_AniTime;

	private bool m_bLock;

	public FStateMachine CurFStateMachine
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public PlayerAIProfile AIProfile
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		private set
		{
		}
	}

	public CarActor(Car car)
	{
	}

	public void AttachAIProfile(PlayerAIProfile profile)
	{
	}

	private void InitStateMachine()
	{
	}

	private void InitPlayerData(Car car)
	{
	}

	public void Uninit()
	{
	}

	public void Update()
	{
	}

	public void UpdatePhysics()
	{
	}

	public Car GetPlayerCar()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CanDrive()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void LockCarActor()
	{
	}

	public void UnLockCarActor()
	{
	}

	public PlayerState GetPlayerState()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPlayerState(PlayerState state)
	{
	}

	public void ResetPlayerState()
	{
	}

	public void PushRole(Role role)
	{
	}

	public void PopRole(Role role)
	{
	}

	public Role GetRole(int idx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void PickProp(ePropType type)
	{
	}

	public void HitByBanana()
	{
	}

	public void HitByTortoise()
	{
	}

	public void HitByMissile(bool right)
	{
	}

	public void BecomeMissile()
	{
	}

	public void RecoverCarFromMissile()
	{
	}

	public void AccelerateCar()
	{
	}

	public void InvincibleCar()
	{
	}

	public void HitByBomb()
	{
	}

	public void HitByLightning()
	{
	}

	public void HitByCuttleFish()
	{
	}

	public void HitByCheatBox()
	{
	}

	public void HitByLuckyStar(bool right)
	{
	}

	public void UseBigMushroom()
	{
	}

	public void QuakeCar()
	{
	}

	public void UseThunderCloud()
	{
	}

	public void DisappearThunderCloud()
	{
	}

	public void PressByBigCar()
	{
	}

	public void HitByRobb()
	{
	}

	public void BackFlip()
	{
	}

	public void CarDead(int deadMode)
	{
	}

	private bool Defected()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void BounceCar()
	{
	}

	public void ShiftAccelerateCar()
	{
	}

	public void DropProp(bool drop)
	{
	}

	public void PlayOther(string name)
	{
	}

	public void PlayAni(string name)
	{
	}

	public void PlayAniOnly(string name)
	{
	}

	public void StopAniOnly()
	{
	}

	public void PlayRoleAnim()
	{
	}
}
