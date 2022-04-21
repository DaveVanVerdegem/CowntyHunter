using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class TippedCowState : CowState
{
	#region Properties
	public override CowStateType State => CowStateType.Tipped;
	public override bool CanBeTipped => false;
	public override bool CanBeAlerted => false;
	#endregion

	#region Constructors
	public TippedCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is tipped.");
		_cow.transform.Rotate(Vector3.right, 180);
	}
	#endregion

	#region Life Cycle
	public override void Enter()
	{
		Settings.SpawnNoise(_cow.transform.position);
	}

	public override void Run()
	{
	}

	public override void Exit()
	{
	}
	#endregion

	#region Methods
	public override void ShowDebugInfo()
	{
	}
	#endregion
}
