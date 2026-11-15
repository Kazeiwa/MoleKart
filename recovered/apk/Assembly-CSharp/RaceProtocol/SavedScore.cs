using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class SavedScore : GeneratedMessageLite<SavedScore, SavedScore.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<SavedScore, Builder>
	{
		private SavedScore result = new SavedScore();

		protected override Builder ThisBuilder => this;

		protected override SavedScore MessageBeingBuilt => result;

		public override SavedScore DefaultInstanceForType => DefaultInstance;

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

		public bool HasTime => result.HasTime;

		[CLSCompliant(false)]
		public uint Time
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

		public bool HasScore => result.HasScore;

		[CLSCompliant(false)]
		public uint Score
		{
			get
			{
				return result.Score;
			}
			set
			{
				SetScore(value);
			}
		}

		public override Builder Clear()
		{
			result = new SavedScore();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override SavedScore BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			SavedScore savedScore = result;
			result = null;
			return savedScore;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is SavedScore)
			{
				return MergeFrom((SavedScore)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(SavedScore other)
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
			if (other.HasTime)
			{
				Time = other.Time;
			}
			if (other.HasScore)
			{
				Score = other.Score;
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
					Time = input.ReadUInt32();
					continue;
				case 32u:
					Score = input.ReadUInt32();
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
		public Builder SetTime(uint value)
		{
			result.hasTime = true;
			result.time_ = value;
			return this;
		}

		public Builder ClearTime()
		{
			result.hasTime = false;
			result.time_ = 0u;
			return this;
		}

		[CLSCompliant(false)]
		public Builder SetScore(uint value)
		{
			result.hasScore = true;
			result.score_ = value;
			return this;
		}

		public Builder ClearScore()
		{
			result.hasScore = false;
			result.score_ = 0u;
			return this;
		}
	}

	public const int ModeFieldNumber = 1;

	public const int TrackFieldNumber = 2;

	public const int TimeFieldNumber = 3;

	public const int ScoreFieldNumber = 4;

	private static readonly SavedScore defaultInstance;

	private bool hasMode;

	private uint mode_;

	private bool hasTrack;

	private uint track_;

	private bool hasTime;

	private uint time_;

	private bool hasScore;

	private uint score_;

	private int memoizedSerializedSize = -1;

	public static SavedScore DefaultInstance => defaultInstance;

	public override SavedScore DefaultInstanceForType => defaultInstance;

	protected override SavedScore ThisMessage => this;

	public bool HasMode => hasMode;

	[CLSCompliant(false)]
	public uint Mode => mode_;

	public bool HasTrack => hasTrack;

	[CLSCompliant(false)]
	public uint Track => track_;

	public bool HasTime => hasTime;

	[CLSCompliant(false)]
	public uint Time => time_;

	public bool HasScore => hasScore;

	[CLSCompliant(false)]
	public uint Score => score_;

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
			if (!hasTime)
			{
				return false;
			}
			if (!hasScore)
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
			if (HasTime)
			{
				num += CodedOutputStream.ComputeUInt32Size(3, Time);
			}
			if (HasScore)
			{
				num += CodedOutputStream.ComputeUInt32Size(4, Score);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static SavedScore()
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
		if (HasTime)
		{
			output.WriteUInt32(3, Time);
		}
		if (HasScore)
		{
			output.WriteUInt32(4, Score);
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
		if (hasTime)
		{
			num ^= time_.GetHashCode();
		}
		if (hasScore)
		{
			num ^= score_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is SavedScore savedScore))
		{
			return false;
		}
		if (hasMode != savedScore.hasMode || (hasMode && !mode_.Equals(savedScore.mode_)))
		{
			return false;
		}
		if (hasTrack != savedScore.hasTrack || (hasTrack && !track_.Equals(savedScore.track_)))
		{
			return false;
		}
		if (hasTime != savedScore.hasTime || (hasTime && !time_.Equals(savedScore.time_)))
		{
			return false;
		}
		if (hasScore != savedScore.hasScore || (hasScore && !score_.Equals(savedScore.score_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<SavedScore, Builder>.PrintField("mode", hasMode, mode_, writer);
		GeneratedMessageLite<SavedScore, Builder>.PrintField("track", hasTrack, track_, writer);
		GeneratedMessageLite<SavedScore, Builder>.PrintField("time", hasTime, time_, writer);
		GeneratedMessageLite<SavedScore, Builder>.PrintField("score", hasScore, score_, writer);
	}

	public static SavedScore ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SavedScore ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SavedScore ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SavedScore ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SavedScore ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SavedScore ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static SavedScore ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static SavedScore ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static SavedScore ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SavedScore ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(SavedScore prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
