using System.Collections.Generic;
using Misc;
using UnityEngine;

public class ATrace : MonoBehaviour
{
	private class Entry
	{
		private float r = 1f;

		private float g = 1f;

		private float b = 1f;

		public int Count { get; set; }

		public int Index { get; set; }

		public string Text { get; set; }

		public float Alpha => 1f - Timer.SaturatedElapsedRate;

		public bool HasText => Text != null;

		public Color Color => new Color(r, g, b, Alpha);

		private CrudeElapsedTimer Timer { get; set; }

		public Entry(int index)
		{
			Count = 0;
			Timer = new CrudeElapsedTimer(32f);
			Text = null;
			Index = index;
		}

		public void Advance(float deltaTime)
		{
			Timer.Advance(deltaTime);
		}

		public void Reset(string text)
		{
			Text = text;
			Timer.Reset();
			Count++;
		}
	}

	private const string TraceName = "ATrace";

	private const int Capability = 32;

	private const float EntryTimeLength = 32f;

	private const float EntryWidth = 1600f;

	private const float EntryHeight = 24f;

	private static ATrace instance;

	private IDictionary<string, Entry> entries = new Dictionary<string, Entry>();

	private int maxIndex;

	public static ATrace Instance
	{
		get
		{
			if (null == instance)
			{
				instance = new GameObject("ATrace").AddComponent<ATrace>();
				Object.DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public static void Say(string text, string key)
	{
		Instance.SetText(key, text);
	}

	public static void Say(string text)
	{
		Instance.SetText(text, text);
	}

	public static void Shout(string title, string text)
	{
		Instance.SetText(title, title + ": " + text);
	}

	protected void Update()
	{
		foreach (Entry value in entries.Values)
		{
			if (value != null && value.HasText)
			{
				value.Advance(Time.deltaTime);
			}
		}
	}

	protected void OnGUI()
	{
	}

	private Entry GetEntry(string key)
	{
		Entry value = null;
		if (!entries.TryGetValue(key, out value))
		{
			int num = AllocIndex();
			if (num >= 0)
			{
				value = new Entry(num);
				entries[key] = value;
			}
		}
		return value;
	}

	private void DrawEntry(Entry entry)
	{
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.normal.textColor = entry.Color;
		GUI.Label(new Rect(0f, (float)entry.Index * 24f, 1600f, 24f), "[" + entry.Count + "]" + entry.Text, gUIStyle);
	}

	private void SetText(string key, string text)
	{
		Entry entry = GetEntry(key);
		entry.Reset(text);
	}

	private int AllocIndex()
	{
		if (maxIndex >= 32)
		{
			return -1;
		}
		return maxIndex++;
	}
}
