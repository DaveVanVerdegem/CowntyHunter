using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Foundation.Patterns.Facade;

public class Player : Agent
{
	#region Inspector Fields
	[SerializeField] private List<GameObject> _playerVisuals = new List<GameObject>();
	[SerializeField] private List<Color> _flashLightColors = new List<Color>();
	#endregion

	#region Properties
	public int ID => _id;
	public int Score { get; private set; } = 0;
    public Cow BonusCow { get; private set; } = null;

    public static List<Player> Players { get; private set; } = new List<Player>();

	[HideInInspector] public UnityEvent<Player> ScoreUpdated = new UnityEvent<Player>();
	#endregion

	#region Fields
	private CowDetector _cowDetector = null;
	private int _id = 0;
	private Light _flashLight = null;
    #endregion

	#region Life Cycle
	private void Awake()
	{
        _cowDetector = GetComponentInChildren<CowDetector>();
		_flashLight = GetComponentInChildren<Light>();

		Instantiate(_playerVisuals[Players.Count], transform);
		_flashLight.color = _flashLightColors[Players.Count];

		Players.Add(this);
		_id = Players.Count;

		GlobalEvents.CowTipped.AddListener(AddScore);
    }

	private void Start()
	{
        foreach (Cow cow in Cow.Cows)
        {
            if (cow.Unique && !cow.Reserved)
            {
                BonusCow = cow;
                BonusCow.Reserved = true;
                break;
            }
        }

        GlobalEvents.PlayerJoined?.Invoke(this);
	}

    private void OnDestroy()
	{
		Players.Remove(this);
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
        if (cow == null) return;

        if (cow == BonusCow)
            Score += 5;
        else
            Score++;

        ScoreUpdated?.Invoke(this);
	}
	#endregion
}
