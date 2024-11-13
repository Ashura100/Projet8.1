using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicGame;
    public AudioSource sfxPlayer;

    [SerializeField]
    private AudioClip clickSound, themeSound, gameSound;

    private void Awake()
    {

        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    // Détermine quelle musique jouer en fonction du nom de la scène
    private void PlayMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "New Scene":
                PlayTheme();
                break;
            default:
                StopCurrentSound();
                break;
        }
    }

    //joue le theme de l'ui du menu start
    public void PlayTheme()
    {
        musicGame.clip = themeSound;
        musicGame.Play();
    }

    //joue le thème du jeu
    public void PlayGameTheme()
    {
        musicGame.clip = gameSound;
        musicGame.Play();
    }

    //joue les son click mineur
    public void PlayClickSound()
    {
        sfxPlayer.clip = clickSound;
        sfxPlayer.Play();
    }

    /*public void PlayScreamSound()
    {
        sfxPlayer.clip = screamSound;
        sfxPlayer.Play();
    }

    public void PlaySwordSound()
    {
        sfxPlayer.clip = swordSound;
        sfxPlayer.Play();
    }

    public void PlayDeathSound()
    {
        sfxPlayer.clip = deathSound;
        sfxPlayer.Play();
    }

    public void PlayWinSound()
    {
        sfxPlayer.clip = winSound;
        sfxPlayer.Play();
    }

    //joue le son de défaite
    public void PlayGameOverSound()
    {
        sfxPlayer.clip = gameOverSound;
        sfxPlayer.Play();
    }*/

    //arrête les sons
    public void StopCurrentSound()
    {
        musicGame.Stop();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
