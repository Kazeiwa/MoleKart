using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class MyScorePercentReq : GeneratedMessageLite<MyScorePercentReq, MyScorePercentReq.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<MyScorePercentReq, Builder>
	{
		private MyScorePercentReq result = new MyScorePercentReq();

		protected override Builder ThisBuilder => this;

		protected override MyScorePercentReq MessageBeingBuilt => result;

		public override MyScorePercentReq DefaultInstanceForType => DefaultInstance;

		public bool HasMode => result.HasMode;

		[CLSCompliant(false)]
		public uint Mode
		{
			get
			{
				return result.Mode;
			}
			set
			{
				SetMode(value);
			}
		}

		public bool HasTrack => result.HasTrack;

		[CLSCompliant(false)]
		public uint Track
		{
			get
			{
				return result.Track;
			}
			set
			{
				SetTrack(value);
			}
		}

		public bool HasRankType => result.HasRankType;

		[CLSCompliant(false)]
		public uint RankType
		{
			get
			{
				return result.RankType;
			}
			set
			{
				SetRankType(value);
			}
		}

		public bool HasDeviceId => result.HasDeviceId;

		public string DeviceId
		{
			get
			{
				return result.DeviceId;
			}
			set
			{
				SetDeviceId(value);
			}
		}

		public override Builder Clear()
		{
			result = new MyScorePercentReq();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override MyScorePercentReq BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			MyScorePercentReq myScorePercentReq = result;
			result = null;
			return myScorePercentReq;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is MyScorePercentReq)
			{
				return MergeFrom((MyScorePercentReq)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(MyScorePercentReq other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasMode)
			{
				Mode = other.Mode;
			}
			if (other.HasTrack)
			{
				Track = other.Track;
			}
			if (other.HasRankType)
			{
				RankType = other.RankType;
			}
			if (other.HasDeviceId)
			{
				DeviceId = other.DeviceId;
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
					Mode = input.ReadUInt32();
					continue;
				case 16u:
					Track = input.ReadUInt32();
					continue;
				case 24u:
					RankType = input.ReadUInt32();
					continue;
				case 34u:
					DeviceId = input.ReadString();
					continue;
				}
				if (WireFormat.IsEndGroupTag(num))
				{
					return this;
				}
				ParseUnknownField(input, extensionRegistry, num);
			}
		}

		[CLSCompliant(false)]
		public Builder SetMode(uint value)
		{
			result.hasMode = true;
			result.mode_ = value;
			return this;
		}

		public Builder ClearMode()
		{
			result.hasMode = false;
			result.mode_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetTrack(uint value)
		{
			result.hasTrack = true;
			result.track_ = value;
			return this;
		}

		public Builder ClearTrack()
		{
			result.hasTrack = false;
			result.track_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetRankType(uint value)
		{
			result.hasRankType = true;
			result.rankType_ = value;
			return this;
		}

		public Builder ClearRankType()
		{
			result.hasRankType = false;
			result.rankType_ = 0u;
			return this;
		}

		public Builder SetDeviceId(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasDeviceId = true;
			result.deviceId_ = value;
			return this;
		}

		public Builder ClearDeviceId()
		{
			result.hasDeviceId = false;
			result.deviceId_ = string.Empty;
			return this;
		}
	}

	public const int ModeFieldNumber = 1;

	public const int TrackFieldNumber = 2;

	public const int RankTypeFieldNumber = 3;

	public const int DeviceIdFieldNumber = 4;

	private static readonly MyScorePercentReq defaultInstance;

	private bool hasMode;

	private uint mode_;

	private bool hasTrack;

	private uint track_;

	private bool hasRankType;

	private uint rankType_;

	private bool hasDeviceId;

	private string deviceId_ = string.Empty;

	private int memoizedSerializedSize = -1;

	public static MyScorePercentReq DefaultInstance => defaultInstance;

	public override MyScorePercentReq DefaultInstanceForType => defaultInstance;

	protected override MyScorePercentReq ThisMessage => this;

	public bool HasMode => hasMode;

	[CLSCompliant(false)]
	public uint Mode => mode_;

	public bool HasTrack => hasTrack;

	[CLSCompliant(false)]
	public uint Track => track_;

	public bool HasRankType => hasRankType;

	[CLSCompliant(false)]
	public uint RankType => rankType_;

	public bool HasDeviceId => hasDeviceId;

	public string DeviceId => deviceId_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasMode)
			{
				return false;
			}
			if (!hasTrack)
			{
				return false;
			}
			if (!hasRankType)
			{
				return false;
			}
			if (!hasDeviceId)
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
			if (HasMode)
			{
				num += CodedOutputStream.ComputeUInt32Size(1, Mode);
			}
			if (HasTrack)
			{
				num += CodedOutputStream.ComputeUInt32Size(2, Track);
			}
			if (HasRankType)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, RankType);
			}
			if (HasDeviceId)
			{
				num += CodedOutputStream.ComputeStringSize(4, DeviceId);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static MyScorePercentReq()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasMode)
		{
			output.WriteUInt32(1, Mode);
		}
		if (HasTrack)
		{
			output.WriteUInt32(2, Track);
		}
		if (HasRankType)
		{
			output.WriteUInt32(3, RankType);
		}
		if (HasDeviceId)
		{
			output.WriteString(4, DeviceId);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasMode)
		{
			num ^= mode_.GetHashCode();
		}
		if (hasTrack)
		{
			num ^= track_.GetHashCode();
		}
		if (hasRankType)
		{
			num ^= rankType_.GetHashCode();
		}
		if (hasDeviceId)
		{
			num ^= deviceId_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is MyScorePercentReq myScorePercentReq))
		{
			return false;
		}
		if (hasMode != myScorePercentReq.hasMode || (hasMode && !mode_.Equals(myScorePercentReq.mode_)))
		{
			return false;
		}
		if (hasTrack != myScorePercentReq.hasTrack || (hasTrack && !track_.Equals(myScorePercentReq.track_)))
		{
			return false;
		}
		if (hasRankType != myScorePercentReq.hasRankType || (hasRankType && !rankType_.Equals(myScorePercentReq.rankType_)))
		{
			return false;
		}
		if (hasDeviceId != myScorePercentReq.hasDeviceId || (hasDeviceId && !deviceId_.Equals(myScorePercentReq.deviceId_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<MyScorePercentReq, Builder>.PrintField("mode", hasMode, mode_, writer);
		GeneratedMessageLite<MyScorePercentReq, Builder>.PrintField("track", hasTrack, track_, writer);
		GeneratedMessageLite<MyScorePercentReq, Builder>.PrintField("rankType", hasRankType, rankType_, writer);
		GeneratedMessageLite<MyScorePercentReq, Builder>.PrintField("deviceId", hasDeviceId, deviceId_, writer);
	}

	public static MyScorePercentReq ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentReq ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static MyScorePercentReq ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static MyScorePercentReq ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(MyScorePercentReq prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
