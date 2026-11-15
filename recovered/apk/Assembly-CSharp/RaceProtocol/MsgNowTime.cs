using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgNowTime : GeneratedMessageLite<MsgNowTime, MsgNowTime.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgNowTime, Builder>
	{
		private MsgNowTime result = new MsgNowTime();

		protected override Builder ThisBuilder => this;

		protected override MsgNowTime MessageBeingBuilt => result;

		public override MsgNowTime DefaultInstanceForType => DefaultInstance;

		public bool HasNowTime => result.HasNowTime;

		public string NowTime
		{
			get
			{
				return result.NowTime;
			}
			set
			{
				SetNowTime(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgNowTime();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgNowTime BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgNowTime msgNowTime = result;
			result = null;
			return msgNowTime;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgNowTime)
			{
				return MergeFrom((MsgNowTime)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgNowTime other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasNowTime)
			{
				NowTime = other.NowTime;
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
					NowTime = input.ReadString();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetNowTime(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasNowTime = true;
			result.nowTime_ = value;
			return this;
		}

		public Builder ClearNowTime()
		{
			result.hasNowTime = false;
			result.nowTime_ = string.Empty;
			return this;
		}
	}

	public const int NowTimeFieldNumber = 1;

	private static readonly MsgNowTime defaultInstance;

	private bool hasNowTime;

	private string nowTime_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static MsgNowTime DefaultInstance => defaultInstance;

	public override MsgNowTime DefaultInstanceForType => defaultInstance;

	protected override MsgNowTime ThisMessage => this;

	public bool HasNowTime => hasNowTime;

	public string NowTime => nowTime_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasNowTime)
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
			if (HasNowTime)
			{
				num += CodedOutputStream.ComputeStringSize(1, NowTime);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgNowTime()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasNowTime)
		{
			output.WriteString(1, NowTime);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasNowTime)
		{
			num ^= nowTime_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgNowTime msgNowTime))
		{
			return false;
		}
		if (hasNowTime != msgNowTime.hasNowTime || (hasNowTime && !nowTime_.Equals(msgNowTime.nowTime_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgNowTime, Builder>.PrintField("nowTime", hasNowTime, nowTime_, writer);
	}

	public static MsgNowTime ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNowTime ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNowTime ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNowTime ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNowTime ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNowTime ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNowTime ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgNowTime ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNowTime ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNowTime ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgNowTime prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
