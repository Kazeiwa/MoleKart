using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CarEngineSoundController : MonoBehaviour
{
	private const int DAudioId = 40013;

	private const int EAudioId = 40014;

	private const int FAudioId = 40015;

	private Car car;

	public float DVolume;

	public float EVolume;

	public float FVolume;

	private float gearShiftTime;

	private bool shiftingGear;

	private int gearShiftsStarted;

	private int crashesStarted;

	private int oneShotLimit;

	private float idleFadeStartSpeed;

	private float idleFadeStopSpeed;

	private float idleFadeSpeedDiff;

	private float speedFadeStartSpeed;

	private float speedFadeStopSpeed;

	private float speedFadeSpeedDiff;

	private int gear;

	private int topGear;

	private float idlePitch;

	private float startPitch;

	private float lowPitch;

	private float medPitch;

	private float highPitchFirst;

	private float highPitchSecond;

	private float highPitchThird;

	private float highPitchFourth;

	private float shiftPitch;

	private float prevPitchFactor;

	private bool enableEngineSound;

	private bool stopEngineSound;

	private float throttle;

	private void Awake()
	{
	}

	public void Start()
	{
	}

	public void PlaySound()
	{
	}

	private void Update()
	{
	}

	private float Coserp(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float Sinerp(float start, float end, float value)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SetPitch(float pitch)
	{
	}

	private void SetVolume(float pitch)
	{
	}

	[DebuggerHidden]
	private IEnumerable<int> GearShift(float oldPitchFactor, float newPitchFactor, int gear, bool shiftUp)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
