using GameNetwork;
using Misc;
using UnityEngine;

public class DogCollisionForwarder : MonoBehaviour
{
	private DogBehaviour monsterComponent;

	public DogBehaviour MonsterComponent
	{
		get
		{
			if (monsterComponent == null)
			{
				Transform parent = base.transform;
				while (parent != null)
				{
					DogBehaviour component = parent.GetComponent<DogBehaviour>();
					if (component != null)
					{
						monsterComponent = component;
						break;
					}
					parent = parent.parent;
				}
			}
			return monsterComponent;
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > 15f)
		{
			Car car = CarUtil.FindCarInCollision(collision.collider);
			if (null != car && (!NetworkManager.Instance.IsClient || car.isHero))
			{
				MonsterComponent.OnCollision(car);
			}
		}
	}
}
