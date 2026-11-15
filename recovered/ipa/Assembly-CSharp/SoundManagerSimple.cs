using System.Collections;
using UnityEngine;

public class SoundManagerSimple : SoundManager
{
	private float MAX_DIS_SQR;

	private float MAX_DIS;

	protected ArrayList m_LoopAudios;

	public bool m_bPauseSound;

	public override void Init()
	{
	}

	public override void Shutdown()
	{
	}

	public override void PlaySound(int id)
	{
	}

	public override void StopSound(int id)
	{
	}

	public override void PauseSound(int id)
	{
	}

	private AudioListener GetListener()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private float GetDistanceSqr(Transform from, Transform to)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override AudioSource AddAudio(GameObject obj, int id)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void PlayAudio(GameObject obj, AudioSource source)
	{
	}

	public void Update()
	{
	}

	public override void StopAudio(AudioSource source)
	{
	}

	public override void FadeSoundToStop(int id, float deltaTime)
	{
	}

	public override void RemoveAudio(AudioSource audio)
	{
	}

	public override void ClearSteroAudios()
	{
	}

	public override void PauseSteroSound()
	{
	}

	public override void ContiueSteroSound()
	{
	}

	public override void StopBackgroundMusic(bool isPause)
	{
	}

	public override void PlayBackgroundMusic()
	{
	}

	public override void PauseBackgroundMusic()
	{
	}

	public override void LoadLevelSounds(int iLevel)
	{
	}

	public override void FreeLevelSounds(int iLevel)
	{
	}
}
