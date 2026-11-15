using UnityEngine;

public class COM_DATA : ABMessageData
{
	public int iValue;

	public float fValue;

	public Vector3 vValue;

	public object data;

	public static COM_DATA EMPTY = new COM_DATA();

	public override ABMessageData clone()
	{
		COM_DATA cOM_DATA = new COM_DATA();
		cOM_DATA.iValue = iValue;
		cOM_DATA.fValue = fValue;
		cOM_DATA.data = data;
		return cOM_DATA;
	}
}
