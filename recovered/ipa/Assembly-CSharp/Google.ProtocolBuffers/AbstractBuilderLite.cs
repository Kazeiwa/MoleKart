using System.IO;

namespace Google.ProtocolBuffers;

public abstract class AbstractBuilderLite<TMessage, TBuilder> : IBuilderLite<TMessage, TBuilder> where TMessage : AbstractMessageLite<TMessage, TBuilder> where TBuilder : AbstractBuilderLite<TMessage, TBuilder>
{
	private class LimitedInputStream : Stream
	{
		private readonly Stream proxied;

		private int bytesLeft;

		public override bool CanRead
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public override bool CanSeek
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public override bool CanWrite
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public override long Length
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public override long Position
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			set
			{
			}
		}

		internal LimitedInputStream(Stream proxied, int size)
		{
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public override void SetLength(long value)
		{
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
		}
	}

	protected abstract TBuilder ThisBuilder { get; }

	public override IMessageLite WeakDefaultInstanceForType
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public override TBuilder MergeFrom(CodedInputStream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeDelimitedFrom(Stream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(ByteString data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(ByteString data, ExtensionRegistry extensionRegistry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(byte[] data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(byte[] data, ExtensionRegistry extensionRegistry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(Stream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override TBuilder MergeFrom(Stream input, ExtensionRegistry extensionRegistry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakClear()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakMergeFrom(IMessageLite message)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakMergeFrom(ByteString data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakMergeFrom(ByteString data, ExtensionRegistry registry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakMergeFrom(CodedInputStream input)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakMergeFrom(CodedInputStream input, ExtensionRegistry registry)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IMessageLite WeakBuild()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IMessageLite WeakBuildPartial()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override IBuilderLite WeakClone()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
