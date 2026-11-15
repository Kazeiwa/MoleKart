using System;
using System.Collections.Generic;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;

namespace RaceProtocol;

public sealed class MsgMonsterMovementList : GeneratedMessageLite<MsgMonsterMovementList, MsgMonsterMovementList.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgMonsterMovementList, Builder>
	{
		private MsgMonsterMovementList result = new MsgMonsterMovementList();

		protected override Builder ThisBuilder => this;

		protected override MsgMonsterMovementList MessageBeingBuilt => result;

		public override MsgMonsterMovementList DefaultInstanceForType => DefaultInstance;

		public IPopsicleList<MsgMonsterMovement> ListList => result.list_;

		public int ListCount => result.ListCount;

		public override Builder Clear()
		{
			result = new MsgMonsterMovementList();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgMonsterMovementList BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			result.list_.MakeReadOnly();
			MsgMonsterMovementList msgMonsterMovementList = result;
			result = null;
			return msgMonsterMovementList;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgMonsterMovementList)
			{
				return MergeFrom((MsgMonsterMovementList)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgMonsterMovementList other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.list_.Count != 0)
			{
				AddRange(other.list_, result.list_);
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
				{
					MsgMonsterMovement.Builder builder = MsgMonsterMovement.CreateBuilder();
					input.ReadMessage(builder, extensionRegistry);
					AddList(builder.BuildPartial());
					continue;
				}
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public MsgMonsterMovement GetList(int index)
		{
			return result.GetList(index);
		}

		public Builder SetList(int index, MsgMonsterMovement value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.list_[index] = value;
			return this;
		}

		public Builder SetList(int index, MsgMonsterMovement.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.list_[index] = builderForValue.Build();
			return this;
		}

		public Builder AddList(MsgMonsterMovement value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.list_.Add(value);
			return this;
		}

		public Builder AddList(MsgMonsterMovement.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.list_.Add(builderForValue.Build());
			return this;
		}

		public Builder AddRangeList(IEnumerable<MsgMonsterMovement> values)
		{
			AddRange(values, result.list_);
			return this;
		}

		public Builder ClearList()
		{
			result.list_.Clear();
			return this;
		}
	}

	public const int ListFieldNumber = 1;

	private static readonly MsgMonsterMovementList defaultInstance;

	private PopsicleList<MsgMonsterMovement> list_ = new PopsicleList<MsgMonsterMovement>();

	private int memoizedSerializedSize = -1;

	public static MsgMonsterMovementList DefaultInstance => defaultInstance;

	public override MsgMonsterMovementList DefaultInstanceForType => defaultInstance;

	protected override MsgMonsterMovementList ThisMessage => this;

	public IList<MsgMonsterMovement> ListList => list_;

	public int ListCount => list_.Count;

	public override bool IsInitialized
	{
		get
		{
			foreach (MsgMonsterMovement list in ListList)
			{
				if (!list.IsInitialized)
				{
					return false;
				}
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
			foreach (MsgMonsterMovement list in ListList)
			{
				num += CodedOutputStream.ComputeMessageSize(1, list);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgMonsterMovementList()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public MsgMonsterMovement GetList(int index)
	{
		return list_[index];
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		foreach (MsgMonsterMovement list in ListList)
		{
			output.WriteMessage(1, list);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		foreach (MsgMonsterMovement item in list_)
		{
			num ^= item.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgMonsterMovementList msgMonsterMovementList))
		{
			return false;
		}
		if (list_.Count != msgMonsterMovementList.list_.Count)
		{
			return false;
		}
		for (int i = 0; i < list_.Count; i++)
		{
			if (!list_[i].Equals(msgMonsterMovementList.list_[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgMonsterMovementList, Builder>.PrintField("List", list_, writer);
	}

	public static MsgMonsterMovementList ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementList ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementList ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovementList ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgMonsterMovementList prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
