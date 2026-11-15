using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPickProp : GeneratedMessageLite<MsgPickProp, MsgPickProp.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPickProp, Builder>
	{
		private MsgPickProp result = new MsgPickProp();

		protected override Builder ThisBuilder => this;

		protected override MsgPickProp MessageBeingBuilt => result;

		public override MsgPickProp DefaultInstanceForType => DefaultInstance;

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

		public override Builder Clear()
		{
			result = new MsgPickProp();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPickProp BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPickProp msgPickProp = result;
			result = null;
			return msgPickProp;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPickProp)
			{
				return MergeFrom((MsgPickProp)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPickProp other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPropType)
			{
				PropType = other.PropType;
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
	}

	public const int PropTypeFieldNumber = 1;

	private static readonly MsgPickProp defaultInstance;

	private bool hasPropType;

	private int propType_;

	private int memoizedSerializedSize = -1;

	public static MsgPickProp DefaultInstance => defaultInstance;

	public override MsgPickProp DefaultInstanceForType => defaultInstance;

	protected override MsgPickProp ThisMessage => this;

	public bool HasPropType => hasPropType;

	public int PropType => propType_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPropType)
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
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPickProp()
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
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPropType)
		{
			num ^= propType_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPickProp msgPickProp))
		{
			return false;
		}
		if (hasPropType != msgPickProp.hasPropType || (hasPropType && !propType_.Equals(msgPickProp.propType_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPickProp, Builder>.PrintField("propType", hasPropType, propType_, writer);
	}

	public static MsgPickProp ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPickProp ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPickProp ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPickProp ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPickProp ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPickProp ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPickProp ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPickProp ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPickProp ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPickProp ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPickProp prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
