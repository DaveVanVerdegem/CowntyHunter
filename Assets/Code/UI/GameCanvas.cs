using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Foundation.Patterns.Facade;
using System;
using System.Linq;

public class GameCanvas : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private List<PlayerUI> _playerUis = new List<PlayerUI>();
	[SerializeField] private GameObject _winScreen = null;
	[SerializeField] private TextMeshProUGUI _winnerText = null;
	#endregion

	#region Properties

	#endregion

	#region Fields
	private bool _gameOver = false;
	private bool _gameWon = false;
	#endregion

	#region Life Cycle
	private void Awake()
	{
		GlobalEvents.PlayerJoined.AddListener(InitializePlayerUI);
		GlobalEvents.GameOver.AddListener(SetGameOver);
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if(_gameOver)
		{
			ShowGameOverScreen();
		}
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

	private void SetGameOver(bool gameWon)
	{
		_gameOver = true;
		_gameWon = gameWon;
	}

	private void ShowGameOverScreen()
	{
		Time.timeScale = 0;

		if (_gameWon)
			ShowWinScreen();
	}

	private void ShowWinScreen()
	{
		Player winner = GetBestPlayer();

		_winnerText.text = $"Player {winner.ID} has won the game with a score of {winner.Score}!";
		_winScreen.SetActive(true);
	}

	private Player GetBestPlayer()
	{
		Player bestPlayer = Player.Players.FirstOrDefault();

		foreach(Player player in Player.Players)
		{
			if(player.Score > bestPlayer.Score)
				bestPlayer = player;
		}

		return bestPlayer;
	}
	#endregion
}
