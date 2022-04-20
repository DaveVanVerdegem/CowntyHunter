using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Foundation.Patterns.Facade;

public class PlayerUI : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _bonusCowDescriptionText;
	[SerializeField] private Image _bonusCowImage;
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
		_bonusCowImage.sprite = _player.BonusCow.Sprite;
		_bonusCowImage.enabled = true;

		_player.ScoreUpdated.AddListener(UpdateScore);
		GlobalEvents.CowTipped.AddListener(FlipBonusCow);
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

	private void FlipBonusCow(Cow cow, Player player)
	{
		if (player == null) return;
		if(cow == null) return;

		if(cow == _player.BonusCow)
			_bonusCowImage.rectTransform.localScale = new Vector3(1, -1, 1);
	}
	#endregion
}
