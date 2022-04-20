using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class GameCanvas : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private List<PlayerUI> _playerUis = new List<PlayerUI>();
	#endregion

	#region Properties

	#endregion

	#region Fields

	#endregion

	#region Life Cycle
	private void Awake()
	{
		GlobalEvents.PlayerJoined.AddListener(InitializePlayerUI);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
	#endregion

	#region Methods
	private void InitializePlayerUI(Player player)
	{
		if (player == null) return;

		foreach(PlayerUI playerUI in _playerUis)
		{
			if (playerUI.Initialized)
				continue;
			
			playerUI.Initialize(player);
			break;
		}	
	}
	#endregion
}
