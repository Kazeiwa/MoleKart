using System;
using System.Collections.Generic;
using Google.ProtocolBuffers;
using RaceProtocol;
using RaceTrack;
using UnityEngine;

namespace TaomeeNetwork;

public class SessionManager : ISessionManager
{
	public Dictionary<string, string> serverDeviceIdToPeers = new Dictionary<string, string>();

	public static long sendedSize;

	public static int recvSize;

	private Dictionary<string, ISession> sessionsList = new Dictionary<string, ISession>();

	public void NetworkAllDisable()
	{
		if (sessionsList.Count > 0)
		{
			Debug.Log("sessionsList NetworkAllDisable");
			MsgOnPeerDisconnect msg = MsgOnPeerDisconnect.CreateBuilder().Build();
			foreach (KeyValuePair<string, ISession> sessions in sessionsList)
			{
				NetHandlerRegistry.Instance.ProcessMsg(sessions.Key, msg);
			}
			sessionsList.Clear();
		}
		else
		{
			MsgNetworkAllDisable msg2 = MsgNetworkAllDisable.CreateBuilder().Build();
			NetHandlerRegistry.Instance.ProcessMsg(null, msg2);
		}
	}

	public void PeerBecameAvailable(string peerID)
	{
		string deviceid = null;
		string displayName = GetDisplayName(peerID, ref deviceid);
		if (displayName != null)
		{
			if (serverDeviceIdToPeers.ContainsKey(deviceid))
			{
				string peerId = serverDeviceIdToPeers[deviceid];
				serverDeviceIdToPeers.Remove(deviceid);
				MsgPeerBecameUnavailable msg = MsgPeerBecameUnavailable.CreateBuilder().Build();
				NetHandlerRegistry.Instance.ProcessMsg(peerId, msg);
			}
			serverDeviceIdToPeers.Add(deviceid, peerID);
			MsgPeerBecameAvailable msg2 = MsgPeerBecameAvailable.CreateBuilder().SetPeerDisplayName(displayName).Build();
			NetHandlerRegistry.Instance.ProcessMsg(peerID, msg2);
		}
	}

	public void PeerBecameUnavailable(string peerID)
	{
		MsgPeerBecameUnavailable msg = MsgPeerBecameUnavailable.CreateBuilder().Build();
		NetHandlerRegistry.Instance.ProcessMsg(peerID, msg);
	}

	public ISession OnCreateSession(string peerID)
	{
		Debug.Log("sessionsList onConnected peerID=" + peerID);
		if (!sessionsList.ContainsKey(peerID))
		{
			string deviceid = null;
			string displayName = GetDisplayName(peerID, ref deviceid);
			ISession session = new Session();
			session.SetPeerID(peerID);
			session.SetPeerName(displayName);
			sessionsList.Add(peerID, session);
			MsgOnPeerConnected msg = MsgOnPeerConnected.CreateBuilder().SetPeerDisplayName(displayName).Build();
			NetHandlerRegistry.Instance.ProcessMsg(peerID, msg);
			return session;
		}
		return null;
	}

	public void OnDisconnectSession(string peerID)
	{
		if (sessionsList.ContainsKey(peerID))
		{
			Debug.Log("sessionsList onDisconnect peerID=" + peerID);
			string deviceid = null;
			string displayName = GetDisplayName(peerID, ref deviceid);
			MsgOnPeerDisconnect msg = MsgOnPeerDisconnect.CreateBuilder().Build();
			NetHandlerRegistry.Instance.ProcessMsg(peerID, msg);
			sessionsList.Remove(peerID);
		}
	}

	public void OnCannotConnected(string peerID)
	{
		MsgCannotConnected msg = MsgCannotConnected.CreateBuilder().Build();
		NetHandlerRegistry.Instance.ProcessMsg(peerID, msg);
	}

	private string GetDisplayName(string peerID, ref string deviceid)
	{
		string displayName = LocalNetFactory.Instance.LocalNet.GetDisplayName(peerID);
		int num = displayName.IndexOf("_-");
		if (num < 0)
		{
			deviceid = null;
			return null;
		}
		string text = displayName.Substring(0, num);
		deviceid = displayName.Substring(num + "_-".Length);
		if (deviceid.Length == 0)
		{
			deviceid = null;
			return null;
		}
		Debug.Log("get displayername=" + text + " deviceid=" + deviceid);
		return text;
	}

	public void SendMonstersMoveList()
	{
		foreach (IMonster monster in RaceWorld.Instance.Monsters)
		{
			if (!monster.ShouldSendMovementInfo)
			{
				continue;
			}
			MsgMonsterMovement msg = MsgMonsterMovement.CreateBuilder().SetX(monster.Position.x).SetY(monster.Position.z)
				.SetH(monster.Position.y)
				.SetVx(monster.Vel.x)
				.SetVy(monster.Vel.y)
				.SetVh(0f)
				.SetRotAngle(monster.RotAngle)
				.SetIndex(monster.IndexInScene)
				.Build();
			foreach (KeyValuePair<string, ISession> sessions in sessionsList)
			{
				bool flag = false;
				uint playerId = GameDataManager.Instance.GetPlayerId(sessions.Key);
				if (playerId != uint.MaxValue)
				{
					Car car = TSingleton<CarManager>.Instance.GetCar((int)playerId);
					if (car != null)
					{
						Vector3 position = monster.Position;
						Vector3 position2 = car.GetPosition();
						Vector3 forwardGlobal = car.GetForwardGlobal();
						Vector3 myPos = position2 - 5f * forwardGlobal;
						if (Vector3.zero != position2)
						{
							flag = Utility.ObjectInSight(myPos, position, forwardGlobal);
						}
					}
				}
				if (flag)
				{
					sessions.Value.Send(msg, available: false);
				}
			}
		}
	}

	public void SendCarsMoves()
	{
		NetMod netMode = LocalNetFactory.Instance.LocalNet.NetMode;
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			uint playerId = GameDataManager.Instance.GetPlayerId(sessions.Key);
			foreach (Car car in TSingleton<CarManager>.Instance.Cars)
			{
				if ((netMode == NetMod.NetClient && car.isHero) || (netMode == NetMod.NetServer && car.ID != playerId))
				{
					Vector3 position = car.GetPosition();
					Vector3 eulerAngles = car.GetRotation().eulerAngles;
					Vector3 vecGlobal = car.GetVecGlobal();
					MsgPlayerData2.Builder builder = MsgPlayerData2.CreateBuilder();
					builder.SetPlayerId(car.ID);
					builder.SetPosX(position.x);
					builder.SetPosY(position.y);
					builder.SetPosZ(position.z);
					builder.SetEulerX(eulerAngles.x);
					builder.SetEulerY(eulerAngles.y);
					builder.SetEulerZ(eulerAngles.z);
					builder.SetVecX(vecGlobal.x);
					builder.SetVecY(vecGlobal.y);
					builder.SetVecZ(vecGlobal.z);
					builder.SetCarState((int)car.carState);
					Vector3 accelate = car.vehicle.GetAccelate();
					Vector3 wecLocal = car.vehicle.GetWecLocal();
					builder.Ax = accelate.x;
					builder.Ay = accelate.y;
					builder.Az = accelate.z;
					builder.Wy = wecLocal.y;
					MsgPlayerData2 msg = builder.Build();
					sessions.Value.Send(msg, available: false);
				}
			}
		}
		LocalNetFactory.Instance.LocalNet.SessionManager.EndUnrelieableSend();
	}

	public bool BroadCastMsgPacket(IMessageLite msg, bool reliable)
	{
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			sessions.Value.Send(msg, reliable);
		}
		return true;
	}

	public bool BroadCastMsgPacketExcept(IMessageLite message, string excludedPeerId, bool reliable)
	{
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			if (sessions.Key != excludedPeerId)
			{
				sessions.Value.Send(message, reliable);
			}
		}
		return true;
	}

	public void EndSend()
	{
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			sessions.Value.EndSend();
		}
	}

	public void EndUnrelieableSend()
	{
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			sessions.Value.EndUnreliableSend();
		}
	}

	public ISession FindSessionByPeerId(string peerId)
	{
		return sessionsList[peerId];
	}

	public void OnReceiveBytes(string peerId, byte[] buff, int index, int recvLen)
	{
		recvSize += recvLen;
		int num = 0;
		int num2 = index;
		while (num2 < recvLen && num < 50)
		{
			num++;
			MsgHead msgHead = null;
			IMessageLite msg = null;
			try
			{
				int num3 = ProtoCodec.Decode(buff, num2, recvLen, ref msgHead, ref msg);
				if (num3 <= 0)
				{
					break;
				}
				num2 += num3;
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
			if (msgHead != null && msg != null)
			{
				if (msg is MsgUseProp)
				{
					MsgUseProp msgUseProp = msg as MsgUseProp;
					Debug.Log("recv MsgUseProp carid=" + msgUseProp.CarID + " propType=" + msgUseProp.PropType);
				}
				if (!sessionsList.ContainsKey(peerId))
				{
					Debug.Log("recv msg=" + msgHead.MsgTypeName + ", but sessionList find null:" + peerId);
				}
				else
				{
					NetHandlerRegistry.Instance.ProcessMsg(peerId, msg);
				}
			}
		}
	}

	public string[] GetConnectedPeers()
	{
		string[] array = new string[sessionsList.Count];
		int num = 0;
		foreach (KeyValuePair<string, ISession> sessions in sessionsList)
		{
			array[num] = sessions.Key;
			num++;
		}
		return array;
	}

	public void Clear()
	{
		sessionsList.Clear();
		sendedSize = 0L;
		recvSize = 0;
		serverDeviceIdToPeers.Clear();
	}
}
