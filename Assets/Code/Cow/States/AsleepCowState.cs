using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsleepCowState : CowState
{
	#region Properties
	public override CowStateType State => CowStateType.Asleep;
	public override bool CanBeTipped => true;
	#endregion

	#region Constructors
	public AsleepCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is asleep.");
	}
	#endregion

	#region Life Cycle
	public override void Run()
	{
	}
	#endregion

	#region Methods
	public override void ShowDebugInfo()
	{
	}
	#endregion
}
