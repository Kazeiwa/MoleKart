using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgRank : GeneratedMessageLite<MsgRank, MsgRank.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgRank, Builder>
	{
		private MsgRank result = new MsgRank();

		protected override Builder ThisBuilder => this;

		protected override MsgRank MessageBeingBuilt => result;

		public override MsgRank DefaultInstanceForType => DefaultInstance;

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

		public bool HasRank => result.HasRank;

		public int Rank
		{
			get
			{
				return result.Rank;
			}
			set
			{
				SetRank(value);
			}
		}

		public bool HasTime => result.HasTime;

		public float Time
		{
			get
			{
				return result.Time;
			}
			set
			{
				SetTime(value);
			}
		}

		public bool HasIsFinish => result.HasIsFinish;

		public int IsFinish
		{
			get
			{
				return result.IsFinish;
			}
			set
			{
				SetIsFinish(value);
			}
		}

		public bool HasID => result.HasID;

		public int ID
		{
			get
			{
				return result.ID;
			}
			set
			{
				SetID(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgRank();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgRank BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgRank msgRank = result;
			result = null;
			return msgRank;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgRank)
			{
				return MergeFrom((MsgRank)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgRank other)
		{
			if (other == DefaultInstance)
			{
				return this;
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
			if (other.HasRank)
			{
				Rank = other.Rank;
			}
			if (other.HasTime)
			{
				Time = other.Time;
			}
			if (other.HasIsFinish)
			{
				IsFinish = other.IsFinish;
			}
			if (other.HasID)
			{
				ID = other.ID;
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
					Name = input.ReadString();
					continue;
				case 16u:
					RoleType = input.ReadInt32();
					continue;
				case 24u:
					CarType = input.ReadInt32();
					continue;
				case 32u:
					Rank = input.ReadInt32();
					continue;
				case 45u:
					Time = input.ReadFloat();
					continue;
				case 48u:
					IsFinish = input.ReadInt32();
					continue;
				case 56u:
					ID = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
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

		public Builder SetRank(int value)
		{
			result.hasRank = true;
			result.rank_ = value;
			return this;
		}

		public Builder ClearRank()
		{
			result.hasRank = false;
			result.rank_ = 0;
			return this;
		}

		public Builder SetTime(float value)
		{
			result.hasTime = true;
			result.time_ = value;
			return this;
		}

		public Builder ClearTime()
		{
			result.hasTime = false;
			result.time_ = 0f;
			return this;
		}

		public Builder SetIsFinish(int value)
		{
			result.hasIsFinish = true;
			result.isFinish_ = value;
			return this;
		}

		public Builder ClearIsFinish()
		{
			result.hasIsFinish = false;
			result.isFinish_ = 0;
			return this;
		}

		public Builder SetID(int value)
		{
			result.hasID = true;
			result.iD_ = value;
			return this;
		}

		public Builder ClearID()
		{
			result.hasID = false;
			result.iD_ = 0;
			return this;
		}
	}

	public const int NameFieldNumber = 1;

	public const int RoleTypeFieldNumber = 2;

	public const int CarTypeFieldNumber = 3;

	public const int RankFieldNumber = 4;

	public const int TimeFieldNumber = 5;

	public const int IsFinishFieldNumber = 6;

	public const int IDFieldNumber = 7;

	private static readonly MsgRank defaultInstance;

	private bool hasName;

	private string name_ = string.Empty;

	private bool hasRoleType;

	private int roleType_;

	private bool hasCarType;

	private int carType_;

	private bool hasRank;

	private int rank_;

	private bool hasTime;

	private float time_;

	private bool hasIsFinish;

	private int isFinish_;

	private bool hasID;

	private int iD_;

	private int memoizedSerializedSize = -1;

	public static MsgRank DefaultInstance => defaultInstance;

	public override MsgRank DefaultInstanceForType => defaultInstance;

	protected override MsgRank ThisMessage => this;

	public bool HasName => hasName;

	public string Name => name_;

	public bool HasRoleType => hasRoleType;

	public int RoleType => roleType_;

	public bool HasCarType => hasCarType;

	public int CarType => carType_;

	public bool HasRank => hasRank;

	public int Rank => rank_;

	public bool HasTime => hasTime;

	public float Time => time_;

	public bool HasIsFinish => hasIsFinish;

	public int IsFinish => isFinish_;

	public bool HasID => hasID;

	public int ID => iD_;

	public override bool IsInitialized
	{
		get
		{
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
			if (!hasRank)
			{
				return false;
			}
			if (!hasTime)
			{
				return false;
			}
			if (!hasIsFinish)
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
			if (HasName)
			{
				num += CodedOutputStream.ComputeStringSize(1, Name);
			}
			if (HasRoleType)
			{
				num += CodedOutputStream.ComputeInt32Size(2, RoleType);
			}
			if (HasCarType)
			{
				num += CodedOutputStream.ComputeInt32Size(3, CarType);
			}
			if (HasRank)
			{
				num += CodedOutputStream.ComputeInt32Size(4, Rank);
			}
			if (HasTime)
			{
				num += CodedOutputStream.ComputeFloatSize(5, Time);
			}
			if (HasIsFinish)
			{
				num += CodedOutputStream.ComputeInt32Size(6, IsFinish);
			}
			if (HasID)
			{
				num += CodedOutputStream.ComputeInt32Size(7, ID);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgRank()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasName)
		{
			output.WriteString(1, Name);
		}
		if (HasRoleType)
		{
			output.WriteInt32(2, RoleType);
		}
		if (HasCarType)
		{
			output.WriteInt32(3, CarType);
		}
		if (HasRank)
		{
			output.WriteInt32(4, Rank);
		}
		if (HasTime)
		{
			output.WriteFloat(5, Time);
		}
		if (HasIsFinish)
		{
			output.WriteInt32(6, IsFinish);
		}
		if (HasID)
		{
			output.WriteInt32(7, ID);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
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
		if (hasRank)
		{
			num ^= rank_.GetHashCode();
		}
		if (hasTime)
		{
			num ^= time_.GetHashCode();
		}
		if (hasIsFinish)
		{
			num ^= isFinish_.GetHashCode();
		}
		if (hasID)
		{
			num ^= iD_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgRank msgRank))
		{
			return false;
		}
		if (hasName != msgRank.hasName || (hasName && !name_.Equals(msgRank.name_)))
		{
			return false;
		}
		if (hasRoleType != msgRank.hasRoleType || (hasRoleType && !roleType_.Equals(msgRank.roleType_)))
		{
			return false;
		}
		if (hasCarType != msgRank.hasCarType || (hasCarType && !carType_.Equals(msgRank.carType_)))
		{
			return false;
		}
		if (hasRank != msgRank.hasRank || (hasRank && !rank_.Equals(msgRank.rank_)))
		{
			return false;
		}
		if (hasTime != msgRank.hasTime || (hasTime && !time_.Equals(msgRank.time_)))
		{
			return false;
		}
		if (hasIsFinish != msgRank.hasIsFinish || (hasIsFinish && !isFinish_.Equals(msgRank.isFinish_)))
		{
			return false;
		}
		if (hasID != msgRank.hasID || (hasID && !iD_.Equals(msgRank.iD_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgRank, Builder>.PrintField("name", hasName, name_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("roleType", hasRoleType, roleType_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("carType", hasCarType, carType_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("rank", hasRank, rank_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("time", hasTime, time_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("isFinish", hasIsFinish, isFinish_, writer);
		GeneratedMessageLite<MsgRank, Builder>.PrintField("ID", hasID, iD_, writer);
	}

	public static MsgRank ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRank ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRank ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRank ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRank ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRank ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRank ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgRank ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRank ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRank ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgRank prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
