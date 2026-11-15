using Google.ProtocolBuffers;

namespace RaceProtocol;

public static class ICarProto
{
	internal static readonly object Descriptor;

	static ICarProto()
	{
		Descriptor = null;
	}

	public static void RegisterAllExtensions(ExtensionRegistry registry)
	{
	}
}
