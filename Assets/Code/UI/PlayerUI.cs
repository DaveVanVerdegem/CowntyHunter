using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _bonusCowDescriptionText;
	#endregion

	#region Properties
	public bool Initialized => _player != null;
	#endregion

	#region Fields
	private Player _player;
	#endregion

	#region Life Cycle
	private void Awake()
	{
		_scoreText.text = "Press button to join.";
	}

	private void Start()
	{
		
	}

	public void Initialize(Player player)
	{
		if (player == null) return;

		_player = player;
		UpdateScore(_player);
		_bonusCowDescriptionText.text = _player.BonusCow.Description;

		_player.ScoreUpdated.AddListener(UpdateScore);
	}

	private void Update()
	{
	}
	#endregion

	#region Methods
	private void UpdateScore(Player player)
	{
		_scoreText.text = player.Score.ToString();
	}
	#endregion
}
