using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class SaveScoreAck : GeneratedMessageLite<SaveScoreAck, SaveScoreAck.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<SaveScoreAck, Builder>
	{
		private SaveScoreAck result = new SaveScoreAck();

		protected override Builder ThisBuilder => this;

		protected override SaveScoreAck MessageBeingBuilt => result;

		public override SaveScoreAck DefaultInstanceForType => DefaultInstance;

		public bool HasSucceed => result.HasSucceed;

		public bool Succeed
		{
			get
			{
				return result.Succeed;
			}
			set
			{
				SetSucceed(value);
			}
		}

		public bool HasSavedScore => result.HasSavedScore;

		public SavedScore SavedScore
		{
			get
			{
				return result.SavedScore;
			}
			set
			{
				SetSavedScore(value);
			}
		}

		public override Builder Clear()
		{
			result = new SaveScoreAck();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override SaveScoreAck BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			SaveScoreAck saveScoreAck = result;
			result = null;
			return saveScoreAck;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is SaveScoreAck)
			{
				return MergeFrom((SaveScoreAck)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(SaveScoreAck other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasSucceed)
			{
				Succeed = other.Succeed;
			}
			if (other.HasSavedScore)
			{
				MergeSavedScore(other.SavedScore);
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
				default:
					if (WireFormat.IsEndGroupTag(num))
					{
						return this;
					}
					ParseUnknownField(input, extensionRegistry, num);
					break;
				case 8u:
					Succeed = input.ReadBool();
					break;
				case 18u:
				{
					SavedScore.Builder builder = SavedScore.CreateBuilder();
					if (HasSavedScore)
					{
						builder.MergeFrom(SavedScore);
					}
					input.ReadMessage(builder, extensionRegistry);
					SavedScore = builder.BuildPartial();
					break;
				}
				}
			}
		}

		public Builder SetSucceed(bool value)
		{
			result.hasSucceed = true;
			result.succeed_ = value;
			return this;
		}

		public Builder ClearSucceed()
		{
			result.hasSucceed = false;
			result.succeed_ = false;
			return this;
		}

		public Builder SetSavedScore(SavedScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasSavedScore = true;
			result.savedScore_ = value;
			return this;
		}

		public Builder SetSavedScore(SavedScore.Builder builderForValue)
		{
			ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
			result.hasSavedScore = true;
			result.savedScore_ = builderForValue.Build();
			return this;
		}

		public Builder MergeSavedScore(SavedScore value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			if (result.HasSavedScore && result.savedScore_ != SavedScore.DefaultInstance)
			{
				result.savedScore_ = SavedScore.CreateBuilder(result.savedScore_).MergeFrom(value).BuildPartial();
			}
			else
			{
				result.savedScore_ = value;
			}
			result.hasSavedScore = true;
			return this;
		}

		public Builder ClearSavedScore()
		{
			result.hasSavedScore = false;
			result.savedScore_ = SavedScore.DefaultInstance;
			return this;
		}
	}

	public const int SucceedFieldNumber = 1;

	public const int SavedScoreFieldNumber = 2;

	private static readonly SaveScoreAck defaultInstance;

	private bool hasSucceed;

	private bool succeed_;

	private bool hasSavedScore;

	private SavedScore savedScore_ = SavedScore.DefaultInstance;

	private int memoizedSerializedSize = -1;

	public static SaveScoreAck DefaultInstance => defaultInstance;

	public override SaveScoreAck DefaultInstanceForType => defaultInstance;

	protected override SaveScoreAck ThisMessage => this;

	public bool HasSucceed => hasSucceed;

	public bool Succeed => succeed_;

	public bool HasSavedScore => hasSavedScore;

	public SavedScore SavedScore => savedScore_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasSucceed)
			{
				return false;
			}
			if (!hasSavedScore)
			{
				return false;
			}
			if (!SavedScore.IsInitialized)
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
			if (HasSucceed)
			{
				num += CodedOutputStream.ComputeBoolSize(1, Succeed);
			}
			if (HasSavedScore)
			{
				num += CodedOutputStream.ComputeMessageSize(2, SavedScore);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static SaveScoreAck()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasSucceed)
		{
			output.WriteBool(1, Succeed);
		}
		if (HasSavedScore)
		{
			output.WriteMessage(2, SavedScore);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasSucceed)
		{
			num ^= succeed_.GetHashCode();
		}
		if (hasSavedScore)
		{
			num ^= savedScore_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is SaveScoreAck saveScoreAck))
		{
			return false;
		}
		if (hasSucceed != saveScoreAck.hasSucceed || (hasSucceed && !succeed_.Equals(saveScoreAck.succeed_)))
		{
			return false;
		}
		if (hasSavedScore != saveScoreAck.hasSavedScore || (hasSavedScore && !savedScore_.Equals(saveScoreAck.savedScore_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<SaveScoreAck, Builder>.PrintField("succeed", hasSucceed, succeed_, writer);
		GeneratedMessageLite<SaveScoreAck, Builder>.PrintField("savedScore", hasSavedScore, savedScore_, writer);
	}

	public static SaveScoreAck ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static SaveScoreAck ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static SaveScoreAck ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SaveScoreAck ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(SaveScoreAck prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
