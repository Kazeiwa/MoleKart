using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class UdpBroadCastServer : GeneratedMessageLite<UdpBroadCastServer, UdpBroadCastServer.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<UdpBroadCastServer, Builder>
	{
		private UdpBroadCastServer result = new UdpBroadCastServer();

		protected override Builder ThisBuilder => this;

		protected override UdpBroadCastServer MessageBeingBuilt => result;

		public override UdpBroadCastServer DefaultInstanceForType => DefaultInstance;

		public bool HasPeerDisplayName => result.HasPeerDisplayName;

		public string PeerDisplayName
		{
			get
			{
				return result.PeerDisplayName;
			}
			set
			{
				SetPeerDisplayName(value);
			}
		}

		public bool HasServerIp => result.HasServerIp;

		public string ServerIp
		{
			get
			{
				return result.ServerIp;
			}
			set
			{
				SetServerIp(value);
			}
		}

		public bool HasServerport => result.HasServerport;

		public int Serverport
		{
			get
			{
				return result.Serverport;
			}
			set
			{
				SetServerport(value);
			}
		}

		public override Builder Clear()
		{
			result = new UdpBroadCastServer();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override UdpBroadCastServer BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			UdpBroadCastServer udpBroadCastServer = result;
			result = null;
			return udpBroadCastServer;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is UdpBroadCastServer)
			{
				return MergeFrom((UdpBroadCastServer)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(UdpBroadCastServer other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPeerDisplayName)
			{
				PeerDisplayName = other.PeerDisplayName;
			}
			if (other.HasServerIp)
			{
				ServerIp = other.ServerIp;
			}
			if (other.HasServerport)
			{
				Serverport = other.Serverport;
			}
			return this;
		}

		public override Builder MergeFrom(CodedInputStream input)
		{
			return MergeFrom(input, ExtensionRegistry.Empty);
		}

		public override Builder MergeFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			while (true)
			{
				uint num = input.ReadTag();
				switch (num)
				{
				case 0u:
					return this;
				case 10u:
					PeerDisplayName = input.ReadString();
					continue;
				case 18u:
					ServerIp = input.ReadString();
					continue;
				case 24u:
					Serverport = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPeerDisplayName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPeerDisplayName = true;
			result.peerDisplayName_ = value;
			return this;
		}

		public Builder ClearPeerDisplayName()
		{
			result.hasPeerDisplayName = false;
			result.peerDisplayName_ = string.Empty;
			return this;
		}

		public Builder SetServerIp(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasServerIp = true;
			result.serverIp_ = value;
			return this;
		}

		public Builder ClearServerIp()
		{
			result.hasServerIp = false;
			result.serverIp_ = string.Empty;
			return this;
		}

		public Builder SetServerport(int value)
		{
			result.hasServerport = true;
			result.serverport_ = value;
			return this;
		}

		public Builder ClearServerport()
		{
			result.hasServerport = false;
			result.serverport_ = 0;
			return this;
		}
	}

	public const int PeerDisplayNameFieldNumber = 1;

	public const int ServerIpFieldNumber = 2;

	public const int ServerportFieldNumber = 3;

	private static readonly UdpBroadCastServer defaultInstance;

	private bool hasPeerDisplayName;

	private string peerDisplayName_ = string.Empty;

	private bool hasServerIp;

	private string serverIp_ = string.Empty;

	private bool hasServerport;

	private int serverport_;

	private int memoizedSerializedSize = -1;

	public static UdpBroadCastServer DefaultInstance => defaultInstance;

	public override UdpBroadCastServer DefaultInstanceForType => defaultInstance;

	protected override UdpBroadCastServer ThisMessage => this;

	public bool HasPeerDisplayName => hasPeerDisplayName;

	public string PeerDisplayName => peerDisplayName_;

	public bool HasServerIp => hasServerIp;

	public string ServerIp => serverIp_;

	public bool HasServerport => hasServerport;

	public int Serverport => serverport_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPeerDisplayName)
			{
				return false;
			}
			if (!hasServerIp)
			{
				return false;
			}
			if (!hasServerport)
			{
				return false;
			}
			return true;
		}
	}

	public override int SerializedSize
	{
		get
		{
			int num = memoizedSerializedSize;
			if (num != -1)
			{
				return num;
			}
			num = 0;
			if (HasPeerDisplayName)
			{
				num += CodedOutputStream.ComputeStringSize(1, PeerDisplayName);
			}
			if (HasServerIp)
			{
				num += CodedOutputStream.ComputeStringSize(2, ServerIp);
			}
			if (HasServerport)
			{
				num += CodedOutputStream.ComputeInt32Size(3, Serverport);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static UdpBroadCastServer()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPeerDisplayName)
		{
			output.WriteString(1, PeerDisplayName);
		}
		if (HasServerIp)
		{
			output.WriteString(2, ServerIp);
		}
		if (HasServerport)
		{
			output.WriteInt32(3, Serverport);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPeerDisplayName)
		{
			num ^= peerDisplayName_.GetHashCode();
		}
		if (hasServerIp)
		{
			num ^= serverIp_.GetHashCode();
		}
		if (hasServerport)
		{
			num ^= serverport_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is UdpBroadCastServer udpBroadCastServer))
		{
			return false;
		}
		if (hasPeerDisplayName != udpBroadCastServer.hasPeerDisplayName || (hasPeerDisplayName && !peerDisplayName_.Equals(udpBroadCastServer.peerDisplayName_)))
		{
			return false;
		}
		if (hasServerIp != udpBroadCastServer.hasServerIp || (hasServerIp && !serverIp_.Equals(udpBroadCastServer.serverIp_)))
		{
			return false;
		}
		if (hasServerport != udpBroadCastServer.hasServerport || (hasServerport && !serverport_.Equals(udpBroadCastServer.serverport_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<UdpBroadCastServer, Builder>.PrintField("peerDisplayName", hasPeerDisplayName, peerDisplayName_, writer);
		GeneratedMessageLite<UdpBroadCastServer, Builder>.PrintField("serverIp", hasServerIp, serverIp_, writer);
		GeneratedMessageLite<UdpBroadCastServer, Builder>.PrintField("serverport", hasServerport, serverport_, writer);
	}

	public static UdpBroadCastServer ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static UdpBroadCastServer ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static UdpBroadCastServer ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static UdpBroadCastServer ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static Builder CreateBuilder()
	{
		return new Builder();
	}

	public override Builder ToBuilder()
	{
		return CreateBuilder(this);
	}

	public override Builder CreateBuilderForType()
	{
		return new Builder();
	}

	public static Builder CreateBuilder(UdpBroadCastServer prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
