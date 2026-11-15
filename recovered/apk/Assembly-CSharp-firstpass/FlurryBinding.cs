using System;
using UnityEngine;

public class FlurryBinding : MonoBehaviour
{
	private static bool bInitial;

	private static IntPtr FlurryClass;

	private static int startSessionMethod;

	private static int stopSessionMethod;

	private static int reportChannelAppRunMethod;

	private static int reportBuyCharacterMethod;

	private static int reportBuyTrackMethod;

	private static int reportUnlockCharacterMethod;

	private static int reportUnlockTrackMethod;

	private static int reportPlayOnlineGameMethod;

	private static int reportGetMedalMethod;

	private static void Init()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr clazz = JNI.FindClass("com/unity3d/player/UnityPlayer");
			int staticFieldID = JNI.GetStaticFieldID(clazz, "currentActivity", "Landroid/app/Activity;");
			IntPtr staticObjectField = JNI.GetStaticObjectField(clazz, staticFieldID);
			IntPtr clazz2 = JNI.FindClass("com/taomee/molekart/FlurryClass");
			int methodID = JNI.GetMethodID(clazz2, "<init>", "(Landroid/app/Activity;)V");
			IntPtr obj = JNI.NewObject(clazz2, methodID, staticObjectField);
			FlurryClass = JNI.NewGlobalRef(obj);
			startSessionMethod = JNI.GetMethodID(clazz2, "startSession", "()V");
			stopSessionMethod = JNI.GetMethodID(clazz2, "stopSession", "()V");
			reportChannelAppRunMethod = JNI.GetMethodID(clazz2, "reportChannelAppRun", "(Ljava/lang/String;)V");
			reportBuyCharacterMethod = JNI.GetMethodID(clazz2, "reportBuyCharacter", "(Ljava/lang/String;)V");
			reportBuyTrackMethod = JNI.GetMethodID(clazz2, "reportBuyTrack", "(Ljava/lang/String;)V");
			reportUnlockCharacterMethod = JNI.GetMethodID(clazz2, "reportUnlockCharacter", "(Ljava/lang/String;)V");
			reportUnlockTrackMethod = JNI.GetMethodID(clazz2, "reportUnlockTrack", "(Ljava/lang/String;)V");
			reportPlayOnlineGameMethod = JNI.GetMethodID(clazz2, "reportPlayOnlineGame", "()V");
			reportGetMedalMethod = JNI.GetMethodID(clazz2, "reportGetMedal", "(Ljava/lang/String;Ljava/lang/String;I)V");
			bInitial = true;
		}
	}

	public static void StartSession()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			if (!bInitial)
			{
				Init();
			}
			JavaVM.AttachCurrentThread();
			JNI.CallVoidMethod(FlurryClass, startSessionMethod);
		}
	}

	public static void StopSession()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			JNI.CallVoidMethod(FlurryClass, stopSessionMethod);
		}
	}

	public static void ReportAppRun(string channelName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(channelName);
			JNI.CallVoidMethod(FlurryClass, reportChannelAppRunMethod, a);
		}
	}

	public static void ReportBuyCharacter(string characterName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(characterName);
			JNI.CallVoidMethod(FlurryClass, reportBuyCharacterMethod, a);
			Debug.Log("Call ReportBuyCharacter");
		}
	}

	public static void ReportBuyTrack(string trackName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(trackName);
			JNI.CallVoidMethod(FlurryClass, reportBuyTrackMethod, a);
			Debug.Log("Call ReportBuyTrack");
		}
	}

	public static void ReportUnlockCharacter(string characterName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(characterName);
			JNI.CallVoidMethod(FlurryClass, reportUnlockCharacterMethod, a);
			Debug.Log("Call ReportUnlockCharacter");
		}
	}

	public static void ReportUnlockTrack(string trackName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(trackName);
			JNI.CallVoidMethod(FlurryClass, reportUnlockTrackMethod, a);
			Debug.Log("Call ReportUnlockTrack");
		}
	}

	public static void ReportPlayOnlineGame()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			JNI.CallVoidMethod(FlurryClass, reportPlayOnlineGameMethod);
			Debug.Log("Call ReportPlayOnlineGame");
		}
	}

	public static void ReportGetMedal(string trackName, string mode, int difficultyLevel)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(trackName);
			IntPtr b = JNI.NewStringUTF(mode);
			JNI.CallVoidMethod(c: new IntPtr(difficultyLevel), clazz: FlurryClass, methodID: reportGetMedalMethod, a: a, b: b);
			Debug.Log("Call ReportGetMedal");
		}
	}
}
