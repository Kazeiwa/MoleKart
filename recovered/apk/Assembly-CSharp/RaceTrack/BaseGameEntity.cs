using System;
using UnityEngine;

namespace RaceTrack;

public class BaseGameEntity
{
	public const int DefaultEntityType = -1;

	public int ID = -1;

	protected Vector2 Pos;

	private Vector2 scale_ = Vector2.one;

	public float BoundingRadius;

	public int EntityType;

	private static int next_id_;

	public Vector2 Scale
	{
		get
		{
			return scale_;
		}
		set
		{
			BoundingRadius *= Math.Max(value.x, value.y) / Math.Max(scale_.x, scale_.y);
			scale_ = value;
		}
	}

	private int NextValidID => next_id_++;

	public BaseGameEntity()
	{
		ID = NextValidID;
		BoundingRadius = 0f;
	}

	public BaseGameEntity(int entity_type)
		: this()
	{
		EntityType = entity_type;
	}

	public BaseGameEntity(int entity_type, Vector2 pos, float r)
		: this(entity_type)
	{
		Pos = pos;
		BoundingRadius = r;
	}
}
