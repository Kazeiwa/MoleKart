using FrameWork;
using GameNetwork;
using RaceProtocol;
using UnityEngine;

public class SendMessageHandler
{
	private static SendMessageHandler instance;

	public static SendMessageHandler Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new SendMessageHandler();
			}
			return instance;
		}
	}

	private SendMessageHandler()
	{
	}

	public void BroadcastPickProp(Car car, ePropType type)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			PlayerData data = new PlayerData();
			MsgPickProp.Builder builder = new MsgPickProp.Builder();
			builder.PropType = (int)type;
			MsgPickProp message = builder.Build();
			if (GameDataManager.Instance.GetPlayerData((ulong)car.ID, ref data))
			{
				NetworkManager.Instance.SendMessageToPeer(data.peerID, message);
			}
		}
	}

	public void BroadcastUseProp(ePropType type, bool forward, int carID, int propID)
	{
		if (!SystemConfig.Instance.SendPropPackage)
		{
			return;
		}
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
		{
			MsgUseProp.Builder builder = new MsgUseProp.Builder();
			builder.PropType = (int)type;
			builder.CarID = carID;
			if (forward)
			{
				builder.Forward = 1;
			}
			else
			{
				builder.Forward = 0;
			}
			builder.PropID = propID;
			MsgUseProp message = builder.Build();
			NetworkManager.Instance.BroadCastMessage(message, reliable: true);
		}
	}

	public void BroadCastCarState(Car car, EventType type)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
			{
				MsgCarState.Builder builder = new MsgCarState.Builder();
				builder.CarState = (int)type;
				builder.CarID = car.ID;
				builder.YDir = 0f;
				builder.XDir = 0f;
				MsgCarState message = builder.Build();
				NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			}
		}
	}

	public void BroadCastCarHitByRobb(Car car, Vector2 direction)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
			{
				MsgCarState.Builder builder = new MsgCarState.Builder();
				builder.CarState = 11;
				builder.CarID = car.ID;
				builder.XDir = direction.x;
				builder.YDir = direction.y;
				MsgCarState message = builder.Build();
				NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			}
		}
	}

	public void BroadCastFireSphere(Vector3 fromPos, Vector3 destPos, float speed, float time)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode() && raceLogicModule.IsYouServer())
			{
				MsgFireSphere.Builder builder = new MsgFireSphere.Builder();
				builder.Time = time;
				builder.Speed = speed;
				builder.FromX = fromPos.x;
				builder.FromY = fromPos.y;
				builder.FromZ = fromPos.z;
				builder.DestX = destPos.x;
				builder.DestY = destPos.y;
				builder.DestZ = destPos.z;
				MsgFireSphere message = builder.Build();
				NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			}
		}
	}

	public void BroadCastThunderCloundTransfer(Car fromCar, Car destCar)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
			{
				MsgTransferThunderCloud.Builder builder = new MsgTransferThunderCloud.Builder();
				builder.FromCar = fromCar.ID;
				builder.DestCar = destCar.ID;
				MsgTransferThunderCloud message = builder.Build();
				NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			}
		}
	}

	public void BroadPropDestroy(ePropType type, string guid)
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode())
			{
				MsgDestroyProp.Builder builder = new MsgDestroyProp.Builder();
				builder.PropType = (int)type;
				builder.Guid = guid;
				MsgDestroyProp message = builder.Build();
				NetworkManager.Instance.BroadCastMessage(message, reliable: true);
			}
		}
	}

	public void RequestID()
	{
		if (AppMain.Instance.IsClient)
		{
			Debug.Log("---------Client request prop ID from server-----------");
			MsgRequsetID.Builder builder = new MsgRequsetID.Builder();
			builder.Request = 1;
			MsgRequsetID message = builder.Build();
			NetworkManager.Instance.BroadCastMessage(message, reliable: true);
		}
	}

	public void SendPropID(int id, string peerID)
	{
		if (AppMain.Instance.IsServer)
		{
			Debug.Log("---------Server send prop ID to client-----------" + peerID);
			MsgSendID.Builder builder = new MsgSendID.Builder();
			builder.ID = id;
			MsgSendID message = builder.Build();
			NetworkManager.Instance.SendMessageToPeer(peerID, message);
		}
	}
}
