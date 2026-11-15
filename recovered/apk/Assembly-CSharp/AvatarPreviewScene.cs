using UnityEngine;

public class AvatarPreviewScene : MonoBehaviour
{
	private Camera mainCamera;

	private Vector3 cameraTarget = new Vector3(0f, 0.5f, 0f);

	private Vector3 prevMousePos;

	private Vector3 curMousePos;

	private float totalVerRot;

	private bool[] prevStates = new bool[4];

	private bool[] curStates = new bool[4];

	private string[] partDesps = new string[4] { "澶撮儴", "韬?綋", "鑳稿墠", "鑴氶儴" };

	private int[] defaultIDs = new int[4] { 0, 15001, 0, 21001 };

	private bool[] isSelecting = new bool[4];

	private int curSel = -1;

	private void Start()
	{
		TSingleton<CarManager>.Instance.DestroyCar();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		mainCamera.transform.LookAt(cameraTarget);
	}

	private void Update()
	{
		UpdateCamera();
		UpdateAvatar();
	}

	private void UpdateCamera()
	{
		float axis = Input.GetAxis("Mouse ScrollWheel");
		mainCamera.transform.Translate(0f, 0f, axis, Space.Self);
		float num = Vector3.Distance(mainCamera.transform.position, cameraTarget);
		if (num < 1f || num > 4f)
		{
			mainCamera.transform.Translate(0f, 0f, 0f - axis, Space.Self);
		}
		if (Input.GetMouseButtonDown(0))
		{
			curMousePos = Input.mousePosition;
		}
		if (Input.GetMouseButton(0))
		{
			prevMousePos = curMousePos;
			curMousePos = Input.mousePosition;
			float angle = (curMousePos.x - prevMousePos.x) * 0.5f;
			float num2 = (curMousePos.y - prevMousePos.y) * 0.5f;
			totalVerRot += num2;
			mainCamera.transform.RotateAround(cameraTarget, new Vector3(0f, 1f, 0f), angle);
			if (totalVerRot > -89.5f && totalVerRot < 89.5f)
			{
				mainCamera.transform.RotateAround(cameraTarget, -mainCamera.transform.right, num2);
			}
			else
			{
				totalVerRot -= num2;
			}
			mainCamera.transform.LookAt(cameraTarget);
		}
	}

	private void UpdateAvatar()
	{
		for (int i = 0; i < curStates.GetLength(0); i++)
		{
			if (curStates[i] != prevStates[i])
			{
				if (curStates[i] || defaultIDs[i] == 0)
				{
				}
				prevStates[i] = curStates[i];
			}
		}
	}

	private void OnGUI()
	{
		int num = 20;
		int num2 = 100;
		int num3 = 50;
		int num4 = 20;
		int num5 = 100;
		int num6 = 200;
		if (GUI.Button(new Rect(860f, 100f, 80f, 40f), "杩斿洖澶у巺"))
		{
		}
		for (int i = 0; i < curStates.GetLength(0); i++)
		{
			curStates[i] = GUI.Toggle(new Rect(num, num2 + num4 * i, num3, num4), curStates[i], partDesps[i]);
		}
		for (int j = 0; j < curStates.GetLength(0); j++)
		{
			if (GUI.Button(new Rect(num + num3, num2 + num4 * j, num3, num4), "閫夋嫨"))
			{
				isSelecting[j] = !isSelecting[j];
				if (curSel >= 0 && curSel != j)
				{
					isSelecting[curSel] = false;
				}
				curSel = ((!isSelecting[j]) ? (-1) : j);
			}
		}
		if (curSel >= 0 && curSel < curStates.GetLength(0))
		{
			GUI.BeginScrollView(new Rect(num + num3 * 2, num2 + num4 * curSel, num5, num6), Vector2.zero, new Rect(num + num3 * 2, num2 + num4 * curSel, num5, num6));
			string[] texts = new string[4] { "aaa1", "aaa2", "aaa3", "aaa4" };
			GUI.SelectionGrid(new Rect(num + num3 * 2, num2 + num4 * curSel, num3, num4 * 4), 0, texts, 1);
			GUI.EndScrollView();
		}
	}
}
