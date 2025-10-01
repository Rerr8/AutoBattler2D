using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class EndScreenUI : MonoBehaviour
{
    [Tooltip("Текстовое поле для отображения результата игры")]
    public TextMeshProUGUI  resultText;

    public Image backgroundImage;

    public Button restartButton;
    public Button exitGameButton;

    public void Start()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        exitGameButton.onClick.AddListener(OnExitGameClicked);

        if (GameManager.Instance != null)
        {

            if (GameManager.Instance.wins >= 5)
            {
                backgroundImage.color = new Color(0.6556604f, 1f, 0.6556604f, 1f);
                resultText.color = Color.green;
                resultText.text = "ГЦ, Игра пройдена!";
            }
            else
            {
                backgroundImage.color = new Color(1f, 0.4381282f, 0.3915094f, 1f);
                resultText.color = Color.red;
                resultText.text = "Вам не повезло, какая жалость, вы проиграли!";
            }
        }
        else
        {
            resultText.text = "Игра окончена";
        }
    }

    private void OnRestartClicked()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        SceneManager.LoadScene("Scene_MainMenu");
    }

    private void OnExitGameClicked()
    {
        Application.Quit();
    }
}
