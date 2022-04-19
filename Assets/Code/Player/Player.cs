using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
	#region Fields
	private CowDetector _cowDetector = null;
	private Vector2 _movement = Vector2.zero;
	#endregion

	#region Life Cycle
	private void Awake()
	{
		_cowDetector = GetComponentInChildren<CowDetector>();
	}

	private void Update()
	{
		ApplyMovement();
	}
	#endregion

	#region Input Methods
	public void OnMove(InputAction.CallbackContext callback)
	{
		_movement = callback.ReadValue<Vector2>();
	}

	public void OnTryTipping(InputAction.CallbackContext callback)
	{
		if (!callback.performed) return;

		if (_cowDetector.CowFound)
			_cowDetector.Cow.TryToTip();
	}
	#endregion

	#region Methods
	private void ApplyMovement()
	{
		if (_movement.Equals(Vector2.zero)) return;

		Move(_movement.normalized);
	}
	#endregion
}
