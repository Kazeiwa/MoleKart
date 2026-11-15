using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class iTweenEvent : MonoBehaviour
{
	public enum TweenType
	{
		AudioFrom,
		AudioTo,
		AudioUpdate,
		CameraFadeFrom,
		CameraFadeTo,
		ColorFrom,
		ColorTo,
		ColorUpdate,
		FadeFrom,
		FadeTo,
		FadeUpdate,
		LookFrom,
		LookTo,
		LookUpdate,
		MoveAdd,
		MoveBy,
		MoveFrom,
		MoveTo,
		MoveUpdate,
		PunchPosition,
		PunchRotation,
		PunchScale,
		RotateAdd,
		RotateBy,
		RotateFrom,
		RotateTo,
		RotateUpdate,
		ScaleAdd,
		ScaleBy,
		ScaleFrom,
		ScaleTo,
		ScaleUpdate,
		ShakePosition,
		ShakeRotation,
		ShakeScale,
		Stab
	}

	public string tweenName;

	public bool playAutomatically;

	public float delay;

	public TweenType type;

	public bool showIconInInspector;

	[SerializeField]
	private string[] keys;

	[SerializeField]
	private int[] indexes;

	[SerializeField]
	private string[] metadatas;

	[SerializeField]
	private int[] ints;

	[SerializeField]
	private float[] floats;

	[SerializeField]
	private bool[] bools;

	[SerializeField]
	private string[] strings;

	[SerializeField]
	private Vector3[] vector3s;

	[SerializeField]
	private Color[] colors;

	[SerializeField]
	private Space[] spaces;

	[SerializeField]
	private iTween.EaseType[] easeTypes;

	[SerializeField]
	private iTween.LoopType[] loopTypes;

	[SerializeField]
	private GameObject[] gameObjects;

	[SerializeField]
	private Transform[] transforms;

	[SerializeField]
	private AudioClip[] audioClips;

	[SerializeField]
	private AudioSource[] audioSources;

	[SerializeField]
	private ArrayIndexes[] vector3Arrays;

	[SerializeField]
	private ArrayIndexes[] transformArrays;

	[SerializeField]
	private iTweenPath[] paths;

	private Dictionary<string, object> values;

	public Dictionary<string, object> Values
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public static iTweenEvent GetEvent(GameObject obj, string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start()
	{
	}

	public void Play()
	{
	}

	public void OnDrawGizmos()
	{
	}

	[DebuggerHidden]
	private IEnumerator StartEvent()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SerializeValues()
	{
	}

	private void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair)
	{
	}

	private void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, KeyValuePair<string, object> pair, string metadata)
	{
	}

	private void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value)
	{
	}

	private void AddToList<T>(List<string> keyList, List<int> indexList, IList<T> valueList, List<string> metadataList, string key, object value, string metadata)
	{
	}

	public void Clear()
	{
	}

	private void DeserializeValues()
	{
	}
}
