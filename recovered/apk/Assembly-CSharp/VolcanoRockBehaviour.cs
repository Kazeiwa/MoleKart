using GameNetwork;
using Misc;
using UnityEngine;

public class VolcanoRockBehaviour : SimpleMonsterBehaviour
{
	public const float CarTimerLimit = 2f;

	public bool shouldBeAvoided = true;

	public override Transform GrabTransform => base.transform.parent.parent;

	public override GameObject GrabGameObject => base.transform.parent.parent.gameObject;

	public override bool ShouldSendMovementInfo => false;

	public override bool ShouldBeAvoided => shouldBeAvoided;

	public VolcanoRockBehaviour()
		: base(2f)
	{
	}

	public override void UpdateState()
	{
		base.UpdateState();
		if (!NetworkManager.Instance.IsClient)
		{
			UpdateCarTimerManager();
		}
	}

	public override void StartState()
	{
		base.StartState();
		PlayAnimation("A_rock_02");
	}

	protected void PlayFlyinggSound()
	{
		PlaySound(20501, 1);
	}

	protected override void FindAndSetAnimation()
	{
		animationComponent = GetComponentInChildren<Animation>();
		if (animationComponent == null)
		{
			Debug.LogError("VolcanoRockBehaviour.FindAndSetAnimation: Can't found the Animation component.");
		}
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (NetworkManager.Instance.IsClient)
		{
			return;
		}
		Car car = CarUtil.FindCarInCollision(other);
		if (null != car)
		{
			CheckAndHitCar(car, delegate(Car c)
			{
				c.HitByTortoise();
			});
		}
	}
}
