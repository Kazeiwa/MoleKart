using UnityEngine;

public class SpriteMesh_Managed : IEZLinkedListItem<SpriteMesh_Managed>, ISpriteMesh
{
	protected SpriteRoot m_sprite;

	protected bool hidden;

	public int index;

	public int drawLayer;

	public SpriteManager m_manager;

	public SpriteMesh_Managed m_next;

	public SpriteMesh_Managed m_prev;

	protected Vector3[] m_vertices;

	protected Vector2[] m_uvs;

	protected Material m_material;

	protected Texture m_texture;

	protected Vector3[] meshVerts;

	protected Vector2[] meshUVs;

	protected Color[] meshColors;

	public int mv1;

	public int mv2;

	public int mv3;

	public int mv4;

	public int uv1;

	public int uv2;

	public int uv3;

	public int uv4;

	public int cv1;

	public int cv2;

	public int cv3;

	public int cv4;

	public SpriteManager manager
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

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

	public SpriteMesh_Managed prev
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public SpriteMesh_Managed next
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public void SetBuffers(Vector3[] verts, Vector2[] uvs, Color[] cols)
	{
	}

	public void Clear()
	{
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
}
