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
		}
	}

	public const int CharTextureHeight = 8;

	public const int CharTextureWidth = 6;

	private const string Characters = "abcdefghijklmnopqrstuvwxyz0123456789 !#%(),.'-;_{}~+=:\\?\"/*";

	private static readonly Dictionary<char, string> unsafeChar2NameMapping;

	private Dictionary<char, string> char2TexturePathMapping;

	private Camera editorCamera;

	public static TextGizmos Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private TextGizmos()
	{
	}

	public void Draw(Vector3 position, string text)
	{
	}
}
