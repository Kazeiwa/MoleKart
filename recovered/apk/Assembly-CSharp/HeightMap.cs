using System;
using System.IO;
using UnityEngine;

public class HeightMap : MonoBehaviour
{
	public int m_Left;

	public int m_Right;

	public int m_Top;

	public int m_Bottom;

	public float m_CollideCheckRadius = 1f;

	private int m_Line;

	public static float m_InvalideHeight = -59f;

	private void Start()
	{
		m_Line = m_Bottom;
		HeightMapManager.Instance.m_HeightMap = null;
		HeightMapManager.Instance.m_NormalMap = null;
		HeightMapManager.Instance.m_ColliderMap = null;
		HeightMapManager.Instance.m_CollideGardbar = null;
		HeightMapManager.Instance.m_Left = m_Left;
		HeightMapManager.Instance.m_Right = m_Right;
		HeightMapManager.Instance.m_Top = m_Top;
		HeightMapManager.Instance.m_Bottom = m_Bottom;
		HeightMapManager.Instance.Inited = false;
		LoadCollideTxt();
	}

	private void Update()
	{
		if (HeightMapManager.Instance.m_HeightMap == null)
		{
			HeightMapManager.Instance.m_HeightMap = new float[(m_Right - m_Left) * (m_Top - m_Bottom)];
		}
		if (HeightMapManager.Instance.m_NormalMap == null)
		{
			HeightMapManager.Instance.m_NormalMap = new Vector3[(m_Right - m_Left) * (m_Top - m_Bottom)];
		}
		if (HeightMapManager.Instance.m_ColliderMap == null)
		{
			HeightMapManager.Instance.m_ColliderMap = new Collider[(m_Right - m_Left) * (m_Top - m_Bottom)];
		}
		if (HeightMapManager.Instance.m_CollideGardbar == null)
		{
			HeightMapManager.Instance.m_CollideGardbar = new bool[(m_Right - m_Left) * (m_Top - m_Bottom)];
		}
		if (m_Line < m_Top)
		{
			CreateMap();
			if (null != SceneUIManager.Instance.m_Loading)
			{
				SceneUIManager.Instance.m_Loading.SetActiveRecursively(state: true);
			}
		}
		else
		{
			HeightMapManager.Instance.Inited = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void SaveSceneInf()
	{
		string path = "Assets/Resources/Config/scene/" + SceneManager.Instance.CurGameSceneId + "_collide.txt";
		StreamWriter streamWriter = new StreamWriter(path);
		if (streamWriter == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		for (num = m_Line; num < m_Top; num++)
		{
			string text = string.Empty;
			for (num2 = m_Left; num2 < m_Right; num2++)
			{
				Vector3 origin = new Vector3(num2, 200f, num);
				bool flag = false;
				if (Physics.Raycast(origin, Vector3.down, out var hitInfo, 1000f, 256))
				{
					Vector3 point = hitInfo.point;
					point.y += 2f;
					Vector3 direction = new Vector3(1f, 0f, 0f);
					if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
					{
						flag = true;
					}
					else
					{
						direction.x = 1f;
						direction.z = 1f;
						if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
						{
							flag = true;
						}
						else
						{
							direction.x = 0f;
							direction.z = 1f;
							if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
							{
								flag = true;
							}
							else
							{
								direction.x = -1f;
								direction.z = 1f;
								if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
								{
									flag = true;
								}
								else
								{
									direction.x = -1f;
									direction.z = 0f;
									if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
									{
										flag = true;
									}
									else
									{
										direction.x = -1f;
										direction.z = -1f;
										if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
										{
											flag = true;
										}
										else
										{
											direction.x = 0f;
											direction.z = -1f;
											if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
											{
												flag = true;
											}
											else
											{
												direction.x = 1f;
												direction.z = -1f;
												if (Physics.Raycast(point, direction, out hitInfo, m_CollideCheckRadius, 512))
												{
													flag = true;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				text = ((!flag) ? (text + "0" + '\t') : (text + "1" + '\t'));
			}
			streamWriter.WriteLine(text);
		}
		streamWriter.Close();
	}

	private void LoadCollideTxt()
	{
		if (HeightMapManager.Instance.m_CollideGardbar == null)
		{
			HeightMapManager.Instance.m_CollideGardbar = new bool[(m_Right - m_Left) * (m_Top - m_Bottom)];
		}
		string path = "Config/scene/" + SceneManager.Instance.CurGameSceneId + "_collide";
		TextAsset textAsset = Resources.Load(path) as TextAsset;
		if (null == textAsset)
		{
			return;
		}
		Stream stream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(stream);
		if (streamReader == null)
		{
			return;
		}
		string text = null;
		for (int i = m_Bottom; i < m_Top; i++)
		{
			text = streamReader.ReadLine();
			if (!string.IsNullOrEmpty(text))
			{
				char[] separator = new char[1] { '\t' };
				string[] array = text.Split(separator);
				for (int j = m_Left; j < m_Right; j++)
				{
					int num = Convert.ToInt32(array[j - m_Left].Replace(" ", string.Empty));
					bool flag = num == 1;
					HeightMapManager.Instance.m_CollideGardbar[(i - m_Bottom) * (m_Right - m_Left) + (j - m_Left)] = flag;
				}
			}
		}
	}

	private void CreateMap()
	{
		int num = 0;
		int num2 = 0;
		for (num = m_Line; num < m_Line + 20 && num < m_Top; num++)
		{
			for (num2 = m_Left; num2 < m_Right; num2++)
			{
				Vector3 origin = new Vector3(num2, 200f, num);
				float num3 = m_InvalideHeight - 1f;
				Vector3 vector = default(Vector3);
				Collider collider = null;
				if (Physics.Raycast(origin, Vector3.down, out var hitInfo, 1000f, 256))
				{
					num3 = hitInfo.point.y;
					vector = hitInfo.normal;
					collider = hitInfo.collider;
				}
				HeightMapManager.Instance.m_HeightMap[(num - m_Bottom) * (m_Right - m_Left) + (num2 - m_Left)] = num3;
				HeightMapManager.Instance.m_NormalMap[(num - m_Bottom) * (m_Right - m_Left) + (num2 - m_Left)] = vector;
				HeightMapManager.Instance.m_ColliderMap[(num - m_Bottom) * (m_Right - m_Left) + (num2 - m_Left)] = collider;
			}
		}
		m_Line = num;
	}
}
