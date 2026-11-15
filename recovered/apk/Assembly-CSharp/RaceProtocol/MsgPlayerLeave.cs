using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayerLeave : GeneratedMessageLite<MsgPlayerLeave, MsgPlayerLeave.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayerLeave, Builder>
	{
		private MsgPlayerLeave result = new MsgPlayerLeave();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayerLeave MessageBeingBuilt => result;

		public override MsgPlayerLeave DefaultInstanceForType => DefaultInstance;

		public bool HasPeerId => result.HasPeerId;

		public string PeerId
		{
			get
			{
				return result.PeerId;
			}
			set
			{
				SetPeerId(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgPlayerLeave();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayerLeave BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayerLeave msgPlayerLeave = result;
			result = null;
			return msgPlayerLeave;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayerLeave)
			{
				return MergeFrom((MsgPlayerLeave)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayerLeave other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPeerId)
			{
				PeerId = other.PeerId;
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
					PeerId = input.ReadString();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPeerId(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasPeerId = true;
			result.peerId_ = value;
			return this;
		}

		public Builder ClearPeerId()
		{
			result.hasPeerId = false;
			result.peerId_ = string.Empty;
			return this;
		}
	}

	public const int PeerIdFieldNumber = 1;

	private static readonly MsgPlayerLeave defaultInstance;

	private bool hasPeerId;

	private string peerId_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static MsgPlayerLeave DefaultInstance => defaultInstance;

	public override MsgPlayerLeave DefaultInstanceForType => defaultInstance;

	protected override MsgPlayerLeave ThisMessage => this;

	public bool HasPeerId => hasPeerId;

	public string PeerId => peerId_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPeerId)
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
			if (HasPeerId)
			{
				num += CodedOutputStream.ComputeStringSize(1, PeerId);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayerLeave()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPeerId)
		{
			output.WriteString(1, PeerId);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPeerId)
		{
			num ^= peerId_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayerLeave msgPlayerLeave))
		{
			return false;
		}
		if (hasPeerId != msgPlayerLeave.hasPeerId || (hasPeerId && !peerId_.Equals(msgPlayerLeave.peerId_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayerLeave, Builder>.PrintField("peerId", hasPeerId, peerId_, writer);
	}

	public static MsgPlayerLeave ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerLeave ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayerLeave ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerLeave ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayerLeave prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
