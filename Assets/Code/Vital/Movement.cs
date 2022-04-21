using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
	[SerializeField] private CharacterController _controller;
	[SerializeField] private Transform _eyes;

	[Header("Functional Options")]
	[SerializeField] private bool _canJump = false;
	[SerializeField] private bool _canSprint = false;
	[SerializeField] private bool _canCrouch = false;
	[SerializeField] private bool _willSlide = false;
	[SerializeField] private bool _useHeadBob = false;

	[Header("Movement Parameters")]
	[SerializeField] private float _speed = 20f;
	[SerializeField] private float _dragOnGround = 5f;
	[SerializeField] private float _slideSpeed = 8f;
	private Vector2 _horizontalInput;
	private Vector3 _velocity;
	private Vector3 _movement;

	[Header("Jump Parameters")]
	[SerializeField] private float _jumpHeight = 2f;
	private Vector3 _jumpForce;
	
	[Header("HeadBob Parameters")]
	[SerializeField] private float _bobSpeed = 14f;
	[SerializeField] private float _bobAmount = .5f;
	private float _defaultYPos = 0;
	private float _timer;

	//Sliding parameters
	private Vector3 _hitNormal;
	private bool _isSliding
	{
		get
		{
			if (_controller.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 1.1f))
			{
				_hitNormal = slopeHit.normal;
				return Vector3.Angle(_hitNormal, Vector3.up) > _controller.slopeLimit;
			}
			else
			{
				return false;
			}
		}
	}
	private Vector3 current_pos;
	private Vector3 last_pos;
	private float _pushTimer = 0;

	public Animator Animator 
	{ 
		get
		{
			if (_animator == null)
				_animator = GetComponentInChildren<Animator>();
			return _animator;
		}
	}

	private Animator _animator;
	

	private void Start()
	{
		_jumpForce = -Physics.gravity.normalized * Mathf.Sqrt(2 * Physics.gravity.magnitude * _jumpHeight);
		_defaultYPos = _eyes.position.y;

		current_pos = transform.position;
		last_pos = transform.position;

	}
	private void Update()
	{
		_movement = new Vector3(_horizontalInput.x, 0, _horizontalInput.y);
	}

	public void OnMove(InputAction.CallbackContext ctx)
	{
		if (ctx.started)
			ApplyStartMovement();
		else if (ctx.performed)
		{
			_horizontalInput = ctx.ReadValue<Vector2>();
			Animator.SetFloat("Velocity", ctx.ReadValue<Vector2>().magnitude);
		}
			
		else if (ctx.canceled)
		{
			_horizontalInput = Vector2.zero;
			ApplyStopMovement();
		}
			
	}

	private void FixedUpdate()
	{
		ApplyGround();
		ApplyGravity();
		ApplyMovement();

		if (_pushTimer <= 0)
		{
			Animator.SetBool("Pushing", false);
			ApplyFinalMovement();
			ApplyRotation();
		}
		else
		{
			_pushTimer -= Time.fixedDeltaTime;
		}
		//ApplyBob();
		ApplyGroundDrag();

	}

	public void ApplyStartMovement()
	{
		Animator.SetBool("Moving", true);
	}
	public void ApplyStopMovement()
	{
		_velocity = Vector3.zero;
		Animator.SetBool("Moving", false);
	}

	private void ApplyBob()
	{
		if (_useHeadBob && _controller.isGrounded && (Mathf.Abs(_movement.x) > 0.1f || Mathf.Abs(_movement.z) > 0.1f))
		{
			_timer += Time.fixedDeltaTime * _bobSpeed;
			_eyes.transform.localPosition = new Vector3(
				_eyes.localPosition.x,
				_defaultYPos + Mathf.Sin(_timer) * _bobAmount,
				_eyes.localPosition.z);
		}
	}

	private void ApplyFinalMovement()
	{
		//if (_willSlide && _isSliding)
		//    _velocity += new Vector3(_hitNormal.x, -_hitNormal.y, _hitNormal.z) * _slideSpeed;
	   _controller.Move(_velocity * Time.fixedDeltaTime);
	}

	private void ApplyRotation()
	{
		if (_controller.isGrounded)
		{
			Vector3 forward = Vector3.Scale(_velocity, new Vector3(1, 0, 1));
			if (forward.normalized.sqrMagnitude <= Mathf.Epsilon) return;
			Vector3 xzAbsoluteForward = forward;
			Quaternion forwardRotation = Quaternion.LookRotation(xzAbsoluteForward);
			this.transform.rotation = forwardRotation;
		}
	}
	private void ApplyGroundDrag()
	{
		if (_controller.isGrounded)
		{
			_velocity *= (1 - Time.fixedDeltaTime * _dragOnGround);
		}
	}
	private void ApplyMovement()
	{
		if (_controller.isGrounded)
		{
			Vector3 forward = Vector3.forward;
			Vector3 xzAbsoluteForward = Vector3.Scale(forward, new Vector3(1, 0, 1));
			Quaternion forwardRotation = Quaternion.LookRotation(xzAbsoluteForward);
			Vector3 relativeMovement = forwardRotation * _movement;
			_velocity += relativeMovement * _speed * Time.fixedDeltaTime;
		}

	}
	public void ApplyJump()
	{
		if (_controller.isGrounded && _canJump)
		{
			_velocity += _jumpForce;
		}
	}
	private void ApplyGround()
	{
		if (_controller.isGrounded)
		{
			_velocity -= Vector3.Project(_velocity, Physics.gravity.normalized);
		}
	}

	private void ApplyGravity()
	{
		if (!_controller.isGrounded)
		{
			_velocity += Physics.gravity * Time.fixedDeltaTime;
		}
	}

	public void ApplyPush()
	{
		if (!Animator.GetBool("Moving") && !Animator.GetBool("Pushing"))
		{
			Animator.SetBool("Pushing", true);
			Animator.SetTrigger("PushTrigger");
			_pushTimer = 2f;
		}
		
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_hitNormal = hit.normal;
	}

	
}
