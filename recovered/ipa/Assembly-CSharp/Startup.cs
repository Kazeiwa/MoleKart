[MessageClass(typeof(Startup))]
public class Startup : ABCmdBehaviour
{
	private static AppMain instance;

	private static bool appInfoReported;

	private void Awake()
	{
	}

	[MessageFun(typeof(Startup))]
	public override void processMsg(int msg, ref ABMessageData data)
	{
	}
}
