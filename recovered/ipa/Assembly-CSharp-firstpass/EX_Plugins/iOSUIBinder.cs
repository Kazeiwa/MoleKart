using System.Runtime.InteropServices;
using UnityEngine;

namespace EX_Plugins;

public class iOSUIBinder : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern void _hideTextField();

	[DllImport("__Internal")]
	private static extern void _createTextFieldWithName(int x, int y, int width, int height, string name);

	[DllImport("__Internal")]
	private static extern void _createTextLabel(int x, int y, int width, int height, string text, int fontSize);

	[DllImport("__Internal")]
	private static extern void _createTextLabelWithTag(int x, int y, int width, int height, string text, int fontSize, int tag);

	[DllImport("__Internal")]
	private static extern void _createWhiteTextLabelWithTag(int x, int y, int width, int height, string text, int fontSize, int tag);

	[DllImport("__Internal")]
	private static extern void _removeLabel(int tag);

	[DllImport("__Internal")]
	private static extern void _createTextFieldWithTag(int x, int y, int width, int height, string text, int fontSize, int tag);

	[DllImport("__Internal")]
	private static extern void _removeTextField(int tag);

	[DllImport("__Internal")]
	private static extern void _createTableView(int x, int y, int width, int height, string[] nameArray, string[] peerIdArray);

	[DllImport("__Internal")]
	private static extern void _createClientRoomPlayersView(int x, int y, int width, int height, string[] nameArray, int[] carIdArray, int selfIndex);

	[DllImport("__Internal")]
	private static extern void _createHostRoomPlayersView(int x, int y, int width, int height, string[] nameArray, int[] carIdArray, int selfIndex);

	[DllImport("__Internal")]
	private static extern void _removeTableView();

	[DllImport("__Internal")]
	private static extern void _createRankTextLabel(string name, int rank);

	[DllImport("__Internal")]
	private static extern void _removeClientRoomPlayersView();

	[DllImport("__Internal")]
	private static extern void _removeHostRoomPlayersView();

	[DllImport("__Internal")]
	private static extern void _removeAllRankTextLabel();

	[DllImport("__Internal")]
	private static extern void _showWaitingView();

	[DllImport("__Internal")]
	private static extern void _removeWaitingView();

	[DllImport("__Internal")]
	private static extern void _closeTextFieldKeyboard();

	[DllImport("__Internal")]
	private static extern void _showAlert(string title, string text, string btnText);

	[DllImport("__Internal")]
	private static extern void _createIndicator(float x, float y);

	[DllImport("__Internal")]
	private static extern void _removeIndicator();

	[DllImport("__Internal")]
	private static extern void _createAlert(string text);

	[DllImport("__Internal")]
	private static extern void _createAlertWithTimer(string text, float time);

	[DllImport("__Internal")]
	private static extern void _dismissAlert();

	[DllImport("__Internal")]
	private static extern void _shareCharacterReleaseAlert();

	[DllImport("__Internal")]
	private static extern void _shareTrackReleaseAlert();

	public static void CreateTextFieldWithName(int x, int y, int width, int height, string name)
	{
	}

	public static void HideNameTextField()
	{
	}

	public static void CreateRankTextLabel(string name, int rank)
	{
	}

	public static void CreateTextLabel(int x, int y, int width, int height, string text, int fontSize)
	{
	}

	public static void CreateTextLabel(int x, int y, int width, int height, string text, int fontSize, int tag)
	{
	}

	public static void CreateWhiteTextLabel(int x, int y, int width, int height, string text, int fontSize, int tag)
	{
	}

	public static void CreateTextField(int x, int y, int width, int height, string text, int fontSize, int tag)
	{
	}

	public static void CreateTableView(int x, int y, int width, int height, string[] nameArray, string[] peerIdArray)
	{
	}

	public static void RemoveTextField(int tag)
	{
	}

	public static void RemoveTextLabel(int tag)
	{
	}

	public static void RemoveTableView()
	{
	}

	public static void RemoveAllRankTextLabel()
	{
	}

	public static void CreateHostRoomPlayersView(int x, int y, int width, int height, string[] nameArray, int[] carIdArray, int selfIndex)
	{
	}

	public static void CreateClientRoomPlayersView(int x, int y, int width, int height, string[] nameArray, int[] carIdArray, int selfIndex)
	{
	}

	public static void RemoveHostRoomPlayersView()
	{
	}

	public static void RemoveClientRoomPlayersView()
	{
	}

	public static void ShowWaitingView()
	{
	}

	public static void RemoveWaitingView()
	{
	}

	public static void CloseTextFieldKeyboard()
	{
	}

	public static void ShowAlert(string title, string text, string btnText)
	{
	}

	public static void CreateIndicator(float x, float y)
	{
	}

	public static void RemoveIndicator()
	{
	}

	public static void CreateAlert(string text)
	{
	}

	public static void CreateAlert(string text, float time)
	{
	}

	public static void DismissAlert()
	{
	}

	public static void ShareCharacterReleaseAlert()
	{
	}

	public static void ShareTrackReleaseAlert()
	{
	}
}
