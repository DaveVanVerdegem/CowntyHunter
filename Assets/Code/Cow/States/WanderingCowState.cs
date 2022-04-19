using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingCowState : CowState
{
	#region Properties
	public override CowStateType State => CowStateType.Wandering;
	public override bool CanBeTipped => false;
	#endregion

	#region Fields
	private Vector3 _targetPosition = Vector3.zero;
	private float _wanderingRange = 3f;
	private float _threshold = .5f;

	private float _wanderTimer = 5f;
	#endregion

	#region Constructors
	public WanderingCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is wandering.");

		GenerateNewTargetPosition();
	}
	#endregion

	#region Life Cycle
	public override void Enter()
	{
	}

	public override void Run()
	{
		if(_wanderTimer > 0)
		{
			_wanderTimer -= Time.deltaTime;
		}
		else
		{
			_cow.SetState(new AsleepCowState(_cow));
		}

		if (TargetReached())
			GenerateNewTargetPosition();

		_cow.Move(_targetPosition - _cow.transform.position);
	}

	public override void Exit()
	{
	}
	#endregion

	#region Methods
	private void GenerateNewTargetPosition()
	{
		Vector3 randomVector = Random.onUnitSphere * _wanderingRange;
		randomVector.y = 0;
		Debug.Log(randomVector);

		_targetPosition = _cow.transform.position + randomVector;

		ClampTargetPosition();
	}

	private void ClampTargetPosition()
	{
		_targetPosition.x = Mathf.Clamp(_targetPosition.x, -_cow.PastureSize.x + _threshold, _cow.PastureSize.x - _threshold);
		_targetPosition.z = Mathf.Clamp(_targetPosition.z, -_cow.PastureSize.y + _threshold, _cow.PastureSize.y - _threshold);
	}

	private bool TargetReached()
	{
		return Vector3.Distance(_cow.transform.position, _targetPosition) < _threshold;
	}
	#endregion

	#region Methods
	public override void ShowDebugInfo()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(_targetPosition, _threshold);
		Gizmos.DrawLine(_cow.transform.position, _targetPosition);
	}
	#endregion
}
