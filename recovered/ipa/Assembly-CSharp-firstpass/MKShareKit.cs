using System.Runtime.InteropServices;
using UnityEngine;

public class MKShareKit : MonoBehaviour
{
	private bool startShareImage;

	private bool imageFileExists;

	private int weiboPlatform;

	private string shareStr;

	private float m_fLastShareTime;

	private static MKShareKit instance;

	private string[] txtWinGame;

	private string[] txtLadder;

	private string[] txtUnlock;

	private string m_TextFilePath;

	public float lastCaptureTime;

	public string captureFileName;

	private bool doCapture;

	public static MKShareKit Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	[DllImport("__Internal")]
	private static extern void _ShareKitTest();

	[DllImport("__Internal")]
	private static extern void _ShareKitlogout();

	[DllImport("__Internal")]
	private static extern bool _ShareKitText(byte[] text, int textLen, int type);

	[DllImport("__Internal")]
	private static extern bool _ShareKitImage(byte[] imageFile, int imageFileLen, byte[] text, int textLen, int type);

	[DllImport("__Internal")]
	private static extern bool _ShareKitUrl(byte[] urlText, int urlLen, byte[] text, int textLen, int type);

	[DllImport("__Internal")]
	private static extern bool _ShareKitAutoCaptureScreen(byte[] text, int textLen, int type, int width, int height);

	private void LoadText()
	{
	}

	public void Init()
	{
	}

	public string GetAppDocumentFolder()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void CaptureScreen()
	{
	}

	public string GetImagePath()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckScreenPictureReady()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Update()
	{
	}

	public void DoShareImage()
	{
	}

	public void SetPlatform(ShareItemType type)
	{
	}

	public void HandleNoNetwork()
	{
	}

	public void DoShareImage_Unlock(int type)
	{
	}

	public void DoShareImage_Win(int rank)
	{
	}

	public void DoShareImage_Ladder()
	{
	}

	private void ShareKitTest()
	{
	}

	private void ShareKitlogout()
	{
	}

	private bool ShareKitText(string text, int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ShareKitAutoImage(string text, int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ShareKitImage(string imageFile, string text, int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ShareKitUrl(string urlContent, string text, int type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
