using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Foundation.Patterns.Facade;

public class Cow : Agent
{
	#region Inspector Fields
	public Vector2 PastureSize = Vector2.one;


	#endregion

	#region Properties
	public CowState State { get; private set; }
	#endregion

	#region Life Cycle
	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		SetState(new AsleepCowState(this));
	}

	private void Update()
	{
		State.Run();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(PastureSize.x * 2, 2f, PastureSize.y * 2));

		if(State != null)
			State.ShowDebugInfo();
	}
	#endregion

	#region Methods
	public void SetState(CowState cowState)
	{
		if(State != null)
			State.Exit();

		State = cowState;
		State.Enter();
	}

	public bool TryToTip(Player player)
	{
		bool tipped = State.CanBeTipped;

		if (tipped)
			Tip(player);
		else
			Alert(player);

		return tipped;
	}

	private void Tip(Player player)
	{
		GlobalEvents.CowTipped?.Invoke(this, player);
		SetState(new TippedCowState(this));

		Debug.Log($"{gameObject.name} has been tipped.");
	}

	private void Alert(Player player)
	{
		GlobalEvents.CowAlerted?.Invoke(this, player);
		SetState(new WanderingCowState(this));

		Debug.Log($"{gameObject.name} has been alerted.");
	}
	#endregion
}
