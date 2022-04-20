using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private TextMeshProUGUI _scoreText;
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
		_scoreText.text = "0";
	}

	private void Update()
	{
	}
	#endregion

	#region Methods

	#endregion
}