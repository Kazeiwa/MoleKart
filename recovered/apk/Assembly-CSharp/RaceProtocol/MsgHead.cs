using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgHead : GeneratedMessageLite<MsgHead, MsgHead.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgHead, Builder>
	{
		private MsgHead result = new MsgHead();

		protected override Builder ThisBuilder => this;

		protected override MsgHead MessageBeingBuilt => result;

		public override MsgHead DefaultInstanceForType => DefaultInstance;

		public bool HasMsgTypeName => result.HasMsgTypeName;

		public string MsgTypeName
		{
			get
			{
				return result.MsgTypeName;
			}
			set
			{
				SetMsgTypeName(value);
			}
		}

		public bool HasDeviceId => result.HasDeviceId;

		public string DeviceId
		{
			get
			{
				return result.DeviceId;
			}
			set
			{
				SetDeviceId(value);
			}
		}

		public bool HasDeviceType => result.HasDeviceType;

		[CLSCompliant(false)]
		public uint DeviceType
		{
			get
			{
				return result.DeviceType;
			}
			set
			{
				SetDeviceType(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgHead();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgHead BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgHead msgHead = result;
			result = null;
			return msgHead;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgHead)
			{
				return MergeFrom((MsgHead)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgHead other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasMsgTypeName)
			{
				MsgTypeName = other.MsgTypeName;
			}
			if (other.HasDeviceId)
			{
				DeviceId = other.DeviceId;
			}
			if (other.HasDeviceType)
			{
				DeviceType = other.DeviceType;
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
					MsgTypeName = input.ReadString();
					continue;
				case 18u:
					DeviceId = input.ReadString();
					continue;
				case 24u:
					DeviceType = input.ReadUInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetMsgTypeName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasMsgTypeName = true;
			result.msgTypeName_ = value;
			return this;
		}

		public Builder ClearMsgTypeName()
		{
			result.hasMsgTypeName = false;
			result.msgTypeName_ = string.Empty;
			return this;
		}

		public Builder SetDeviceId(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasDeviceId = true;
			result.deviceId_ = value;
			return this;
		}

		public Builder ClearDeviceId()
		{
			result.hasDeviceId = false;
			result.deviceId_ = string.Empty;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetDeviceType(uint value)
		{
			result.hasDeviceType = true;
			result.deviceType_ = value;
			return this;
		}

		public Builder ClearDeviceType()
		{
			result.hasDeviceType = false;
			result.deviceType_ = 0u;
			return this;
		}
	}

	public const int MsgTypeNameFieldNumber = 1;

	public const int DeviceIdFieldNumber = 2;

	public const int DeviceTypeFieldNumber = 3;

	private static readonly MsgHead defaultInstance;

	private bool hasMsgTypeName;

	private string msgTypeName_ = string.Empty;

	private bool hasDeviceId;

	private string deviceId_ = string.Empty;

	private bool hasDeviceType;

	private uint deviceType_;

	private int memoizedSerializedSize = -1;

	public static MsgHead DefaultInstance => defaultInstance;

	public override MsgHead DefaultInstanceForType => defaultInstance;

	protected override MsgHead ThisMessage => this;

	public bool HasMsgTypeName => hasMsgTypeName;

	public string MsgTypeName => msgTypeName_;

	public bool HasDeviceId => hasDeviceId;

	public string DeviceId => deviceId_;

	public bool HasDeviceType => hasDeviceType;

	[CLSCompliant(false)]
	public uint DeviceType => deviceType_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasMsgTypeName)
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
			if (HasMsgTypeName)
			{
				num += CodedOutputStream.ComputeStringSize(1, MsgTypeName);
			}
			if (HasDeviceId)
			{
				num += CodedOutputStream.ComputeStringSize(2, DeviceId);
			}
			if (HasDeviceType)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, DeviceType);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgHead()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasMsgTypeName)
		{
			output.WriteString(1, MsgTypeName);
		}
		if (HasDeviceId)
		{
			output.WriteString(2, DeviceId);
		}
		if (HasDeviceType)
		{
			output.WriteUInt32(3, DeviceType);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasMsgTypeName)
		{
			num ^= msgTypeName_.GetHashCode();
		}
		if (hasDeviceId)
		{
			num ^= deviceId_.GetHashCode();
		}
		if (hasDeviceType)
		{
			num ^= deviceType_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgHead msgHead))
		{
			return false;
		}
		if (hasMsgTypeName != msgHead.hasMsgTypeName || (hasMsgTypeName && !msgTypeName_.Equals(msgHead.msgTypeName_)))
		{
			return false;
		}
		if (hasDeviceId != msgHead.hasDeviceId || (hasDeviceId && !deviceId_.Equals(msgHead.deviceId_)))
		{
			return false;
		}
		if (hasDeviceType != msgHead.hasDeviceType || (hasDeviceType && !deviceType_.Equals(msgHead.deviceType_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgHead, Builder>.PrintField("msgTypeName", hasMsgTypeName, msgTypeName_, writer);
		GeneratedMessageLite<MsgHead, Builder>.PrintField("deviceId", hasDeviceId, deviceId_, writer);
		GeneratedMessageLite<MsgHead, Builder>.PrintField("deviceType", hasDeviceType, deviceType_, writer);
	}

	public static MsgHead ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgHead ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgHead ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgHead ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgHead ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgHead ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgHead ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgHead ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgHead ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgHead ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgHead prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
