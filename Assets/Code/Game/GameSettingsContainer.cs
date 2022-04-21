using Foundation.Patterns;
using UnityEngine;

public class GameSettingsContainer : Singleton<GameSettingsContainer>
{
	#region Inspector Fields
	[SerializeField] private GameSettings _gameSettings = null;
	#endregion

	#region Properties
	public GameSettings GameSettings => _gameSettings;
	#endregion
}
