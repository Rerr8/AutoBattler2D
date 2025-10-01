using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip mainMenuMusic;
    public AudioClip battleMusic;

    public AudioClip hitSound;
    public AudioClip missSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene_MainMenu" || scene.name == "Scene_CharacterCreation" || scene.name == "Scene_EndScreen")
        {
            PlayMusic(mainMenuMusic);
        }
        else if (scene.name == "Scene_Battle")
        {
            PlayMusic(battleMusic);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource == null || (musicSource.clip == musicClip && musicSource.isPlaying))
        {
            return;
        }

        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
