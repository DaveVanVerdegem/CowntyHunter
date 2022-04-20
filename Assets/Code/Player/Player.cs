using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Foundation.Patterns.Facade;

public class Player : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private List<GameObject> _playerVisuals = new List<GameObject>();
	#endregion

	#region Properties
	public int ID => _id;
	public int Score { get; private set; } = 0;
	[HideInInspector] public UnityEvent<Player> ScoreUpdated = new UnityEvent<Player>();
	#endregion

	#region Fields
	private CowDetector _cowDetector = null;
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

		GlobalEvents.CowTipped.AddListener(AddScore);
    }

	private void Start()
	{
		GlobalEvents.PlayerJoined?.Invoke(this);
	}

    private void OnDestroy()
	{
		_players.Remove(this);
	}
	#endregion

	#region Input Methods

    public void TryTipping(InputAction.CallbackContext callback)
	{
		if (!callback.performed) return;

		if (_cowDetector.CowFound)
			_cowDetector.Cow.TryToTip(this);
	}
	#endregion

	#region Methods

    private void AddScore(Cow cow, Player player)
	{
		if (player != this) return;

		Score++;

		ScoreUpdated?.Invoke(this);
	}
	#endregion
}
