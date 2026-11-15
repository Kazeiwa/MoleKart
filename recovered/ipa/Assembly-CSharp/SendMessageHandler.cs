using UnityEngine;

public class SendMessageHandler
{
	private static SendMessageHandler instance;

	public static SendMessageHandler Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private SendMessageHandler()
	{
	}

	public void BroadcastPickProp(Car car, ePropType type)
	{
	}

	public void BroadcastUseProp(ePropType type, bool forward, int carID, int propID)
	{
	}

	public void BroadCastCarState(Car car, EventType type)
	{
	}

	public void BroadCastCarHitByRobb(Car car, Vector2 direction)
	{
	}

	public void BroadCastFireSphere(Vector3 fromPos, Vector3 destPos, float speed, float time)
	{
	}

	public void BroadCastThunderCloundTransfer(Car fromCar, Car destCar)
	{
	}

	public void BroadPropDestroy(ePropType type, string guid)
	{
	}

	public void RequestID()
	{
	}

	public void SendPropID(int id, string peerID)
	{
	}
}
