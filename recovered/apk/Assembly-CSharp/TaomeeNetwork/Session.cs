using System;
using Google.ProtocolBuffers;
using RaceProtocol;
using UnityEngine;

namespace TaomeeNetwork;

public class Session : ISession
{
	private SendBuf availableBuf = new SendBuf();

	private SendBuf unavailableBuf = new SendBuf();

	private string[] peers = new string[1];

	private string m_peerId;

	private string m_peerName;

	public bool Send(IMessageLite msg, bool reliable)
	{
		try
		{
			if (msg is MsgUseProp)
			{
				MsgUseProp msgUseProp = msg as MsgUseProp;
				Debug.Log("send MsgUseProp carid=" + msgUseProp.CarID + " propType=" + msgUseProp.PropType);
			}
			SendBuf sendBuf = null;
			sendBuf = ((!reliable) ? unavailableBuf : availableBuf);
			string myselfPeerId = LocalNetFactory.Instance.LocalNet.GetMyselfPeerId();
			int num = 0;
			do
			{
				num = ProtoCodec.Encode(msg, sendBuf.buff, sendBuf.pos);
				if (num == 0 && sendBuf.pos > 0)
				{
					LocalNetFactory.Instance.LocalNet.SendRawBytesToPeers(peers, sendBuf.buff, sendBuf.pos, reliable);
					sendBuf.pos = 0;
				}
				if (num < 0)
				{
					return false;
				}
				SessionManager.sendedSize += num;
			}
			while (num == 0 && sendBuf.pos == 0);
			sendBuf.pos += num;
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		return true;
	}

	public void EndSend()
	{
		if (availableBuf.pos > 0)
		{
			LocalNetFactory.Instance.LocalNet.SendRawBytesToPeers(peers, availableBuf.buff, availableBuf.pos, reliable: true);
			availableBuf.pos = 0;
		}
	}

	public void EndUnreliableSend()
	{
		if (unavailableBuf.pos > 0)
		{
			LocalNetFactory.Instance.LocalNet.SendRawBytesToPeers(peers, unavailableBuf.buff, unavailableBuf.pos, reliable: false);
			unavailableBuf.pos = 0;
		}
	}

	public void OnRecv(byte[] buff, int lenth)
	{
	}

	public void SetPeerID(string peerId)
	{
		m_peerId = peerId;
		peers[0] = peerId;
	}

	public string GetPeerID()
	{
		return m_peerId;
	}

	public void SetPeerName(string peerName)
	{
		m_peerName = peerName;
	}

	public string GetPeerName()
	{
		return m_peerName;
	}
}
