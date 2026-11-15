using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgMonsterMovement : GeneratedMessageLite<MsgMonsterMovement, MsgMonsterMovement.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgMonsterMovement, Builder>
	{
		private MsgMonsterMovement result = new MsgMonsterMovement();

		protected override Builder ThisBuilder => this;

		protected override MsgMonsterMovement MessageBeingBuilt => result;

		public override MsgMonsterMovement DefaultInstanceForType => DefaultInstance;

		public bool HasX => result.HasX;

		public float X
		{
			get
			{
				return result.X;
			}
			set
			{
				SetX(value);
			}
		}

		public bool HasY => result.HasY;

		public float Y
		{
			get
			{
				return result.Y;
			}
			set
			{
				SetY(value);
			}
		}

		public bool HasVx => result.HasVx;

		public float Vx
		{
			get
			{
				return result.Vx;
			}
			set
			{
				SetVx(value);
			}
		}

		public bool HasVy => result.HasVy;

		public float Vy
		{
			get
			{
				return result.Vy;
			}
			set
			{
				SetVy(value);
			}
		}

		public bool HasRotAngle => result.HasRotAngle;

		public float RotAngle
		{
			get
			{
				return result.RotAngle;
			}
			set
			{
				SetRotAngle(value);
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

		public bool HasH => result.HasH;

		public float H
		{
			get
			{
				return result.H;
			}
			set
			{
				SetH(value);
			}
		}

		public bool HasVh => result.HasVh;

		public float Vh
		{
			get
			{
				return result.Vh;
			}
			set
			{
				SetVh(value);
			}
		}

		public override Builder Clear()
		{
			result = new MsgMonsterMovement();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgMonsterMovement BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgMonsterMovement msgMonsterMovement = result;
			result = null;
			return msgMonsterMovement;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgMonsterMovement)
			{
				return MergeFrom((MsgMonsterMovement)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgMonsterMovement other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasX)
			{
				X = other.X;
			}
			if (other.HasY)
			{
				Y = other.Y;
			}
			if (other.HasVx)
			{
				Vx = other.Vx;
			}
			if (other.HasVy)
			{
				Vy = other.Vy;
			}
			if (other.HasRotAngle)
			{
				RotAngle = other.RotAngle;
			}
			if (other.HasIndex)
			{
				Index = other.Index;
			}
			if (other.HasH)
			{
				H = other.H;
			}
			if (other.HasVh)
			{
				Vh = other.Vh;
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
					X = input.ReadFloat();
					continue;
				case 21u:
					Y = input.ReadFloat();
					continue;
				case 29u:
					Vx = input.ReadFloat();
					continue;
				case 37u:
					Vy = input.ReadFloat();
					continue;
				case 45u:
					RotAngle = input.ReadFloat();
					continue;
				case 48u:
					Index = input.ReadInt32();
					continue;
				case 61u:
					H = input.ReadFloat();
					continue;
				case 69u:
					Vh = input.ReadFloat();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetX(float value)
		{
			result.hasX = true;
			result.x_ = value;
			return this;
		}

		public Builder ClearX()
		{
			result.hasX = false;
			result.x_ = 0f;
			return this;
		}

		public Builder SetY(float value)
		{
			result.hasY = true;
			result.y_ = value;
			return this;
		}

		public Builder ClearY()
		{
			result.hasY = false;
			result.y_ = 0f;
			return this;
		}

		public Builder SetVx(float value)
		{
			result.hasVx = true;
			result.vx_ = value;
			return this;
		}

		public Builder ClearVx()
		{
			result.hasVx = false;
			result.vx_ = 0f;
			return this;
		}

		public Builder SetVy(float value)
		{
			result.hasVy = true;
			result.vy_ = value;
			return this;
		}

		public Builder ClearVy()
		{
			result.hasVy = false;
			result.vy_ = 0f;
			return this;
		}

		public Builder SetRotAngle(float value)
		{
			result.hasRotAngle = true;
			result.rotAngle_ = value;
			return this;
		}

		public Builder ClearRotAngle()
		{
			result.hasRotAngle = false;
			result.rotAngle_ = 0f;
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

		public Builder SetH(float value)
		{
			result.hasH = true;
			result.h_ = value;
			return this;
		}

		public Builder ClearH()
		{
			result.hasH = false;
			result.h_ = 0f;
			return this;
		}

		public Builder SetVh(float value)
		{
			result.hasVh = true;
			result.vh_ = value;
			return this;
		}

		public Builder ClearVh()
		{
			result.hasVh = false;
			result.vh_ = 0f;
			return this;
		}
	}

	public const int XFieldNumber = 1;

	public const int YFieldNumber = 2;

	public const int VxFieldNumber = 3;

	public const int VyFieldNumber = 4;

	public const int RotAngleFieldNumber = 5;

	public const int IndexFieldNumber = 6;

	public const int HFieldNumber = 7;

	public const int VhFieldNumber = 8;

	private static readonly MsgMonsterMovement defaultInstance;

	private bool hasX;

	private float x_;

	private bool hasY;

	private float y_;

	private bool hasVx;

	private float vx_;

	private bool hasVy;

	private float vy_;

	private bool hasRotAngle;

	private float rotAngle_;

	private bool hasIndex;

	private int index_;

	private bool hasH;

	private float h_;

	private bool hasVh;

	private float vh_;

	private int memoizedSerializedSize = -1;

	public static MsgMonsterMovement DefaultInstance => defaultInstance;

	public override MsgMonsterMovement DefaultInstanceForType => defaultInstance;

	protected override MsgMonsterMovement ThisMessage => this;

	public bool HasX => hasX;

	public float X => x_;

	public bool HasY => hasY;

	public float Y => y_;

	public bool HasVx => hasVx;

	public float Vx => vx_;

	public bool HasVy => hasVy;

	public float Vy => vy_;

	public bool HasRotAngle => hasRotAngle;

	public float RotAngle => rotAngle_;

	public bool HasIndex => hasIndex;

	public int Index => index_;

	public bool HasH => hasH;

	public float H => h_;

	public bool HasVh => hasVh;

	public float Vh => vh_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasX)
			{
				return false;
			}
			if (!hasY)
			{
				return false;
			}
			if (!hasVx)
			{
				return false;
			}
			if (!hasVy)
			{
				return false;
			}
			if (!hasRotAngle)
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
			if (HasX)
			{
				num += CodedOutputStream.ComputeFloatSize(1, X);
			}
			if (HasY)
			{
				num += CodedOutputStream.ComputeFloatSize(2, Y);
			}
			if (HasVx)
			{
				num += CodedOutputStream.ComputeFloatSize(3, Vx);
			}
			if (HasVy)
			{
				num += CodedOutputStream.ComputeFloatSize(4, Vy);
			}
			if (HasRotAngle)
			{
				num += CodedOutputStream.ComputeFloatSize(5, RotAngle);
			}
			if (HasIndex)
			{
				num += CodedOutputStream.ComputeInt32Size(6, Index);
			}
			if (HasH)
			{
				num += CodedOutputStream.ComputeFloatSize(7, H);
			}
			if (HasVh)
			{
				num += CodedOutputStream.ComputeFloatSize(8, Vh);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgMonsterMovement()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasX)
		{
			output.WriteFloat(1, X);
		}
		if (HasY)
		{
			output.WriteFloat(2, Y);
		}
		if (HasVx)
		{
			output.WriteFloat(3, Vx);
		}
		if (HasVy)
		{
			output.WriteFloat(4, Vy);
		}
		if (HasRotAngle)
		{
			output.WriteFloat(5, RotAngle);
		}
		if (HasIndex)
		{
			output.WriteInt32(6, Index);
		}
		if (HasH)
		{
			output.WriteFloat(7, H);
		}
		if (HasVh)
		{
			output.WriteFloat(8, Vh);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasX)
		{
			num ^= x_.GetHashCode();
		}
		if (hasY)
		{
			num ^= y_.GetHashCode();
		}
		if (hasVx)
		{
			num ^= vx_.GetHashCode();
		}
		if (hasVy)
		{
			num ^= vy_.GetHashCode();
		}
		if (hasRotAngle)
		{
			num ^= rotAngle_.GetHashCode();
		}
		if (hasIndex)
		{
			num ^= index_.GetHashCode();
		}
		if (hasH)
		{
			num ^= h_.GetHashCode();
		}
		if (hasVh)
		{
			num ^= vh_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgMonsterMovement msgMonsterMovement))
		{
			return false;
		}
		if (hasX != msgMonsterMovement.hasX || (hasX && !x_.Equals(msgMonsterMovement.x_)))
		{
			return false;
		}
		if (hasY != msgMonsterMovement.hasY || (hasY && !y_.Equals(msgMonsterMovement.y_)))
		{
			return false;
		}
		if (hasVx != msgMonsterMovement.hasVx || (hasVx && !vx_.Equals(msgMonsterMovement.vx_)))
		{
			return false;
		}
		if (hasVy != msgMonsterMovement.hasVy || (hasVy && !vy_.Equals(msgMonsterMovement.vy_)))
		{
			return false;
		}
		if (hasRotAngle != msgMonsterMovement.hasRotAngle || (hasRotAngle && !rotAngle_.Equals(msgMonsterMovement.rotAngle_)))
		{
			return false;
		}
		if (hasIndex != msgMonsterMovement.hasIndex || (hasIndex && !index_.Equals(msgMonsterMovement.index_)))
		{
			return false;
		}
		if (hasH != msgMonsterMovement.hasH || (hasH && !h_.Equals(msgMonsterMovement.h_)))
		{
			return false;
		}
		if (hasVh != msgMonsterMovement.hasVh || (hasVh && !vh_.Equals(msgMonsterMovement.vh_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("x", hasX, x_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("y", hasY, y_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("vx", hasVx, vx_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("vy", hasVy, vy_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("rotAngle", hasRotAngle, rotAngle_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("index", hasIndex, index_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("h", hasH, h_, writer);
		GeneratedMessageLite<MsgMonsterMovement, Builder>.PrintField("vh", hasVh, vh_, writer);
	}

	public static MsgMonsterMovement ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovement ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgMonsterMovement ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgMonsterMovement ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgMonsterMovement prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
