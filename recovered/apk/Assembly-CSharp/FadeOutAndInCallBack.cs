using System.Collections;
using UnityEngine;

public class FadeOutAndInCallBack : MonoBehaviour
{
	public void FadeInAndOut()
	{
		Hashtable hashtable = new Hashtable();
		iTween.CameraFadeAdd();
		hashtable["time"] = 1f;
		hashtable["amount"] = 1f;
		hashtable["oncomplete"] = "FadeInCompleteFun";
		hashtable["oncompletetarget"] = base.gameObject;
		iTween.CameraFadeTo(hashtable);
	}

	public void FadeInCompleteFun()
	{
		CameraManager.instance.ChangeMainCamera();
		Hashtable hashtable = new Hashtable();
		hashtable["delay"] = 1f;
		hashtable["amount"] = 0f;
		hashtable["time"] = 1f;
		hashtable["oncomplete"] = "FadeOutCompleteFun";
		hashtable["oncompletetarget"] = base.gameObject;
		iTween.CameraFadeTo(hashtable);
	}

	public void FadeOutCompleteFun()
	{
		CameraManager.instance.SetMainState(CameraManager.MainCameraState.AniState);
	}
}
