using Misc;
using UnityEngine;

public class CollisionMessageForwarder : MonoBehaviour
{
	private BaseMonsterBehaviour monsterComponent;

	public BaseMonsterBehaviour MonsterComponent
	{
		get
		{
			if (monsterComponent == null)
			{
				Transform parent = base.transform;
				while (parent != null)
				{
					BaseMonsterBehaviour component = parent.GetComponent<BaseMonsterBehaviour>();
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

	protected void OnCollision(Collider other)
	{
		Car car = CarUtil.FindCarInCollision(other);
		if (car != null && MonsterComponent != null)
		{
			MonsterComponent.SendMessage("OnCollision", car, SendMessageOptions.DontRequireReceiver);
		}
	}
}
