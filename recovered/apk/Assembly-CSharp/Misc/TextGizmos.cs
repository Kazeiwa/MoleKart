using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Misc;

public class TextGizmos
{
	private class Nested
	{
		internal static readonly TextGizmos Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new TextGizmos();
		}
	}

	public const int CharTextureHeight = 8;

	public const int CharTextureWidth = 6;

	private const string Characters = "abcdefghijklmnopqrstuvwxyz0123456789 !#%(),.'-;_{}~+=:\\?\"/*";

	private static readonly Dictionary<char, string> unsafeChar2NameMapping = new Dictionary<char, string>
	{
		{ ':', "colon" },
		{ '\\', "backslash" },
		{ '?', "questionmark" },
		{ '"', "quotes" },
		{ '/', "slash" },
		{ '*', "star" }
	};

	private Dictionary<char, string> char2TexturePathMapping;

	private Camera editorCamera;

	public static TextGizmos Instance => Nested.Instance;

	private TextGizmos()
	{
		editorCamera = Camera.current;
		char2TexturePathMapping = new Dictionary<char, string>();
		for (int i = 0; i < "abcdefghijklmnopqrstuvwxyz0123456789 !#%(),.'-;_{}~+=:\\?\"/*".Length; i++)
		{
			char key = "abcdefghijklmnopqrstuvwxyz0123456789 !#%(),.'-;_{}~+=:\\?\"/*"[i];
			if (!unsafeChar2NameMapping.TryGetValue(key, out var value))
			{
				value = key.ToString();
			}
			char2TexturePathMapping.Add(key, "TextGizmos/text_" + value + ".png");
		}
	}

	public void Draw(Vector3 position, string text)
	{
		if (!(editorCamera != null))
		{
			return;
		}
		string text2 = text.ToLower();
		Vector3 vector = editorCamera.WorldToScreenPoint(position);
		int num = 20;
		for (int i = 0; i < text2.Length; i++)
		{
			if (char2TexturePathMapping.ContainsKey(text2[i]))
			{
				Vector3 center = editorCamera.ScreenToWorldPoint(new Vector3(vector.x + (float)num, vector.y, vector.z));
				Gizmos.DrawIcon(center, char2TexturePathMapping[text2[i]]);
				num += 6;
			}
		}
	}
}
