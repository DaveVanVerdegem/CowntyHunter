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
	public Cow GenericCow = null;
	[FormerlySerializedAs("_cowPrefabs")]
	public List<Cow> CowPrefabs = new List<Cow>();
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
		return CowPrefabs[Random.Range(0, CowPrefabs.Count)];
	}
	#endregion
}
