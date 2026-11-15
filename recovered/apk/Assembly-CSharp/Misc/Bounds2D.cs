using UnityEngine;

namespace Misc;

public struct Bounds2D
{
	private Vector2 center;

	private Vector2 extents;

	public Vector2 Center
	{
		get
		{
			return center;
		}
		set
		{
			center = value;
		}
	}

	public Vector2 Size
	{
		get
		{
			return extents * 2f;
		}
		set
		{
			extents = value * 0.5f;
		}
	}

	public Vector2 Extents
	{
		get
		{
			return extents;
		}
		set
		{
			extents = value;
		}
	}

	public Vector2 Min
	{
		get
		{
			return Center - Extents;
		}
		set
		{
			SetMinMax(value, Max);
		}
	}

	public Vector2 Max
	{
		get
		{
			return Center + Extents;
		}
		set
		{
			SetMinMax(Min, value);
		}
	}

	public static Bounds2D Default => new Bounds2D(Vector2.zero, Vector2.zero);

	public Bounds2D(Vector2 center, Vector2 size)
	{
		this.center = center;
		extents = size * 0.5f;
	}

	public override int GetHashCode()
	{
		return Center.GetHashCode() ^ (Extents.GetHashCode() << 2);
	}

	public override bool Equals(object other)
	{
		if (!(other is Bounds2D bounds2D))
		{
			return false;
		}
		return Center == bounds2D.Center && Extents == bounds2D.Extents;
	}

	public void SetMinMax(Vector2 min, Vector2 max)
	{
		Extents = (max - min) * 0.5f;
		Center = min + Extents;
	}

	public void Encapsulate(Vector2 point)
	{
		SetMinMax(Vector2.Min(Min, point), Vector2.Max(Max, point));
	}

	public void Encapsulate(Bounds2D bounds)
	{
		Encapsulate(bounds.Center - bounds.Extents);
		Encapsulate(bounds.Center + bounds.Extents);
	}

	public void Expand(float amount)
	{
		amount *= 0.5f;
		Extents += new Vector2(amount, amount);
	}

	public void Expand(Vector2 amount)
	{
		Extents += amount * 0.5f;
	}

	public bool Intersects(Bounds2D bounds)
	{
		return Min.x <= bounds.Max.x && Max.x >= bounds.Min.x && Min.y <= bounds.Max.y && Max.y >= bounds.Min.y;
	}

	public bool Contains(Vector2 point)
	{
		Vector2 min = Min;
		if (point.x < min.x || point.y < min.y)
		{
			return false;
		}
		Vector2 max = Max;
		return point.x <= max.x && point.y <= max.y;
	}

	public override string ToString()
	{
		return $"Center: {center}, Extents: {extents}";
	}

	public string ToString(string format)
	{
		return $"Center: {center.ToString(format)}, Extents: {extents.ToString(format)}";
	}

	public static bool operator ==(Bounds2D lhs, Bounds2D rhs)
	{
		return lhs.Center == rhs.Center && lhs.Extents == rhs.Extents;
	}

	public static bool operator !=(Bounds2D lhs, Bounds2D rhs)
	{
		return !(lhs == rhs);
	}
}
