using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName ="Cownty Hunter/Game Settings")]
public class GameSettings : ScriptableSingleton<GameSettings>
{
	#region Inspector Fields
	public int CowsInGame = 10;
	public Vector2 PastureSize = new Vector2(40, 24.5f);
	[SerializeField] private List<Cow> _cowPrefabs = new List<Cow>();
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
		return _cowPrefabs[Random.Range(0, _cowPrefabs.Count)];
	}
	#endregion
}
