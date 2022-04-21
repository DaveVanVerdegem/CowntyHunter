using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Foundation.Patterns.Facade;

public class Cow : Agent, IHearable
{
	#region Inspector Fields
	public bool Unique = false;
	[Multiline]
	public string Description = "The cow with the spots.";
	public Sprite Sprite = null;
	public Transform TippingPoint = null;

	public Billboard AlertedBillboard = null;
	public Billboard SleepingBillboard = null;
	#endregion

	#region Properties
	public CowState State { get; private set; }
	public bool Tipped => State.State == CowState.CowStateType.Tipped;

	public static List<Cow> Cows { get; private set; } = new List<Cow>();
	[HideInInspector] public bool Reserved = false;
	#endregion

	#region Life Cycle
	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		SetState(new AsleepCowState(this));

		Cows.Add(this);
	}

	private void Update()
	{
		State.Run();
	}

	private void OnDrawGizmosSelected()
	{
		if (Settings == null) return;

		Gizmos.color = Color.cyan;
		Vector2 pastureSize = Settings.PastureSize;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(pastureSize.x * 2, 2f, pastureSize.y * 2));

		if(State != null)
			State.ShowDebugInfo();
	}

	private void OnDisable()
	{
		Cows.Remove(this);
	}
	#endregion

	#region Methods
	public void SetState(CowState cowState)
	{
		if(State != null)
			State.Exit();

		State = cowState;
	}

	public bool TryToTip(Player player)
	{
		if (Tipped) return false;

		bool tipped = State.CanBeTipped;

		if (tipped)
			Tip(player);
		else
			Alert(player);

		return tipped;
	}

	private void Tip(Player player)
	{
		SetState(new TippedCowState(this));
		GlobalEvents.CowTipped?.Invoke(this, player);

		//Debug.Log($"{gameObject.name} has been tipped.");
	}

	private void Alert(Player player)
	{
		SetState(new AlertedCowState(this));
		GlobalEvents.CowAlerted?.Invoke(this, player);

		//Debug.Log($"{gameObject.name} has been alerted.");
	}

	public void HearNoise(Noise noise)
	{
		//Debug.LogWarning($"{name} heard noise.");
		if (State.CanBeAlerted)
			Alert(null);
	}
	#endregion
}
