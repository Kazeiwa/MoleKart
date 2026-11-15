using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class LimitFreeMsgReq : GeneratedMessageLite<LimitFreeMsgReq, LimitFreeMsgReq.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<LimitFreeMsgReq, Builder>
	{
		private LimitFreeMsgReq result = new LimitFreeMsgReq();

		protected override Builder ThisBuilder => this;

		protected override LimitFreeMsgReq MessageBeingBuilt => result;

		public override LimitFreeMsgReq DefaultInstanceForType => DefaultInstance;

		public bool HasMsgId => result.HasMsgId;

		[CLSCompliant(false)]
		public uint MsgId
		{
			get
			{
				return result.MsgId;
			}
			set
			{
				SetMsgId(value);
			}
		}

		public bool HasVersion => result.HasVersion;

		[CLSCompliant(false)]
		public uint Version
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

		public bool HasLanguage => result.HasLanguage;

		[CLSCompliant(false)]
		public uint Language
		{
			get
			{
				return result.Language;
			}
			set
			{
				SetLanguage(value);
			}
		}

		public override Builder Clear()
		{
			result = new LimitFreeMsgReq();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override LimitFreeMsgReq BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			LimitFreeMsgReq limitFreeMsgReq = result;
			result = null;
			return limitFreeMsgReq;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is LimitFreeMsgReq)
			{
				return MergeFrom((LimitFreeMsgReq)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(LimitFreeMsgReq other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasMsgId)
			{
				MsgId = other.MsgId;
			}
			if (other.HasVersion)
			{
				Version = other.Version;
			}
			if (other.HasLanguage)
			{
				Language = other.Language;
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
					MsgId = input.ReadUInt32();
					continue;
				case 16u:
					Version = input.ReadUInt32();
					continue;
				case 24u:
					Language = input.ReadUInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		[CLSCompliant(false)]
		public Builder SetMsgId(uint value)
		{
			result.hasMsgId = true;
			result.msgId_ = value;
			return this;
		}

		public Builder ClearMsgId()
		{
			result.hasMsgId = false;
			result.msgId_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetVersion(uint value)
		{
			result.hasVersion = true;
			result.version_ = value;
			return this;
		}

		public Builder ClearVersion()
		{
			result.hasVersion = false;
			result.version_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetLanguage(uint value)
		{
			result.hasLanguage = true;
			result.language_ = value;
			return this;
		}

		public Builder ClearLanguage()
		{
			result.hasLanguage = false;
			result.language_ = 0u;
			return this;
		}
	}

	public const int MsgIdFieldNumber = 1;

	public const int VersionFieldNumber = 2;

	public const int LanguageFieldNumber = 3;

	private static readonly LimitFreeMsgReq defaultInstance;

	private bool hasMsgId;

	private uint msgId_;

	private bool hasVersion;

	private uint version_;

	private bool hasLanguage;

	private uint language_;

	private int memoizedSerializedSize = -1;

	public static LimitFreeMsgReq DefaultInstance => defaultInstance;

	public override LimitFreeMsgReq DefaultInstanceForType => defaultInstance;

	protected override LimitFreeMsgReq ThisMessage => this;

	public bool HasMsgId => hasMsgId;

	[CLSCompliant(false)]
	public uint MsgId => msgId_;

	public bool HasVersion => hasVersion;

	[CLSCompliant(false)]
	public uint Version => version_;

	public bool HasLanguage => hasLanguage;

	[CLSCompliant(false)]
	public uint Language => language_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasMsgId)
			{
				return false;
			}
			if (!hasVersion)
			{
				return false;
			}
			if (!hasLanguage)
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
			if (HasMsgId)
			{
				num += CodedOutputStream.ComputeUInt32Size(1, MsgId);
			}
			if (HasVersion)
			{
				num += CodedOutputStream.ComputeUInt32Size(2, Version);
			}
			if (HasLanguage)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, Language);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static LimitFreeMsgReq()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasMsgId)
		{
			output.WriteUInt32(1, MsgId);
		}
		if (HasVersion)
		{
			output.WriteUInt32(2, Version);
		}
		if (HasLanguage)
		{
			output.WriteUInt32(3, Language);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasMsgId)
		{
			num ^= msgId_.GetHashCode();
		}
		if (hasVersion)
		{
			num ^= version_.GetHashCode();
		}
		if (hasLanguage)
		{
			num ^= language_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is LimitFreeMsgReq limitFreeMsgReq))
		{
			return false;
		}
		if (hasMsgId != limitFreeMsgReq.hasMsgId || (hasMsgId && !msgId_.Equals(limitFreeMsgReq.msgId_)))
		{
			return false;
		}
		if (hasVersion != limitFreeMsgReq.hasVersion || (hasVersion && !version_.Equals(limitFreeMsgReq.version_)))
		{
			return false;
		}
		if (hasLanguage != limitFreeMsgReq.hasLanguage || (hasLanguage && !language_.Equals(limitFreeMsgReq.language_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<LimitFreeMsgReq, Builder>.PrintField("msgId", hasMsgId, msgId_, writer);
		GeneratedMessageLite<LimitFreeMsgReq, Builder>.PrintField("version", hasVersion, version_, writer);
		GeneratedMessageLite<LimitFreeMsgReq, Builder>.PrintField("language", hasLanguage, language_, writer);
	}

	public static LimitFreeMsgReq ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgReq ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static LimitFreeMsgReq ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static LimitFreeMsgReq ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(LimitFreeMsgReq prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
