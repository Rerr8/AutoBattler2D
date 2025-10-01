using UnityEngine;
using TMPro;

public class TalentTreeManager : MonoBehaviour
{
    public TalentSlot[] talentSlots;
    public TMP_Text pointsText;
    public int availablePoints;
    public int spentPoints;

    private void OnEnable()
    {
        TalentSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        TalentSlot.OnAbilityMaxed += HandleTalentMaxed;
    }

    private void OnDisable()
    {
        TalentSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        TalentSlot.OnAbilityMaxed -= HandleTalentMaxed; 
    }

    // private void Start()
    // {
    //     foreach (TalentSlot slot in talentSlots)
    //     {
    //         slot.talentButton.onClick.AddListener(() => CheckAvailablePoints(slot));

    //     }
    //     UpdateAbilityPoints(0);
    // }

    private void Start()
    {
        foreach (TalentSlot slot in talentSlots)
        {
            slot.talentButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }
        InitializeFromPlayerData();
    }

    public void InitializeFromPlayerData()
    {
        PlayerData playerData = GameManager.Instance.player;
        if (playerData == null) return;

        //pointsText.text = "Available Points: " + playerData.availableTalentPoints;
        UpdateAbilityPoints(playerData.availableTalentPoints);

        foreach (TalentSlot slot in talentSlots)
        {
            int currentLevel = 0;
            playerData.unlockedTalentsLevels.TryGetValue(slot.talentSO, out currentLevel);
            slot.SetState(currentLevel);
        }

    }

    // private void TryUpgradeTalentInSlot(TalentSlot slot)
    // {
    //     PlayerData playerData = GameManager.Instance.player;
    //     if (playerData.availableTalentsPoints <= 0) return;
    //     if (!slot.isUnlocked) return;

    //     playerData.UpgradeTalent(slot.talentSO);

    //     // После изменения данных, полностью обновляем UI
    //     //InitializeFromPlayerData();
    // }

    private void CheckAvailablePoints(TalentSlot slot)
    {
        if (availablePoints > 0)
        {
            slot.TryUpgradeTalent();
        }
    }

    private void HandleAbilityPointSpent(TalentSlot talentSlot)
    {
        UpdateAbilityPoints(-1);
    }

    private void HandleTalentMaxed(TalentSlot talentSlot)
    {
        foreach (TalentSlot slot in talentSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockTalent())
            {
                slot.Unlock();
            }
        }
    }

    public void UpdateAbilityPoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = "Available Points: " + availablePoints;
    }
}
