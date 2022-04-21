using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private string _gameScene = "";
	#endregion

	#region Methods
	public void LoadNextScene(InputAction.CallbackContext callback)
	{
		if (!callback.performed) return;

		SceneManager.LoadScene(_gameScene);
	}
	#endregion
}
