using System.Diagnostics.CodeAnalysis;

public class TSingleton<T> where T : class, new()
{
	private class Nested
	{
		internal static readonly T Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new T();
		}
	}

	public static T Instance => Nested.Instance;
}
