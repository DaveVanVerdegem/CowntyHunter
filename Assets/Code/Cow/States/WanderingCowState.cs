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
	#endregion

	#region Constructors
	public WanderingCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is wandering.");

		GenerateNewTargetPosition();
	}
	#endregion

	#region Life Cycle
	public override void Run()
	{
		if (TargetReached())
			GenerateNewTargetPosition();

		_cow.Move((_targetPosition - _cow.transform.position));
	}
	#endregion

	#region Methods
	private void GenerateNewTargetPosition()
	{
		Vector3 randomVector = Random.onUnitSphere * _wanderingRange;
		randomVector.y = 0;
		Debug.Log(randomVector);
		_targetPosition = _cow.transform.position + randomVector;
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
