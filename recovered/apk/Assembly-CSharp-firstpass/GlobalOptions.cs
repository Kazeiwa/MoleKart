using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[ExecuteInEditMode]
public class GlobalOptions
{
	private class Nested
	{
		internal static readonly GlobalOptions Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new GlobalOptions();
		}
	}

	public static GlobalOptions Instance => Nested.Instance;

	public bool ShouldDrawRaceLine => true;

	public bool ShouldDrawWallLine => false;

	private GlobalOptions()
	{
	}
}
