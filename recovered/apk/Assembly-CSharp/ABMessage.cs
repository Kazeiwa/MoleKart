public struct ABMessage
{
	public int mMessage;

	public int mTimeStampSent;

	public int mTimeExpired;

	public ABMessageData mData;

	public ABCmdTarget mReceiverInfo;

	public ABMessage(ABMessageData data)
	{
		mMessage = 0;
		mTimeStampSent = 0;
		mTimeExpired = 0;
		mReceiverInfo = null;
		mData = data;
	}

	public ABMessage(ABCmdTarget target, int msg, ref ABMessageData data, int expire)
	{
		mMessage = msg;
		mData = data.clone();
		mReceiverInfo = target;
		mTimeExpired = expire;
		mTimeStampSent = 0;
	}
}
