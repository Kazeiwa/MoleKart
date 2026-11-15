using UnityEngine;

internal class Engine
{
	private static bool bInit;

	public static bool Init()
	{
		bInit = CollisionLayer.Instance.Init();
		Physics.gravity = new Vector3(0f, -9.8f, 0f);
		Physics.IgnoreLayerCollision(10, 13, ignore: true);
		Physics.IgnoreLayerCollision(25, 13, ignore: true);
		Physics.IgnoreLayerCollision(12, 13, ignore: true);
		Physics.IgnoreLayerCollision(12, 12, ignore: true);
		Physics.IgnoreLayerCollision(12, 8, ignore: true);
		Physics.IgnoreLayerCollision(10, 10, ignore: true);
		Physics.IgnoreLayerCollision(25, 25, ignore: true);
		Physics.IgnoreLayerCollision(10, 25, ignore: true);
		Physics.IgnoreLayerCollision(25, 12, ignore: true);
		Physics.IgnoreLayerCollision(15, 8, ignore: true);
		Physics.IgnoreLayerCollision(16, 8, ignore: true);
		Physics.IgnoreLayerCollision(16, 15, ignore: true);
		Physics.IgnoreLayerCollision(18, 9, ignore: true);
		for (int i = 0; i < 29; i++)
		{
			Physics.IgnoreLayerCollision(19, i, ignore: true);
		}
		Physics.IgnoreLayerCollision(19, 9, ignore: false);
		Physics.IgnoreLayerCollision(19, 14, ignore: false);
		Physics.IgnoreLayerCollision(19, 12, ignore: false);
		Physics.IgnoreLayerCollision(19, 22, ignore: false);
		for (int j = 0; j < 29; j++)
		{
			Physics.IgnoreLayerCollision(24, j, ignore: true);
		}
		Physics.IgnoreLayerCollision(24, 9, ignore: false);
		Physics.IgnoreLayerCollision(24, 14, ignore: false);
		Physics.IgnoreLayerCollision(24, 22, ignore: false);
		EnableAutoRotate();
		SoundManager.Instance.Init();
		CollisionCenter.Instance.Init();
		return bInit;
	}

	public static void Shutdown()
	{
		bInit = false;
		CollisionCenter.Instance.Shutdown();
		CollisionLayer.Instance.Shutdown();
		SoundManager.Instance.Shutdown();
	}

	public static void EnableAutoRotate()
	{
		iPhoneKeyboard.autorotateToPortrait = false;
		iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
		iPhoneKeyboard.autorotateToLandscapeLeft = true;
		iPhoneKeyboard.autorotateToLandscapeRight = true;
	}

	public static void DisableAutoRotate()
	{
		iPhoneKeyboard.autorotateToPortrait = false;
		iPhoneKeyboard.autorotateToPortraitUpsideDown = false;
		iPhoneKeyboard.autorotateToLandscapeLeft = false;
		iPhoneKeyboard.autorotateToLandscapeRight = false;
	}

	public static void Update()
	{
		InputManager.Instance.Update();
	}

	public static void UpdatePhysx()
	{
		if (bInit)
		{
			CollisionCenter.Instance.UpdatePhysx();
		}
	}
}
