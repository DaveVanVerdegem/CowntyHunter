using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CowState
{
	#region Enums
	public enum CowStateType
	{
		Asleep = 0,
		Tipped = 1,
		Wandering = 2,
	}
	#endregion

	#region Properties
	public abstract CowStateType State { get; }
	public abstract bool CanBeTipped { get; }
	#endregion

	#region Fields
	protected Cow _cow = null;
	#endregion

	#region Constructors
	protected CowState(Cow cow)
	{
		_cow = cow;
	}
	#endregion

	#region Life Cycle
	public abstract void Enter();
	public abstract void Run();
	public abstract void Exit();
	#endregion

	#region Methods
	public abstract void ShowDebugInfo();
	#endregion
}
