using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgMonsterStateInfo : GeneratedMessageLite<MsgMonsterStateInfo, MsgMonsterStateInfo.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgMonsterStateInfo, Builder>
	{
		private MsgMonsterStateInfo result = new MsgMonsterStateInfo();

		protected override Builder ThisBuilder => this;

		protected override MsgMonsterStateInfo MessageBeingBuilt => result;

		public override MsgMonsterStateInfo DefaultInstanceForType => DefaultInstance;

		public bool HasStateId => result.HasStateId;

		public int StateId
		{
			get
			{
				return result.StateId;
			}
			set
			{
				SetStateId(value);
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
			result = new MsgMonsterStateInfo();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgMonsterStateInfo BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgMonsterStateInfo msgMonsterStateInfo = result;
			result = null;
			return msgMonsterStateInfo;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgMonsterStateInfo)
			{
				return MergeFrom((MsgMonsterStateInfo)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgMonsterStateInfo other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasStateId)
			{
				StateId = other.StateId;
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
					StateId = input.ReadInt32();
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

		public Builder SetStateId(int value)
		{
			result.hasStateId = true;
			result.stateId_ = value;
			return this;
		}

		public Builder ClearStateId()
		{
			result.hasStateId = false;
			result.stateId_ = 0;
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

	public const int StateIdFieldNumber = 1;

	public const int IndexFieldNumber = 2;

	private static readonly MsgMonsterStateInfo defaultInstance;

	private bool hasStateId;

	private int stateId_;

	private bool hasIndex;

	private int index_;

	private int memoizedSerializedSize = -1;

	public static MsgMonsterStateInfo DefaultInstance => defaultInstance;

	public override MsgMonsterStateInfo DefaultInstanceForType => defaultInstance;

	protected override MsgMonsterStateInfo ThisMessage => this;

	public bool HasStateId => hasStateId;

	public int StateId => stateId_;

	public bool HasIndex => hasIndex;

	public int Index => index_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasStateId)
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
			if (HasStateId)
			{
				num += CodedOutputStream.ComputeInt32Size(1, StateId);
			}
			if (HasIndex)
			{
				num += CodedOutputStream.ComputeInt32Size(2, Index);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgMonsterStateInfo()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasStateId)
		{
			output.WriteInt32(1, StateId);
		}
		if (HasIndex)
		{
			output.WriteInt32(2, Index);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasStateId)
		{
			num ^= stateId_.GetHashCode();
		}
		if (hasIndex)
		{
			num ^= index_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgMonsterStateInfo msgMonsterStateInfo))
		{
			return false;
		}
		if (hasStateId != msgMonsterStateInfo.hasStateId || (hasStateId && !stateId_.Equals(msgMonsterStateInfo.stateId_)))
		{
			return false;
		}
		if (hasIndex != msgMonsterStateInfo.hasIndex || (hasIndex && !index_.Equals(msgMonsterStateInfo.index_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgMonsterStateInfo, Builder>.PrintField("stateId", hasStateId, stateId_, writer);
		GeneratedMessageLite<MsgMonsterStateInfo, Builder>.PrintField("index", hasIndex, index_, writer);
	}

	public static MsgMonsterStateInfo ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterStateInfo ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgMonsterStateInfo prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
