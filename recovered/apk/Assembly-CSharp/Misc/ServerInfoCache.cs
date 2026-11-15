using System.Diagnostics.CodeAnalysis;
using TaomeeNetwork;

namespace Misc;

public class ServerInfoCache
{
	private class Nested
	{
		internal static readonly ServerInfoCache Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new ServerInfoCache();
		}
	}

	public const float ServerMessageInterval = 0.3f;

	private CrudeElapsedTimer serverMessageTimer;

	public static ServerInfoCache Instance => Nested.Instance;

	private ServerInfoCache()
	{
		serverMessageTimer = new CrudeElapsedTimer(0.3f);
	}

	public void Update(float deltaTime)
	{
		UpdateTimer(deltaTime);
		if (serverMessageTimer.TimeOutCount > 0)
		{
			Send();
			serverMessageTimer.ResetTimeOutCount();
		}
	}

	public void UpdateTimer(float deltaTime)
	{
		serverMessageTimer.Advance(deltaTime);
	}

	private void Send()
	{
		LocalNetFactory.Instance.LocalNet.SessionManager.SendMonstersMoveList();
	}
}
