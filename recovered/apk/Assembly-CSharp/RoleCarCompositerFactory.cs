using UnityEngine;

public class RoleCarCompositerFactory
{
	private static RoleCarCompositerFactory instance;

	public static RoleCarCompositerFactory Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RoleCarCompositerFactory();
			}
			return instance;
		}
	}

	private RoleCarCompositerFactory()
	{
	}

	public RoleCarCopositer CreateCompositer(Role role, GameObject car, bool isHero)
	{
		if (role != null && null != car)
		{
			return new RoleCarCopositer(role, car, isHero);
		}
		return null;
	}
}
