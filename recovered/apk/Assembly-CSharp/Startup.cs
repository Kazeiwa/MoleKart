using System;
using Misc;
using UnityEngine;

[MessageClass(typeof(Startup))]
public class Startup : ABCmdBehaviour
{
	private static AppMain instance;

	public string channel;

	public string currVersion;

	private void Awake()
	{
		UnityEngine.Random.seed = Environment.TickCount;
		CheapMathf.Init();
		MessageFun<ABCmdBehaviour>(typeof(Startup), processMsg);
		instance = AppMain.Instance;
		instance.Init();
		instance.channel = channel;
		instance.currVersion = currVersion;
	}

	[MessageFun(typeof(Startup))]
	public override void processMsg(int msg, ref ABMessageData data)
	{
		if (msg == 0)
		{
		}
	}
}
