using UnityEngine;

public class FXMgrShadow : MonoBehaviour
{
	public struct Shadow
	{
		public Vector3 lt;

		public Vector3 rt;

		public Vector3 rb;

		public Vector3 lb;

		public Vector4 uv;

		public Color clr;
	}

	public int _Cap = 32;

	public Material _Material;

	private float _texWidth;

	private float _texHeight;

	protected Shadow[] _Buf;

	protected int _Size;

	protected Mesh _Mesh;

	public void AddShadow(Vector3 pos, Vector3 fwd, Vector3 side, float fwdScale, float sideScale, Vector4 uv, Color clr)
	{
		if (_Size < _Cap)
		{
			Shadow shadow = default(Shadow);
			shadow.lt = pos + fwd * fwdScale - side * sideScale;
			shadow.rt = pos + fwd * fwdScale + side * sideScale;
			shadow.rb = pos - fwd * fwdScale + side * sideScale;
			shadow.lb = pos - fwd * fwdScale - side * sideScale;
			shadow.uv = uv;
			shadow.uv.z += shadow.uv.x;
			shadow.uv.w += shadow.uv.y;
			shadow.clr = clr;
			_Buf[_Size] = shadow;
			_Size++;
		}
	}

	public void AddShadow(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Vector4 uv, Color clr)
	{
		if (_Size < _Cap)
		{
			Shadow shadow = default(Shadow);
			shadow.lt = lt;
			shadow.rt = rt;
			shadow.rb = rb;
			shadow.lb = lb;
			shadow.uv = uv;
			shadow.uv.z += shadow.uv.x;
			shadow.uv.w += shadow.uv.y;
			shadow.clr = clr;
			_Buf[_Size] = shadow;
			_Size++;
		}
	}

	private void Awake()
	{
		if (null != _Material)
		{
			Texture2D tex = _Material.GetTexture("_MainTex") as Texture2D;
			InitImpl(_Material, tex, _Cap);
		}
	}

	public void Init(Material mtl, Texture2D tex, int Cap)
	{
		if (!(null != _Material))
		{
			InitImpl(mtl, tex, Cap);
		}
	}

	private void InitImpl(Material mtl, Texture2D tex, int Cap)
	{
		_Cap = Cap;
		_Material = mtl;
		if (null != tex)
		{
			_texWidth = tex.width;
			_texHeight = tex.height;
		}
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		MeshFilter meshFilter = base.gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		_Mesh = meshFilter.mesh;
		MeshRenderer meshRenderer = base.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		meshRenderer.material = _Material;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = false;
		_Buf = new Shadow[_Cap];
		_Size = 0;
	}

	private void LateUpdate()
	{
		_Mesh.Clear();
		if (_Size > 0)
		{
			Vector3[] array = new Vector3[_Size * 4];
			Vector2[] array2 = new Vector2[_Size * 4];
			Color[] array3 = new Color[_Size * 4];
			int[] array4 = new int[_Size * 3 * 2];
			float num = 1f / _texWidth;
			float num2 = 1f / _texHeight;
			for (int i = 0; i < _Size; i++)
			{
				Shadow shadow = _Buf[i];
				ref Vector3 reference = ref array[i * 4];
				reference = shadow.lt;
				ref Vector3 reference2 = ref array[i * 4 + 1];
				reference2 = shadow.rt;
				ref Vector3 reference3 = ref array[i * 4 + 2];
				reference3 = shadow.rb;
				ref Vector3 reference4 = ref array[i * 4 + 3];
				reference4 = shadow.lb;
				Vector4 uv = shadow.uv;
				float y = uv.y;
				uv.y = _texHeight - uv.w - 1f;
				uv.w = _texHeight - y - 1f;
				uv.x *= num;
				uv.z *= num;
				uv.y *= num2;
				uv.w *= num2;
				ref Vector2 reference5 = ref array2[i * 4];
				reference5 = new Vector2(uv.x, uv.y);
				ref Vector2 reference6 = ref array2[i * 4 + 1];
				reference6 = new Vector2(uv.z, uv.y);
				ref Vector2 reference7 = ref array2[i * 4 + 2];
				reference7 = new Vector2(uv.z, uv.w);
				ref Vector2 reference8 = ref array2[i * 4 + 3];
				reference8 = new Vector2(uv.x, uv.w);
				ref Color reference9 = ref array3[i * 4];
				reference9 = shadow.clr;
				ref Color reference10 = ref array3[i * 4 + 1];
				reference10 = shadow.clr;
				ref Color reference11 = ref array3[i * 4 + 2];
				reference11 = shadow.clr;
				ref Color reference12 = ref array3[i * 4 + 3];
				reference12 = shadow.clr;
				array4[i * 6] = i * 4;
				array4[i * 6 + 1] = i * 4 + 1;
				array4[i * 6 + 2] = i * 4 + 2;
				array4[i * 6 + 3] = i * 4;
				array4[i * 6 + 4] = i * 4 + 2;
				array4[i * 6 + 5] = i * 4 + 3;
			}
			_Mesh.vertices = array;
			_Mesh.uv = array2;
			_Mesh.colors = array3;
			_Mesh.triangles = array4;
			_Size = 0;
		}
	}
}
