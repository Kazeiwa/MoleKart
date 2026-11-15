using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgTransferThunderCloud : GeneratedMessageLite<MsgTransferThunderCloud, MsgTransferThunderCloud.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgTransferThunderCloud, Builder>
	{
		private MsgTransferThunderCloud result = new MsgTransferThunderCloud();

		protected override Builder ThisBuilder => this;

		protected override MsgTransferThunderCloud MessageBeingBuilt => result;

		public override MsgTransferThunderCloud DefaultInstanceForType => DefaultInstance;

		public bool HasFromCar => result.HasFromCar;

		public int FromCar
		{
			get
			{
				return result.FromCar;
			}
			set
			{
				SetFromCar(value);
			}
		}

		public bool HasDestCar => result.HasDestCar;

		public int DestCar
		{
			get
			{
				return result.DestCar;
			}
			set
			{
				SetDestCar(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgTransferThunderCloud();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgTransferThunderCloud BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgTransferThunderCloud msgTransferThunderCloud = result;
			result = null;
			return msgTransferThunderCloud;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgTransferThunderCloud)
			{
				return MergeFrom((MsgTransferThunderCloud)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgTransferThunderCloud other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasFromCar)
			{
				FromCar = other.FromCar;
			}
			if (other.HasDestCar)
			{
				DestCar = other.DestCar;
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
					FromCar = input.ReadInt32();
					continue;
				case 16u:
					DestCar = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetFromCar(int value)
		{
			result.hasFromCar = true;
			result.fromCar_ = value;
			return this;
		}

		public Builder ClearFromCar()
		{
			result.hasFromCar = false;
			result.fromCar_ = 0;
			return this;
		}

		public Builder SetDestCar(int value)
		{
			result.hasDestCar = true;
			result.destCar_ = value;
			return this;
		}

		public Builder ClearDestCar()
		{
			result.hasDestCar = false;
			result.destCar_ = 0;
			return this;
		}
	}

	public const int FromCarFieldNumber = 1;

	public const int DestCarFieldNumber = 2;

	private static readonly MsgTransferThunderCloud defaultInstance;

	private bool hasFromCar;

	private int fromCar_;

	private bool hasDestCar;

	private int destCar_;

	private int memoizedSerializedSize = -1;

	public static MsgTransferThunderCloud DefaultInstance => defaultInstance;

	public override MsgTransferThunderCloud DefaultInstanceForType => defaultInstance;

	protected override MsgTransferThunderCloud ThisMessage => this;

	public bool HasFromCar => hasFromCar;

	public int FromCar => fromCar_;

	public bool HasDestCar => hasDestCar;

	public int DestCar => destCar_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasFromCar)
			{
				return false;
			}
			if (!hasDestCar)
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
			if (HasFromCar)
			{
				num += CodedOutputStream.ComputeInt32Size(1, FromCar);
			}
			if (HasDestCar)
			{
				num += CodedOutputStream.ComputeInt32Size(2, DestCar);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgTransferThunderCloud()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasFromCar)
		{
			output.WriteInt32(1, FromCar);
		}
		if (HasDestCar)
		{
			output.WriteInt32(2, DestCar);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasFromCar)
		{
			num ^= fromCar_.GetHashCode();
		}
		if (hasDestCar)
		{
			num ^= destCar_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgTransferThunderCloud msgTransferThunderCloud))
		{
			return false;
		}
		if (hasFromCar != msgTransferThunderCloud.hasFromCar || (hasFromCar && !fromCar_.Equals(msgTransferThunderCloud.fromCar_)))
		{
			return false;
		}
		if (hasDestCar != msgTransferThunderCloud.hasDestCar || (hasDestCar && !destCar_.Equals(msgTransferThunderCloud.destCar_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgTransferThunderCloud, Builder>.PrintField("fromCar", hasFromCar, fromCar_, writer);
		GeneratedMessageLite<MsgTransferThunderCloud, Builder>.PrintField("destCar", hasDestCar, destCar_, writer);
	}

	public static MsgTransferThunderCloud ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgTransferThunderCloud ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgTransferThunderCloud prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
