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

	protected Vector3[] m_vertices = new Vector3[4];

	protected Vector2[] m_uvs = new Vector2[4];

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
			return m_manager;
		}
		set
		{
			m_manager = value;
			m_material = m_manager.renderer.sharedMaterial;
			if (m_material != null)
			{
				m_texture = m_material.GetTexture("_MainTex");
			}
		}
	}

	public virtual SpriteRoot sprite
	{
		get
		{
			return m_sprite;
		}
		set
		{
			m_sprite = value;
			if (m_sprite != null)
			{
				UpdateColors(m_sprite.color);
			}
		}
	}

	public virtual Texture texture => m_texture;

	public virtual Material material => m_material;

	public virtual Vector3[] vertices => m_vertices;

	public virtual Vector2[] uvs => m_uvs;

	public SpriteMesh_Managed prev
	{
		get
		{
			return m_prev;
		}
		set
		{
			m_prev = value;
		}
	}

	public SpriteMesh_Managed next
	{
		get
		{
			return m_next;
		}
		set
		{
			m_next = value;
		}
	}

	public void SetBuffers(Vector3[] verts, Vector2[] uvs, Color[] cols)
	{
		meshVerts = verts;
		meshUVs = uvs;
		meshColors = cols;
	}

	public void Clear()
	{
		hidden = false;
	}

	public virtual void Init()
	{
		if (!m_sprite.Started)
		{
			return;
		}
		m_sprite.InitUVs();
		m_sprite.SetBleedCompensation(m_sprite.bleedCompensation);
		if (m_sprite.pixelPerfect)
		{
			if (m_texture != null)
			{
				m_sprite.SetPixelToUV(m_texture);
			}
			if (m_sprite.renderCamera == null)
			{
				m_sprite.SetCamera(Camera.mainCamera);
			}
			else
			{
				m_sprite.SetCamera(m_sprite.renderCamera);
			}
		}
		else if (!m_sprite.hideAtStart)
		{
			m_sprite.SetSize(m_sprite.width, m_sprite.height);
		}
		m_sprite.SetColor(m_sprite.color);
	}

	public virtual void UpdateVerts()
	{
		if (!hidden)
		{
			ref Vector3 reference = ref meshVerts[mv1];
			reference = m_vertices[0];
			ref Vector3 reference2 = ref meshVerts[mv2];
			reference2 = m_vertices[1];
			ref Vector3 reference3 = ref meshVerts[mv3];
			reference3 = m_vertices[2];
			ref Vector3 reference4 = ref meshVerts[mv4];
			reference4 = m_vertices[3];
			m_manager.UpdatePositions();
		}
	}

	public virtual void UpdateUVs()
	{
		ref Vector2 reference = ref meshUVs[uv1];
		reference = uvs[0];
		ref Vector2 reference2 = ref meshUVs[uv2];
		reference2 = uvs[1];
		ref Vector2 reference3 = ref meshUVs[uv3];
		reference3 = uvs[2];
		ref Vector2 reference4 = ref meshUVs[uv4];
		reference4 = uvs[3];
		m_manager.UpdateUVs();
	}

	public virtual void UpdateColors(Color color)
	{
		meshColors[cv1] = color;
		meshColors[cv2] = color;
		meshColors[cv3] = color;
		meshColors[cv4] = color;
		m_manager.UpdateColors();
	}

	public virtual void Hide(bool tf)
	{
		if (tf)
		{
			ref Vector3 reference = ref m_vertices[0];
			reference = Vector3.zero;
			ref Vector3 reference2 = ref m_vertices[1];
			reference2 = Vector3.zero;
			ref Vector3 reference3 = ref m_vertices[2];
			reference3 = Vector3.zero;
			ref Vector3 reference4 = ref m_vertices[3];
			reference4 = Vector3.zero;
			UpdateVerts();
			hidden = tf;
		}
		else
		{
			hidden = tf;
			if (m_sprite.pixelPerfect)
			{
				m_sprite.CalcSize();
			}
			else
			{
				m_sprite.SetSize(m_sprite.width, m_sprite.height);
			}
		}
	}

	public virtual bool IsHidden()
	{
		return hidden;
	}
}
