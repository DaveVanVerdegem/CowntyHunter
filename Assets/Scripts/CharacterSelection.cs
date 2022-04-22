using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public int selectedCharacter = 0;

	public void NextCharacter(InputAction.CallbackContext ctx)
	{
        if (ctx.phase == InputActionPhase.Started)
        {
			characters[selectedCharacter].SetActive(false);
            selectedCharacter = (selectedCharacter + 1) % characters.Length;
            characters[selectedCharacter].SetActive(true);
		}

	}

	public void PreviousCharacter(InputAction.CallbackContext ctx)
	{

        if (ctx.phase == InputActionPhase.Started)
        {
			characters[selectedCharacter].SetActive(false);
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter += characters.Length;
            }
            characters[selectedCharacter].SetActive(true);
		}

	}

	public void StartGame()
	{
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}
}
