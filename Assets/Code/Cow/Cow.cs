using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cow : Agent
{
	#region Inspector Fields

	public UnityEvent Tipped = new UnityEvent();
	public UnityEvent Alerted = new UnityEvent();
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
		State = new AsleepCowState(this);
	}

	private void Update()
	{
		State.Run();
	}

	private void OnDrawGizmosSelected()
	{
		if(State != null)
			State.ShowDebugInfo();
	}
	#endregion

	#region Methods
	public bool TryToTip()
	{
		bool tipped = State.CanBeTipped;

		if (tipped)
			Tip();
		else
			Alert();

		return tipped;
	}

	private void Tip()
	{
		Tipped?.Invoke();
		State = new TippedCowState(this);

		Debug.Log($"{gameObject.name} has been tipped.");
	}

	private void Alert()
	{
		Alerted?.Invoke();
		State = new WanderingCowState(this);

		Debug.Log($"{gameObject.name} has been alerted.");
	}
	#endregion
}
