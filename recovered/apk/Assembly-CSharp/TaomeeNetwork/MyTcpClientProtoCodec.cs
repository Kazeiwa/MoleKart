using System;
using System.Net;
using Google.ProtocolBuffers;
using RaceProtocol;
using UnityEngine;

namespace TaomeeNetwork;

public class MyTcpClientProtoCodec
{
	public static int Encode(IMessageLite msg, byte[] buff, int startIndex)
	{
		int num = -1;
		try
		{
			MsgHead msgHead = MsgHead.CreateBuilder().SetMsgTypeName(msg.GetType().Name).SetDeviceId(SystemInfo.deviceUniqueIdentifier)
				.SetDeviceType(2u)
				.Build();
			int num2 = msgHead.SerializedSize + msg.SerializedSize + 12;
			if (buff.Length < num2)
			{
				return -1;
			}
			if (buff.Length < num2 + startIndex)
			{
				return 0;
			}
			CodedOutputStream codedOutputStream = CodedOutputStream.CreateInstance(buff, startIndex + 8, num2 - 12);
			msgHead.WriteTo(codedOutputStream);
			int num3 = num2 - 12 - codedOutputStream.SpaceLeft;
			if (num3 != msgHead.SerializedSize)
			{
				return -1;
			}
			num3 += 4;
			byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num3));
			Array.Copy(bytes, 0, buff, startIndex + 4, 4);
			msg.WriteTo(codedOutputStream);
			if (codedOutputStream.SpaceLeft != 0)
			{
				return -1;
			}
			byte[] bytes2 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(num2));
			Array.Copy(bytes2, 0, buff, startIndex, 4);
			int host = CheckSum(buff, startIndex, num2 - 4);
			byte[] bytes3 = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
			Array.Copy(bytes3, 0, buff, startIndex + num2 - 4, 4);
			return num2;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static int Decode(byte[] package, int startIndex, int length, ref MsgHead msgHead, ref IMessageLite msg)
	{
		if (length < 6)
		{
			return -1;
		}
		int network = BitConverter.ToInt32(package, startIndex);
		network = IPAddress.NetworkToHostOrder(network);
		if (network < 6 || network > length)
		{
			return -1;
		}
		int network2 = BitConverter.ToInt32(package, startIndex + network - 4);
		network2 = IPAddress.NetworkToHostOrder(network2);
		int num = CheckSum(package, startIndex, network - 4);
		if (num != network2)
		{
			return -1;
		}
		int network3 = BitConverter.ToInt32(package, startIndex + 4);
		network3 = IPAddress.NetworkToHostOrder(network3);
		CodedInputStream input = CodedInputStream.CreateInstance(package, startIndex + 8, network3 - 4);
		msgHead = MsgHead.ParseFrom(input);
		input = CodedInputStream.CreateInstance(package, startIndex + 4 + network3, network - network3 - 8);
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

	private static int CheckSum(byte[] buff, int startIndex, int lenth)
	{
		int num = 0;
		for (int i = 0; i < lenth / 4; i++)
		{
			int network = BitConverter.ToInt32(buff, startIndex + i * 4);
			network = IPAddress.NetworkToHostOrder(network);
			num += network;
		}
		return num;
	}
}
