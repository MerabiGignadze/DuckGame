using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	public CoreSounds coreSounds;
	public OtherSounds otherSounds;

	public void PlaySound(AudioSource _audio, bool _randomPitch)
	{
		if (_randomPitch)
		{
			float randomPitch = Random.Range(0.9f, 1.1f);
			_audio.pitch = randomPitch;
		}
		_audio.Play();
	}
	public void PlayRandomSound(AudioSource[] _audioSource, bool _randomPitch)
	{
		int randomIndex = Random.Range(0,_audioSource.Length);
		PlaySound(_audioSource[randomIndex], _randomPitch);
	}
	public void TurnLoopedSoundOn(AudioSource audioSource, bool _turnOn)
	{
		if (_turnOn) { audioSource.Play(); }
		else { audioSource.Stop(); }
	}
}
[System.Serializable]
public class CoreSounds
{
	public AudioSource[] fx_duck;
	public AudioSource fx_pop;
	public AudioSource fx_smoke;
	public AudioSource fx_sparkles;
	public AudioSource fx_waterSplash;
	public AudioSource fx_wrong;
	public AudioSource fx_correct;
	public AudioSource Music;
}
[System.Serializable]
public class OtherSounds
{

}
