using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartButton()
    {
        GameManager.Instance.StartGame();
        //SceneManager.LoadScene("Scene_CharacterCreation");
    }
    public void OnExitButton() {
        Application.Quit();
    }
}
