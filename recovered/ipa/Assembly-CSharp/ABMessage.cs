public struct ABMessage
{
	public int mMessage;

	public int mTimeStampSent;

	public int mTimeExpired;

	public ABMessageData mData;

	public ABCmdTarget mReceiverInfo;

	public ABMessage(ABMessageData data)
	{
	}

	public ABMessage(ABCmdTarget target, int msg, ref ABMessageData data, int expire)
	{
	}
}
