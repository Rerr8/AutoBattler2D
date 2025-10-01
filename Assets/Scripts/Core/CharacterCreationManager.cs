using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationManager : MonoBehaviour
{
    public Button startBattleButton;
    public TalentSO talentChosen;


    public CanvasGroup warningCanvasGroup;
    public float fadeDuration = 1.5f;
    public string warningMessage = "Сначала выберите класс!";


    public void SelectTalent(TalentSO talentSO)
    {
        talentChosen = talentSO;
    }
    
    public void OnStartBattleClicked()
    {
        if (talentChosen != null)
        {
            PlayerData player = new PlayerData();
            GameManager.Instance.player = player;
            player.UpgradeTalent(talentChosen);
            player.playerSprite = talentChosen.characterSprite;
            GameManager.Instance.StartBattlePhase();
        }
        else
        {
            ShowWarningMessage();
        }
    }

    private void ShowWarningMessage()
    {
        if (warningCanvasGroup.alpha > 0) return;

        StopAllCoroutines();
        StartCoroutine(FadeWarning());
    }
    
    private System.Collections.IEnumerator FadeWarning()
    {
        warningCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(1.5f);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            warningCanvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }

        warningCanvasGroup.alpha = 0;
    }
    
}
