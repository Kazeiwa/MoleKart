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

	public int _Cap;

	protected Material _Material;

	protected CameraClearFlags _ClearFlags;

	protected Sprite[] _Buf;

	protected int _Size;

	protected Mesh _Mesh;

	protected Camera _Camera;

	protected bool m_enable;

	private bool m_bDirty;

	private Vector3[] vb;

	private Vector2[] uvs;

	private Color[] clrs;

	private int[] ib;

	public float GetScrWidth()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetScrHeight()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetEnable(bool enable)
	{
	}

	public void AddSprite(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
	}

	public void SetSprite(int index, Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb)
	{
	}

	public void Init(Material mtl, int Cap, CameraClearFlags clearFlag)
	{
	}

	public void BuildMesh()
	{
	}
}
