using UnityEngine;

public class AndroidLocalizationBinder : MonoBehaviour
{
	public static string GetLanguageIdentifier()
	{
		return "zh-Hans";
	}

	public static string GetPlatformIdentifier()
	{
		return "Android";
	}

	public static void CheckHardware()
	{
	}
}
