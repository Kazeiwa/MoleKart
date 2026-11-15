public class VehicleShiftSound
{
	public const int SoundNormal = 40003;

	public const int SoundSmallFire = 40011;

	public const int SoundBigFire = 40012;

	private bool SoundNormalPlaying;

	private bool SoundSmallFirePlaying;

	private bool SoundBigFirePlaying;

	public void PlayShiftNormal()
	{
		if (!SoundNormalPlaying)
		{
			SoundManager.Instance.PlaySoundEx(40003);
		}
		SoundNormalPlaying = true;
	}

	public void PlayShiftSmallFire()
	{
		if (!SoundSmallFirePlaying)
		{
			SoundManager.Instance.PlaySoundEx(40011);
		}
		SoundSmallFirePlaying = true;
	}

	public void PlayShiftBigFire()
	{
		if (!SoundBigFirePlaying)
		{
			SoundManager.Instance.PlaySoundEx(40012);
		}
		SoundBigFirePlaying = true;
	}

	public void Reset()
	{
		SoundNormalPlaying = false;
		SoundSmallFirePlaying = false;
		SoundBigFirePlaying = false;
	}
}
