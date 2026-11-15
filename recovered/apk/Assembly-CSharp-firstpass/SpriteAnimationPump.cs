using System.Collections;
using UnityEngine;

public class SpriteAnimationPump : MonoBehaviour
{
	private static SpriteAnimationPump instance;

	protected static SpriteBase head;

	protected static SpriteBase cur;

	protected static bool pumpIsRunning;

	protected static bool pumpIsDone = true;

	public static float animationPumpInterval = 0.03333f;

	public bool IsRunning => pumpIsRunning;

	public static SpriteAnimationPump Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject gameObject = new GameObject("SpriteAnimationPump");
				instance = (SpriteAnimationPump)gameObject.AddComponent(typeof(SpriteAnimationPump));
			}
			return instance;
		}
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(this);
	}

	public void StartAnimationPump()
	{
		if (!pumpIsRunning)
		{
			pumpIsRunning = true;
			StartCoroutine(PumpStarter());
		}
	}

	protected IEnumerator PumpStarter()
	{
		while (!pumpIsDone)
		{
			yield return null;
		}
		StartCoroutine(AnimationPump());
	}

	public static void StopAnimationPump()
	{
	}

	protected static IEnumerator AnimationPump()
	{
		float startTime = Time.realtimeSinceStartup;
		pumpIsDone = false;
		while (pumpIsRunning)
		{
			yield return null;
			float time = Time.realtimeSinceStartup;
			float elapsed = time - startTime;
			startTime = time;
			cur = head;
			while (cur != null)
			{
				SpriteBase next = (SpriteBase)cur.next;
				cur.StepAnim(elapsed);
				cur = next;
			}
		}
		pumpIsDone = true;
	}

	public static void Add(SpriteBase s)
	{
		if (head != null)
		{
			s.next = head;
			head.prev = s;
			head = s;
		}
		else
		{
			head = s;
			Instance.StartAnimationPump();
		}
	}

	public static void Remove(SpriteBase s)
	{
		if (head == s)
		{
			head = (SpriteBase)s.next;
			if (head == null)
			{
				StopAnimationPump();
			}
		}
		else if (s.next != null)
		{
			s.prev.next = s.next;
			s.next.prev = s.prev;
		}
		else if (s.prev != null)
		{
			s.prev.next = null;
		}
		s.next = null;
		s.prev = null;
	}
}
