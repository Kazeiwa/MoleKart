using System.Collections;
using UnityEngine;

public class FXSpeedy : MonoBehaviour
{
	public class LineFx
	{
		public float fAccumTime;

		public float fInRadius;

		public float fOutRadius;

		public Vector2 pCenter;

		public Vector2 pDir;

		public int iLineType;

		public float fMainAng;

		public Vector3 head;

		public Vector3 tail;
	}

	private const float MAX_TIME = 0.05f;

	protected MgrSprite _MgrSprite;

	protected float _pps;

	private ArrayList m_kLines;

	private bool _Enabled;

	public void Init(MgrSprite mgs)
	{
	}

	private void CreateLines(int Num)
	{
	}

	private void UpdateLines(float timeDelta)
	{
	}

	private void DrawLines()
	{
	}

	public void EnableEffect(bool bEnabled)
	{
	}

	private float GetVanishX()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Update()
	{
	}
}
