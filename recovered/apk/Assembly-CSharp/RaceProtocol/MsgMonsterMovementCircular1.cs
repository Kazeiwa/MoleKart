using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgMonsterMovementCircular1 : GeneratedMessageLite<MsgMonsterMovementCircular1, MsgMonsterMovementCircular1.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgMonsterMovementCircular1, Builder>
	{
		private MsgMonsterMovementCircular1 result = new MsgMonsterMovementCircular1();

		protected override Builder ThisBuilder => this;

		protected override MsgMonsterMovementCircular1 MessageBeingBuilt => result;

		public override MsgMonsterMovementCircular1 DefaultInstanceForType => DefaultInstance;

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

		public bool HasT => result.HasT;

		public float T
		{
			get
			{
				return result.T;
			}
			set
			{
				SetT(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgMonsterMovementCircular1();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgMonsterMovementCircular1 BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgMonsterMovementCircular1 msgMonsterMovementCircular = result;
			result = null;
			return msgMonsterMovementCircular;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgMonsterMovementCircular1)
			{
				return MergeFrom((MsgMonsterMovementCircular1)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgMonsterMovementCircular1 other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasIndex)
			{
				Index = other.Index;
			}
			if (other.HasT)
			{
				T = other.T;
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
					Index = input.ReadInt32();
					continue;
				case 21u:
					T = input.ReadFloat();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
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

		public Builder SetT(float value)
		{
			result.hasT = true;
			result.t_ = value;
			return this;
		}

		public Builder ClearT()
		{
			result.hasT = false;
			result.t_ = 0f;
			return this;
		}
	}

	public const int IndexFieldNumber = 1;

	public const int TFieldNumber = 2;

	private static readonly MsgMonsterMovementCircular1 defaultInstance;

	private bool hasIndex;

	private int index_;

	private bool hasT;

	private float t_;

	private int memoizedSerializedSize = -1;

	public static MsgMonsterMovementCircular1 DefaultInstance => defaultInstance;

	public override MsgMonsterMovementCircular1 DefaultInstanceForType => defaultInstance;

	protected override MsgMonsterMovementCircular1 ThisMessage => this;

	public bool HasIndex => hasIndex;

	public int Index => index_;

	public bool HasT => hasT;

	public float T => t_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasIndex)
			{
				return false;
			}
			if (!hasT)
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
			if (HasIndex)
			{
				num += CodedOutputStream.ComputeInt32Size(1, Index);
			}
			if (HasT)
			{
				num += CodedOutputStream.ComputeFloatSize(2, T);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgMonsterMovementCircular1()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasIndex)
		{
			output.WriteInt32(1, Index);
		}
		if (HasT)
		{
			output.WriteFloat(2, T);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasIndex)
		{
			num ^= index_.GetHashCode();
		}
		if (hasT)
		{
			num ^= t_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgMonsterMovementCircular1 msgMonsterMovementCircular))
		{
			return false;
		}
		if (hasIndex != msgMonsterMovementCircular.hasIndex || (hasIndex && !index_.Equals(msgMonsterMovementCircular.index_)))
		{
			return false;
		}
		if (hasT != msgMonsterMovementCircular.hasT || (hasT && !t_.Equals(msgMonsterMovementCircular.t_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgMonsterMovementCircular1, Builder>.PrintField("index", hasIndex, index_, writer);
		GeneratedMessageLite<MsgMonsterMovementCircular1, Builder>.PrintField("t", hasT, t_, writer);
	}

	public static MsgMonsterMovementCircular1 ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementCircular1 ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgMonsterMovementCircular1 prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
