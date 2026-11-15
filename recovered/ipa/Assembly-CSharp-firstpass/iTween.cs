using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class iTween : MonoBehaviour
{
	public enum EaseType
	{
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		linear,
		spring,
		bounce,
		easeInBack,
		easeOutBack,
		easeInOutBack,
		elastic,
		punch
	}

	public enum LoopType
	{
		none,
		loop,
		pingPong
	}

	public enum NamedValueColor
	{
		_Color,
		_SpecColor,
		_Emission,
		_ReflectColor
	}

	public static class Defaults
	{
		public static float time;

		public static float delay;

		public static NamedValueColor namedColorValue;

		public static LoopType loopType;

		public static EaseType easeType;

		public static float lookSpeed;

		public static bool isLocal;

		public static Space space;

		public static bool orientToPath;

		public static Color color;

		public static float updateTimePercentage;

		public static float updateTime;

		public static int cameraFadeDepth;

		public static float lookAhead;

		public static bool useRealTime;

		public static Vector3 up;
	}

	private class CRSpline
	{
		public Vector3[] pts;

		public CRSpline(params Vector3[] pts)
		{
		}

		public Vector3 Interp(float t)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private delegate float EasingFunction(float start, float end, float value);

	private delegate void ApplyTween();

	public static ArrayList tweens;

	private static GameObject cameraFade;

	public string id;

	public string type;

	public string method;

	public EaseType easeType;

	public float time;

	public float delay;

	public LoopType loopType;

	public bool isRunning;

	public bool isPaused;

	private float runningTime;

	private float percentage;

	private float delayStarted;

	private bool kinematic;

	private bool isLocal;

	private bool loop;

	private bool reverse;

	private bool wasPaused;

	private bool physics;

	private Hashtable tweenArguments;

	private Space space;

	private EasingFunction ease;

	private ApplyTween apply;

	private AudioSource audioSource;

	private Vector3[] vector3s;

	private Vector2[] vector2s;

	private Color[,] colors;

	private float[] floats;

	private Rect[] rects;

	private CRSpline path;

	private Vector3 preUpdate;

	private Vector3 postUpdate;

	private NamedValueColor namedcolorvalue;

	private float lastRealTime;

	private bool useRealTime;

	public static void CameraFadeFrom(float amount, float time)
	{
	}

	public static void CameraFadeFrom(Hashtable args)
	{
	}

	public static void CameraFadeTo(float amount, float time)
	{
	}

	public static void CameraFadeTo(Hashtable args)
	{
	}

	public static void ValueTo(GameObject target, Hashtable args)
	{
	}

	public static void FadeFrom(GameObject target, float alpha, float time)
	{
	}

	public static void FadeFrom(GameObject target, Hashtable args)
	{
	}

	public static void FadeTo(GameObject target, float alpha, float time)
	{
	}

	public static void FadeTo(GameObject target, Hashtable args)
	{
	}

	public static void ColorFrom(GameObject target, Color color, float time)
	{
	}

	public static void ColorFrom(GameObject target, Hashtable args)
	{
	}

	public static void ColorTo(GameObject target, Color color, float time)
	{
	}

	public static void ColorTo(GameObject target, Hashtable args)
	{
	}

	public static void AudioFrom(GameObject target, float volume, float pitch, float time)
	{
	}

	public static void AudioFrom(GameObject target, Hashtable args)
	{
	}

	public static void AudioTo(GameObject target, float volume, float pitch, float time)
	{
	}

	public static void AudioTo(GameObject target, Hashtable args)
	{
	}

	public static void Stab(GameObject target, AudioClip audioclip, float delay)
	{
	}

	public static void Stab(GameObject target, Hashtable args)
	{
	}

	public static void LookFrom(GameObject target, Vector3 looktarget, float time)
	{
	}

	public static void LookFrom(GameObject target, Hashtable args)
	{
	}

	public static void LookTo(GameObject target, Vector3 looktarget, float time)
	{
	}

	public static void LookTo(GameObject target, Hashtable args)
	{
	}

	public static void MoveTo(GameObject target, Vector3 position, float time)
	{
	}

	public static void MoveTo(GameObject target, Hashtable args)
	{
	}

	public static void MoveFrom(GameObject target, Vector3 position, float time)
	{
	}

	public static void MoveFrom(GameObject target, Hashtable args)
	{
	}

	public static void MoveAdd(GameObject target, Vector3 amount, float time)
	{
	}

	public static void MoveAdd(GameObject target, Hashtable args)
	{
	}

	public static void MoveBy(GameObject target, Vector3 amount, float time)
	{
	}

	public static void MoveBy(GameObject target, Hashtable args)
	{
	}

	public static void ScaleTo(GameObject target, Vector3 scale, float time)
	{
	}

	public static void ScaleTo(GameObject target, Hashtable args)
	{
	}

	public static void ScaleFrom(GameObject target, Vector3 scale, float time)
	{
	}

	public static void ScaleFrom(GameObject target, Hashtable args)
	{
	}

	public static void ScaleAdd(GameObject target, Vector3 amount, float time)
	{
	}

	public static void ScaleAdd(GameObject target, Hashtable args)
	{
	}

	public static void ScaleBy(GameObject target, Vector3 amount, float time)
	{
	}

	public static void ScaleBy(GameObject target, Hashtable args)
	{
	}

	public static void RotateTo(GameObject target, Vector3 rotation, float time)
	{
	}

	public static void RotateTo(GameObject target, Hashtable args)
	{
	}

	public static void RotateFrom(GameObject target, Vector3 rotation, float time)
	{
	}

	public static void RotateFrom(GameObject target, Hashtable args)
	{
	}

	public static void RotateAdd(GameObject target, Vector3 amount, float time)
	{
	}

	public static void RotateAdd(GameObject target, Hashtable args)
	{
	}

	public static void RotateBy(GameObject target, Vector3 amount, float time)
	{
	}

	public static void RotateBy(GameObject target, Hashtable args)
	{
	}

	public static void ShakePosition(GameObject target, Vector3 amount, float time)
	{
	}

	public static void ShakePosition(GameObject target, Hashtable args)
	{
	}

	public static void ShakeScale(GameObject target, Vector3 amount, float time)
	{
	}

	public static void ShakeScale(GameObject target, Hashtable args)
	{
	}

	public static void ShakeRotation(GameObject target, Vector3 amount, float time)
	{
	}

	public static void ShakeRotation(GameObject target, Hashtable args)
	{
	}

	public static void PunchPosition(GameObject target, Vector3 amount, float time)
	{
	}

	public static void PunchPosition(GameObject target, Hashtable args)
	{
	}

	public static void PunchRotation(GameObject target, Vector3 amount, float time)
	{
	}

	public static void PunchRotation(GameObject target, Hashtable args)
	{
	}

	public static void PunchScale(GameObject target, Vector3 amount, float time)
	{
	}

	public static void PunchScale(GameObject target, Hashtable args)
	{
	}

	private void GenerateTargets()
	{
	}

	private void GenerateRectTargets()
	{
	}

	private void GenerateColorTargets()
	{
	}

	private void GenerateVector3Targets()
	{
	}

	private void GenerateVector2Targets()
	{
	}

	private void GenerateFloatTargets()
	{
	}

	private void GenerateColorToTargets()
	{
	}

	private void GenerateAudioToTargets()
	{
	}

	private void GenerateStabTargets()
	{
	}

	private void GenerateLookToTargets()
	{
	}

	private void GenerateMoveToPathTargets()
	{
	}

	private void GenerateMoveToTargets()
	{
	}

	private void GenerateMoveByTargets()
	{
	}

	private void GenerateScaleToTargets()
	{
	}

	private void GenerateScaleByTargets()
	{
	}

	private void GenerateScaleAddTargets()
	{
	}

	private void GenerateRotateToTargets()
	{
	}

	private void GenerateRotateAddTargets()
	{
	}

	private void GenerateRotateByTargets()
	{
	}

	private void GenerateShakePositionTargets()
	{
	}

	private void GenerateShakeScaleTargets()
	{
	}

	private void GenerateShakeRotationTargets()
	{
	}

	private void GeneratePunchPositionTargets()
	{
	}

	private void GeneratePunchRotationTargets()
	{
	}

	private void GeneratePunchScaleTargets()
	{
	}

	private void ApplyRectTargets()
	{
	}

	private void ApplyColorTargets()
	{
	}

	private void ApplyVector3Targets()
	{
	}

	private void ApplyVector2Targets()
	{
	}

	private void ApplyFloatTargets()
	{
	}

	private void ApplyColorToTargets()
	{
	}

	private void ApplyAudioToTargets()
	{
	}

	private void ApplyStabTargets()
	{
	}

	private void ApplyMoveToPathTargets()
	{
	}

	private void ApplyMoveToTargets()
	{
	}

	private void ApplyMoveByTargets()
	{
	}

	private void ApplyScaleToTargets()
	{
	}

	private void ApplyLookToTargets()
	{
	}

	private void ApplyRotateToTargets()
	{
	}

	private void ApplyRotateAddTargets()
	{
	}

	private void ApplyShakePositionTargets()
	{
	}

	private void ApplyShakeScaleTargets()
	{
	}

	private void ApplyShakeRotationTargets()
	{
	}

	private void ApplyPunchPositionTargets()
	{
	}

	private void ApplyPunchRotationTargets()
	{
	}

	private void ApplyPunchScaleTargets()
	{
	}

	[DebuggerHidden]
	private IEnumerator TweenDelay()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void TweenStart()
	{
	}

	[DebuggerHidden]
	private IEnumerator TweenRestart()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void TweenUpdate()
	{
	}

	private void TweenComplete()
	{
	}

	private void TweenLoop()
	{
	}

	public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float FloatUpdate(float currentValue, float targetValue, float speed)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void FadeUpdate(GameObject target, Hashtable args)
	{
	}

	public static void FadeUpdate(GameObject target, float alpha, float time)
	{
	}

	public static void ColorUpdate(GameObject target, Hashtable args)
	{
	}

	public static void ColorUpdate(GameObject target, Color color, float time)
	{
	}

	public static void AudioUpdate(GameObject target, Hashtable args)
	{
	}

	public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
	{
	}

	public static void RotateUpdate(GameObject target, Hashtable args)
	{
	}

	public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
	{
	}

	public static void ScaleUpdate(GameObject target, Hashtable args)
	{
	}

	public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
	{
	}

	public static void MoveUpdate(GameObject target, Hashtable args)
	{
	}

	public static void MoveUpdate(GameObject target, Vector3 position, float time)
	{
	}

	public static void LookUpdate(GameObject target, Hashtable args)
	{
	}

	public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
	{
	}

	public static float PathLength(Transform[] path)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static float PathLength(Vector3[] path)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static Texture2D CameraTexture(Color color)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static Texture2D CameraTexture(Color color, int w, int h)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void PutOnPath(GameObject target, Vector3[] path, float percent)
	{
	}

	public static void PutOnPath(Transform target, Vector3[] path, float percent)
	{
	}

	public static void PutOnPath(GameObject target, Transform[] path, float percent)
	{
	}

	public static void PutOnPath(Transform target, Transform[] path, float percent)
	{
	}

	public static Vector3 PointOnPath(Transform[] path, float percent)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void DrawLine(Vector3[] line)
	{
	}

	public static void DrawLine(Vector3[] line, Color color)
	{
	}

	public static void DrawLine(Transform[] line)
	{
	}

	public static void DrawLine(Transform[] line, Color color)
	{
	}

	public static void DrawLineGizmos(Vector3[] line)
	{
	}

	public static void DrawLineGizmos(Vector3[] line, Color color)
	{
	}

	public static void DrawLineGizmos(Transform[] line)
	{
	}

	public static void DrawLineGizmos(Transform[] line, Color color)
	{
	}

	public static void DrawLineHandles(Vector3[] line)
	{
	}

	public static void DrawLineHandles(Vector3[] line, Color color)
	{
	}

	public static void DrawLineHandles(Transform[] line)
	{
	}

	public static void DrawLineHandles(Transform[] line, Color color)
	{
	}

	public static Vector3 PointOnPath(Vector3[] path, float percent)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void DrawPath(Vector3[] path)
	{
	}

	public static void DrawPath(Vector3[] path, Color color)
	{
	}

	public static void DrawPath(Transform[] path)
	{
	}

	public static void DrawPath(Transform[] path, Color color)
	{
	}

	public static void DrawPathGizmos(Vector3[] path)
	{
	}

	public static void DrawPathGizmos(Vector3[] path, Color color)
	{
	}

	public static void DrawPathGizmos(Transform[] path)
	{
	}

	public static void DrawPathGizmos(Transform[] path, Color color)
	{
	}

	public static void DrawPathHandles(Vector3[] path)
	{
	}

	public static void DrawPathHandles(Vector3[] path, Color color)
	{
	}

	public static void DrawPathHandles(Transform[] path)
	{
	}

	public static void DrawPathHandles(Transform[] path, Color color)
	{
	}

	public static void CameraFadeDepth(int depth)
	{
	}

	public static void CameraFadeDestroy()
	{
	}

	public static void CameraFadeSwap(Texture2D texture)
	{
	}

	public static GameObject CameraFadeAdd(Texture2D texture, int depth)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static GameObject CameraFadeAdd(Texture2D texture)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static GameObject CameraFadeAdd()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void Resume(GameObject target)
	{
	}

	public static void Resume(GameObject target, bool includechildren)
	{
	}

	public static void Resume(GameObject target, string type)
	{
	}

	public static void Resume(GameObject target, string type, bool includechildren)
	{
	}

	public static void Resume()
	{
	}

	public static void Resume(string type)
	{
	}

	public static void Pause(GameObject target)
	{
	}

	public static void Pause(GameObject target, bool includechildren)
	{
	}

	public static void Pause(GameObject target, string type)
	{
	}

	public static void Pause(GameObject target, string type, bool includechildren)
	{
	}

	public static void Pause()
	{
	}

	public static void Pause(string type)
	{
	}

	public static int Count()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int Count(string type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int Count(GameObject target)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static int Count(GameObject target, string type)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void Stop()
	{
	}

	public static void Stop(string type)
	{
	}

	public static void Stop(GameObject target)
	{
	}

	public static void Stop(GameObject target, bool includechildren)
	{
	}

	public static void Stop(GameObject target, string type)
	{
	}

	public static void Stop(GameObject target, string type, bool includechildren)
	{
	}

	public static Hashtable Hash(params object[] args)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Awake()
	{
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
	}

	private void LateUpdate()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private static void DrawLineHelper(Vector3[] line, Color color, string method)
	{
	}

	private static void DrawPathHelper(Vector3[] path, Color color, string method)
	{
	}

	private static Vector3[] PathControlPointGenerator(Vector3[] path)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static Vector3 Interp(Vector3[] pts, float t)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static void Launch(GameObject target, Hashtable args)
	{
	}

	private static Hashtable CleanArgs(Hashtable args)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static string GenerateID()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void RetrieveArgs()
	{
	}

	private void GetEasingFunction()
	{
	}

	private void UpdatePercentage()
	{
	}

	private void CallBack(string callbackType)
	{
	}

	private void Dispose()
	{
	}

	private void ConflictCheck()
	{
	}

	private void EnableKinematic()
	{
	}

	private void DisableKinematic()
	{
	}

	private void ResumeDelay()
	{
	}

	private float linear(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float clerp(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float spring(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInQuad(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutQuad(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutQuad(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInCubic(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutCubic(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutCubic(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInQuart(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutQuart(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutQuart(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInQuint(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutQuint(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutQuint(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInSine(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutSine(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutSine(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInExpo(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutExpo(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutExpo(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInCirc(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutCirc(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutCirc(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float bounce(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInBack(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeOutBack(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float easeInOutBack(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float punch(float amplitude, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float elastic(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
