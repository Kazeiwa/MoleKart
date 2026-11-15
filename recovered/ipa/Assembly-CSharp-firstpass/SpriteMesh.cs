using UnityEngine;

public class SpriteMesh : ISpriteMesh
{
	protected SpriteRoot m_sprite;

	protected MeshFilter meshFilter;

	protected MeshRenderer meshRenderer;

	protected Mesh m_mesh;

	protected Texture m_texture;

	protected Vector3[] m_vertices;

	protected Color[] m_colors;

	protected Vector2[] m_uvs;

	protected int[] m_faces;

	public virtual SpriteRoot sprite
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual Texture texture
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual Material material
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual Vector3[] vertices
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual Vector2[] uvs
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public virtual Mesh mesh
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public virtual void Init()
	{
	}

	public virtual void UpdateVerts()
	{
	}

	public virtual void UpdateUVs()
	{
	}

	public virtual void UpdateColors(Color color)
	{
	}

	public virtual void Hide(bool tf)
	{
	}

	public virtual bool IsHidden()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetPersistent()
	{
	}

	public virtual void SetWindingOrder(SpriteRoot.WINDING_ORDER winding)
	{
	}
}
