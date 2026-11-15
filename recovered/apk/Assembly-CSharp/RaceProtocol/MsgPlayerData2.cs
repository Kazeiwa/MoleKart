using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MsgPlayerData2 : GeneratedMessageLite<MsgPlayerData2, MsgPlayerData2.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MsgPlayerData2, Builder>
	{
		private MsgPlayerData2 result = new MsgPlayerData2();

		protected override Builder ThisBuilder => this;

		protected override MsgPlayerData2 MessageBeingBuilt => result;

		public override MsgPlayerData2 DefaultInstanceForType => DefaultInstance;

		public bool HasPlayerId => result.HasPlayerId;

		public int PlayerId
		{
			get
			{
				return result.PlayerId;
			}
			set
			{
				SetPlayerId(value);
			}
		}

		public bool HasPosX => result.HasPosX;

		public float PosX
		{
			get
			{
				return result.PosX;
			}
			set
			{
				SetPosX(value);
			}
		}

		public bool HasPosY => result.HasPosY;

		public float PosY
		{
			get
			{
				return result.PosY;
			}
			set
			{
				SetPosY(value);
			}
		}

		public bool HasPosZ => result.HasPosZ;

		public float PosZ
		{
			get
			{
				return result.PosZ;
			}
			set
			{
				SetPosZ(value);
			}
		}

		public bool HasEulerX => result.HasEulerX;

		public float EulerX
		{
			get
			{
				return result.EulerX;
			}
			set
			{
				SetEulerX(value);
			}
		}

		public bool HasEulerY => result.HasEulerY;

		public float EulerY
		{
			get
			{
				return result.EulerY;
			}
			set
			{
				SetEulerY(value);
			}
		}

		public bool HasEulerZ => result.HasEulerZ;

		public float EulerZ
		{
			get
			{
				return result.EulerZ;
			}
			set
			{
				SetEulerZ(value);
			}
		}

		public bool HasVecX => result.HasVecX;

		public float VecX
		{
			get
			{
				return result.VecX;
			}
			set
			{
				SetVecX(value);
			}
		}

		public bool HasVecY => result.HasVecY;

		public float VecY
		{
			get
			{
				return result.VecY;
			}
			set
			{
				SetVecY(value);
			}
		}

		public bool HasVecZ => result.HasVecZ;

		public float VecZ
		{
			get
			{
				return result.VecZ;
			}
			set
			{
				SetVecZ(value);
			}
		}

		public bool HasAx => result.HasAx;

		public float Ax
		{
			get
			{
				return result.Ax;
			}
			set
			{
				SetAx(value);
			}
		}

		public bool HasAy => result.HasAy;

		public float Ay
		{
			get
			{
				return result.Ay;
			}
			set
			{
				SetAy(value);
			}
		}

		public bool HasAz => result.HasAz;

		public float Az
		{
			get
			{
				return result.Az;
			}
			set
			{
				SetAz(value);
			}
		}

		public bool HasWy => result.HasWy;

		public float Wy
		{
			get
			{
				return result.Wy;
			}
			set
			{
				SetWy(value);
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

		public override Builder Clear()
		{
			result = new MsgPlayerData2();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MsgPlayerData2 BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MsgPlayerData2 msgPlayerData = result;
			result = null;
			return msgPlayerData;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MsgPlayerData2)
			{
				return MergeFrom((MsgPlayerData2)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MsgPlayerData2 other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasPlayerId)
			{
				PlayerId = other.PlayerId;
			}
			if (other.HasPosX)
			{
				PosX = other.PosX;
			}
			if (other.HasPosY)
			{
				PosY = other.PosY;
			}
			if (other.HasPosZ)
			{
				PosZ = other.PosZ;
			}
			if (other.HasEulerX)
			{
				EulerX = other.EulerX;
			}
			if (other.HasEulerY)
			{
				EulerY = other.EulerY;
			}
			if (other.HasEulerZ)
			{
				EulerZ = other.EulerZ;
			}
			if (other.HasVecX)
			{
				VecX = other.VecX;
			}
			if (other.HasVecY)
			{
				VecY = other.VecY;
			}
			if (other.HasVecZ)
			{
				VecZ = other.VecZ;
			}
			if (other.HasAx)
			{
				Ax = other.Ax;
			}
			if (other.HasAy)
			{
				Ay = other.Ay;
			}
			if (other.HasAz)
			{
				Az = other.Az;
			}
			if (other.HasWy)
			{
				Wy = other.Wy;
			}
			if (other.HasCarState)
			{
				CarState = other.CarState;
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
					PlayerId = input.ReadInt32();
					continue;
				case 21u:
					PosX = input.ReadFloat();
					continue;
				case 29u:
					PosY = input.ReadFloat();
					continue;
				case 37u:
					PosZ = input.ReadFloat();
					continue;
				case 45u:
					EulerX = input.ReadFloat();
					continue;
				case 53u:
					EulerY = input.ReadFloat();
					continue;
				case 61u:
					EulerZ = input.ReadFloat();
					continue;
				case 69u:
					VecX = input.ReadFloat();
					continue;
				case 77u:
					VecY = input.ReadFloat();
					continue;
				case 85u:
					VecZ = input.ReadFloat();
					continue;
				case 93u:
					Ax = input.ReadFloat();
					continue;
				case 101u:
					Ay = input.ReadFloat();
					continue;
				case 109u:
					Az = input.ReadFloat();
					continue;
				case 117u:
					Wy = input.ReadFloat();
					continue;
				case 120u:
					CarState = input.ReadInt32();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		public Builder SetPlayerId(int value)
		{
			result.hasPlayerId = true;
			result.playerId_ = value;
			return this;
		}

		public Builder ClearPlayerId()
		{
			result.hasPlayerId = false;
			result.playerId_ = 0;
			return this;
		}

		public Builder SetPosX(float value)
		{
			result.hasPosX = true;
			result.posX_ = value;
			return this;
		}

		public Builder ClearPosX()
		{
			result.hasPosX = false;
			result.posX_ = 0f;
			return this;
		}

		public Builder SetPosY(float value)
		{
			result.hasPosY = true;
			result.posY_ = value;
			return this;
		}

		public Builder ClearPosY()
		{
			result.hasPosY = false;
			result.posY_ = 0f;
			return this;
		}

		public Builder SetPosZ(float value)
		{
			result.hasPosZ = true;
			result.posZ_ = value;
			return this;
		}

		public Builder ClearPosZ()
		{
			result.hasPosZ = false;
			result.posZ_ = 0f;
			return this;
		}

		public Builder SetEulerX(float value)
		{
			result.hasEulerX = true;
			result.eulerX_ = value;
			return this;
		}

		public Builder ClearEulerX()
		{
			result.hasEulerX = false;
			result.eulerX_ = 0f;
			return this;
		}

		public Builder SetEulerY(float value)
		{
			result.hasEulerY = true;
			result.eulerY_ = value;
			return this;
		}

		public Builder ClearEulerY()
		{
			result.hasEulerY = false;
			result.eulerY_ = 0f;
			return this;
		}

		public Builder SetEulerZ(float value)
		{
			result.hasEulerZ = true;
			result.eulerZ_ = value;
			return this;
		}

		public Builder ClearEulerZ()
		{
			result.hasEulerZ = false;
			result.eulerZ_ = 0f;
			return this;
		}

		public Builder SetVecX(float value)
		{
			result.hasVecX = true;
			result.vecX_ = value;
			return this;
		}

		public Builder ClearVecX()
		{
			result.hasVecX = false;
			result.vecX_ = 0f;
			return this;
		}

		public Builder SetVecY(float value)
		{
			result.hasVecY = true;
			result.vecY_ = value;
			return this;
		}

		public Builder ClearVecY()
		{
			result.hasVecY = false;
			result.vecY_ = 0f;
			return this;
		}

		public Builder SetVecZ(float value)
		{
			result.hasVecZ = true;
			result.vecZ_ = value;
			return this;
		}

		public Builder ClearVecZ()
		{
			result.hasVecZ = false;
			result.vecZ_ = 0f;
			return this;
		}

		public Builder SetAx(float value)
		{
			result.hasAx = true;
			result.ax_ = value;
			return this;
		}

		public Builder ClearAx()
		{
			result.hasAx = false;
			result.ax_ = 0f;
			return this;
		}

		public Builder SetAy(float value)
		{
			result.hasAy = true;
			result.ay_ = value;
			return this;
		}

		public Builder ClearAy()
		{
			result.hasAy = false;
			result.ay_ = 0f;
			return this;
		}

		public Builder SetAz(float value)
		{
			result.hasAz = true;
			result.az_ = value;
			return this;
		}

		public Builder ClearAz()
		{
			result.hasAz = false;
			result.az_ = 0f;
			return this;
		}

		public Builder SetWy(float value)
		{
			result.hasWy = true;
			result.wy_ = value;
			return this;
		}

		public Builder ClearWy()
		{
			result.hasWy = false;
			result.wy_ = 0f;
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
	}

	public const int PlayerIdFieldNumber = 1;

	public const int PosXFieldNumber = 2;

	public const int PosYFieldNumber = 3;

	public const int PosZFieldNumber = 4;

	public const int EulerXFieldNumber = 5;

	public const int EulerYFieldNumber = 6;

	public const int EulerZFieldNumber = 7;

	public const int VecXFieldNumber = 8;

	public const int VecYFieldNumber = 9;

	public const int VecZFieldNumber = 10;

	public const int AxFieldNumber = 11;

	public const int AyFieldNumber = 12;

	public const int AzFieldNumber = 13;

	public const int WyFieldNumber = 14;

	public const int CarStateFieldNumber = 15;

	private static readonly MsgPlayerData2 defaultInstance;

	private bool hasPlayerId;

	private int playerId_;

	private bool hasPosX;

	private float posX_;

	private bool hasPosY;

	private float posY_;

	private bool hasPosZ;

	private float posZ_;

	private bool hasEulerX;

	private float eulerX_;

	private bool hasEulerY;

	private float eulerY_;

	private bool hasEulerZ;

	private float eulerZ_;

	private bool hasVecX;

	private float vecX_;

	private bool hasVecY;

	private float vecY_;

	private bool hasVecZ;

	private float vecZ_;

	private bool hasAx;

	private float ax_;

	private bool hasAy;

	private float ay_;

	private bool hasAz;

	private float az_;

	private bool hasWy;

	private float wy_;

	private bool hasCarState;

	private int carState_;

	private int memoizedSerializedSize = -1;

	public static MsgPlayerData2 DefaultInstance => defaultInstance;

	public override MsgPlayerData2 DefaultInstanceForType => defaultInstance;

	protected override MsgPlayerData2 ThisMessage => this;

	public bool HasPlayerId => hasPlayerId;

	public int PlayerId => playerId_;

	public bool HasPosX => hasPosX;

	public float PosX => posX_;

	public bool HasPosY => hasPosY;

	public float PosY => posY_;

	public bool HasPosZ => hasPosZ;

	public float PosZ => posZ_;

	public bool HasEulerX => hasEulerX;

	public float EulerX => eulerX_;

	public bool HasEulerY => hasEulerY;

	public float EulerY => eulerY_;

	public bool HasEulerZ => hasEulerZ;

	public float EulerZ => eulerZ_;

	public bool HasVecX => hasVecX;

	public float VecX => vecX_;

	public bool HasVecY => hasVecY;

	public float VecY => vecY_;

	public bool HasVecZ => hasVecZ;

	public float VecZ => vecZ_;

	public bool HasAx => hasAx;

	public float Ax => ax_;

	public bool HasAy => hasAy;

	public float Ay => ay_;

	public bool HasAz => hasAz;

	public float Az => az_;

	public bool HasWy => hasWy;

	public float Wy => wy_;

	public bool HasCarState => hasCarState;

	public int CarState => carState_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasPlayerId)
			{
				return false;
			}
			if (!hasPosX)
			{
				return false;
			}
			if (!hasPosY)
			{
				return false;
			}
			if (!hasPosZ)
			{
				return false;
			}
			if (!hasEulerX)
			{
				return false;
			}
			if (!hasEulerY)
			{
				return false;
			}
			if (!hasEulerZ)
			{
				return false;
			}
			if (!hasVecX)
			{
				return false;
			}
			if (!hasVecY)
			{
				return false;
			}
			if (!hasVecZ)
			{
				return false;
			}
			if (!hasAx)
			{
				return false;
			}
			if (!hasAy)
			{
				return false;
			}
			if (!hasAz)
			{
				return false;
			}
			if (!hasWy)
			{
				return false;
			}
			if (!hasCarState)
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
			if (HasPlayerId)
			{
				num += CodedOutputStream.ComputeInt32Size(1, PlayerId);
			}
			if (HasPosX)
			{
				num += CodedOutputStream.ComputeFloatSize(2, PosX);
			}
			if (HasPosY)
			{
				num += CodedOutputStream.ComputeFloatSize(3, PosY);
			}
			if (HasPosZ)
			{
				num += CodedOutputStream.ComputeFloatSize(4, PosZ);
			}
			if (HasEulerX)
			{
				num += CodedOutputStream.ComputeFloatSize(5, EulerX);
			}
			if (HasEulerY)
			{
				num += CodedOutputStream.ComputeFloatSize(6, EulerY);
			}
			if (HasEulerZ)
			{
				num += CodedOutputStream.ComputeFloatSize(7, EulerZ);
			}
			if (HasVecX)
			{
				num += CodedOutputStream.ComputeFloatSize(8, VecX);
			}
			if (HasVecY)
			{
				num += CodedOutputStream.ComputeFloatSize(9, VecY);
			}
			if (HasVecZ)
			{
				num += CodedOutputStream.ComputeFloatSize(10, VecZ);
			}
			if (HasAx)
			{
				num += CodedOutputStream.ComputeFloatSize(11, Ax);
			}
			if (HasAy)
			{
				num += CodedOutputStream.ComputeFloatSize(12, Ay);
			}
			if (HasAz)
			{
				num += CodedOutputStream.ComputeFloatSize(13, Az);
			}
			if (HasWy)
			{
				num += CodedOutputStream.ComputeFloatSize(14, Wy);
			}
			if (HasCarState)
			{
				num += CodedOutputStream.ComputeInt32Size(15, CarState);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MsgPlayerData2()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasPlayerId)
		{
			output.WriteInt32(1, PlayerId);
		}
		if (HasPosX)
		{
			output.WriteFloat(2, PosX);
		}
		if (HasPosY)
		{
			output.WriteFloat(3, PosY);
		}
		if (HasPosZ)
		{
			output.WriteFloat(4, PosZ);
		}
		if (HasEulerX)
		{
			output.WriteFloat(5, EulerX);
		}
		if (HasEulerY)
		{
			output.WriteFloat(6, EulerY);
		}
		if (HasEulerZ)
		{
			output.WriteFloat(7, EulerZ);
		}
		if (HasVecX)
		{
			output.WriteFloat(8, VecX);
		}
		if (HasVecY)
		{
			output.WriteFloat(9, VecY);
		}
		if (HasVecZ)
		{
			output.WriteFloat(10, VecZ);
		}
		if (HasAx)
		{
			output.WriteFloat(11, Ax);
		}
		if (HasAy)
		{
			output.WriteFloat(12, Ay);
		}
		if (HasAz)
		{
			output.WriteFloat(13, Az);
		}
		if (HasWy)
		{
			output.WriteFloat(14, Wy);
		}
		if (HasCarState)
		{
			output.WriteInt32(15, CarState);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasPlayerId)
		{
			num ^= playerId_.GetHashCode();
		}
		if (hasPosX)
		{
			num ^= posX_.GetHashCode();
		}
		if (hasPosY)
		{
			num ^= posY_.GetHashCode();
		}
		if (hasPosZ)
		{
			num ^= posZ_.GetHashCode();
		}
		if (hasEulerX)
		{
			num ^= eulerX_.GetHashCode();
		}
		if (hasEulerY)
		{
			num ^= eulerY_.GetHashCode();
		}
		if (hasEulerZ)
		{
			num ^= eulerZ_.GetHashCode();
		}
		if (hasVecX)
		{
			num ^= vecX_.GetHashCode();
		}
		if (hasVecY)
		{
			num ^= vecY_.GetHashCode();
		}
		if (hasVecZ)
		{
			num ^= vecZ_.GetHashCode();
		}
		if (hasAx)
		{
			num ^= ax_.GetHashCode();
		}
		if (hasAy)
		{
			num ^= ay_.GetHashCode();
		}
		if (hasAz)
		{
			num ^= az_.GetHashCode();
		}
		if (hasWy)
		{
			num ^= wy_.GetHashCode();
		}
		if (hasCarState)
		{
			num ^= carState_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MsgPlayerData2 msgPlayerData))
		{
			return false;
		}
		if (hasPlayerId != msgPlayerData.hasPlayerId || (hasPlayerId && !playerId_.Equals(msgPlayerData.playerId_)))
		{
			return false;
		}
		if (hasPosX != msgPlayerData.hasPosX || (hasPosX && !posX_.Equals(msgPlayerData.posX_)))
		{
			return false;
		}
		if (hasPosY != msgPlayerData.hasPosY || (hasPosY && !posY_.Equals(msgPlayerData.posY_)))
		{
			return false;
		}
		if (hasPosZ != msgPlayerData.hasPosZ || (hasPosZ && !posZ_.Equals(msgPlayerData.posZ_)))
		{
			return false;
		}
		if (hasEulerX != msgPlayerData.hasEulerX || (hasEulerX && !eulerX_.Equals(msgPlayerData.eulerX_)))
		{
			return false;
		}
		if (hasEulerY != msgPlayerData.hasEulerY || (hasEulerY && !eulerY_.Equals(msgPlayerData.eulerY_)))
		{
			return false;
		}
		if (hasEulerZ != msgPlayerData.hasEulerZ || (hasEulerZ && !eulerZ_.Equals(msgPlayerData.eulerZ_)))
		{
			return false;
		}
		if (hasVecX != msgPlayerData.hasVecX || (hasVecX && !vecX_.Equals(msgPlayerData.vecX_)))
		{
			return false;
		}
		if (hasVecY != msgPlayerData.hasVecY || (hasVecY && !vecY_.Equals(msgPlayerData.vecY_)))
		{
			return false;
		}
		if (hasVecZ != msgPlayerData.hasVecZ || (hasVecZ && !vecZ_.Equals(msgPlayerData.vecZ_)))
		{
			return false;
		}
		if (hasAx != msgPlayerData.hasAx || (hasAx && !ax_.Equals(msgPlayerData.ax_)))
		{
			return false;
		}
		if (hasAy != msgPlayerData.hasAy || (hasAy && !ay_.Equals(msgPlayerData.ay_)))
		{
			return false;
		}
		if (hasAz != msgPlayerData.hasAz || (hasAz && !az_.Equals(msgPlayerData.az_)))
		{
			return false;
		}
		if (hasWy != msgPlayerData.hasWy || (hasWy && !wy_.Equals(msgPlayerData.wy_)))
		{
			return false;
		}
		if (hasCarState != msgPlayerData.hasCarState || (hasCarState && !carState_.Equals(msgPlayerData.carState_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("playerId", hasPlayerId, playerId_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("PosX", hasPosX, posX_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("PosY", hasPosY, posY_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("PosZ", hasPosZ, posZ_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("EulerX", hasEulerX, eulerX_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("EulerY", hasEulerY, eulerY_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("EulerZ", hasEulerZ, eulerZ_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("VecX", hasVecX, vecX_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("VecY", hasVecY, vecY_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("VecZ", hasVecZ, vecZ_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("Ax", hasAx, ax_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("Ay", hasAy, ay_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("Az", hasAz, az_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("Wy", hasWy, wy_, writer);
		GeneratedMessageLite<MsgPlayerData2, Builder>.PrintField("CarState", hasCarState, carState_, writer);
	}

	public static MsgPlayerData2 ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerData2 ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MsgPlayerData2 ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MsgPlayerData2 ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MsgPlayerData2 prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
