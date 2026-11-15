using System;
using System.Net;
using Google.ProtocolBuffers;
using RaceProtocol;
using UnityEngine;

namespace TaomeeNetwork;

public class ProtoCodec
{
	public static int Encode(IMessageLite msg, byte[] buff, int startIndex)
	{
		int num = -1;
		try
		{
			MsgHead msgHead = MsgHead.CreateBuilder().SetMsgTypeName(msg.GetType().Name).Build();
			short num2 = (short)(msgHead.SerializedSize + msg.SerializedSize + 6);
			if (buff.Length < num2)
			{
				Debug.Log("buff.Length " + buff.Length + " < totalLen " + num2);
				return -1;
			}
			if (buff.Length < num2 + startIndex)
			{
				return 0;
			}
			CodedOutputStream codedOutputStream = CodedOutputStream.CreateInstance(buff, startIndex + 4, num2 - 6);
			msgHead.WriteTo(codedOutputStream);
			short num3 = (short)(num2 - 6 - codedOutputStream.SpaceLeft);
			if (num3 != msgHead.SerializedSize)
			{
				Debug.Log("headLen " + num3 + " != seriSize " + msgHead.SerializedSize);
				return -1;
			}
			num3 += 2;
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num3));
			Array.Copy(bytes, 0, buff, startIndex + 2, 2);
			msg.WriteTo(codedOutputStream);
			if (codedOutputStream.SpaceLeft != 0)
			{
				Debug.Log("spaceleft != 0");
				return -1;
			}
			byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num2));
			Array.Copy(bytes2, 0, buff, startIndex, 2);
			short host = CheckSum(buff, startIndex, num2 - 2);
			byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
			Array.Copy(bytes3, 0, buff, startIndex + num2 - 2, 2);
			return num2;
		}
		catch (Exception ex)
		{
			Debug.Log(ex);
			throw ex;
		}
	}

	public static int Decode(byte[] package, int startIndex, int length, ref MsgHead msgHead, ref IMessageLite msg)
	{
		if (length < 6)
		{
			return -1;
		}
		short network = BitConverter.ToInt16(package, startIndex);
		network = IPAddress.NetworkToHostOrder(network);
		if (network < 6 || network > length)
		{
			Debug.Log("totalLen " + network + "< length " + length);
			return -1;
		}
		short network2 = BitConverter.ToInt16(package, startIndex + network - 2);
		network2 = IPAddress.NetworkToHostOrder(network2);
		short num = CheckSum(package, startIndex, network - 2);
		if (num != network2)
		{
			Debug.Log("checkSum fail! inBuffCheckSum=" + network2 + " checkSum=" + num);
			return -1;
		}
		short network3 = BitConverter.ToInt16(package, startIndex + 2);
		network3 = IPAddress.NetworkToHostOrder(network3);
		CodedInputStream input = CodedInputStream.CreateInstance(package, startIndex + 4, network3 - 2);
		msgHead = MsgHead.ParseFrom(input);
		input = CodedInputStream.CreateInstance(package, startIndex + 2 + network3, network - network3 - 4);
		Delegate obj = NetHandlerRegistry.Instance.FindHandler(msgHead.MsgTypeName);
		if (obj == null)
		{
			Debug.Log("ProtoCodec.Decode:" + msgHead.MsgTypeName + " handler == null");
			return network;
		}
		Type parameterType = obj.Method.GetParameters()[1].ParameterType;
		IMessageLite defaultMessage = MessageUtil.GetDefaultMessage(parameterType);
		IBuilderLite builderLite = defaultMessage.WeakCreateBuilderForType();
		builderLite.WeakMergeFrom(input);
		msg = builderLite.WeakBuild();
		return network;
	}

	private static short CheckSum(byte[] buff, int startIndex, int lenth)
	{
		int num = 0;
		for (int i = 0; i < lenth / 4; i++)
		{
			num += BitConverter.ToInt32(buff, startIndex + i * 4);
		}
		return (short)num;
	}
}
