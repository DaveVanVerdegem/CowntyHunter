using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Foundation.Patterns.Facade;

#region Enums
public enum MixingGroup
{
	Music = 0,
	DefaultSounds = 1,
	Footsteps = 2,
}
#endregion

public class AudioPlayer : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private AudioSource _audioSource = null;

	[Space]
	[SerializeField] private AudioMixerGroup _musicMixer = null;
	[SerializeField] private AudioMixerGroup _defaultSoundsMixer = null;
	[SerializeField] private AudioMixerGroup _footstepsMixer = null;
	#endregion

	#region Life Cycle
	public void Initialize(AudioClip clip, MixingGroup mixerGroup)
	{
		SetMixingGroup(mixerGroup);
		_audioSource.clip = clip;
		_audioSource.Play();
	}

	private void Update()
	{
		if(!_audioSource.isPlaying)
			Destroy(gameObject);
	}
	#endregion

	#region Methods
	public static void Play(AudioClipProperties audioClipProperties, Vector3 position)
	{
		Play(audioClipProperties.GetClip(), position, audioClipProperties.MixingGroup);
	}

	public static void Play(AudioClip clip, Vector3 position, MixingGroup mixerGroup)
	{
		
		AudioPlayer player = Instantiate(Settings.AudioPlayerPrefab, position, Quaternion.identity);
		player.Initialize(clip, mixerGroup);
	}

	private void SetMixingGroup(MixingGroup mixingGroup)
	{
		switch(mixingGroup)
		{
			default:
			case MixingGroup.DefaultSounds:
				_audioSource.outputAudioMixerGroup = _defaultSoundsMixer;
				return;

			case MixingGroup.Music:
				_audioSource.outputAudioMixerGroup = _musicMixer;
				return;

			case MixingGroup.Footsteps:
				_audioSource.outputAudioMixerGroup = _footstepsMixer;
				return;

		}
	}
	#endregion
}
