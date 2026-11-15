using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class Utility
{
	private static float _glowAlpha = 0f;

	private static float _time = 0f;

	public static float MAX_DISTANCE = 128f;

	public static float MIN_DOT = Mathf.Cos((float)Math.PI * 13f / 36f);

	private static float _CPU_fTime0 = 0f;

	public static float StringToFloat(string floatString)
	{
		if (floatString != null && floatString.Length > 0)
		{
			if (float.TryParse(floatString, out var result))
			{
				return result;
			}
			return 0f;
		}
		return 0f;
	}

	public static void ParseXml(string text, string nodeName, Rect[] rectTexture)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(text);
		ParseXml(xmlDocument, nodeName, rectTexture);
	}

	public static void ParseXml(XmlDocument xmlDoc, string nodeName, Rect[] rectTexture)
	{
		XmlNode xmlNode = xmlDoc.SelectSingleNode(nodeName);
		if (xmlNode == null)
		{
			return;
		}
		int num = 0;
		foreach (XmlNode childNode in xmlNode.ChildNodes)
		{
			rectTexture[num].x = StringToFloat(childNode.SelectSingleNode("x").InnerText);
			rectTexture[num].y = StringToFloat(childNode.SelectSingleNode("y").InnerText);
			XmlNode xmlNode3 = childNode.SelectSingleNode("width");
			if (xmlNode3 != null)
			{
				rectTexture[num].width = StringToFloat(xmlNode3.InnerText);
			}
			xmlNode3 = childNode.SelectSingleNode("height");
			if (xmlNode3 != null)
			{
				rectTexture[num].height = StringToFloat(xmlNode3.InnerText);
			}
			rectTexture[num].xMin = rectTexture[num].x;
			rectTexture[num].xMax = rectTexture[num].x + rectTexture[num].width;
			rectTexture[num].yMin = rectTexture[num].y;
			rectTexture[num].yMax = rectTexture[num].y + rectTexture[num].height;
			num++;
			if (rectTexture.GetLength(0) <= num)
			{
				break;
			}
		}
	}

	public static float GetGlowAlpha()
	{
		float deltaTime = Time.deltaTime;
		_glowAlpha += deltaTime * ((float)Math.PI / 180f) * 500f;
		if (_glowAlpha >= float.MaxValue)
		{
			_glowAlpha = 0f;
		}
		float num = Mathf.Sin(_glowAlpha);
		num = (num + 1f) * 0.5f;
		return Mathf.Lerp(0.5f, 1f, num);
	}

	public static int GetAnimationFrame(int totalFrame, int fps)
	{
		_time += Time.deltaTime;
		if (_time >= float.MaxValue)
		{
			_time = 0f;
		}
		int num = (int)(_time * (float)fps);
		return num % totalFrame;
	}

	public static void Reshuffle(ArrayList intDatas)
	{
		int count = intDatas.Count;
		for (int i = 0; i < count; i++)
		{
			int num = UnityEngine.Random.Range(0, count - i - 1);
			if (num >= 0 && num < count)
			{
				int num2 = (int)intDatas[num];
				intDatas[num] = intDatas[count - i - 1];
				intDatas[count - i - 1] = num2;
			}
		}
	}

	public static Transform FindChildTranform(Transform tf, string target_string)
	{
		Transform result = null;
		Transform[] componentsInChildren = tf.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform != tf.transform)
			{
				string text = transform.gameObject.name.ToLower();
				if (text.CompareTo(target_string.ToLower()) == 0)
				{
					result = transform;
					break;
				}
			}
		}
		return result;
	}

	public static Transform FindFirstChildTranform(Transform tf)
	{
		Transform result = null;
		Transform[] componentsInChildren = tf.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform != tf.transform)
			{
				result = transform;
				break;
			}
		}
		return result;
	}

	public static Transform[] GetObjectTransforms(GameObject go)
	{
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>();
		Transform[] array = new Transform[componentsInChildren.Length - 1];
		int num = 0;
		Transform[] array2 = componentsInChildren;
		foreach (Transform transform in array2)
		{
			if (transform != go.transform)
			{
				array[num++] = transform;
			}
		}
		return array;
	}

	public static IEnumerator Sleep(int seconds)
	{
		yield return seconds;
	}

	public static float Rand(float min, float max)
	{
		float num = min + (max - min) * UnityEngine.Random.value;
		if ((double)num > (double)max + 0.001)
		{
			num = max;
		}
		else if ((double)num < (double)min - 0.001)
		{
			num = min;
		}
		return num;
	}

	public static bool ObjectInSight(Vector3 myPos, Vector3 targetPos, Vector3 dir)
	{
		Vector3 lhs = targetPos - myPos;
		float magnitude = lhs.magnitude;
		if (magnitude > MAX_DISTANCE)
		{
			return false;
		}
		if (magnitude < 14f)
		{
			return true;
		}
		lhs.Normalize();
		dir.Normalize();
		float num = Vector3.Dot(lhs, dir);
		if (num < MIN_DOT)
		{
			return false;
		}
		return true;
	}

	public static void CPU_Profile_Begin()
	{
		_CPU_fTime0 = DateTime.Now.Millisecond;
	}

	public static void CPU_Profile_End(string strTag)
	{
		_CPU_fTime0 = (float)DateTime.Now.Millisecond - _CPU_fTime0;
		NiLog.Log(strTag + _CPU_fTime0, 3);
		_CPU_fTime0 = DateTime.Now.Millisecond;
	}

	public static void CPU_Profile_Begin_X()
	{
		_CPU_fTime0 = DateTime.Now.Millisecond;
	}

	public static void CPU_Profile_End_X(string strTag)
	{
		_CPU_fTime0 = (float)DateTime.Now.Millisecond - _CPU_fTime0;
		NiLog.LogX(strTag + _CPU_fTime0, 3);
		_CPU_fTime0 = DateTime.Now.Millisecond;
	}
}
