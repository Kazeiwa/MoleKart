using System;
using System.Collections.Generic;
using UnityEngine;

public class StatisticAgent : MonoBehaviour
{
	private static bool bInitial;

	private static IntPtr statisticAgentClass;

	private static int startSessionMethod;

	private static int stopSessionMethod;

	private static int logEventMethod;

	private static int addEventDataMothod;

	private static void Init()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			IntPtr clazz = JNI.FindClass("com/unity3d/player/UnityPlayer");
			int staticFieldID = JNI.GetStaticFieldID(clazz, "currentActivity", "Landroid/app/Activity;");
			IntPtr staticObjectField = JNI.GetStaticObjectField(clazz, staticFieldID);
			IntPtr clazz2 = JNI.FindClass("com/taomee/AnalyseMain/AnalyseManager");
			int methodID = JNI.GetMethodID(clazz2, "<init>", "(Landroid/app/Activity;)V");
			IntPtr obj = JNI.NewObject(clazz2, methodID, staticObjectField);
			statisticAgentClass = JNI.NewGlobalRef(obj);
			startSessionMethod = JNI.GetMethodID(clazz2, "startSession", "(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V");
			stopSessionMethod = JNI.GetMethodID(clazz2, "endSession", "()V");
			logEventMethod = JNI.GetMethodID(clazz2, "logEvent", "(Ljava/lang/String;Ljava/lang/String;)V");
			addEventDataMothod = JNI.GetMethodID(clazz2, "addLogEventData", "(Ljava/lang/String;Ljava/lang/String;)V");
			bInitial = true;
		}
	}

	public static void StartSession(string gameName, string gameVersion, string channelName)
	{
		if (Application.platform != RuntimePlatform.WindowsEditor && !bInitial)
		{
			Init();
			JavaVM.AttachCurrentThread();
			IntPtr a = JNI.NewStringUTF(gameName);
			IntPtr b = JNI.NewStringUTF(gameVersion);
			IntPtr c = JNI.NewStringUTF(channelName);
			JNI.CallVoidMethod(statisticAgentClass, startSessionMethod, a, b, c);
		}
	}

	public static void StopSession()
	{
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			JavaVM.AttachCurrentThread();
			JNI.CallVoidMethod(statisticAgentClass, stopSessionMethod);
		}
	}

	public static void logEvent(string eventName, Dictionary<string, string> map_parameters, bool is_realTime = true)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor || !bInitial)
		{
			return;
		}
		JavaVM.AttachCurrentThread();
		foreach (KeyValuePair<string, string> map_parameter in map_parameters)
		{
			IntPtr a = JNI.NewStringUTF(map_parameter.Key);
			IntPtr b = JNI.NewStringUTF(map_parameter.Value);
			JNI.CallVoidMethod(statisticAgentClass, addEventDataMothod, a, b);
		}
		IntPtr a2 = JNI.NewStringUTF(eventName);
		JNI.CallVoidMethod(b: (!is_realTime) ? JNI.NewStringUTF("FALSE") : JNI.NewStringUTF("TRUE"), clazz: statisticAgentClass, methodID: logEventMethod, a: a2);
	}
}
