using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text;
using TMPro;
public class BattleManager : MonoBehaviour
{
    public TMP_Text playerStatsText;
    public TMP_Text enemyStatsText;
    public TMP_Text battleLogText;

    public Character player;
    public Character enemy;

    public SpriteRenderer playerRenderer;
    public SpriteRenderer enemyRenderer;

    [Tooltip("Список всех возможных врагов, которые могут появиться в бою")]
    public List<EnemySO> allEnemies;

    public Button takeWeaponButton;
    public Button rejectWeaponButton;
    public Button continueButton;

    [Header("Панели UI")]
    public GameObject talentTreePanel;
    public GameObject postBattleUI;
    public Image newWeaponIcon;

    public GameObject playerHitMark;
    public GameObject enemyHitMark;
    public List<Sprite> hitMarkSprites;
    public List<Sprite> missMarkSprites;

    private const int MAX_TURNS = 100;

    private StringBuilder battleLog = new StringBuilder();
    private void AddToLog(string message) { battleLog.AppendLine(message); }

    private void Start()
    {
        postBattleUI.SetActive(false);
        newWeaponIcon.gameObject.SetActive(false);
        talentTreePanel.SetActive(false);

        PlayerData playerData = GameManager.Instance.player;
        player = new Character(playerData);
        playerRenderer.sprite = player.characterSprite;

        EnemySO enemySO = GetRandomEnemy();
        enemy = new Character(enemySO);
        enemyRenderer.sprite = enemy.characterSprite;

        UpdateUI();
        StartCoroutine(BattleCoroutine());
    }

    private System.Collections.IEnumerator BattleCoroutine()
    {
        int currentTurn = 1;
        Character first = (player.agility >= enemy.agility) ? player : enemy;
        Character second = (first == player) ? enemy : player;

        AddToLog($"--- Бой начинается! {player.name} против {enemy.name} ---");
        AddToLog($"{first.name} ходит первым благодаря высокой ловкости!\n");
        yield return new WaitForSeconds(4.5f);

        while (player.currentHealth > 0 && enemy.currentHealth > 0 && currentTurn < MAX_TURNS)
        {
            yield return new WaitForSeconds(1.5f);
            Attack(first, second);
            UpdateUI();
            yield return new WaitForSeconds(2.5f);
            if (second.currentHealth <= 0) break;

            Attack(second, first);
            UpdateUI();
            yield return new WaitForSeconds(1.5f);

            currentTurn++;
        }

        EndBattle();
    }

    private void UpdateUI()
    {
        playerStatsText.text = $"<b>Игрок</b>\n" +
                               $"HP: {player.currentHealth} / {player.maxHealth}\n" +
                               $"Сила: {player.strength}\n" +
                               $"Ловкость: {player.agility}\n" +
                               $"Выносливость: {player.stamina}\n" +
                               $"Оружие: {player.weapon.weaponName}";

        enemyStatsText.text = $"<b>{enemy.name}</b>\n" +
                              $"HP: {enemy.currentHealth} / {enemy.maxHealth}\n" +
                              $"Сила: {enemy.strength}\n" +
                              $"Ловкость: {enemy.agility}\n" +
                              $"Выносливость: {enemy.stamina}";
        battleLogText.text = battleLog.ToString();

    }

    public EnemySO GetRandomEnemy()
    {
        if (allEnemies == null || allEnemies.Count == 0)
        {
            Debug.LogError("Список врагов пуст!");
            return null;
        }
        return allEnemies[Random.Range(0, allEnemies.Count)];
    }

    private void Attack(Character attacker, Character defender)
    {
        attacker.turnsTaken++;
        int hitRange = attacker.agility + defender.agility;
        int roll = Random.Range(1, hitRange + 1);
        GameObject targetHitMarkObject = (defender == player) ? playerHitMark : enemyHitMark;
        if (roll <= defender.agility)
        {
            AddToLog($"{attacker.name} промахнулся по {defender.name}!");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.missSound);

            StartCoroutine(ShowStatusEffectCoroutine(targetHitMarkObject, missMarkSprites[Random.Range(0, missMarkSprites.Count)], attacker == enemy));
            return;
        }

        
        int damage = attacker.weapon.baseDamage + attacker.strength;
        foreach (var ability in attacker.abilities) { damage = ability.OnBeforeAttack(attacker, defender, damage);}
        foreach (var ability in defender.abilities) { damage = ability.OnBeforeDamage(defender, attacker, damage);}
        damage = Mathf.Max(0, damage);
        defender.TakeDamage(damage);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);

        StartCoroutine(ShowStatusEffectCoroutine(targetHitMarkObject, hitMarkSprites[Random.Range(0, hitMarkSprites.Count)], attacker == enemy));
        
        AddToLog($"{attacker.name} наносит {defender.name} <color=red>{damage}</color> урона!");
    }

    private void EndBattle()
    {
        if (player.currentHealth > 0)
        {
            AddToLog($"\n<color=green>Вы победили!</color>");
            GameManager.Instance.PlayerWin();
            if (GameManager.Instance.wins >= 5) return;

            battleLogText.gameObject.SetActive(false);
            enemyStatsText.gameObject.SetActive(false);

            if (GameManager.Instance.player.currentLevel < PlayerData.MAX_LEVEL)
            {
                GameManager.Instance.player.LevelUp();
            }

            // // 2. Показываем UI выбора оружия
            if (newWeaponIcon != null && enemy.rewardWeapon.weaponIcon != null)
            {
                newWeaponIcon.sprite = enemy.rewardWeapon.weaponIcon;
                newWeaponIcon.gameObject.SetActive(true);
            }

            postBattleUI.SetActive(true);
            takeWeaponButton.GetComponentInChildren<TMP_Text>().text = $"Заменить на {enemy.rewardWeapon.weaponName} (Урон: {enemy.rewardWeapon.baseDamage})";
            rejectWeaponButton.GetComponentInChildren<TMP_Text>().text = $"Оставить текущее {player.weapon.weaponName} (Урон: {player.weapon.baseDamage})";
        }
        else if (player.currentHealth < 0)
        {
            AddToLog($"\n<color=grey>Вы проиграли...</color>");
            GameManager.Instance.PlayerLose();
        }
        else
        {
            // что делаем если никто друг друга убить не может?
            GameManager.Instance.PlayerLose();
        }
    }

    public void OnContinueClicked()
    {
        talentTreePanel.SetActive(false);
        RestartBattle();
    }
    private void RestartBattle()
    {
        GameManager.Instance.player.RestoreHealth();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTakeWeaponClicked()
    {
        GameManager.Instance.player.currentWeapon = enemy.rewardWeapon;
        postBattleUI.SetActive(false);
        HandlePostWeaponChoice();
    }

    public void OnRejectWeaponClicked()
    {
        postBattleUI.SetActive(false);
        HandlePostWeaponChoice();
    }

    private void HandlePostWeaponChoice()
    {
        PlayerData player = GameManager.Instance.player;
        if (player.availableTalentPoints > 0)
        {
            ShowTalentTree();
        }
        else
        {
            AddToLog("Нет доступных очков. Начинаем следующий бой...");
            RestartBattle();
        }
    }

    private void ShowTalentTree()
    {
        talentTreePanel.SetActive(true);
    }

    private IEnumerator ShowStatusEffectCoroutine(GameObject statusObject, Sprite effectSprite, bool flipX)
    {
        if (statusObject == null || effectSprite == null)
        {
            yield break;
        }
        SpriteRenderer renderer = statusObject.GetComponent<SpriteRenderer>();
        renderer.sprite = effectSprite;
        renderer.flipX = flipX;
        statusObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        statusObject.SetActive(false);
        renderer.flipX = false;
    }

}
