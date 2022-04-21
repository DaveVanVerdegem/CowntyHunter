using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName ="Cownty Hunter/Game Settings")]
public class GameSettings : ScriptableSingleton<GameSettings>
{
	#region Inspector Fields
	public int CowsInGame = 10;
	public Vector2 PastureSize = new Vector2(40, 24.5f);
	public List<Cow> GenericCows = new List<Cow>();
	public List<Cow> CowPrefabs = new List<Cow>();

	public Noise NoisePrefab = null;
	#endregion

	#region Properties

	#endregion

	#region Fields

	#endregion

	#region Life Cycle
	private void Start()
	{
	}

	private void Update()
	{
	}
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
