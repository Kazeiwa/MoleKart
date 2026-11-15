using System.Runtime.CompilerServices;
using UnityEngine;

public class OpenFeint : MonoBehaviour
{
	public enum eDashboardPage
	{
		Main,
		Leaderboards,
		Achievements,
		Challenges,
		FindFriends,
		WhosPlaying,
		GlobalChatRooms,
		GameChatRooms,
		SwitchUser
	}

	public enum eNotificationCategory
	{
		Foreground = 1,
		Login,
		Challenge,
		HighScore,
		Leaderboard,
		Achievement,
		SocialNotification,
		Presence
	}

	public enum eNotificationType
	{
		None,
		Submitting,
		Downloading,
		Error,
		Success,
		NewResources,
		UserPresenceOnline,
		UserPresenceOffline,
		NewMessage
	}

	public enum eChallengeResult
	{
		Incomplete,
		RecipientWon,
		RecipientLost,
		Tie
	}

	public struct ChallengeDefinition
	{
		public string title;

		public long challengeId;

		public string iconUrl;

		public bool multiAttempt;
	}

	public delegate void GeneralEventHandler();

	public delegate void DeferredResultEventHandler(bool result);

	public delegate void NotificationEventHandler(string text, eNotificationCategory category, eNotificationType type);

	public delegate void UserLogInEventHandler(string userId);

	public delegate void StartedChallengeEventHandler(ChallengeDefinition definition);

	public delegate void FollowingResultDelegate(bool success, bool isFollowingAnyone);

	public delegate void ChallengeDefinitionResultDelegate(bool success, ChallengeDefinition definition);

	public delegate void DeferredResultDelegate(bool success);

	private static OpenFeint instance;

	private static FollowingResultDelegate followingResultCallback;

	private static ChallengeDefinitionResultDelegate challengeDefinitionResultCallback;

	private static DeferredResultDelegate downloadCloudBlobResultCallback;

	public static OpenFeint Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static bool hasUserApprovedFeint
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static bool isOnline
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static string localUserName
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static string localUserID
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static bool allowNotifications
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static int numberOfUnviewedChallengesAtLogin
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static string releaseVersionString
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static bool BoolResult
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static int IntResult
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static float FloatResult
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static string StringResult
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private static bool HasResult
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static event NotificationEventHandler OnNotification
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event DeferredResultEventHandler OnHighScoreSubmitted
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event DeferredResultEventHandler OnAchievementUnlocked
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event StartedChallengeEventHandler OnStartedChallenge
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event GeneralEventHandler OnRestartedChallenge
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event GeneralEventHandler OnRestartedCreateChallenge
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event GeneralEventHandler OnCompletedChallengeScreenClosed
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event GeneralEventHandler OnSendChallengeScreenClosed
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event GeneralEventHandler OnUserSentChallenges
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event DeferredResultEventHandler OnChallengeResultSubmitted
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event DeferredResultEventHandler OnUploadedCloudBlob
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event UserLogInEventHandler OnUserLoggedIn
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static event UserLogInEventHandler OnUserLoggedOut
	{
		[MethodImpl((MethodImplOptions)32)]
		add
		{
		}
		[MethodImpl((MethodImplOptions)32)]
		remove
		{
		}
	}

	public static void LaunchDashboard(eDashboardPage dashboardPage)
	{
	}

	public static void LaunchDashboard()
	{
	}

	public static void LaunchDashboardWithLeaderboardID(string leaderboardId)
	{
	}

	public static void LaunchDashboardWithLeaderboardID(long leaderboardId)
	{
	}

	public static void InGameNotification(string message, string imageName)
	{
	}

	public static int GetLeaderboardHighScore(string leaderboardId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int GetLeaderboardHighScore(int leaderboardId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string GetAchivementElementByIdAndField(string achivementId, string field)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string GetAchivementElementByIdAndField(int achivementId, string field)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static bool IsAchievementUnlocked(string achievementId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static bool IsAchievementUnlocked(int achievementId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void SubmitHighScore(long score, string displayText, string leaderboardId, bool silently)
	{
	}

	public static void SubmitHighScore(long score, string displayText, long leaderboardId, bool silently)
	{
	}

	public static void SubmitHighScore(long score, string displayText, string leaderboardId)
	{
	}

	public static void SubmitHighScore(long score, string displayText, long leaderboardId)
	{
	}

	public static void SubmitHighScore(long score, string leaderboardId)
	{
	}

	public static void SubmitHighScore(long score, long leaderboardId)
	{
	}

	public static void UnlockAchievement(string achievementId)
	{
	}

	public static void UnlockAchievement(long achievementId)
	{
	}

	public static void SwitchGameFeed()
	{
	}

	public static void RefreshGameFeed()
	{
	}

	public static void SwitchCustomizeGameFeed()
	{
	}

	public static void sendWithPrepopulatedText(string textStr, string message, string imageName, string customUrl)
	{
	}

	public static void UpdateAchievement(string achievementId, int percentageComplete, bool showNotification)
	{
	}

	public static void UpdateAchievement(long achievementId, int percentageComplete, bool showNotification)
	{
	}

	public static void SendSocialNotification(string text, string imageIdentifierName)
	{
	}

	public static void SendSocialNotification(string text)
	{
	}

	public static void GetChallengeDataToPlayerPrefs(string[] playerPrefsKeys)
	{
	}

	public static void GetChallengeDataToFile(string filePath)
	{
	}

	public static void DisplaySendChallengeModalWithPlayerPrefs(string challengeId, string challengeText, string[] dataKeys)
	{
	}

	public static void DisplaySendChallengeModalWithPlayerPrefs(long challengeId, string challengeText, string[] dataKeys)
	{
	}

	public static void DisplaySendChallengeModalWithFile(string challengeId, string challengeText, string dataFilePath)
	{
	}

	public static void DisplaySendChallengeModalWithFile(long challengeId, string challengeText, string dataFilePath)
	{
	}

	public static void SubmitChallengeResult(eChallengeResult result, string resultDescription)
	{
	}

	public static void DisplayChallengeCompletedModalWithPlayerPrefs(eChallengeResult result, string resultDescription, string reChallengeDescription, string[] reChallengeDataKeys)
	{
	}

	public static void DisplayChallengeCompletedModalWithFile(eChallengeResult result, string resultDescription, string reChallengeDescription, string reChallengeDataFilePath)
	{
	}

	public static void GetChallengeDefinition(string challengeId, ChallengeDefinitionResultDelegate callback)
	{
	}

	public static void GetChallengeDefinition(long challengeId, ChallengeDefinitionResultDelegate callback)
	{
	}

	public static void UploadBlobFromPlayerPrefs(string blobKey, string[] playerPrefsKeys)
	{
	}

	public static void UploadBlobFromFile(string blobKey, string filePath)
	{
	}

	public static void DownloadBlobToPlayerPrefs(string blobKey, string[] playerPrefsKeys, DeferredResultDelegate callback)
	{
	}

	public static void DownloadBlobToFilePath(string blobKey, string filePath, DeferredResultDelegate callback)
	{
	}

	public static void SetDashboardOrientation(DeviceOrientation orientation)
	{
	}

	public static void PresentUserFeintApprovalModal()
	{
	}

	public static void IsLocalUserFollowingAnyone(FollowingResultDelegate callback)
	{
	}

	public static void ClearCallbacks()
	{
	}

	private static void PerformCommand(string cmd)
	{
	}

	private void TriggerGeneralEvent(string message)
	{
	}

	private void TriggerDeferredResultEvent(string message)
	{
	}

	private void TriggerNotificationEvent(string message)
	{
	}

	private void TriggerUserLogInEvent(string message)
	{
	}

	private void TriggerStartedChallengeEvent(string message)
	{
	}

	private void TriggerFollowingResultCallback(string message)
	{
	}

	private void TriggerChallengeDefinitionResultCallback(string message)
	{
	}

	private void TriggerDownloadCloudBlobResultCallback(string message)
	{
	}

	private void PauseAudio(string message)
	{
	}

	private static void TriggerGeneralEvent(int eventIndex)
	{
	}

	private static void TriggerDeferredResultEvent(int eventIndex, bool result)
	{
	}

	private static void TriggerNotificationEvent(string text, int category, int type)
	{
	}

	private static void TriggerUserLogInEvent(int eventIndex, string userId)
	{
	}

	private static void TriggerStartedChallengeEvent(string title, long id, string iconUrl, bool multiAttempt)
	{
	}

	private static void TriggerFollowingResultCallback(bool success, bool result)
	{
	}

	private static void TriggerChallengeDefinitionResultCallback(bool success)
	{
	}

	private static void TriggerDownloadCloudBlobResultCallback(bool success)
	{
	}

	public void Login()
	{
	}
}
