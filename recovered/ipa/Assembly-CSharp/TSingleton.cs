using System.Diagnostics.CodeAnalysis;

public class TSingleton<T> where T : class, new()
{
	private class Nested
	{
		internal static readonly T Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
		}
	}

	public static T Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}
}
