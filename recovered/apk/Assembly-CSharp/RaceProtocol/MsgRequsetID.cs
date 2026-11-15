using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgRequsetID : GeneratedMessageLite<MsgRequsetID, MsgRequsetID.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgRequsetID, Builder>
	{
		private MsgRequsetID result = new MsgRequsetID();

		protected override Builder ThisBuilder => this;

		protected override MsgRequsetID MessageBeingBuilt => result;

		public override MsgRequsetID DefaultInstanceForType => DefaultInstance;

		public bool HasRequest => result.HasRequest;

		public int Request
		{
			get
			{
				return result.Request;
			}
			set
			{
				SetRequest(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgRequsetID();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgRequsetID BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgRequsetID msgRequsetID = result;
			result = null;
			return msgRequsetID;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgRequsetID)
			{
				return MergeFrom((MsgRequsetID)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgRequsetID other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasRequest)
			{
				Request = other.Request;
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
					Request = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetRequest(int value)
		{
			result.hasRequest = true;
			result.request_ = value;
			return this;
		}

		public Builder ClearRequest()
		{
			result.hasRequest = false;
			result.request_ = 0;
			return this;
		}
	}

	public const int RequestFieldNumber = 1;

	private static readonly MsgRequsetID defaultInstance;

	private bool hasRequest;

	private int request_;

	private int memoizedSerializedSize = -1;

	public static MsgRequsetID DefaultInstance => defaultInstance;

	public override MsgRequsetID DefaultInstanceForType => defaultInstance;

	protected override MsgRequsetID ThisMessage => this;

	public bool HasRequest => hasRequest;

	public int Request => request_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasRequest)
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
			if (HasRequest)
			{
				num += CodedOutputStream.ComputeInt32Size(1, Request);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgRequsetID()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasRequest)
		{
			output.WriteInt32(1, Request);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasRequest)
		{
			num ^= request_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgRequsetID msgRequsetID))
		{
			return false;
		}
		if (hasRequest != msgRequsetID.hasRequest || (hasRequest && !request_.Equals(msgRequsetID.request_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgRequsetID, Builder>.PrintField("Request", hasRequest, request_, writer);
	}

	public static MsgRequsetID ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRequsetID ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgRequsetID ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgRequsetID ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgRequsetID prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
