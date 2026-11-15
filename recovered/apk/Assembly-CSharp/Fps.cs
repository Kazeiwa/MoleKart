using TaomeeNetwork;
using UnityEngine;

public class Fps : MonoBehaviour
{
	public bool bVisual = true;

	private float fCount;

	private float fTime;

	private float fFrameRate;

	private float fMaxSendBytesPerSecond;

	private float fSendBytesPerSecond;

	private float fSendBytesTime;

	private float fRecvBytesPerSecond;

	public string version;

	public static float HeroCarSpeed
	{
		get
		{
			Car heroCar = TSingleton<CarManager>.Instance.GetHeroCar();
			if (heroCar == null)
			{
				return 0f;
			}
			return heroCar.GetVecGlobal().magnitude;
		}
	}

	private void Awake()
	{
		base.guiText.text = "Hello World";
		if (!bVisual)
		{
			base.active = false;
		}
	}

	private void Update()
	{
		fCount += 1f;
		fTime += Time.deltaTime;
		if (fTime > 0.5f)
		{
			fFrameRate = fCount / fTime;
			fTime = 0f;
			fCount = 0f;
		}
		fSendBytesTime += Time.deltaTime;
		if (fSendBytesTime > 1f)
		{
			fSendBytesPerSecond = (float)(SessionManager.sendedSize * 8) / (1024f * fSendBytesTime);
			fRecvBytesPerSecond = (float)(SessionManager.recvSize * 8) / (1024f * fSendBytesTime);
			if (fMaxSendBytesPerSecond < fSendBytesPerSecond)
			{
				fMaxSendBytesPerSecond = fSendBytesPerSecond;
			}
			fSendBytesTime = 0f;
			SessionManager.sendedSize = 0L;
			SessionManager.recvSize = 0;
		}
		base.guiText.text = "Fps: " + fFrameRate + " Send/s: " + fSendBytesPerSecond + "kbps MaxSend/s: " + fMaxSendBytesPerSecond + "kbps Recv/s: " + fRecvBytesPerSecond + "kbps Ver:" + version;
		GUIText gUIText = base.guiText;
		string text = gUIText.text;
		gUIText.text = text + " HeroSpeed: " + HeroCarSpeed + " ";
		if (LocalNetFactory.Instance.LocalNet != null)
		{
			GUIText gUIText2 = base.guiText;
			gUIText2.text = gUIText2.text + "----PeerID = " + LocalNetFactory.Instance.LocalNet.GetMyselfPeerId();
		}
	}
}
