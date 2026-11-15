using System.Diagnostics.CodeAnalysis;

namespace Misc;

public class ServerInfoCache
{
	private class Nested
	{
		internal static readonly ServerInfoCache Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
		}
	}

	public const float ServerMessageInterval = 0.3f;

	private CrudeElapsedTimer serverMessageTimer;

	public static ServerInfoCache Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private ServerInfoCache()
	{
	}

	public void Update(float deltaTime)
	{
	}

	public void UpdateTimer(float deltaTime)
	{
	}

	private void Send()
	{
	}
}
