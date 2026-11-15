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

	protected int m_Cap;

	public Material m_Material;

	protected CameraClearFlags m_ClearFlags;

	protected Sprite[] m_Buf;

	protected int m_Size;

	protected Mesh m_Mesh;

	protected Camera m_Camera;

	private Transform m_objTransform;

	public bool m_Enable;

	private float m_alpha;

	public float GetScrWidth()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetScrHeight()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start()
	{
	}

	public void AddSprite(Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
	}

	public void UpdateSprite(int num, Vector3 lt, Vector3 rt, Vector3 rb, Vector3 lb, Color clr)
	{
	}

	public void Init(int Cap, CameraClearFlags clearFlag)
	{
	}

	public void SetMeshEnable(bool enable)
	{
	}

	public bool GetMeshEnable()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void TransformMesh()
	{
	}

	public void SetAlpha(float alpha)
	{
	}

	public void UpdateShader()
	{
	}

	public void Reset()
	{
	}

	private void LateUpdate()
	{
	}
}
