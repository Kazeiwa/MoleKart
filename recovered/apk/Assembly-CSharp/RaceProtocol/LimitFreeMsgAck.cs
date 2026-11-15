using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class LimitFreeMsgAck : GeneratedMessageLite<LimitFreeMsgAck, LimitFreeMsgAck.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<LimitFreeMsgAck, Builder>
	{
		private LimitFreeMsgAck result = new LimitFreeMsgAck();

		protected override Builder ThisBuilder => this;

		protected override LimitFreeMsgAck MessageBeingBuilt => result;

		public override LimitFreeMsgAck DefaultInstanceForType => DefaultInstance;

		public bool HasStartTime => result.HasStartTime;

		[CLSCompliant(false)]
		public uint StartTime
		{
			get
			{
				return result.StartTime;
			}
			set
			{
				SetStartTime(value);
			}
		}

		public bool HasEndTime => result.HasEndTime;

		[CLSCompliant(false)]
		public uint EndTime
		{
			get
			{
				return result.EndTime;
			}
			set
			{
				SetEndTime(value);
			}
		}

		public bool HasItemId => result.HasItemId;

		[CLSCompliant(false)]
		public uint ItemId
		{
			get
			{
				return result.ItemId;
			}
			set
			{
				SetItemId(value);
			}
		}

		public bool HasMsgType => result.HasMsgType;

		[CLSCompliant(false)]
		public uint MsgType
		{
			get
			{
				return result.MsgType;
			}
			set
			{
				SetMsgType(value);
			}
		}

		public bool HasTitle => result.HasTitle;

		public string Title
		{
			get
			{
				return result.Title;
			}
			set
			{
				SetTitle(value);
			}
		}

		public bool HasMsg => result.HasMsg;

		public string Msg
		{
			get
			{
				return result.Msg;
			}
			set
			{
				SetMsg(value);
			}
		}

		public override Builder Clear()
		{
			result = new LimitFreeMsgAck();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override LimitFreeMsgAck BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			LimitFreeMsgAck limitFreeMsgAck = result;
			result = null;
			return limitFreeMsgAck;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is LimitFreeMsgAck)
			{
				return MergeFrom((LimitFreeMsgAck)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(LimitFreeMsgAck other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasStartTime)
			{
				StartTime = other.StartTime;
			}
			if (other.HasEndTime)
			{
				EndTime = other.EndTime;
			}
			if (other.HasItemId)
			{
				ItemId = other.ItemId;
			}
			if (other.HasMsgType)
			{
				MsgType = other.MsgType;
			}
			if (other.HasTitle)
			{
				Title = other.Title;
			}
			if (other.HasMsg)
			{
				Msg = other.Msg;
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
					StartTime = input.ReadUInt32();
					continue;
				case 16u:
					EndTime = input.ReadUInt32();
					continue;
				case 24u:
					ItemId = input.ReadUInt32();
					continue;
				case 32u:
					MsgType = input.ReadUInt32();
					continue;
				case 42u:
					Title = input.ReadString();
					continue;
				case 50u:
					Msg = input.ReadString();
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
		public Builder SetStartTime(uint value)
		{
			result.hasStartTime = true;
			result.startTime_ = value;
			return this;
		}

		public Builder ClearStartTime()
		{
			result.hasStartTime = false;
			result.startTime_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetEndTime(uint value)
		{
			result.hasEndTime = true;
			result.endTime_ = value;
			return this;
		}

		public Builder ClearEndTime()
		{
			result.hasEndTime = false;
			result.endTime_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetItemId(uint value)
		{
			result.hasItemId = true;
			result.itemId_ = value;
			return this;
		}

		public Builder ClearItemId()
		{
			result.hasItemId = false;
			result.itemId_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetMsgType(uint value)
		{
			result.hasMsgType = true;
			result.msgType_ = value;
			return this;
		}

		public Builder ClearMsgType()
		{
			result.hasMsgType = false;
			result.msgType_ = 0u;
			return this;
		}

		public Builder SetTitle(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasTitle = true;
			result.title_ = value;
			return this;
		}

		public Builder ClearTitle()
		{
			result.hasTitle = false;
			result.title_ = string.Empty;
			return this;
		}

		public Builder SetMsg(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasMsg = true;
			result.msg_ = value;
			return this;
		}

		public Builder ClearMsg()
		{
			result.hasMsg = false;
			result.msg_ = string.Empty;
			return this;
		}
	}

	public const int StartTimeFieldNumber = 1;

	public const int EndTimeFieldNumber = 2;

	public const int ItemIdFieldNumber = 3;

	public const int MsgTypeFieldNumber = 4;

	public const int TitleFieldNumber = 5;

	public const int MsgFieldNumber = 6;

	private static readonly LimitFreeMsgAck defaultInstance;

	private bool hasStartTime;

	private uint startTime_;

	private bool hasEndTime;

	private uint endTime_;

	private bool hasItemId;

	private uint itemId_;

	private bool hasMsgType;

	private uint msgType_;

	private bool hasTitle;

	private string title_ = string.Empty;

	private bool hasMsg;

	private string msg_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static LimitFreeMsgAck DefaultInstance => defaultInstance;

	public override LimitFreeMsgAck DefaultInstanceForType => defaultInstance;

	protected override LimitFreeMsgAck ThisMessage => this;

	public bool HasStartTime => hasStartTime;

	[CLSCompliant(false)]
	public uint StartTime => startTime_;

	public bool HasEndTime => hasEndTime;

	[CLSCompliant(false)]
	public uint EndTime => endTime_;

	public bool HasItemId => hasItemId;

	[CLSCompliant(false)]
	public uint ItemId => itemId_;

	public bool HasMsgType => hasMsgType;

	[CLSCompliant(false)]
	public uint MsgType => msgType_;

	public bool HasTitle => hasTitle;

	public string Title => title_;

	public bool HasMsg => hasMsg;

	public string Msg => msg_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasMsg)
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
			if (HasStartTime)
			{
				num += CodedOutputStream.ComputeUInt32Size(1, StartTime);
			}
			if (HasEndTime)
			{
				num += CodedOutputStream.ComputeUInt32Size(2, EndTime);
			}
			if (HasItemId)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, ItemId);
			}
			if (HasMsgType)
			{
				num += CodedOutputStream.ComputeUInt32Size(4, MsgType);
			}
			if (HasTitle)
			{
				num += CodedOutputStream.ComputeStringSize(5, Title);
			}
			if (HasMsg)
			{
				num += CodedOutputStream.ComputeStringSize(6, Msg);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static LimitFreeMsgAck()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasStartTime)
		{
			output.WriteUInt32(1, StartTime);
		}
		if (HasEndTime)
		{
			output.WriteUInt32(2, EndTime);
		}
		if (HasItemId)
		{
			output.WriteUInt32(3, ItemId);
		}
		if (HasMsgType)
		{
			output.WriteUInt32(4, MsgType);
		}
		if (HasTitle)
		{
			output.WriteString(5, Title);
		}
		if (HasMsg)
		{
			output.WriteString(6, Msg);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasStartTime)
		{
			num ^= startTime_.GetHashCode();
		}
		if (hasEndTime)
		{
			num ^= endTime_.GetHashCode();
		}
		if (hasItemId)
		{
			num ^= itemId_.GetHashCode();
		}
		if (hasMsgType)
		{
			num ^= msgType_.GetHashCode();
		}
		if (hasTitle)
		{
			num ^= title_.GetHashCode();
		}
		if (hasMsg)
		{
			num ^= msg_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is LimitFreeMsgAck limitFreeMsgAck))
		{
			return false;
		}
		if (hasStartTime != limitFreeMsgAck.hasStartTime || (hasStartTime && !startTime_.Equals(limitFreeMsgAck.startTime_)))
		{
			return false;
		}
		if (hasEndTime != limitFreeMsgAck.hasEndTime || (hasEndTime && !endTime_.Equals(limitFreeMsgAck.endTime_)))
		{
			return false;
		}
		if (hasItemId != limitFreeMsgAck.hasItemId || (hasItemId && !itemId_.Equals(limitFreeMsgAck.itemId_)))
		{
			return false;
		}
		if (hasMsgType != limitFreeMsgAck.hasMsgType || (hasMsgType && !msgType_.Equals(limitFreeMsgAck.msgType_)))
		{
			return false;
		}
		if (hasTitle != limitFreeMsgAck.hasTitle || (hasTitle && !title_.Equals(limitFreeMsgAck.title_)))
		{
			return false;
		}
		if (hasMsg != limitFreeMsgAck.hasMsg || (hasMsg && !msg_.Equals(limitFreeMsgAck.msg_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("startTime", hasStartTime, startTime_, writer);
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("endTime", hasEndTime, endTime_, writer);
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("itemId", hasItemId, itemId_, writer);
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("msgType", hasMsgType, msgType_, writer);
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("title", hasTitle, title_, writer);
		GeneratedMessageLite<LimitFreeMsgAck, Builder>.PrintField("msg", hasMsg, msg_, writer);
	}

	public static LimitFreeMsgAck ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgAck ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static LimitFreeMsgAck ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static LimitFreeMsgAck ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(LimitFreeMsgAck prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
