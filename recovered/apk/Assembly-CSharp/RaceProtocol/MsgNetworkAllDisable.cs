using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgNetworkAllDisable : GeneratedMessageLite<MsgNetworkAllDisable, MsgNetworkAllDisable.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgNetworkAllDisable, Builder>
	{
		private MsgNetworkAllDisable result = new MsgNetworkAllDisable();

		protected override Builder ThisBuilder => this;

		protected override MsgNetworkAllDisable MessageBeingBuilt => result;

		public override MsgNetworkAllDisable DefaultInstanceForType => DefaultInstance;

		public override Builder Clear()
		{
			result = new MsgNetworkAllDisable();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgNetworkAllDisable BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgNetworkAllDisable msgNetworkAllDisable = result;
			result = null;
			return msgNetworkAllDisable;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgNetworkAllDisable)
			{
				return MergeFrom((MsgNetworkAllDisable)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgNetworkAllDisable other)
		{
			if (other == DefaultInstance)
			{
				return this;
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
				if (num == 0)
				{
					return this;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					break;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
			return this;
		}
	}

	private static readonly MsgNetworkAllDisable defaultInstance;

	private int memoizedSerializedSize = -1;

	public static MsgNetworkAllDisable DefaultInstance => defaultInstance;

	public override MsgNetworkAllDisable DefaultInstanceForType => defaultInstance;

	protected override MsgNetworkAllDisable ThisMessage => this;

	public override bool IsInitialized => true;

	public override int SerializedSize
	{
		get
		{
			int num = memoizedSerializedSize;
			if (num != -1)
			{
				return num;
			}
			return memoizedSerializedSize = 0;
		}
	}

	static MsgNetworkAllDisable()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
	}

	public override int GetHashCode()
	{
		return GetType().GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgNetworkAllDisable))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
	}

	public static MsgNetworkAllDisable ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgNetworkAllDisable ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgNetworkAllDisable prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
