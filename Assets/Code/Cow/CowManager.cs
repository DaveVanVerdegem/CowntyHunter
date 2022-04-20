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

		for (int i = 0; i < Settings.CowsInGame; i++)
		{
			SpawnCow(GetRandomPosition());
		}
	}

	private void Update()
	{
	}
	#endregion

	#region Methods
	private void SpawnCow(Vector3 position)
	{
		Instantiate(Settings.GetRandomCowPrefab(), position, Quaternion.AngleAxis(Random.Range(0, 180f), Vector3.up));
	}

	private Vector3 GetRandomPosition()
	{
		float x = Random.Range(-Settings.PastureSize.x, Settings.PastureSize.x);
		float z = Random.Range(-Settings.PastureSize.y, Settings.PastureSize.y);

		return new Vector3(x, 0, z);
	}
	#endregion
}
