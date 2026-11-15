using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgSendID : GeneratedMessageLite<MsgSendID, MsgSendID.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgSendID, Builder>
	{
		private MsgSendID result = new MsgSendID();

		protected override Builder ThisBuilder => this;

		protected override MsgSendID MessageBeingBuilt => result;

		public override MsgSendID DefaultInstanceForType => DefaultInstance;

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
			result = new MsgSendID();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgSendID BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgSendID msgSendID = result;
			result = null;
			return msgSendID;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgSendID)
			{
				return MergeFrom((MsgSendID)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgSendID other)
		{
			if (other == DefaultInstance)
			{
				return this;
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
				case 8u:
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

	public const int IDFieldNumber = 1;

	private static readonly MsgSendID defaultInstance;

	private bool hasID;

	private int iD_;

	private int memoizedSerializedSize = -1;

	public static MsgSendID DefaultInstance => defaultInstance;

	public override MsgSendID DefaultInstanceForType => defaultInstance;

	protected override MsgSendID ThisMessage => this;

	public bool HasID => hasID;

	public int ID => iD_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasID)
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
			if (HasID)
			{
				num += CodedOutputStream.ComputeInt32Size(1, ID);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgSendID()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasID)
		{
			output.WriteInt32(1, ID);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasID)
		{
			num ^= iD_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgSendID msgSendID))
		{
			return false;
		}
		if (hasID != msgSendID.hasID || (hasID && !iD_.Equals(msgSendID.iD_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgSendID, Builder>.PrintField("ID", hasID, iD_, writer);
	}

	public static MsgSendID ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgSendID ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgSendID ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgSendID ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgSendID ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgSendID ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgSendID ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgSendID ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgSendID ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgSendID ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgSendID prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
