using FrameWork;
using GameNetwork;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

public class ReceiveMessageHandler
{
	private static ReceiveMessageHandler instance;

	private Handler<MsgPickProp> m_PickPropHandler;

	private Handler<MsgUseProp> m_UsePropHandler;

	private Handler<MsgCarState> m_CarStateHandler;

	private Handler<MsgFireSphere> m_FireSphereHandler;

	private Handler<MsgTransferThunderCloud> m_TransferThunderHandler;

	private Handler<MsgDestroyProp> m_DestroyPropHandler;

	public static ReceiveMessageHandler Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ReceiveMessageHandler();
			}
			return instance;
		}
	}

	private ReceiveMessageHandler()
	{
	}

	public void Init()
	{
		if (SystemConfig.Instance.SendPropPackage)
		{
			NetHandlerRegistry.Instance.RegistHandler<MsgPickProp>(OnRecvPickPropMsg);
			NetHandlerRegistry.Instance.RegistHandler<MsgUseProp>(OnRecvUsePropMsg);
			NetHandlerRegistry.Instance.RegistHandler<MsgCarState>(OnRecvCarState);
			NetHandlerRegistry.Instance.RegistHandler<MsgFireSphere>(OnRecvFireSphere);
			NetHandlerRegistry.Instance.RegistHandler<MsgTransferThunderCloud>(OnRecvTransferThunder);
			NetHandlerRegistry.Instance.RegistHandler<MsgDestroyProp>(OnRecvDestroyProp);
			NetHandlerRegistry.Instance.RegistHandler<MsgRequsetID>(OnRequestID);
			NetHandlerRegistry.Instance.RegistHandler<MsgSendID>(ReceiveID);
		}
	}

	public void UnInit()
	{
		Debug.Log("UnInit----> ReceiveMesssageHandler");
		NetHandlerRegistry.Instance.UnregistHandler<MsgPickProp>(OnRecvPickPropMsg);
		NetHandlerRegistry.Instance.UnregistHandler<MsgUseProp>(OnRecvUsePropMsg);
		NetHandlerRegistry.Instance.UnregistHandler<MsgCarState>(OnRecvCarState);
		NetHandlerRegistry.Instance.UnregistHandler<MsgFireSphere>(OnRecvFireSphere);
		NetHandlerRegistry.Instance.UnregistHandler<MsgTransferThunderCloud>(OnRecvTransferThunder);
		NetHandlerRegistry.Instance.UnregistHandler<MsgDestroyProp>(OnRecvDestroyProp);
		NetHandlerRegistry.Instance.UnregistHandler<MsgRequsetID>(OnRequestID);
		NetHandlerRegistry.Instance.UnregistHandler<MsgSendID>(ReceiveID);
	}

	private void OnRecvPickPropMsg(string peerId, MsgPickProp msg)
	{
		Debug.Log("OnRecvPickPropMsg [" + msg.PropType + "]");
		PlayerData data = new PlayerData();
		Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
		if (null != heroCar && msg != null && RaceSceneData.GetPlayerData((ulong)heroCar.ID, ref data) && data.selProp == ePropType.NONE && data.altProp == ePropType.NONE)
		{
			ePropType propType = (ePropType)msg.PropType;
			SceneUIManager.Instance.RollPropUI(propType);
		}
	}

	private void OnRecvUsePropMsg(string peerId, MsgUseProp msg)
	{
		Debug.Log("OnRecvUsePropMsg [" + msg.PropType + "]");
		if (msg == null)
		{
			return;
		}
		Car car = TSingleton<CarManager>.Instance.GetCar(msg.CarID);
		if (null != car)
		{
			PlayerData data = new PlayerData();
			if (RaceSceneData.GetPlayerData((ulong)msg.CarID, ref data))
			{
				data.selProp = (ePropType)msg.PropType;
				RaceSceneData.SetPlayerData((ulong)msg.CarID, data);
			}
			bool throwForward = msg.Forward == 1;
			PropIDManager.Instance.m_OtherCarUsePropID = msg.PropID;
			SkillManager.Instance.UseProp(car, throwForward);
		}
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsYouServer())
		{
			NetworkManager.Instance.BroadCastMessageExcept(msg, peerId, reliable: true);
		}
	}

	private void OnRecvCarState(string peerId, MsgCarState msg)
	{
		Debug.Log("OnRecvCarState [" + msg.CarState + "]");
		if (msg == null)
		{
			return;
		}
		Car car = TSingleton<CarManager>.Instance.GetCar(msg.CarID);
		if (!(null != car))
		{
			return;
		}
		switch ((EventType)msg.CarState)
		{
		case EventType.ToRotate:
			car.carActor.HitByBanana();
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToRotate);
			}
			break;
		case EventType.ToUpBound:
			car.carActor.HitByTortoise();
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToUpBound);
			}
			break;
		case EventType.ToRightBound:
			car.carActor.HitByMissile(right: true);
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToRightBound);
			}
			break;
		case EventType.ToLeftBound:
			car.carActor.HitByMissile(right: false);
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToLeftBound);
			}
			break;
		case EventType.ToBigBound:
			car.m_ForwardHitByRobb.y = msg.YDir;
			car.m_ForwardHitByRobb.x = msg.XDir;
			car.carActor.HitByRobb();
			if (AppMain.Instance.IsServer)
			{
				if (msg.YDir == 0f && msg.XDir == 0f)
				{
					SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToBigBound);
					break;
				}
				Vector2 direction = new Vector2(msg.XDir, msg.YDir);
				SendMessageHandler.Instance.BroadCastCarHitByRobb(car, direction);
			}
			break;
		case EventType.ToScaleSmaller:
			car.carActor.HitByLightning();
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToScaleSmaller);
			}
			break;
		case EventType.ToScaleFlat:
			car.carActor.PressByBigCar();
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToScaleFlat);
			}
			break;
		case EventType.ToBackFlip:
			car.carActor.BackFlip();
			if (AppMain.Instance.IsServer)
			{
				SendMessageHandler.Instance.BroadCastCarState(car, EventType.ToBackFlip);
			}
			break;
		case EventType.ToMissile:
		case EventType.ToAccelerate:
		case EventType.ToInvincible:
		case EventType.ToScaleBigger:
		case EventType.ToQuake:
		case EventType.ToThunderCloud:
		case EventType.ToLeafShelter:
		case EventType.ToDead:
			break;
		}
	}

	private void OnRecvFireSphere(string peerID, MsgFireSphere msg)
	{
		if (msg != null)
		{
			Vector3 fromPos = new Vector3(msg.FromX, msg.FromY, msg.FromZ);
			Vector3 destPos = new Vector3(msg.DestX, msg.DestY, msg.DestZ);
			SkillManager.Instance.FireSphere(fromPos, destPos, msg.Speed, msg.Time);
		}
	}

	private void OnRecvTransferThunder(string peerID, MsgTransferThunderCloud msg)
	{
		if (msg == null)
		{
			return;
		}
		Car car = TSingleton<CarManager>.Instance.GetCar(msg.FromCar);
		Car car2 = TSingleton<CarManager>.Instance.GetCar(msg.DestCar);
		if (null != car && null != car2 && car != car2)
		{
			car.ThunderTransfer(car2);
			IFrameWork frameWork = CFrameWork.GetFrameWork();
			if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule && raceLogicModule.IsOnlineMode() && raceLogicModule.IsYouServer())
			{
				SendMessageHandler.Instance.BroadCastThunderCloundTransfer(car, car2);
			}
		}
	}

	private void OnRecvDestroyProp(string peerID, MsgDestroyProp msg)
	{
		if (msg != null)
		{
			TSingleton<PropManager>.Instance.DestroyProp((ePropType)msg.PropType, msg.Guid);
			if (AppMain.Instance.IsServer)
			{
				NetworkManager.Instance.BroadCastMessageExcept(msg, peerID, reliable: true);
			}
		}
	}

	private void OnRequestID(string peerID, MsgRequsetID msg)
	{
		if (AppMain.Instance.IsServer)
		{
			Debug.Log("------------Server receive client request prop ID-------" + peerID);
			SendMessageHandler.Instance.SendPropID(PropIDManager.Instance.GetID(TSingleton<CarManager>.Instance.GetHeroCar()), peerID);
		}
	}

	private void ReceiveID(string peerID, MsgSendID msg)
	{
		if (AppMain.Instance.IsClient)
		{
			Debug.Log("--------Client receive prop id from server---------");
			if (msg != null)
			{
				PropIDManager.Instance.SetID(msg.ID);
			}
		}
	}
}
