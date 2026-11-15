public struct NET_PACKAGE_HEADER
{
	public uint PackageLength;

	public ushort CommandId;

	public uint UserId;

	public uint SequenceNumber;

	public uint Result;

	public static uint Length => 18u;
}
