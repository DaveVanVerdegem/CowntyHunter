using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;

    private CowntyHunter.PlayerActions _groundMovement;

    private Vector2 _horizontalInput;
    private Player _player;
    private CowntyHunter _playerControls;


    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerControls = new CowntyHunter();
        _groundMovement = _playerControls.Player;

        _groundMovement.Move.performed += ctx => _horizontalInput = ctx.ReadValue<Vector2>();

        _groundMovement.Move.started += ctx => _movement.ApplyStartMovement();
        _groundMovement.Move.canceled += ctx => _horizontalInput = Vector2.zero;
        _groundMovement.Move.canceled += ctx => _movement.ApplyStopMovement();

        _groundMovement.Fire.performed += ctx => Push(ctx);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _groundMovement.Disable();
    }
    
    private void Update()
    {
        //_movement.ReceiveInput(_horizontalInput);
    }

    private void Push(InputAction.CallbackContext context)
    {
        _player.TryTipping(context);
        _movement.ApplyPush();
    }
}
