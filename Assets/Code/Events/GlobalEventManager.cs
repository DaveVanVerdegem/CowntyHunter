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
	public UnityEvent<Player> PlayerJoined = new UnityEvent<Player>();
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
