using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

public class ATrace : MonoBehaviour
{
	private class Entry
	{
		private float r;

		private float g;

		private float b;

		public int Count
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public int Index
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public string Text
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public float Alpha
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public bool HasText
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public Color Color
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		private CrudeElapsedTimer Timer
		{
			[CompilerGenerated]
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		public Entry(int index)
		{
		}

		public void Advance(float deltaTime)
		{
		}

		public void Reset(string text)
		{
		}
	}

	private const string TraceName = "ATrace";

	private const int Capability = 32;

	private const float EntryTimeLength = 32f;

	private const float EntryWidth = 1600f;

	private const float EntryHeight = 24f;

	private static ATrace instance;

	private IDictionary<string, Entry> entries;

	private int maxIndex;

	public static ATrace Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static void Say(string text, string key)
	{
	}

	public static void Say(string text)
	{
	}

	public static void Shout(string title, string text)
	{
	}

	public static void Shout(string title, int num)
	{
	}

	protected void Update()
	{
	}

	protected void OnGUI()
	{
	}

	private Entry GetEntry(string key)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void DrawEntry(Entry entry)
	{
	}

	private void SetText(string key, string text)
	{
	}

	private int AllocIndex()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
