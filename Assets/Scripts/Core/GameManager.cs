using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerData player;
    
    [Tooltip("Счетчик побед в текущей сессии")]
    public int wins = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        wins = 0;
        SceneManager.LoadScene("Scene_CharacterCreation");
    }

    public void StartBattlePhase()
    {
        SceneManager.LoadScene("Scene_Battle");
    }

    public void PlayerWin()
    {
        wins++;
        if (wins >= 5)
        {
            SceneManager.LoadScene("Scene_EndScreen");
        }
    }

    public void PlayerLose()
    {
        SceneManager.LoadScene("Scene_EndScreen");
    }

}