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
	[SerializeField] private float _footStepInterval = .5f;

	[Space]
	[SerializeField] private Billboard _playerBillboard = null;
	[SerializeField] private List<Material> _billboardIndicators = new List<Material>();
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

	private Vector3 _lastFootStepPosition = Vector3.zero;
	private Movement _movement = null;
    #endregion

	#region Life Cycle
	private void Awake()
	{
        _cowDetector = GetComponentInChildren<CowDetector>();
		_flashLight = GetComponentInChildren<Light>();
		_movement = GetComponentInChildren<Movement>();

		Instantiate(_playerVisuals[Players.Count], transform);
		_flashLight.color = _flashLightColors[Players.Count];
		_playerBillboard.SetMaterial(_billboardIndicators[Players.Count]);

		Players.Add(this);
		_id = Players.Count;

		_lastFootStepPosition = transform.position;

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

		_playerBillboard.Blink(5f);

        GlobalEvents.PlayerJoined?.Invoke(this);
	}

	private void Update()
	{
		if(_movement.Velocity.magnitude > 0)
		{
			if(Vector3.Distance(_lastFootStepPosition, transform.position) > _footStepInterval)
			{
				AudioPlayer.Play(Settings.FootstepClip, transform.position);
				_lastFootStepPosition = transform.position;
			}
		}
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

		AudioPlayer.Play(Settings.PushClip, transform.position);

		if (_cowDetector.CowFound)
			_cowDetector.Cow.TryToTip(this);
	}

	public void QuitGame(InputAction.CallbackContext callback)
	{
		if(!callback.performed) return;

		Application.Quit();
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
