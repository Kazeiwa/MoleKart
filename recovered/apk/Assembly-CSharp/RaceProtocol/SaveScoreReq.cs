using System;
using System.IO;
using Google.ProtocolBuffers;

namespace RaceProtocol;

public sealed class SaveScoreReq : GeneratedMessageLite<SaveScoreReq, SaveScoreReq.Builder>
{
	public sealed class Builder : GeneratedBuilderLite<SaveScoreReq, Builder>
	{
		private SaveScoreReq result = new SaveScoreReq();

		protected override Builder ThisBuilder => this;

		protected override SaveScoreReq MessageBeingBuilt => result;

		public override SaveScoreReq DefaultInstanceForType => DefaultInstance;

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

		public bool HasNickName => result.HasNickName;

		public string NickName
		{
			get
			{
				return result.NickName;
			}
			set
			{
				SetNickName(value);
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
			result = new SaveScoreReq();
			return this;
		}

		public override Builder Clone()
		{
			return new Builder().MergeFrom(result);
		}

		public override SaveScoreReq BuildPartial()
		{
			if (result == null)
			{
				throw new InvalidOperationException("build() has already been called on this Builder");
			}
			SaveScoreReq saveScoreReq = result;
			result = null;
			return saveScoreReq;
		}

		public override Builder MergeFrom(IMessageLite other)
		{
			if (other is SaveScoreReq)
			{
				return MergeFrom((SaveScoreReq)other);
			}
			base.MergeFrom(other);
			return this;
		}

		public override Builder MergeFrom(SaveScoreReq other)
		{
			if (other == DefaultInstance)
			{
				return this;
			}
			if (other.HasDeviceId)
			{
				DeviceId = other.DeviceId;
			}
			if (other.HasNickName)
			{
				NickName = other.NickName;
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
				case 10u:
					DeviceId = input.ReadString();
					break;
				case 18u:
					NickName = input.ReadString();
					break;
				case 26u:
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

		public Builder SetNickName(string value)
		{
			ThrowHelper.ThrowIfNull(value, "value");
			result.hasNickName = true;
			result.nickName_ = value;
			return this;
		}

		public Builder ClearNickName()
		{
			result.hasNickName = false;
			result.nickName_ = string.Empty;
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

	public const int DeviceIdFieldNumber = 1;

	public const int NickNameFieldNumber = 2;

	public const int SavedScoreFieldNumber = 3;

	private static readonly SaveScoreReq defaultInstance;

	private bool hasDeviceId;

	private string deviceId_ = string.Empty;

	private bool hasNickName;

	private string nickName_ = string.Empty;

	private bool hasSavedScore;

	private SavedScore savedScore_ = SavedScore.DefaultInstance;

	private int memoizedSerializedSize = -1;

	public static SaveScoreReq DefaultInstance => defaultInstance;

	public override SaveScoreReq DefaultInstanceForType => defaultInstance;

	protected override SaveScoreReq ThisMessage => this;

	public bool HasDeviceId => hasDeviceId;

	public string DeviceId => deviceId_;

	public bool HasNickName => hasNickName;

	public string NickName => nickName_;

	public bool HasSavedScore => hasSavedScore;

	public SavedScore SavedScore => savedScore_;

	public override bool IsInitialized
	{
		get
		{
			if (!hasDeviceId)
			{
				return false;
			}
			if (!hasNickName)
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
			if (HasDeviceId)
			{
				num += CodedOutputStream.ComputeStringSize(1, DeviceId);
			}
			if (HasNickName)
			{
				num += CodedOutputStream.ComputeStringSize(2, NickName);
			}
			if (HasSavedScore)
			{
				num += CodedOutputStream.ComputeMessageSize(3, SavedScore);
			}
			memoizedSerializedSize = num;
			return num;
		}
	}

	static SaveScoreReq()
	{
		defaultInstance = new Builder().BuildPartial();
		object.ReferenceEquals(ICarProto.Descriptor, null);
	}

	public override void WriteTo(CodedOutputStream output)
	{
		int serializedSize = SerializedSize;
		if (HasDeviceId)
		{
			output.WriteString(1, DeviceId);
		}
		if (HasNickName)
		{
			output.WriteString(2, NickName);
		}
		if (HasSavedScore)
		{
			output.WriteMessage(3, SavedScore);
		}
	}

	public override int GetHashCode()
	{
		int num = GetType().GetHashCode();
		if (hasDeviceId)
		{
			num ^= deviceId_.GetHashCode();
		}
		if (hasNickName)
		{
			num ^= nickName_.GetHashCode();
		}
		if (hasSavedScore)
		{
			num ^= savedScore_.GetHashCode();
		}
		return num;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is SaveScoreReq saveScoreReq))
		{
			return false;
		}
		if (hasDeviceId != saveScoreReq.hasDeviceId || (hasDeviceId && !deviceId_.Equals(saveScoreReq.deviceId_)))
		{
			return false;
		}
		if (hasNickName != saveScoreReq.hasNickName || (hasNickName && !nickName_.Equals(saveScoreReq.nickName_)))
		{
			return false;
		}
		if (hasSavedScore != saveScoreReq.hasSavedScore || (hasSavedScore && !savedScore_.Equals(saveScoreReq.savedScore_)))
		{
			return false;
		}
		return true;
	}

	public override void PrintTo(TextWriter writer)
	{
		GeneratedMessageLite<SaveScoreReq, Builder>.PrintField("deviceId", hasDeviceId, deviceId_, writer);
		GeneratedMessageLite<SaveScoreReq, Builder>.PrintField("nickName", hasNickName, nickName_, writer);
		GeneratedMessageLite<SaveScoreReq, Builder>.PrintField("savedScore", hasSavedScore, savedScore_, writer);
	}

	public static SaveScoreReq ParseFrom(ByteString data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(byte[] data)
	{
		return CreateBuilder().MergeFrom(data).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(Stream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
	}

	public static SaveScoreReq ParseDelimitedFrom(Stream input)
	{
		return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
	}

	public static SaveScoreReq ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(CodedInputStream input)
	{
		return CreateBuilder().MergeFrom(input).BuildParsed();
	}

	public static SaveScoreReq ParseFrom(CodedInputStream input, ExtensionRegistry extensionRegistry)
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

	public static Builder CreateBuilder(SaveScoreReq prototype)
	{
		return new Builder().MergeFrom(prototype);
	}
}
