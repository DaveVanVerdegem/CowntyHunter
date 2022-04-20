using Foundation.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : Singleton<GlobalEventManager>
{
	#region Inspector Fields

	#endregion

	#region Properties
	// Players
	public UnityEvent<Player> PlayerJoined = new UnityEvent<Player>();

	// Cows
	public UnityEvent<Cow, Player> CowTipped = new UnityEvent<Cow, Player>();
	public UnityEvent<Cow, Player> CowAlerted = new UnityEvent<Cow, Player>();
	#endregion

	#region Fields

	#endregion

	#region Life Cycle
	private void Start()
	{
	}

	private void Update()
	{
	}
	#endregion

	#region Methods

	#endregion
}
