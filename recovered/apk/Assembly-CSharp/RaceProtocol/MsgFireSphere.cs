using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgFireSphere : GeneratedMessageLite<MsgFireSphere, MsgFireSphere.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgFireSphere, Builder>
	{
		private MsgFireSphere result = new MsgFireSphere();

		protected override Builder ThisBuilder => this;

		protected override MsgFireSphere MessageBeingBuilt => result;

		public override MsgFireSphere DefaultInstanceForType => DefaultInstance;

		public bool HasSpeed => result.HasSpeed;

		public float Speed
		{
			get
			{
				return result.Speed;
			}
			set
			{
				SetSpeed(value);
			}
		}

		public bool HasTime => result.HasTime;

		public float Time
		{
			get
			{
				return result.Time;
			}
			set
			{
				SetTime(value);
			}
		}

		public bool HasFromX => result.HasFromX;

		public float FromX
		{
			get
			{
				return result.FromX;
			}
			set
			{
				SetFromX(value);
			}
		}

		public bool HasFromY => result.HasFromY;

		public float FromY
		{
			get
			{
				return result.FromY;
			}
			set
			{
				SetFromY(value);
			}
		}

		public bool HasFromZ => result.HasFromZ;

		public float FromZ
		{
			get
			{
				return result.FromZ;
			}
			set
			{
				SetFromZ(value);
			}
		}

		public bool HasDestX => result.HasDestX;

		public float DestX
		{
			get
			{
				return result.DestX;
			}
			set
			{
				SetDestX(value);
			}
		}

		public bool HasDestY => result.HasDestY;

		public float DestY
		{
			get
			{
				return result.DestY;
			}
			set
			{
				SetDestY(value);
			}
		}

		public bool HasDestZ => result.HasDestZ;

		public float DestZ
		{
			get
			{
				return result.DestZ;
			}
			set
			{
				SetDestZ(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgFireSphere();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgFireSphere BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgFireSphere msgFireSphere = result;
			result = null;
			return msgFireSphere;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgFireSphere)
			{
				return MergeFrom((MsgFireSphere)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgFireSphere other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasSpeed)
			{
				Speed = other.Speed;
			}
			if (other.HasTime)
			{
				Time = other.Time;
			}
			if (other.HasFromX)
			{
				FromX = other.FromX;
			}
			if (other.HasFromY)
			{
				FromY = other.FromY;
			}
			if (other.HasFromZ)
			{
				FromZ = other.FromZ;
			}
			if (other.HasDestX)
			{
				DestX = other.DestX;
			}
			if (other.HasDestY)
			{
				DestY = other.DestY;
			}
			if (other.HasDestZ)
			{
				DestZ = other.DestZ;
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
				case 13u:
					Speed = input.ReadFloat();
					continue;
				case 21u:
					Time = input.ReadFloat();
					continue;
				case 29u:
					FromX = input.ReadFloat();
					continue;
				case 37u:
					FromY = input.ReadFloat();
					continue;
				case 45u:
					FromZ = input.ReadFloat();
					continue;
				case 53u:
					DestX = input.ReadFloat();
					continue;
				case 61u:
					DestY = input.ReadFloat();
					continue;
				case 69u:
					DestZ = input.ReadFloat();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetSpeed(float value)
		{
			result.hasSpeed = true;
			result.speed_ = value;
			return this;
		}

		public Builder ClearSpeed()
		{
			result.hasSpeed = false;
			result.speed_ = 0f;
			return this;
		}

		public Builder SetTime(float value)
		{
			result.hasTime = true;
			result.time_ = value;
			return this;
		}

		public Builder ClearTime()
		{
			result.hasTime = false;
			result.time_ = 0f;
			return this;
		}

		public Builder SetFromX(float value)
		{
			result.hasFromX = true;
			result.fromX_ = value;
			return this;
		}

		public Builder ClearFromX()
		{
			result.hasFromX = false;
			result.fromX_ = 0f;
			return this;
		}

		public Builder SetFromY(float value)
		{
			result.hasFromY = true;
			result.fromY_ = value;
			return this;
		}

		public Builder ClearFromY()
		{
			result.hasFromY = false;
			result.fromY_ = 0f;
			return this;
		}

		public Builder SetFromZ(float value)
		{
			result.hasFromZ = true;
			result.fromZ_ = value;
			return this;
		}

		public Builder ClearFromZ()
		{
			result.hasFromZ = false;
			result.fromZ_ = 0f;
			return this;
		}

		public Builder SetDestX(float value)
		{
			result.hasDestX = true;
			result.destX_ = value;
			return this;
		}

		public Builder ClearDestX()
		{
			result.hasDestX = false;
			result.destX_ = 0f;
			return this;
		}

		public Builder SetDestY(float value)
		{
			result.hasDestY = true;
			result.destY_ = value;
			return this;
		}

		public Builder ClearDestY()
		{
			result.hasDestY = false;
			result.destY_ = 0f;
			return this;
		}

		public Builder SetDestZ(float value)
		{
			result.hasDestZ = true;
			result.destZ_ = value;
			return this;
		}

		public Builder ClearDestZ()
		{
			result.hasDestZ = false;
			result.destZ_ = 0f;
			return this;
		}
	}

	public const int SpeedFieldNumber = 1;

	public const int TimeFieldNumber = 2;

	public const int FromXFieldNumber = 3;

	public const int FromYFieldNumber = 4;

	public const int FromZFieldNumber = 5;

	public const int DestXFieldNumber = 6;

	public const int DestYFieldNumber = 7;

	public const int DestZFieldNumber = 8;

	private static readonly MsgFireSphere defaultInstance;

	private bool hasSpeed;

	private float speed_;

	private bool hasTime;

	private float time_;

	private bool hasFromX;

	private float fromX_;

	private bool hasFromY;

	private float fromY_;

	private bool hasFromZ;

	private float fromZ_;

	private bool hasDestX;

	private float destX_;

	private bool hasDestY;

	private float destY_;

	private bool hasDestZ;

	private float destZ_;

	private int memoizedSerializedSize = -1;

	public static MsgFireSphere DefaultInstance => defaultInstance;

	public override MsgFireSphere DefaultInstanceForType => defaultInstance;

	protected override MsgFireSphere ThisMessage => this;

	public bool HasSpeed => hasSpeed;

	public float Speed => speed_;

	public bool HasTime => hasTime;

	public float Time => time_;

	public bool HasFromX => hasFromX;

	public float FromX => fromX_;

	public bool HasFromY => hasFromY;

	public float FromY => fromY_;

	public bool HasFromZ => hasFromZ;

	public float FromZ => fromZ_;

	public bool HasDestX => hasDestX;

	public float DestX => destX_;

	public bool HasDestY => hasDestY;

	public float DestY => destY_;

	public bool HasDestZ => hasDestZ;

	public float DestZ => destZ_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasSpeed)
			{
				return false;
			}
			if (!hasTime)
			{
				return false;
			}
			if (!hasFromX)
			{
				return false;
			}
			if (!hasFromY)
			{
				return false;
			}
			if (!hasFromZ)
			{
				return false;
			}
			if (!hasDestX)
			{
				return false;
			}
			if (!hasDestY)
			{
				return false;
			}
			if (!hasDestZ)
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
			if (HasSpeed)
			{
				num += CodedOutputStream.ComputeFloatSize(1, Speed);
			}
			if (HasTime)
			{
				num += CodedOutputStream.ComputeFloatSize(2, Time);
			}
			if (HasFromX)
			{
				num += CodedOutputStream.ComputeFloatSize(3, FromX);
			}
			if (HasFromY)
			{
				num += CodedOutputStream.ComputeFloatSize(4, FromY);
			}
			if (HasFromZ)
			{
				num += CodedOutputStream.ComputeFloatSize(5, FromZ);
			}
			if (HasDestX)
			{
				num += CodedOutputStream.ComputeFloatSize(6, DestX);
			}
			if (HasDestY)
			{
				num += CodedOutputStream.ComputeFloatSize(7, DestY);
			}
			if (HasDestZ)
			{
				num += CodedOutputStream.ComputeFloatSize(8, DestZ);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgFireSphere()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasSpeed)
		{
			output.WriteFloat(1, Speed);
		}
		if (HasTime)
		{
			output.WriteFloat(2, Time);
		}
		if (HasFromX)
		{
			output.WriteFloat(3, FromX);
		}
		if (HasFromY)
		{
			output.WriteFloat(4, FromY);
		}
		if (HasFromZ)
		{
			output.WriteFloat(5, FromZ);
		}
		if (HasDestX)
		{
			output.WriteFloat(6, DestX);
		}
		if (HasDestY)
		{
			output.WriteFloat(7, DestY);
		}
		if (HasDestZ)
		{
			output.WriteFloat(8, DestZ);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasSpeed)
		{
			num ^= speed_.GetHashCode();
		}
		if (hasTime)
		{
			num ^= time_.GetHashCode();
		}
		if (hasFromX)
		{
			num ^= fromX_.GetHashCode();
		}
		if (hasFromY)
		{
			num ^= fromY_.GetHashCode();
		}
		if (hasFromZ)
		{
			num ^= fromZ_.GetHashCode();
		}
		if (hasDestX)
		{
			num ^= destX_.GetHashCode();
		}
		if (hasDestY)
		{
			num ^= destY_.GetHashCode();
		}
		if (hasDestZ)
		{
			num ^= destZ_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgFireSphere msgFireSphere))
		{
			return false;
		}
		if (hasSpeed != msgFireSphere.hasSpeed || (hasSpeed && !speed_.Equals(msgFireSphere.speed_)))
		{
			return false;
		}
		if (hasTime != msgFireSphere.hasTime || (hasTime && !time_.Equals(msgFireSphere.time_)))
		{
			return false;
		}
		if (hasFromX != msgFireSphere.hasFromX || (hasFromX && !fromX_.Equals(msgFireSphere.fromX_)))
		{
			return false;
		}
		if (hasFromY != msgFireSphere.hasFromY || (hasFromY && !fromY_.Equals(msgFireSphere.fromY_)))
		{
			return false;
		}
		if (hasFromZ != msgFireSphere.hasFromZ || (hasFromZ && !fromZ_.Equals(msgFireSphere.fromZ_)))
		{
			return false;
		}
		if (hasDestX != msgFireSphere.hasDestX || (hasDestX && !destX_.Equals(msgFireSphere.destX_)))
		{
			return false;
		}
		if (hasDestY != msgFireSphere.hasDestY || (hasDestY && !destY_.Equals(msgFireSphere.destY_)))
		{
			return false;
		}
		if (hasDestZ != msgFireSphere.hasDestZ || (hasDestZ && !destZ_.Equals(msgFireSphere.destZ_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("speed", hasSpeed, speed_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("time", hasTime, time_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("from_X", hasFromX, fromX_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("from_Y", hasFromY, fromY_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("from_Z", hasFromZ, fromZ_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("dest_X", hasDestX, destX_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("dest_Y", hasDestY, destY_, writer);
		GeneratedMessageLite<MsgFireSphere, Builder>.PrintField("dest_Z", hasDestZ, destZ_, writer);
	}

	public static MsgFireSphere ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFireSphere ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgFireSphere ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgFireSphere ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgFireSphere prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
