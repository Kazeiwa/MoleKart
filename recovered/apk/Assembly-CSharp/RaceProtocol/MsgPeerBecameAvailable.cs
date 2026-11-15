using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPeerBecameAvailable : GeneratedMessageLite<MsgPeerBecameAvailable, MsgPeerBecameAvailable.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPeerBecameAvailable, Builder>
	{
		private MsgPeerBecameAvailable result = new MsgPeerBecameAvailable();

		protected override Builder ThisBuilder => this;

		protected override MsgPeerBecameAvailable MessageBeingBuilt => result;

		public override MsgPeerBecameAvailable DefaultInstanceForType => DefaultInstance;

		public bool HasPeerDisplayName => result.HasPeerDisplayName;

		public string PeerDisplayName
		{
			get
			{
				return result.PeerDisplayName;
			}
			set
			{
				SetPeerDisplayName(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPeerBecameAvailable();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPeerBecameAvailable BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPeerBecameAvailable msgPeerBecameAvailable = result;
			result = null;
			return msgPeerBecameAvailable;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPeerBecameAvailable)
			{
				return MergeFrom((MsgPeerBecameAvailable)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPeerBecameAvailable other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPeerDisplayName)
			{
				PeerDisplayName = other.PeerDisplayName;
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
					PeerDisplayName = input.ReadString();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPeerDisplayName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPeerDisplayName = true;
			result.peerDisplayName_ = value;
			return this;
		}

		public Builder ClearPeerDisplayName()
		{
			result.hasPeerDisplayName = false;
			result.peerDisplayName_ = string.Empty;
			return this;
		}
	}

	public const int PeerDisplayNameFieldNumber = 1;

	private static readonly MsgPeerBecameAvailable defaultInstance;

	private bool hasPeerDisplayName;

	private string peerDisplayName_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static MsgPeerBecameAvailable DefaultInstance => defaultInstance;

	public override MsgPeerBecameAvailable DefaultInstanceForType => defaultInstance;

	protected override MsgPeerBecameAvailable ThisMessage => this;

	public bool HasPeerDisplayName => hasPeerDisplayName;

	public string PeerDisplayName => peerDisplayName_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPeerDisplayName)
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
			if (HasPeerDisplayName)
			{
				num += CodedOutputStream.ComputeStringSize(1, PeerDisplayName);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPeerBecameAvailable()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPeerDisplayName)
		{
			output.WriteString(1, PeerDisplayName);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPeerDisplayName)
		{
			num ^= peerDisplayName_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPeerBecameAvailable msgPeerBecameAvailable))
		{
			return false;
		}
		if (hasPeerDisplayName != msgPeerBecameAvailable.hasPeerDisplayName || (hasPeerDisplayName && !peerDisplayName_.Equals(msgPeerBecameAvailable.peerDisplayName_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPeerBecameAvailable, Builder>.PrintField("peerDisplayName", hasPeerDisplayName, peerDisplayName_, writer);
	}

	public static MsgPeerBecameAvailable ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPeerBecameAvailable ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPeerBecameAvailable prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
