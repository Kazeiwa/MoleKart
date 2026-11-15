using UnityEngine;

public class MgrSprite : MonoBehaviour
{
	public struct Sprite
	{
		public Vector3 lt;

		public Vector3 rt;

		public Vector3 lb;

		public Vector3 rb;

		public Color clr;
	}

	public int _Cap = 32;

	protected Material _Material;

	protected CameraClearFlags _ClearFlags = CameraClearFlags.Nothing;

	protected Sprite[] _Buf;

	protected int _Size;

	protected Mesh _Mesh;

	protected Camera _Camera;

	protected bool m_enable;

	private bool m_bDirty = true;

	private Vector3[] vb;

	private Vector2[] uvs;

	private Color[] clrs;

	private int[] ib;

	public float GetScrWidth()
	{
		return _Camera.pixelWidth;
	}

	public float GetScrHeight()
	{
		return _Camera.pixelHeight;
	}

	public void SetEnable(bool enable)
	{
		m_enable = enable;
		if (_Mesh != null && !enable)
		{
			_Mesh.Clear();
		}
	}

	public void AddSprite(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
		if (_Size < _Cap)
		{
			Sprite sprite = default(Sprite);
			sprite.lt = lt;
			sprite.rt = rt;
			sprite.rb = rb;
			sprite.lb = lb;
			sprite.clr = clr;
			_Buf[_Size] = sprite;
			_Size++;
		}
	}

	public void SetSprite(int index, Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb)
	{
		if (index >= 0 && index < _Size)
		{
			_Buf[index].lt = lt;
			_Buf[index].rt = rt;
			_Buf[index].rb = rb;
			_Buf[index].lb = lb;
			m_bDirty = true;
		}
	}

	public void Init(Material mtl, int Cap, CameraClearFlags clearFlag)
	{
		int num = 10;
		base.gameObject.layer = num;
		_Cap = Cap;
		_Material = mtl;
		_ClearFlags = CameraClearFlags.Depth;
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		MeshFilter meshFilter = base.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		_Mesh = meshFilter.mesh;
		MeshRenderer meshRenderer = base.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		meshRenderer.material = _Material;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		_Buf = new Sprite[_Cap];
		_Size = 0;
		GameObject gameObject = new GameObject();
		_Camera = gameObject.AddComponent(typeof(Camera)) as Camera;
		_Camera.name = "__SpriteCamera__";
		_Camera.nearClipPlane = -1f;
		_Camera.farClipPlane = 1f;
		_Camera.orthographic = true;
		_Camera.clearFlags = _ClearFlags;
		_Camera.cullingMask = 1 << num;
		_Camera.aspect = 1f;
	}

	public void BuildMesh()
	{
		if (_Size > 0 && m_bDirty)
		{
			m_bDirty = false;
			if (vb == null)
			{
				vb = new Vector3[_Size * 4];
				uvs = new Vector2[_Size * 4];
				clrs = new Color[_Size * 4];
				ib = new int[_Size * 3 * 2];
			}
			for (int i = 0; i < _Size; i++)
			{
				Sprite sprite = _Buf[i];
				ref Vector3 reference = ref vb[i * 4];
				reference = sprite.lb;
				ref Vector3 reference2 = ref vb[i * 4 + 1];
				reference2 = sprite.rb;
				ref Vector3 reference3 = ref vb[i * 4 + 2];
				reference3 = sprite.rt;
				ref Vector3 reference4 = ref vb[i * 4 + 3];
				reference4 = sprite.lt;
				ref Vector2 reference5 = ref uvs[i * 4];
				reference5 = new Vector2(0f, 0f);
				ref Vector2 reference6 = ref uvs[i * 4 + 1];
				reference6 = new Vector2(0f, 1f);
				ref Vector2 reference7 = ref uvs[i * 4 + 2];
				reference7 = new Vector2(1f, 1f);
				ref Vector2 reference8 = ref uvs[i * 4 + 3];
				reference8 = new Vector2(1f, 0f);
				ref Color reference9 = ref clrs[i * 4];
				reference9 = sprite.clr;
				ref Color reference10 = ref clrs[i * 4 + 1];
				reference10 = sprite.clr;
				ref Color reference11 = ref clrs[i * 4 + 2];
				reference11 = sprite.clr;
				ref Color reference12 = ref clrs[i * 4 + 3];
				reference12 = sprite.clr;
				clrs[i * 4 + 2].a *= 0.1f;
				clrs[i * 4 + 3].a *= 0.1f;
				ib[i * 6] = i * 4;
				ib[i * 6 + 1] = i * 4 + 1;
				ib[i * 6 + 2] = i * 4 + 2;
				ib[i * 6 + 3] = i * 4;
				ib[i * 6 + 4] = i * 4 + 2;
				ib[i * 6 + 5] = i * 4 + 3;
			}
			_Mesh.vertices = vb;
			_Mesh.uv = uvs;
			_Mesh.colors = clrs;
			_Mesh.triangles = ib;
		}
	}
}
