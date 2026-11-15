using System.Runtime.InteropServices;
using UnityEngine;

public class MUTaomeeMoreGame : MonoBehaviour
{
	private static MUTaomeeMoreGame instance;

	private bool m_bShow;

	public static MUTaomeeMoreGame Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool bShowTaomeeMoreGame
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	[DllImport("__Internal")]
	private static extern void _ShowTaomeeMoreGame();

	[DllImport("__Internal")]
	private static extern void _showTMAdView();

	[DllImport("__Internal")]
	private static extern void _removeTMAdView();

	public void Init()
	{
	}

	public void ShowTaomeeMoreGame()
	{
	}

	public void OnCloseShowTaomeeMoreGame()
	{
	}

	public void ShowTMAdView()
	{
	}

	public void RemoveTMAdView()
	{
	}
}
