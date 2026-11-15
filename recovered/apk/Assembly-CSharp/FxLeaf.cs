using System;
using System.Collections;
using UnityEngine;

public class FxLeaf : MonoBehaviour
{
	public struct LeafFx
	{
		public float age;

		public Vector3 endpos;

		public Vector3 startpos;

		public Vector3 curpos;
	}

	protected SpritStanderd m_SprSta;

	private int m_Num = 6;

	public float m_LeafEffectTime = 5f;

	public float m_TransTime = 2f;

	private float m_CurrentTime;

	private ArrayList m_Array;

	private Vector3 offset1 = new Vector3(-100f, 100f, 0f);

	private Vector3 offset2 = new Vector3(100f, 100f, 0f);

	private Vector3 offset3 = new Vector3(100f, -100f, 0f);

	private Vector3 offset4 = new Vector3(-100f, -100f, 0f);

	public Vector2[] speedarry = new Vector2[6]
	{
		new Vector2(-10f, -2f),
		new Vector2(-8f, -3f),
		new Vector2(9f, -2f),
		new Vector2(9f, -5f),
		new Vector2(8f, -1f),
		new Vector2(-10f, -7f)
	};

	private Vector3[] m_Vec3;

	public bool m_Enable { get; set; }

	public void Init(SpritStanderd mgs)
	{
		m_SprSta = mgs;
		m_Array = new ArrayList();
		m_Vec3 = new Vector3[m_Num];
		float num = m_SprSta.GetScrWidth() * 0.5f;
		float num2 = m_SprSta.GetScrHeight() * 0.5f;
		Vector3 vector = new Vector3((0f - num) * 0.4f, (0f - num2) * 0.3f, 1f);
		Vector3 vector2 = new Vector3(num * 0.55f, (0f - num2) * 0.54f, 1f);
		Vector3 vector3 = new Vector3(num * 0.225f, (0f - num2) * 0.1f, 1f);
		Vector3 vector4 = new Vector3((0f - num) * 0.1f, num2 * 0.1f, 1f);
		Vector3 vector5 = new Vector3((0f - num) * 0.4f, num2 * 0.2f, 1f);
		Vector3 vector6 = new Vector3(num * 0.52f, num2 * 0.8f, 1f);
		m_Vec3[0] = vector;
		m_Vec3[1] = vector2;
		m_Vec3[2] = vector3;
		m_Vec3[3] = vector4;
		m_Vec3[4] = vector5;
		m_Vec3[5] = vector6;
	}

	public void CreateLeaf(int num)
	{
		float num2 = m_SprSta.GetScrHeight() * 0.5f;
		m_Array.Clear();
		LeafFx leafFx = default(LeafFx);
		for (int i = 0; i < num; i++)
		{
			leafFx.age = 0f;
			leafFx.endpos = new Vector3(m_Vec3[i].x, m_Vec3[i].y, 0f);
			leafFx.startpos = new Vector3(0f, num2 * 0.5f, 0f);
			leafFx.curpos = new Vector3(0f, num2 * 0.5f, 0f);
			m_Array.Add(leafFx);
			m_SprSta.AddSprite(leafFx.curpos, leafFx.curpos, leafFx.curpos, leafFx.curpos, new Color(1f, 1f, 1f, 0.5f));
		}
	}

	public Vector3 Fxlerp(ref Vector3 v1, ref Vector3 v2, float fac)
	{
		return v1 + (v2 - v1) * fac;
	}

	private void UpdateLeaf(float timeDelta)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		foreach (LeafFx item in m_Array)
		{
			LeafFx leafFx2 = item;
			leafFx2.age += timeDelta;
			if (leafFx2.age <= 0.75f)
			{
				float num2 = 1f - Mathf.Cos(leafFx2.age * leafFx2.age * 1.77778f * (float)Math.PI / 2f);
				leafFx2.curpos = leafFx2.startpos + (leafFx2.endpos - leafFx2.startpos) * num2;
				m_SprSta.UpdateSprite(num, leafFx2.curpos + offset1 * num2, leafFx2.curpos + offset2 * num2, leafFx2.curpos + offset3 * num2, leafFx2.curpos + offset4 * num2, new Color(1f, 1f, 1f, 0.5f));
			}
			else if (leafFx2.age < 3f)
			{
				leafFx2.curpos = leafFx2.endpos;
				m_SprSta.UpdateSprite(num, leafFx2.curpos + offset1, leafFx2.curpos + offset2, leafFx2.curpos + offset3, leafFx2.curpos + offset4, new Color(1f, 1f, 1f, 0.5f));
			}
			else if (leafFx2.age < 5f)
			{
				float age = leafFx2.age;
				leafFx2.curpos.x -= age * speedarry[num].x;
				leafFx2.curpos.y -= age * speedarry[num].y;
				m_SprSta.UpdateSprite(num, leafFx2.curpos + offset1, leafFx2.curpos + offset2, leafFx2.curpos + offset3, leafFx2.curpos + offset4, new Color(1f, 1f, 1f, 0.5f));
				m_SprSta.SetAlpha(1f - leafFx2.age * 0.1f);
			}
			arrayList.Add(leafFx2);
			num++;
		}
		m_Array = arrayList;
	}

	private void Update()
	{
		m_CurrentTime += Time.deltaTime;
		if (m_Enable && m_CurrentTime >= m_LeafEffectTime)
		{
			m_Enable = false;
			m_SprSta.SetMeshEnable(enable: false);
		}
		if (m_Enable)
		{
			UpdateLeaf(Time.deltaTime);
		}
	}

	public void ResetLeaf()
	{
		m_CurrentTime = 0f;
		m_Enable = true;
		m_SprSta.Reset();
		m_SprSta.SetMeshEnable(enable: true);
		m_SprSta.SetAlpha(1f);
		CreateLeaf(m_Num);
	}
}
