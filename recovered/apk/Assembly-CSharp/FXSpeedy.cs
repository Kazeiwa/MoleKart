using System;
using System.Collections;
using UnityEngine;

public class FXSpeedy : MonoBehaviour
{
	public class LineFx
	{
		public float fAccumTime;

		public float fInRadius;

		public float fOutRadius = 100f;

		public Vector2 pCenter = new Vector2(0f, 0f);

		public Vector2 pDir = new Vector2(0f, 1f);

		public int iLineType;

		public float fMainAng;

		public Vector3 head = default(Vector3);

		public Vector3 tail = default(Vector3);
	}

	private const float MAX_TIME = 0.05f;

	protected MgrSprite _MgrSprite;

	protected float _pps = 30f;

	private ArrayList m_kLines = new ArrayList();

	private bool _Enabled;

	public void Init(MgrSprite mgs)
	{
		_MgrSprite = mgs;
		_Enabled = false;
		CreateLines(30);
		DrawLines();
		_MgrSprite.BuildMesh();
		_MgrSprite.SetEnable(_Enabled);
	}

	private void CreateLines(int Num)
	{
		for (int i = 0; i < Num; i++)
		{
			LineFx lineFx = new LineFx();
			float num = 0f;
			int num2 = 0;
			float value = UnityEngine.Random.value;
			if (value < 0.25f)
			{
				num = UnityEngine.Random.value * (float)Math.PI * 2f;
			}
			else
			{
				float value2 = UnityEngine.Random.value;
				num2 = 2;
				if (value2 < 0.2f)
				{
					num = 225f;
				}
				else if (value2 < 0.4f)
				{
					num = 135f;
				}
				else if (value2 < 0.6f)
				{
					num = 45f;
				}
				else if (value2 < 0.8f)
				{
					num = 315f;
				}
				else
				{
					num2 = 1;
					num = 270f;
				}
				lineFx.fMainAng = num;
				num *= (float)Math.PI / 180f;
				num = ((num2 != 1) ? (num + (float)Math.PI / 180f * UnityEngine.Random.Range(-2.5f, 2.5f)) : (num + (float)Math.PI / 180f * UnityEngine.Random.Range(-7.5f, 7.5f)));
			}
			float x = Mathf.Cos(num);
			float y = Mathf.Sin(num);
			lineFx.fInRadius = 40f;
			lineFx.fOutRadius = 150f;
			lineFx.pDir.x = x;
			lineFx.pDir.y = y;
			lineFx.head = lineFx.pDir * lineFx.fInRadius + lineFx.pCenter;
			lineFx.tail = lineFx.pDir * lineFx.fOutRadius + lineFx.pCenter;
			lineFx.iLineType = num2;
			lineFx.fAccumTime = UnityEngine.Random.Range(0f, 0.05f);
			m_kLines.Add(lineFx);
		}
	}

	private void UpdateLines(float timeDelta)
	{
		int num = 0;
		foreach (LineFx kLine in m_kLines)
		{
			kLine.fAccumTime += timeDelta;
			if (kLine.fAccumTime > 0.05f)
			{
				kLine.fAccumTime = 0f;
				float f = 0f;
				if (kLine.iLineType == 0)
				{
					f = UnityEngine.Random.value * (float)Math.PI * 2f;
				}
				else if (kLine.iLineType == 1)
				{
					f = (float)Math.PI / 180f * (kLine.fMainAng + UnityEngine.Random.Range(-7.5f, 7.5f));
				}
				else if (kLine.iLineType == 2)
				{
					f = (float)Math.PI / 180f * (kLine.fMainAng + UnityEngine.Random.Range(-2.5f, 2.5f));
				}
				float x = Mathf.Cos(f);
				float y = Mathf.Sin(f);
				kLine.pDir.x = x;
				kLine.pDir.y = y;
				kLine.head = kLine.pDir * kLine.fInRadius + kLine.pCenter;
				kLine.tail = kLine.pDir * kLine.fOutRadius + kLine.pCenter;
				Vector3 head = kLine.head;
				Vector3 tail = kLine.tail;
				Vector3 vector = new Vector3(kLine.pDir.y, 0f - kLine.pDir.x, 0f);
				float num2 = 1f;
				float num3 = 2f;
				if (kLine.iLineType == 0)
				{
					num2 = 0.5f;
					num3 = 1f;
				}
				else if (kLine.iLineType == 1)
				{
					num2 = 1f;
					num3 = 1.5f;
				}
				else if (kLine.iLineType == 2)
				{
					num2 = 1f;
					num3 = 2f;
				}
				_MgrSprite.SetSprite(num, head + vector * num2, head - vector * num2, tail - vector * num3, tail + vector * num3);
			}
			num++;
		}
	}

	private void DrawLines()
	{
		foreach (LineFx kLine in m_kLines)
		{
			Vector3 head = kLine.head;
			Vector3 tail = kLine.tail;
			Vector3 vector = new Vector3(kLine.pDir.y, 0f - kLine.pDir.x, 0f);
			float num = 1f;
			float num2 = 2f;
			Color clr = default(Color);
			if (kLine.iLineType == 0)
			{
				clr = new Color(1f, 1f, 1f, 0.55f);
				num = 0.5f;
				num2 = 1f;
			}
			else if (kLine.iLineType == 1)
			{
				clr = new Color(1f, 1f, 1f, 0.55f);
				num = 1f;
				num2 = 1.5f;
			}
			else if (kLine.iLineType == 2)
			{
				clr = new Color(1f, 1f, 1f, 0.95f);
				num = 1f;
				num2 = 2f;
			}
			_MgrSprite.AddSprite(head + vector * num, head - vector * num, tail - vector * num2, tail + vector * num2, clr);
		}
	}

	public void EnableEffect(bool bEnabled)
	{
		if (bEnabled != _Enabled)
		{
			_Enabled = bEnabled;
			_MgrSprite.SetEnable(bEnabled);
		}
	}

	private float GetVanishX()
	{
		return 0f;
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_kLines.Count > 0 && _Enabled)
		{
			UpdateLines(deltaTime);
			_MgrSprite.BuildMesh();
		}
	}
}
