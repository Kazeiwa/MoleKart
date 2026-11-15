using UnityEngine;

public class SpritStanderd : MonoBehaviour
{
	public struct Sprite
	{
		public Vector3 lt;

		public Vector3 rt;

		public Vector3 lb;

		public Vector3 rb;

		public Color clr;
	}

	protected int m_Cap = 32;

	public Material m_Material;

	protected CameraClearFlags m_ClearFlags = CameraClearFlags.Nothing;

	protected Sprite[] m_Buf;

	protected int m_Size;

	protected Mesh m_Mesh;

	protected Camera m_Camera;

	private Transform m_objTransform;

	public bool m_Enable;

	private float m_alpha = 1f;

	public float GetScrWidth()
	{
		return m_Camera.pixelWidth;
	}

	public float GetScrHeight()
	{
		return m_Camera.pixelHeight;
	}

	public void Start()
	{
		Init(m_Cap, CameraClearFlags.Nothing);
		FxLeaf component = base.gameObject.GetComponent<FxLeaf>();
		component.Init(this);
	}

	public void AddSprite(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
		if (m_Size < m_Cap)
		{
			Sprite sprite = default(Sprite);
			sprite.lt = lt;
			sprite.rt = rt;
			sprite.rb = rb;
			sprite.lb = lb;
			sprite.clr = clr;
			m_Buf[m_Size] = sprite;
			m_Size++;
		}
	}

	public void UpdateSprite(int num, Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
		if (num < m_Size)
		{
			m_Buf[num].lt = lt;
			m_Buf[num].rt = rt;
			m_Buf[num].lb = lb;
			m_Buf[num].rb = rb;
			m_Buf[num].clr = clr;
		}
	}

	public void Init(int Cap, CameraClearFlags clearFlag)
	{
		int num = 11;
		base.gameObject.layer = num;
		m_Cap = Cap;
		m_ClearFlags = clearFlag;
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		m_objTransform = base.transform;
		MeshFilter meshFilter = base.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		m_Mesh = meshFilter.mesh;
		MeshRenderer meshRenderer = base.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		meshRenderer.material = m_Material;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		m_Buf = new Sprite[m_Cap];
		m_Size = 0;
		GameObject gameObject = new GameObject();
		m_Camera = gameObject.AddComponent(typeof(Camera)) as Camera;
		m_Camera.name = "__SpriteStanderd__";
		m_Camera.nearClipPlane = -1f;
		m_Camera.farClipPlane = 1f;
		m_Camera.orthographic = true;
		m_Camera.clearFlags = m_ClearFlags;
		m_Camera.cullingMask = 1 << num;
		float farClipPlane = m_Camera.farClipPlane;
		float nearClipPlane = m_Camera.nearClipPlane;
		float pixelWidth = m_Camera.pixelWidth;
		float pixelHeight = m_Camera.pixelHeight;
		Matrix4x4 projectionMatrix = default(Matrix4x4);
		projectionMatrix.m00 = 2f / pixelWidth;
		projectionMatrix.m10 = 0f;
		projectionMatrix.m20 = 0f;
		projectionMatrix.m30 = 0f;
		projectionMatrix.m01 = 0f;
		projectionMatrix.m11 = 2f / pixelHeight;
		projectionMatrix.m21 = 0f;
		projectionMatrix.m31 = 0f;
		projectionMatrix.m02 = 0f;
		projectionMatrix.m12 = 0f;
		projectionMatrix.m22 = 1f / (farClipPlane - nearClipPlane);
		projectionMatrix.m32 = 0f;
		projectionMatrix.m03 = 0f;
		projectionMatrix.m13 = 0f;
		projectionMatrix.m23 = (0f - nearClipPlane) / (farClipPlane - nearClipPlane);
		projectionMatrix.m33 = 1f;
		m_Camera.projectionMatrix = projectionMatrix;
	}

	public void SetMeshEnable(bool enable)
	{
		m_Enable = enable;
	}

	public bool GetMeshEnable()
	{
		return base.gameObject.active;
	}

	public void TransformMesh()
	{
		m_objTransform.position += new Vector3(0f, Random.value * 10f, 0f);
	}

	public void SetAlpha(float alpha)
	{
		m_alpha = alpha;
	}

	public void UpdateShader()
	{
		base.renderer.material.SetFloat("_AlphaChange", m_alpha);
	}

	public void Reset()
	{
		m_Size = 0;
		m_objTransform.position = Vector3.zero;
	}

	private void LateUpdate()
	{
		m_Mesh.Clear();
		if (m_Enable && m_Size > 0)
		{
			Vector3[] array = new Vector3[m_Size * 4];
			Vector2[] array2 = new Vector2[m_Size * 4];
			int[] array3 = new int[m_Size * 3 * 2];
			for (int i = 0; i < m_Size; i++)
			{
				Sprite sprite = m_Buf[i];
				ref Vector3 reference = ref array[i * 4];
				reference = sprite.lb;
				ref Vector3 reference2 = ref array[i * 4 + 1];
				reference2 = sprite.rb;
				ref Vector3 reference3 = ref array[i * 4 + 2];
				reference3 = sprite.rt;
				ref Vector3 reference4 = ref array[i * 4 + 3];
				reference4 = sprite.lt;
				ref Vector2 reference5 = ref array2[i * 4];
				reference5 = new Vector2(0f, 0f);
				ref Vector2 reference6 = ref array2[i * 4 + 1];
				reference6 = new Vector2(1f, 0f);
				ref Vector2 reference7 = ref array2[i * 4 + 2];
				reference7 = new Vector2(1f, 1f);
				ref Vector2 reference8 = ref array2[i * 4 + 3];
				reference8 = new Vector2(0f, 1f);
				array3[i * 6] = i * 4;
				array3[i * 6 + 1] = i * 4 + 1;
				array3[i * 6 + 2] = i * 4 + 2;
				array3[i * 6 + 3] = i * 4;
				array3[i * 6 + 4] = i * 4 + 2;
				array3[i * 6 + 5] = i * 4 + 3;
			}
			m_Mesh.vertices = array;
			m_Mesh.uv = array2;
			m_Mesh.triangles = array3;
			UpdateShader();
		}
	}
}
