using UnityEngine;

public class TurorialUI : ISceneUI
{
	private enum MOVE_DIR
	{
		NONE,
		MID_TO_TOP,
		TOP_TO_BOTTOM,
		BOTTOM_TO_TOP
	}

	private bool showUI;

	private MOVE_DIR moveDir;

	private GameObject hand;

	private GameObject words;

	private GameObject arrow;

	private static Vector3 handOriPos = new Vector3(84f, -67f, -50f);

	private static Vector3 topOriPos = new Vector3(84f, 40f, -50f);

	private static Vector3 bottomOriPos = new Vector3(84f, -162f, -50f);

	private Vector3 currentPos = handOriPos;

	public float moveSpeed = 3f;

	private float startTime;

	private float currentTime;

	private void Start()
	{
	}

	public void InitUI(Transform transform)
	{
		hand = transform.FindChild("Tutorial/hand").gameObject;
		arrow = transform.FindChild("Tutorial/arrow").gameObject;
		words = transform.FindChild("Tutorial/words").gameObject;
		moveDir = MOVE_DIR.MID_TO_TOP;
	}

	public void ShowTutorialUI(bool show)
	{
		showUI = show;
		hand.active = showUI;
		arrow.active = showUI;
		words.active = showUI;
		if (show)
		{
			startTime = Time.time;
			currentTime = startTime;
		}
	}

	public void UpdateUI()
	{
		if (!showUI)
		{
			return;
		}
		switch (moveDir)
		{
		case MOVE_DIR.MID_TO_TOP:
			currentTime += Time.deltaTime;
			currentPos = Vector3.Lerp(currentPos, topOriPos, (currentTime - startTime) * moveSpeed);
			if (currentPos == topOriPos)
			{
				moveDir = MOVE_DIR.TOP_TO_BOTTOM;
				startTime = Time.time;
				currentTime = startTime;
			}
			break;
		case MOVE_DIR.TOP_TO_BOTTOM:
			currentTime += Time.deltaTime;
			currentPos = Vector3.Lerp(currentPos, bottomOriPos, (currentTime - startTime) * moveSpeed);
			if (currentPos == bottomOriPos)
			{
				moveDir = MOVE_DIR.BOTTOM_TO_TOP;
				startTime = Time.time;
				currentTime = startTime;
			}
			break;
		case MOVE_DIR.BOTTOM_TO_TOP:
			currentTime += Time.deltaTime;
			currentPos = Vector3.Lerp(currentPos, topOriPos, (currentTime - startTime) * moveSpeed);
			if (currentPos == topOriPos)
			{
				moveDir = MOVE_DIR.TOP_TO_BOTTOM;
				startTime = Time.time;
				currentTime = startTime;
			}
			break;
		}
		hand.transform.localPosition = currentPos;
	}
}
