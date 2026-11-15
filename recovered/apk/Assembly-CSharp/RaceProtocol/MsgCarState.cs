using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgCarState : GeneratedMessageLite<MsgCarState, MsgCarState.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgCarState, Builder>
	{
		private MsgCarState result = new MsgCarState();

		protected override Builder ThisBuilder => this;

		protected override MsgCarState MessageBeingBuilt => result;

		public override MsgCarState DefaultInstanceForType => DefaultInstance;

		public bool HasCarID => result.HasCarID;

		public int CarID
		{
			get
			{
				return result.CarID;
			}
			set
			{
				SetCarID(value);
			}
		}

		public bool HasCarState => result.HasCarState;

		public int CarState
		{
			get
			{
				return result.CarState;
			}
			set
			{
				SetCarState(value);
			}
		}

		public bool HasXDir => result.HasXDir;

		public float XDir
		{
			get
			{
				return result.XDir;
			}
			set
			{
				SetXDir(value);
			}
		}

		public bool HasYDir => result.HasYDir;

		public float YDir
		{
			get
			{
				return result.YDir;
			}
			set
			{
				SetYDir(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgCarState();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgCarState BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgCarState msgCarState = result;
			result = null;
			return msgCarState;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgCarState)
			{
				return MergeFrom((MsgCarState)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgCarState other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasCarID)
			{
				CarID = other.CarID;
			}
			if (other.HasCarState)
			{
				CarState = other.CarState;
			}
			if (other.HasXDir)
			{
				XDir = other.XDir;
			}
			if (other.HasYDir)
			{
				YDir = other.YDir;
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
					CarID = input.ReadInt32();
					continue;
				case 16u:
					CarState = input.ReadInt32();
					continue;
				case 29u:
					XDir = input.ReadFloat();
					continue;
				case 37u:
					YDir = input.ReadFloat();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetCarID(int value)
		{
			result.hasCarID = true;
			result.carID_ = value;
			return this;
		}

		public Builder ClearCarID()
		{
			result.hasCarID = false;
			result.carID_ = 0;
			return this;
		}

		public Builder SetCarState(int value)
		{
			result.hasCarState = true;
			result.carState_ = value;
			return this;
		}

		public Builder ClearCarState()
		{
			result.hasCarState = false;
			result.carState_ = 0;
			return this;
		}

		public Builder SetXDir(float value)
		{
			result.hasXDir = true;
			result.xDir_ = value;
			return this;
		}

		public Builder ClearXDir()
		{
			result.hasXDir = false;
			result.xDir_ = 0f;
			return this;
		}

		public Builder SetYDir(float value)
		{
			result.hasYDir = true;
			result.yDir_ = value;
			return this;
		}

		public Builder ClearYDir()
		{
			result.hasYDir = false;
			result.yDir_ = 0f;
			return this;
		}
	}

	public const int CarIDFieldNumber = 1;

	public const int CarStateFieldNumber = 2;

	public const int XDirFieldNumber = 3;

	public const int YDirFieldNumber = 4;

	private static readonly MsgCarState defaultInstance;

	private bool hasCarID;

	private int carID_;

	private bool hasCarState;

	private int carState_;

	private bool hasXDir;

	private float xDir_;

	private bool hasYDir;

	private float yDir_;

	private int memoizedSerializedSize = -1;

	public static MsgCarState DefaultInstance => defaultInstance;

	public override MsgCarState DefaultInstanceForType => defaultInstance;

	protected override MsgCarState ThisMessage => this;

	public bool HasCarID => hasCarID;

	public int CarID => carID_;

	public bool HasCarState => hasCarState;

	public int CarState => carState_;

	public bool HasXDir => hasXDir;

	public float XDir => xDir_;

	public bool HasYDir => hasYDir;

	public float YDir => yDir_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasCarID)
			{
				return false;
			}
			if (!hasCarState)
			{
				return false;
			}
			if (!hasXDir)
			{
				return false;
			}
			if (!hasYDir)
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
			if (HasCarID)
			{
				num += CodedOutputStream.ComputeInt32Size(1, CarID);
			}
			if (HasCarState)
			{
				num += CodedOutputStream.ComputeInt32Size(2, CarState);
			}
			if (HasXDir)
			{
				num += CodedOutputStream.ComputeFloatSize(3, XDir);
			}
			if (HasYDir)
			{
				num += CodedOutputStream.ComputeFloatSize(4, YDir);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgCarState()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasCarID)
		{
			output.WriteInt32(1, CarID);
		}
		if (HasCarState)
		{
			output.WriteInt32(2, CarState);
		}
		if (HasXDir)
		{
			output.WriteFloat(3, XDir);
		}
		if (HasYDir)
		{
			output.WriteFloat(4, YDir);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasCarID)
		{
			num ^= carID_.GetHashCode();
		}
		if (hasCarState)
		{
			num ^= carState_.GetHashCode();
		}
		if (hasXDir)
		{
			num ^= xDir_.GetHashCode();
		}
		if (hasYDir)
		{
			num ^= yDir_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgCarState msgCarState))
		{
			return false;
		}
		if (hasCarID != msgCarState.hasCarID || (hasCarID && !carID_.Equals(msgCarState.carID_)))
		{
			return false;
		}
		if (hasCarState != msgCarState.hasCarState || (hasCarState && !carState_.Equals(msgCarState.carState_)))
		{
			return false;
		}
		if (hasXDir != msgCarState.hasXDir || (hasXDir && !xDir_.Equals(msgCarState.xDir_)))
		{
			return false;
		}
		if (hasYDir != msgCarState.hasYDir || (hasYDir && !yDir_.Equals(msgCarState.yDir_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgCarState, Builder>.PrintField("carID", hasCarID, carID_, writer);
		GeneratedMessageLite<MsgCarState, Builder>.PrintField("carState", hasCarState, carState_, writer);
		GeneratedMessageLite<MsgCarState, Builder>.PrintField("xDir", hasXDir, xDir_, writer);
		GeneratedMessageLite<MsgCarState, Builder>.PrintField("yDir", hasYDir, yDir_, writer);
	}

	public static MsgCarState ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgCarState ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgCarState ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgCarState ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgCarState ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgCarState ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgCarState ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgCarState ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgCarState ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgCarState ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgCarState prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
