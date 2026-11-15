using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgMonsterCommand : GeneratedMessageLite<MsgMonsterCommand, MsgMonsterCommand.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgMonsterCommand, Builder>
	{
		private MsgMonsterCommand result = new MsgMonsterCommand();

		protected override Builder ThisBuilder => this;

		protected override MsgMonsterCommand MessageBeingBuilt => result;

		public override MsgMonsterCommand DefaultInstanceForType => DefaultInstance;

		public bool HasCommandId => result.HasCommandId;

		public int CommandId
		{
			get
			{
				return result.CommandId;
			}
			set
			{
				SetCommandId(value);
			}
		}

		public bool HasIndex => result.HasIndex;

		public int Index
		{
			get
			{
				return result.Index;
			}
			set
			{
				SetIndex(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgMonsterCommand();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgMonsterCommand BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgMonsterCommand msgMonsterCommand = result;
			result = null;
			return msgMonsterCommand;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgMonsterCommand)
			{
				return MergeFrom((MsgMonsterCommand)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgMonsterCommand other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasCommandId)
			{
				CommandId = other.CommandId;
			}
			if (other.HasIndex)
			{
				Index = other.Index;
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
					CommandId = input.ReadInt32();
					continue;
				case 16u:
					Index = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetCommandId(int value)
		{
			result.hasCommandId = true;
			result.commandId_ = value;
			return this;
		}

		public Builder ClearCommandId()
		{
			result.hasCommandId = false;
			result.commandId_ = 0;
			return this;
		}

		public Builder SetIndex(int value)
		{
			result.hasIndex = true;
			result.index_ = value;
			return this;
		}

		public Builder ClearIndex()
		{
			result.hasIndex = false;
			result.index_ = 0;
			return this;
		}
	}

	public const int CommandIdFieldNumber = 1;

	public const int IndexFieldNumber = 2;

	private static readonly MsgMonsterCommand defaultInstance;

	private bool hasCommandId;

	private int commandId_;

	private bool hasIndex;

	private int index_;

	private int memoizedSerializedSize = -1;

	public static MsgMonsterCommand DefaultInstance => defaultInstance;

	public override MsgMonsterCommand DefaultInstanceForType => defaultInstance;

	protected override MsgMonsterCommand ThisMessage => this;

	public bool HasCommandId => hasCommandId;

	public int CommandId => commandId_;

	public bool HasIndex => hasIndex;

	public int Index => index_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasCommandId)
			{
				return false;
			}
			if (!hasIndex)
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
			if (HasCommandId)
			{
				num += CodedOutputStream.ComputeInt32Size(1, CommandId);
			}
			if (HasIndex)
			{
				num += CodedOutputStream.ComputeInt32Size(2, Index);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgMonsterCommand()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasCommandId)
		{
			output.WriteInt32(1, CommandId);
		}
		if (HasIndex)
		{
			output.WriteInt32(2, Index);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasCommandId)
		{
			num ^= commandId_.GetHashCode();
		}
		if (hasIndex)
		{
			num ^= index_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgMonsterCommand msgMonsterCommand))
		{
			return false;
		}
		if (hasCommandId != msgMonsterCommand.hasCommandId || (hasCommandId && !commandId_.Equals(msgMonsterCommand.commandId_)))
		{
			return false;
		}
		if (hasIndex != msgMonsterCommand.hasIndex || (hasIndex && !index_.Equals(msgMonsterCommand.index_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgMonsterCommand, Builder>.PrintField("commandId", hasCommandId, commandId_, writer);
		GeneratedMessageLite<MsgMonsterCommand, Builder>.PrintField("index", hasIndex, index_, writer);
	}

	public static MsgMonsterCommand ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterCommand ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgMonsterCommand ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterCommand ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgMonsterCommand prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
