using System.Collections.Generic;
using UnityEngine;

internal class ABMessageQueue
{
	private static ABMessageQueue instance;

	private List<ABMessage> mHighPriority = new List<ABMessage>();

	private List<ABMessage> mNormalPriority = new List<ABMessage>();

	private List<ABMessage> mLowPriority = new List<ABMessage>();

	~ABMessageQueue()
	{
		mHighPriority.Clear();
		mNormalPriority.Clear();
		mLowPriority.Clear();
	}

	public void insert(ref ABMessage msg, ABMessagePriority msgP)
	{
		switch (msgP)
		{
		case ABMessagePriority.Priority_High:
			insert(ref msg, mHighPriority);
			break;
		case ABMessagePriority.Priority_Normal:
			insert(ref msg, mNormalPriority);
			break;
		case ABMessagePriority.Priority_Low:
			insert(ref msg, mLowPriority);
			break;
		}
	}

	public bool getMessage(ref ABMessage msg)
	{
		int tick = (int)(Time.time * 1000f);
		return getMessage(ref msg, mHighPriority, tick) || getMessage(ref msg, mNormalPriority, tick) || getMessage(ref msg, mLowPriority, tick);
	}

	public static ABMessageQueue getInstance()
	{
		if (instance == null)
		{
			instance = new ABMessageQueue();
		}
		return instance;
	}

	private void insert(ref ABMessage msg, List<ABMessage> msgQueue)
	{
		int num = (int)(1000f * Time.time);
		if (msgQueue.Count > 0)
		{
			List<ABMessage>.Enumerator enumerator = msgQueue.GetEnumerator();
			int num2 = msg.mTimeExpired + num;
			int num3 = 0;
			while (enumerator.MoveNext())
			{
				ABMessage current = enumerator.Current;
				if (current.mTimeExpired + current.mTimeStampSent > num2)
				{
					break;
				}
				num3++;
			}
			msg.mTimeStampSent = num;
			msgQueue.Insert(num3, msg);
		}
		else
		{
			msg.mTimeStampSent = num;
			msgQueue.Add(msg);
		}
	}

	private bool getMessage(ref ABMessage msg, List<ABMessage> msgQueue, int tick)
	{
		if (msgQueue.Count > 0)
		{
			ABMessage aBMessage = msgQueue[0];
			if (tick > aBMessage.mTimeExpired + aBMessage.mTimeStampSent)
			{
				msg = aBMessage;
				msgQueue.RemoveAt(0);
				return true;
			}
		}
		return false;
	}
}
