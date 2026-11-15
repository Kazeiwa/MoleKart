using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgUseProp : GeneratedMessageLite<MsgUseProp, MsgUseProp.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgUseProp, Builder>
	{
		private MsgUseProp result = new MsgUseProp();

		protected override Builder ThisBuilder => this;

		protected override MsgUseProp MessageBeingBuilt => result;

		public override MsgUseProp DefaultInstanceForType => DefaultInstance;

		public bool HasPropType => result.HasPropType;

		public int PropType
		{
			get
			{
				return result.PropType;
			}
			set
			{
				SetPropType(value);
			}
		}

		public bool HasCarID => result.HasCarID;

		public int CarID
		{
			get
			{
				return result.CarID;
			}
			set
			{
				SetCarID(value);
			}
		}

		public bool HasForward => result.HasForward;

		public int Forward
		{
			get
			{
				return result.Forward;
			}
			set
			{
				SetForward(value);
			}
		}

		public bool HasPropID => result.HasPropID;

		public int PropID
		{
			get
			{
				return result.PropID;
			}
			set
			{
				SetPropID(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgUseProp();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgUseProp BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgUseProp msgUseProp = result;
			result = null;
			return msgUseProp;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgUseProp)
			{
				return MergeFrom((MsgUseProp)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgUseProp other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPropType)
			{
				PropType = other.PropType;
			}
			if (other.HasCarID)
			{
				CarID = other.CarID;
			}
			if (other.HasForward)
			{
				Forward = other.Forward;
			}
			if (other.HasPropID)
			{
				PropID = other.PropID;
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
				case 8u:
					PropType = input.ReadInt32();
					continue;
				case 16u:
					CarID = input.ReadInt32();
					continue;
				case 24u:
					Forward = input.ReadInt32();
					continue;
				case 32u:
					PropID = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPropType(int value)
		{
			result.hasPropType = true;
			result.propType_ = value;
			return this;
		}

		public Builder ClearPropType()
		{
			result.hasPropType = false;
			result.propType_ = 0;
			return this;
		}

		public Builder SetCarID(int value)
		{
			result.hasCarID = true;
			result.carID_ = value;
			return this;
		}

		public Builder ClearCarID()
		{
			result.hasCarID = false;
			result.carID_ = 0;
			return this;
		}

		public Builder SetForward(int value)
		{
			result.hasForward = true;
			result.forward_ = value;
			return this;
		}

		public Builder ClearForward()
		{
			result.hasForward = false;
			result.forward_ = 0;
			return this;
		}

		public Builder SetPropID(int value)
		{
			result.hasPropID = true;
			result.propID_ = value;
			return this;
		}

		public Builder ClearPropID()
		{
			result.hasPropID = false;
			result.propID_ = 0;
			return this;
		}
	}

	public const int PropTypeFieldNumber = 1;

	public const int CarIDFieldNumber = 2;

	public const int ForwardFieldNumber = 3;

	public const int PropIDFieldNumber = 4;

	private static readonly MsgUseProp defaultInstance;

	private bool hasPropType;

	private int propType_;

	private bool hasCarID;

	private int carID_;

	private bool hasForward;

	private int forward_;

	private bool hasPropID;

	private int propID_;

	private int memoizedSerializedSize = -1;

	public static MsgUseProp DefaultInstance => defaultInstance;

	public override MsgUseProp DefaultInstanceForType => defaultInstance;

	protected override MsgUseProp ThisMessage => this;

	public bool HasPropType => hasPropType;

	public int PropType => propType_;

	public bool HasCarID => hasCarID;

	public int CarID => carID_;

	public bool HasForward => hasForward;

	public int Forward => forward_;

	public bool HasPropID => hasPropID;

	public int PropID => propID_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPropType)
			{
				return false;
			}
			if (!hasCarID)
			{
				return false;
			}
			if (!hasForward)
			{
				return false;
			}
			if (!hasPropID)
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
			if (HasPropType)
			{
				num += CodedOutputStream.ComputeInt32Size(1, PropType);
			}
			if (HasCarID)
			{
				num += CodedOutputStream.ComputeInt32Size(2, CarID);
			}
			if (HasForward)
			{
				num += CodedOutputStream.ComputeInt32Size(3, Forward);
			}
			if (HasPropID)
			{
				num += CodedOutputStream.ComputeInt32Size(4, PropID);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgUseProp()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPropType)
		{
			output.WriteInt32(1, PropType);
		}
		if (HasCarID)
		{
			output.WriteInt32(2, CarID);
		}
		if (HasForward)
		{
			output.WriteInt32(3, Forward);
		}
		if (HasPropID)
		{
			output.WriteInt32(4, PropID);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPropType)
		{
			num ^= propType_.GetHashCode();
		}
		if (hasCarID)
		{
			num ^= carID_.GetHashCode();
		}
		if (hasForward)
		{
			num ^= forward_.GetHashCode();
		}
		if (hasPropID)
		{
			num ^= propID_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgUseProp msgUseProp))
		{
			return false;
		}
		if (hasPropType != msgUseProp.hasPropType || (hasPropType && !propType_.Equals(msgUseProp.propType_)))
		{
			return false;
		}
		if (hasCarID != msgUseProp.hasCarID || (hasCarID && !carID_.Equals(msgUseProp.carID_)))
		{
			return false;
		}
		if (hasForward != msgUseProp.hasForward || (hasForward && !forward_.Equals(msgUseProp.forward_)))
		{
			return false;
		}
		if (hasPropID != msgUseProp.hasPropID || (hasPropID && !propID_.Equals(msgUseProp.propID_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgUseProp, Builder>.PrintField("propType", hasPropType, propType_, writer);
		GeneratedMessageLite<MsgUseProp, Builder>.PrintField("carID", hasCarID, carID_, writer);
		GeneratedMessageLite<MsgUseProp, Builder>.PrintField("forward", hasForward, forward_, writer);
		GeneratedMessageLite<MsgUseProp, Builder>.PrintField("propID", hasPropID, propID_, writer);
	}

	public static MsgUseProp ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgUseProp ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgUseProp ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgUseProp ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgUseProp ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgUseProp ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgUseProp ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgUseProp ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgUseProp ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgUseProp ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgUseProp prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
