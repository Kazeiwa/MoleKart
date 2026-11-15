using System.Collections.Generic;
using UnityEngine;

public class GestureChecker
{
	private const float MAX_COOL_TIME = 0.5f;

	private float swapCoolDown;

	private Dictionary<int, Vector2> Touch2DeltaDistance = new Dictionary<int, Vector2>();

	public Rect breakRect = new Rect(0f, 0f, 0f, 0f);

	public Rect driftRect = new Rect(0f, 0f, 0f, 0f);

	public Rect sliderRect = new Rect(0f, 0f, 0f, 0f);

	public Rect pauseRect = new Rect(0f, 0f, 0f, 0f);

	public GameObject breakUI;

	public GameObject driftUI;

	public GameObject sliderUI;

	private bool SwapReady()
	{
		swapCoolDown -= Time.deltaTime;
		if (swapCoolDown >= 0f)
		{
			return false;
		}
		return true;
	}

	public int CheckSwap()
	{
		if (AppMain.Instance.Pause)
		{
			return 0;
		}
		if (!SwapReady())
		{
			return 0;
		}
		int num = CheckAnyFingureSwapAndDirection();
		switch (num)
		{
		case 1:
			Debug.Log("CheckSwap : forward");
			swapCoolDown = 0.5f;
			break;
		case 2:
			Debug.Log("CheckSwap : backward");
			swapCoolDown = 0.5f;
			break;
		case 3:
			Debug.Log("CheckSwap : Tap");
			swapCoolDown = 0.5f;
			break;
		}
		return num;
	}

	private int CheckAnyFingureSwapAndDirection()
	{
		Vector2 zero = Vector2.zero;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		Touch[] touches = Input.touches;
		for (int i = 0; i < touches.Length; i++)
		{
			Touch touch = touches[i];
			switch (touch.phase)
			{
			case TouchPhase.Began:
			{
				bool flag4 = true;
				if (breakUI != null && breakUI.active && breakRect.Contains(touch.position))
				{
					flag4 = false;
				}
				if (driftUI != null && driftUI.active && driftRect.Contains(touch.position))
				{
					flag4 = false;
				}
				if (sliderUI != null && sliderUI.active && sliderRect.Contains(touch.position))
				{
					flag4 = false;
				}
				if (pauseRect.Contains(touch.position))
				{
					flag4 = false;
				}
				if (flag4 && !Touch2DeltaDistance.ContainsKey(touch.fingerId))
				{
					Touch2DeltaDistance.Add(touch.fingerId, Vector2.zero);
				}
				break;
			}
			case TouchPhase.Moved:
				zero = touch.deltaPosition;
				if (Touch2DeltaDistance.ContainsKey(touch.fingerId))
				{
					Vector2 vector = Touch2DeltaDistance[touch.fingerId];
					Touch2DeltaDistance[touch.fingerId] = vector + zero;
				}
				break;
			case TouchPhase.Ended:
				if (Touch2DeltaDistance.ContainsKey(touch.fingerId))
				{
					Vector2 vector2 = Touch2DeltaDistance[touch.fingerId];
					Touch2DeltaDistance.Remove(touch.fingerId);
					if (vector2.y >= 20f)
					{
						swapCoolDown = 0.5f;
						flag = true;
					}
					if (vector2.y <= -20f)
					{
						swapCoolDown = 0.5f;
						flag2 = true;
					}
					if ((vector2.y > -5f && vector2.y < 5f) || (vector2.x > -5f && vector2.x < -5f))
					{
						swapCoolDown = 0.5f;
						flag3 = true;
					}
				}
				break;
			case TouchPhase.Canceled:
				if (Touch2DeltaDistance.ContainsKey(touch.fingerId))
				{
					Touch2DeltaDistance.Remove(touch.fingerId);
				}
				break;
			}
		}
		if (flag)
		{
			return 1;
		}
		if (flag2)
		{
			return 2;
		}
		if (flag3)
		{
			return 3;
		}
		return 0;
	}

	public Vector2[] GetTouchPoints()
	{
		int touchCount = Input.touchCount;
		Vector2[] array = new Vector2[touchCount];
		for (int i = 0; i < touchCount; i++)
		{
			ref Vector2 reference = ref array[i];
			reference = Input.GetTouch(i).position;
			array[i].y = (float)Screen.height - array[i].y;
		}
		return array;
	}

	public void Reset()
	{
		Touch2DeltaDistance.Clear();
		breakUI = null;
		driftUI = null;
		sliderUI = null;
	}
}
