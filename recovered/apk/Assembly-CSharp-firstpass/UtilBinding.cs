using System;
using UnityEngine;

public class UtilBinding : MonoBehaviour
{
	private static bool bInitial;

	private static IntPtr UtilClass;

	private static int setScreenWakeMethod;

	private static int showWaitingViewMethod;

	private static int showLoadingProgressMethod;

	private static int showToastMethod;

	private static int showAdmobMethod;

	private static void Init()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr clazz = JNI.FindClass("com/unity3d/player/UnityPlayer");
			int staticFieldID = JNI.GetStaticFieldID(clazz, "currentActivity", "Landroid/app/Activity;");
			IntPtr staticObjectField = JNI.GetStaticObjectField(clazz, staticFieldID);
			IntPtr clazz2 = JNI.FindClass("com/taomee/molekart/UtilClass");
			int methodID = JNI.GetMethodID(clazz2, "<init>", "(Landroid/app/Activity;)V");
			IntPtr obj = JNI.NewObject(clazz2, methodID, staticObjectField);
			UtilClass = JNI.NewGlobalRef(obj);
			setScreenWakeMethod = JNI.GetMethodID(clazz2, "setScreenWake", "(Z)V");
			showWaitingViewMethod = JNI.GetMethodID(clazz2, "showLoadingView", "(Z)V");
			showLoadingProgressMethod = JNI.GetMethodID(clazz2, "showLoadingProgress", "(Z)V");
			showToastMethod = JNI.GetMethodID(clazz2, "showToast", "(Ljava/lang/String;)V");
			showAdmobMethod = JNI.GetMethodID(clazz2, "showAdmob", "(Z)V");
			bInitial = true;
		}
	}

	public static void SetScreenWake(bool bOn)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			IntPtr a = new IntPtr(bOn ? 1 : 0);
			JNI.CallVoidMethod(UtilClass, setScreenWakeMethod, a);
			Debug.Log("Call setScreenWake");
		}
	}

	public static void ShowWaitingView(bool bShow)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			IntPtr a = new IntPtr(bShow ? 1 : 0);
			JNI.CallVoidMethod(UtilClass, showWaitingViewMethod, a);
			Debug.Log("Call showLoadingView:" + bShow);
		}
	}

	public static void ShowLoadingIndicator(bool bShow)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			IntPtr a = new IntPtr(bShow ? 1 : 0);
			JNI.CallVoidMethod(UtilClass, showLoadingProgressMethod, a);
			Debug.Log("Call showLoadingProgress:" + bShow);
		}
		ShowAdmob(bShow);
	}

	public static void ShowToast(string msg)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(msg);
			JNI.CallVoidMethod(UtilClass, showToastMethod, a);
			Debug.Log("Call ShowToast:" + msg);
		}
	}

	public static void ShowAdmob(bool bShow)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			IntPtr a = new IntPtr(bShow ? 1 : 0);
			JNI.CallVoidMethod(UtilClass, showAdmobMethod, a);
			Debug.Log("Call showAdmob:" + bShow);
		}
	}
}
