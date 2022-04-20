using Foundation.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : Singleton<GlobalEventManager>
{
	#region Properties
	// Players
	public UnityEvent<Player> PlayerJoined = new UnityEvent<Player>();

	// Cows
	public UnityEvent<Cow, Player> CowTipped = new UnityEvent<Cow, Player>();
	public UnityEvent<Cow, Player> CowAlerted = new UnityEvent<Cow, Player>();

	// Game
	public UnityEvent<bool> GameOver = new UnityEvent<bool>();
	#endregion
}
