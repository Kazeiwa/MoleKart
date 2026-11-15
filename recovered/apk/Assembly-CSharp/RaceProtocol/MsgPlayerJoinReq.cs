using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayerJoinReq : GeneratedMessageLite<MsgPlayerJoinReq, MsgPlayerJoinReq.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayerJoinReq, Builder>
	{
		private MsgPlayerJoinReq result = new MsgPlayerJoinReq();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayerJoinReq MessageBeingBuilt => result;

		public override MsgPlayerJoinReq DefaultInstanceForType => DefaultInstance;

		public bool HasPeerId => result.HasPeerId;

		public string PeerId
		{
			get
			{
				return result.PeerId;
			}
			set
			{
				SetPeerId(value);
			}
		}

		public bool HasName => result.HasName;

		public string Name
		{
			get
			{
				return result.Name;
			}
			set
			{
				SetName(value);
			}
		}

		public bool HasRoleType => result.HasRoleType;

		public int RoleType
		{
			get
			{
				return result.RoleType;
			}
			set
			{
				SetRoleType(value);
			}
		}

		public bool HasCarType => result.HasCarType;

		public int CarType
		{
			get
			{
				return result.CarType;
			}
			set
			{
				SetCarType(value);
			}
		}

		public bool HasVersion => result.HasVersion;

		public int Version
		{
			get
			{
				return result.Version;
			}
			set
			{
				SetVersion(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPlayerJoinReq();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayerJoinReq BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayerJoinReq msgPlayerJoinReq = result;
			result = null;
			return msgPlayerJoinReq;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayerJoinReq)
			{
				return MergeFrom((MsgPlayerJoinReq)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayerJoinReq other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPeerId)
			{
				PeerId = other.PeerId;
			}
			if (other.HasName)
			{
				Name = other.Name;
			}
			if (other.HasRoleType)
			{
				RoleType = other.RoleType;
			}
			if (other.HasCarType)
			{
				CarType = other.CarType;
			}
			if (other.HasVersion)
			{
				Version = other.Version;
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
					PeerId = input.ReadString();
					continue;
				case 18u:
					Name = input.ReadString();
					continue;
				case 24u:
					RoleType = input.ReadInt32();
					continue;
				case 32u:
					CarType = input.ReadInt32();
					continue;
				case 40u:
					Version = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPeerId(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPeerId = true;
			result.peerId_ = value;
			return this;
		}

		public Builder ClearPeerId()
		{
			result.hasPeerId = false;
			result.peerId_ = string.Empty;
			return this;
		}

		public Builder SetName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasName = true;
			result.name_ = value;
			return this;
		}

		public Builder ClearName()
		{
			result.hasName = false;
			result.name_ = string.Empty;
			return this;
		}

		public Builder SetRoleType(int value)
		{
			result.hasRoleType = true;
			result.roleType_ = value;
			return this;
		}

		public Builder ClearRoleType()
		{
			result.hasRoleType = false;
			result.roleType_ = 0;
			return this;
		}

		public Builder SetCarType(int value)
		{
			result.hasCarType = true;
			result.carType_ = value;
			return this;
		}

		public Builder ClearCarType()
		{
			result.hasCarType = false;
			result.carType_ = 0;
			return this;
		}

		public Builder SetVersion(int value)
		{
			result.hasVersion = true;
			result.version_ = value;
			return this;
		}

		public Builder ClearVersion()
		{
			result.hasVersion = false;
			result.version_ = 0;
			return this;
		}
	}

	public const int PeerIdFieldNumber = 1;

	public const int NameFieldNumber = 2;

	public const int RoleTypeFieldNumber = 3;

	public const int CarTypeFieldNumber = 4;

	public const int VersionFieldNumber = 5;

	private static readonly MsgPlayerJoinReq defaultInstance;

	private bool hasPeerId;

	private string peerId_ = string.Empty;

	private bool hasName;

	private string name_ = string.Empty;

	private bool hasRoleType;

	private int roleType_;

	private bool hasCarType;

	private int carType_;

	private bool hasVersion;

	private int version_;

	private int memoizedSerializedSize = -1;

	public static MsgPlayerJoinReq DefaultInstance => defaultInstance;

	public override MsgPlayerJoinReq DefaultInstanceForType => defaultInstance;

	protected override MsgPlayerJoinReq ThisMessage => this;

	public bool HasPeerId => hasPeerId;

	public string PeerId => peerId_;

	public bool HasName => hasName;

	public string Name => name_;

	public bool HasRoleType => hasRoleType;

	public int RoleType => roleType_;

	public bool HasCarType => hasCarType;

	public int CarType => carType_;

	public bool HasVersion => hasVersion;

	public int Version => version_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPeerId)
			{
				return false;
			}
			if (!hasName)
			{
				return false;
			}
			if (!hasRoleType)
			{
				return false;
			}
			if (!hasCarType)
			{
				return false;
			}
			if (!hasVersion)
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
			if (HasPeerId)
			{
				num += CodedOutputStream.ComputeStringSize(1, PeerId);
			}
			if (HasName)
			{
				num += CodedOutputStream.ComputeStringSize(2, Name);
			}
			if (HasRoleType)
			{
				num += CodedOutputStream.ComputeInt32Size(3, RoleType);
			}
			if (HasCarType)
			{
				num += CodedOutputStream.ComputeInt32Size(4, CarType);
			}
			if (HasVersion)
			{
				num += CodedOutputStream.ComputeInt32Size(5, Version);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayerJoinReq()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPeerId)
		{
			output.WriteString(1, PeerId);
		}
		if (HasName)
		{
			output.WriteString(2, Name);
		}
		if (HasRoleType)
		{
			output.WriteInt32(3, RoleType);
		}
		if (HasCarType)
		{
			output.WriteInt32(4, CarType);
		}
		if (HasVersion)
		{
			output.WriteInt32(5, Version);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPeerId)
		{
			num ^= peerId_.GetHashCode();
		}
		if (hasName)
		{
			num ^= name_.GetHashCode();
		}
		if (hasRoleType)
		{
			num ^= roleType_.GetHashCode();
		}
		if (hasCarType)
		{
			num ^= carType_.GetHashCode();
		}
		if (hasVersion)
		{
			num ^= version_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayerJoinReq msgPlayerJoinReq))
		{
			return false;
		}
		if (hasPeerId != msgPlayerJoinReq.hasPeerId || (hasPeerId && !peerId_.Equals(msgPlayerJoinReq.peerId_)))
		{
			return false;
		}
		if (hasName != msgPlayerJoinReq.hasName || (hasName && !name_.Equals(msgPlayerJoinReq.name_)))
		{
			return false;
		}
		if (hasRoleType != msgPlayerJoinReq.hasRoleType || (hasRoleType && !roleType_.Equals(msgPlayerJoinReq.roleType_)))
		{
			return false;
		}
		if (hasCarType != msgPlayerJoinReq.hasCarType || (hasCarType && !carType_.Equals(msgPlayerJoinReq.carType_)))
		{
			return false;
		}
		if (hasVersion != msgPlayerJoinReq.hasVersion || (hasVersion && !version_.Equals(msgPlayerJoinReq.version_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayerJoinReq, Builder>.PrintField("peerId", hasPeerId, peerId_, writer);
		GeneratedMessageLite<MsgPlayerJoinReq, Builder>.PrintField("name", hasName, name_, writer);
		GeneratedMessageLite<MsgPlayerJoinReq, Builder>.PrintField("roleType", hasRoleType, roleType_, writer);
		GeneratedMessageLite<MsgPlayerJoinReq, Builder>.PrintField("carType", hasCarType, carType_, writer);
		GeneratedMessageLite<MsgPlayerJoinReq, Builder>.PrintField("version", hasVersion, version_, writer);
	}

	public static MsgPlayerJoinReq ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerJoinReq ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayerJoinReq prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
