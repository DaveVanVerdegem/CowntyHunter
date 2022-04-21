using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class AudioPlayer : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private AudioSource _audioSource = null;
	#endregion

	#region Life Cycle
	public void Initialize(AudioClip clip)
	{
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
		Play(audioClipProperties.GetClip(), position);
	}

	public static void Play(AudioClip clip, Vector3 position)
	{
		AudioPlayer player = Instantiate(Settings.AudioPlayerPrefab, position, Quaternion.identity);
		player.Initialize(clip);
	}
	#endregion
}
