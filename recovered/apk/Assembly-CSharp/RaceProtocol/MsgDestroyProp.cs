using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgDestroyProp : GeneratedMessageLite<MsgDestroyProp, MsgDestroyProp.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgDestroyProp, Builder>
	{
		private MsgDestroyProp result = new MsgDestroyProp();

		protected override Builder ThisBuilder => this;

		protected override MsgDestroyProp MessageBeingBuilt => result;

		public override MsgDestroyProp DefaultInstanceForType => DefaultInstance;

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

		public bool HasGuid => result.HasGuid;

		public string Guid
		{
			get
			{
				return result.Guid;
			}
			set
			{
				SetGuid(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgDestroyProp();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgDestroyProp BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgDestroyProp msgDestroyProp = result;
			result = null;
			return msgDestroyProp;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgDestroyProp)
			{
				return MergeFrom((MsgDestroyProp)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgDestroyProp other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPropType)
			{
				PropType = other.PropType;
			}
			if (other.HasGuid)
			{
				Guid = other.Guid;
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
				case 18u:
					Guid = input.ReadString();
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

		public Builder SetGuid(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasGuid = true;
			result.guid_ = value;
			return this;
		}

		public Builder ClearGuid()
		{
			result.hasGuid = false;
			result.guid_ = string.Empty;
			return this;
		}
	}

	public const int PropTypeFieldNumber = 1;

	public const int GuidFieldNumber = 2;

	private static readonly MsgDestroyProp defaultInstance;

	private bool hasPropType;

	private int propType_;

	private bool hasGuid;

	private string guid_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static MsgDestroyProp DefaultInstance => defaultInstance;

	public override MsgDestroyProp DefaultInstanceForType => defaultInstance;

	protected override MsgDestroyProp ThisMessage => this;

	public bool HasPropType => hasPropType;

	public int PropType => propType_;

	public bool HasGuid => hasGuid;

	public string Guid => guid_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPropType)
			{
				return false;
			}
			if (!hasGuid)
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
			if (HasGuid)
			{
				num += CodedOutputStream.ComputeStringSize(2, Guid);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgDestroyProp()
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
		if (HasGuid)
		{
			output.WriteString(2, Guid);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPropType)
		{
			num ^= propType_.GetHashCode();
		}
		if (hasGuid)
		{
			num ^= guid_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgDestroyProp msgDestroyProp))
		{
			return false;
		}
		if (hasPropType != msgDestroyProp.hasPropType || (hasPropType && !propType_.Equals(msgDestroyProp.propType_)))
		{
			return false;
		}
		if (hasGuid != msgDestroyProp.hasGuid || (hasGuid && !guid_.Equals(msgDestroyProp.guid_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgDestroyProp, Builder>.PrintField("propType", hasPropType, propType_, writer);
		GeneratedMessageLite<MsgDestroyProp, Builder>.PrintField("guid", hasGuid, guid_, writer);
	}

	public static MsgDestroyProp ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgDestroyProp ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgDestroyProp ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgDestroyProp ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgDestroyProp prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
