using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Cownty Hunter/Game Settings")]
public class GameSettings : ScriptableObject
{
	#region Inspector Fields
	public int CowsInGame = 10;
	public Vector2 PastureSize = new Vector2(40, 24.5f);
	public List<Cow> GenericCows = new List<Cow>();
	public List<Cow> CowPrefabs = new List<Cow>();

	public Noise NoisePrefab = null;

	public int StartingPoops = 10;
	public Poop PoopPrefab = null;

	[Header("Audio")]
	public AudioPlayer AudioPlayerPrefab = null;

	[Space]
	public AudioClipProperties FartClip = null;
	public AudioClipProperties FallingCowClip = null;
	public AudioClipProperties FristiFallingCowClip = null;
	public AudioClipProperties PushClip = null;

	public AudioClipProperties AlertedCowClip = null;
	public AudioClipProperties FristiAlertedCowClip = null;
	public AudioClipProperties FootstepClip = null;
	#endregion

	#region Methods
	public Cow GetRandomCowPrefab()
	{
		return GenericCows[Random.Range(0, GenericCows.Count)];
	}

	public void SpawnNoise(Vector3 position, float magnitude = -1)
	{
		Noise noise = Instantiate(NoisePrefab, position, Quaternion.identity);
		noise.Initialize(magnitude);
	}
	#endregion
}
