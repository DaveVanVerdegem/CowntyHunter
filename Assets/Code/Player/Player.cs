using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
	#region Inspector Fields
	[SerializeField] private List<GameObject> _playerVisuals = new List<GameObject>();
	#endregion

	#region Fields
	private CowDetector _cowDetector = null;
	private Vector2 _movement = Vector2.zero;
	private int _id = 0;

	private static List<Player> _players = new List<Player>();
	#endregion

	#region Life Cycle
	private void Awake()
	{
		_cowDetector = GetComponentInChildren<CowDetector>();

		_id = _players.Count;
		Instantiate(_playerVisuals[_id], transform);

		_players.Add(this);
	}

	private void Start()
	{
		GlobalEventManager.Instance.PlayerJoined?.Invoke(this);
	}

	private void Update()
	{
		ApplyMovement();
	}

	private void OnDestroy()
	{
		_players.Remove(this);
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
