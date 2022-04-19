using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CowDetector : MonoBehaviour
{
	#region Properties
	public UnityEvent<Cow> CowDetected = new UnityEvent<Cow>();
	public UnityEvent<Cow> CowLeft = new UnityEvent<Cow>();
	public bool CowFound => Cow != null;
	public Cow Cow { get; private set; }
	#endregion

	#region Life Cycle
	private void OnTriggerEnter(Collider collider)
	{
		if(collider.TryGetComponent(out Cow cow))
		{
			Cow = cow;
			
			Debug.Log($"Detected {Cow.name}");
			CowDetected?.Invoke(Cow);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.TryGetComponent(out Cow cow) && cow == Cow)
		{
			Cow = null;

			Debug.Log($"{cow.name} left detector.");
			CowLeft?.Invoke(cow);
		}
	}
	#endregion
}
