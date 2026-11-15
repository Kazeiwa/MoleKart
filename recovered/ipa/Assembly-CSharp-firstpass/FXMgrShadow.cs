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

	public int _Cap;

	public Material _Material;

	private float _texWidth;

	private float _texHeight;

	protected Shadow[] _Buf;

	protected int _Size;

	protected Mesh _Mesh;

	public void AddShadow(Vector3 pos, Vector3 fwd, Vector3 side, float fwdScale, float sideScale, Vector4 uv, Color clr)
	{
	}

	public void AddShadow(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Vector4 uv, Color clr)
	{
	}

	private void Awake()
	{
	}

	public void Init(Material mtl, Texture2D tex, int Cap)
	{
	}

	private void InitImpl(Material mtl, Texture2D tex, int Cap)
	{
	}

	private void LateUpdate()
	{
	}
}
