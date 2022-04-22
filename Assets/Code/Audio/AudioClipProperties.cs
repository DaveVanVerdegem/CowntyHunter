using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cownty Hunter/ Audio Clip Properties")]
public class AudioClipProperties : ScriptableObject
{
	#region Inspector Fields
	[SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();
	public MixingGroup MixingGroup = MixingGroup.DefaultSounds;
	#endregion

	#region Methods
	public AudioClip GetClip()
	{
		return _audioClips[Random.Range(0, _audioClips.Count)];
	}
	#endregion
}
