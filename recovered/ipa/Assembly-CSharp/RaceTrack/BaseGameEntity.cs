using UnityEngine;

namespace RaceTrack;

public class BaseGameEntity
{
	public const int DefaultEntityType = -1;

	public int ID;

	protected Vector2 Pos;

	private Vector2 scale_;

	public float BoundingRadius;

	public int EntityType;

	private static int next_id_;

	public Vector2 Scale
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	private int NextValidID
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public BaseGameEntity()
	{
	}

	public BaseGameEntity(int entity_type)
	{
	}

	public BaseGameEntity(int entity_type, Vector2 pos, float r)
	{
	}
}
