using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Foundation.Patterns.Facade;

public class Poop : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private UnityEvent _onNoiseTriggered = new UnityEvent();
	[SerializeField] private float _cooldown = 1f;
	#endregion

	#region Properties

	#endregion

	#region Fields
	private float _lastTriggered = 0;
	#endregion

	#region Life Cycle
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.TryGetComponent(out Agent agent))
		{
			if (Time.time <= _lastTriggered + _cooldown) return;

			Settings.SpawnNoise(transform.position, 10);
			_lastTriggered = Time.time;
			_onNoiseTriggered?.Invoke();
		}
	}
	#endregion

	#region Methods

	#endregion
}
