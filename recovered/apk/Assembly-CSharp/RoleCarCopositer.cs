using UnityEngine;

public class RoleCarCopositer
{
	public RoleCarCopositer(Role role, GameObject car, bool isHero)
	{
		if (role != null && null != car)
		{
			role.Parent = Utility.FindChildTranform(car.transform, "seat");
			Car component = car.GetComponent<Car>();
			if (null != component)
			{
				component.PushRole(role);
				component.ResetComputeRender();
			}
		}
	}
}
