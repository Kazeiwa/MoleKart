using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayer : GeneratedMessageLite<MsgPlayer, MsgPlayer.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayer, Builder>
	{
		private MsgPlayer result = new MsgPlayer();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayer MessageBeingBuilt => result;

		public override MsgPlayer DefaultInstanceForType => DefaultInstance;

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

		public bool HasPlayerId => result.HasPlayerId;

		public int PlayerId
		{
			get
			{
				return result.PlayerId;
			}
			set
			{
				SetPlayerId(value);
			}
		}

		public bool HasStartPosition => result.HasStartPosition;

		public int StartPosition
		{
			get
			{
				return result.StartPosition;
			}
			set
			{
				SetStartPosition(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPlayer();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayer BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayer msgPlayer = result;
			result = null;
			return msgPlayer;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayer)
			{
				return MergeFrom((MsgPlayer)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayer other)
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
			if (other.HasPlayerId)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HasStartPosition)
			{
				StartPosition = other.StartPosition;
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
					PlayerId = input.ReadInt32();
					continue;
				case 48u:
					StartPosition = input.ReadInt32();
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

		public Builder SetPlayerId(int value)
		{
			result.hasPlayerId = true;
			result.playerId_ = value;
			return this;
		}

		public Builder ClearPlayerId()
		{
			result.hasPlayerId = false;
			result.playerId_ = 0;
			return this;
		}

		public Builder SetStartPosition(int value)
		{
			result.hasStartPosition = true;
			result.startPosition_ = value;
			return this;
		}

		public Builder ClearStartPosition()
		{
			result.hasStartPosition = false;
			result.startPosition_ = 0;
			return this;
		}
	}

	public const int PeerIdFieldNumber = 1;

	public const int NameFieldNumber = 2;

	public const int RoleTypeFieldNumber = 3;

	public const int CarTypeFieldNumber = 4;

	public const int PlayerIdFieldNumber = 5;

	public const int StartPositionFieldNumber = 6;

	private static readonly MsgPlayer defaultInstance;

	private bool hasPeerId;

	private string peerId_ = string.Empty;

	private bool hasName;

	private string name_ = string.Empty;

	private bool hasRoleType;

	private int roleType_;

	private bool hasCarType;

	private int carType_;

	private bool hasPlayerId;

	private int playerId_;

	private bool hasStartPosition;

	private int startPosition_;

	private int memoizedSerializedSize = -1;

	public static MsgPlayer DefaultInstance => defaultInstance;

	public override MsgPlayer DefaultInstanceForType => defaultInstance;

	protected override MsgPlayer ThisMessage => this;

	public bool HasPeerId => hasPeerId;

	public string PeerId => peerId_;

	public bool HasName => hasName;

	public string Name => name_;

	public bool HasRoleType => hasRoleType;

	public int RoleType => roleType_;

	public bool HasCarType => hasCarType;

	public int CarType => carType_;

	public bool HasPlayerId => hasPlayerId;

	public int PlayerId => playerId_;

	public bool HasStartPosition => hasStartPosition;

	public int StartPosition => startPosition_;

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
			if (!hasPlayerId)
			{
				return false;
			}
			if (!hasStartPosition)
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
			if (HasPlayerId)
			{
				num += CodedOutputStream.ComputeInt32Size(5, PlayerId);
			}
			if (HasStartPosition)
			{
				num += CodedOutputStream.ComputeInt32Size(6, StartPosition);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayer()
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
		if (HasPlayerId)
		{
			output.WriteInt32(5, PlayerId);
		}
		if (HasStartPosition)
		{
			output.WriteInt32(6, StartPosition);
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
		if (hasPlayerId)
		{
			num ^= playerId_.GetHashCode();
		}
		if (hasStartPosition)
		{
			num ^= startPosition_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayer msgPlayer))
		{
			return false;
		}
		if (hasPeerId != msgPlayer.hasPeerId || (hasPeerId && !peerId_.Equals(msgPlayer.peerId_)))
		{
			return false;
		}
		if (hasName != msgPlayer.hasName || (hasName && !name_.Equals(msgPlayer.name_)))
		{
			return false;
		}
		if (hasRoleType != msgPlayer.hasRoleType || (hasRoleType && !roleType_.Equals(msgPlayer.roleType_)))
		{
			return false;
		}
		if (hasCarType != msgPlayer.hasCarType || (hasCarType && !carType_.Equals(msgPlayer.carType_)))
		{
			return false;
		}
		if (hasPlayerId != msgPlayer.hasPlayerId || (hasPlayerId && !playerId_.Equals(msgPlayer.playerId_)))
		{
			return false;
		}
		if (hasStartPosition != msgPlayer.hasStartPosition || (hasStartPosition && !startPosition_.Equals(msgPlayer.startPosition_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("peerId", hasPeerId, peerId_, writer);
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("name", hasName, name_, writer);
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("roleType", hasRoleType, roleType_, writer);
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("carType", hasCarType, carType_, writer);
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("playerId", hasPlayerId, playerId_, writer);
		GeneratedMessageLite<MsgPlayer, Builder>.PrintField("startPosition", hasStartPosition, startPosition_, writer);
	}

	public static MsgPlayer ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayer ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayer ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayer ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayer ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayer ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayer ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayer ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayer ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayer ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayer prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
