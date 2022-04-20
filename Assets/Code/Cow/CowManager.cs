using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class CowManager : MonoBehaviour
{
	#region Inspector Fields

	#endregion

	#region Properties

	#endregion

	#region Fields

	#endregion

	#region Life Cycle
	private void Start()
	{
		//List<Cow> cows = 

		int cowsSpawned = 0;

		for (int i = 0; i < Settings.CowsInGame; i++)
		{
			Cow cow = null;
			if (cowsSpawned < Settings.CowPrefabs.Count)
				cow = Settings.CowPrefabs[i];
			else
				cow = Settings.GetRandomCowPrefab();

			SpawnCow(cow, GetRandomPosition());

			cowsSpawned++;
		}

		GlobalEvents.CowTipped.AddListener(CheckCowsTipped);
	}

	private void Update()
	{
	}
	#endregion

	#region Methods
	private void SpawnCow(Cow cow, Vector3 position)
	{
		Instantiate(cow, position, Quaternion.AngleAxis(Random.Range(0, 180f), Vector3.up));
	}

	private Vector3 GetRandomPosition()
	{
		float x = Random.Range(-Settings.PastureSize.x, Settings.PastureSize.x);
		float z = Random.Range(-Settings.PastureSize.y, Settings.PastureSize.y);

		return new Vector3(x, 0, z);
	}

	private void CheckCowsTipped(Cow tippedCow, Player player)
	{
		foreach(Cow cow in Cow.Cows)
		{
			if (!cow.Tipped)
				return;
		}

		GlobalEvents.GameOver?.Invoke(true);
	}
	#endregion
}
