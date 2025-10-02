using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
public class TalentSlot : MonoBehaviour
{
    public TalentSO talentSO;
    public List<TalentSlot> prerequisiteTalentSlots;

    public int currentLevel;
    public bool isUnlocked;

    public Button talentButton;
    public Image talentIcon;
    public TMP_Text talentLevelText;
    public TMP_Text talentDescription;
    public GameObject descriptionPanel;

    public static event Action<TalentSlot> OnTalentPointSpent;
    public static event Action<TalentSlot> OnTalentMaxed;

    private void OnValidate()
    {
        if (talentSO != null)
        {
            if (talentDescription != null)
            {
                talentDescription.text = talentSO.description;
            }
            if (talentLevelText != null)
            {
                UpdateUI();
            }
        }
    }
    
    public void SetState(int level)
    {
        currentLevel = level;
        if (currentLevel >= talentSO.maxLevel)
        {
            OnTalentMaxed?.Invoke(this);
        }
        UpdateUI();
    }

    public void TryUpgradeTalent()
    {
        if (isUnlocked && currentLevel < talentSO.maxLevel)
        {
            if (GameManager.Instance.player == null) return;
            GameManager.Instance.player.UpgradeTalent(talentSO);
            currentLevel++;
            OnTalentPointSpent?.Invoke(this);

            if (currentLevel >= talentSO.maxLevel)
            {
                OnTalentMaxed?.Invoke(this);
            }
            UpdateUI();
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    public bool CanUnlockTalent()
    {
        foreach (TalentSlot slot in prerequisiteTalentSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.talentSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateUI()
    {
        talentIcon.sprite = talentSO.talentIcon;

        if (isUnlocked)
        {
            talentButton.interactable = true;
            talentLevelText.text = currentLevel.ToString() + "/" + talentSO.maxLevel.ToString();
            talentIcon.color = Color.white;
        }
        else
        {
            talentButton.interactable = false;
            talentLevelText.text = "Locked";
            talentIcon.color = Color.grey;
        }
    }
}
