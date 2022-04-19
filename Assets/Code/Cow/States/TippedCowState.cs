using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TippedCowState : CowState
{
	#region Properties
	public override CowStateType State => CowStateType.Tipped;
	public override bool CanBeTipped => false;
	#endregion

	#region Constructors
	public TippedCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is tipped.");
		_cow.transform.Rotate(Vector3.right, -90);
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
