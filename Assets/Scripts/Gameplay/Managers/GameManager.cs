using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //To-do
    // link up managers
    //get audio
    //get aniamtions
    //quality of life
    //placement working smoothing
    //durablity
    //menu / ui

    public static GameManager instance;

    public bool holdingItem = false;
    public GameObject placementConfirmation;
    private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int startingCurrency;
    public int scoreFor3Star;
    public static int finalScore;
    public Vector3 globalTrapRotation;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    public const string MUSIC_KEY = "Music";
    public const string SFX_KEY = "SFX";


    //Manager references
    public ScoreManager scoreManager { get; private set; }
    public CurrencyManager currencyManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public WaveManager waveManager { get; private set; }
    public AnimationManager animationManager { get; private set; }
    public AudioManager audioManager { get; private set; }
    public VFXManager vfxManager { get; private set; }


    public static int level;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }

        InitialiseSystems();

        //   holdingItem = false;
    }

    void LoadVolume()
    {
       float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
       float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

       audioMixer.SetFloat(MUSIC_KEY, Mathf.Log10(musicVolume) * 20);
       audioMixer.SetFloat(SFX_KEY, Mathf.Log10(sfxVolume) * 20);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MIXER_MUSIC, audioManager.musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MIXER_SFX, audioManager.sfxSlider.value);
    }

    private void InitialiseSystems()
    {
        health = maxHealth;
        //Display health (has to happen after UImanager is assigned)

        uiManager = GetComponent<UIManager>();
        uiManager.DisplayHealth(health);

        scoreManager = GetComponent<ScoreManager>();
        scoreManager.InitializeScore();

        currencyManager = GetComponent<CurrencyManager>();
        currencyManager.InitializeCurrency(startingCurrency);

        waveManager = GetComponent<WaveManager>();

        animationManager = GetComponent<AnimationManager>();

        audioManager = GetComponent<AudioManager>();

        vfxManager = GetComponent<VFXManager>();

        DisplayHealth(health);

        LoadVolume();

        if (SceneManager.GetActiveScene().name == "TutorialLevel")
            level = 1;
        if (SceneManager.GetActiveScene().name == "FinnLevel")
            level = 2;

        if (SceneManager.GetActiveScene().name == "MainMenu")
            MainMenuStar();
    }

    public void StartWave()
    {
        StartCoroutine(waveManager.StartWave()); //To be hooked up to UI button. Fully functional and ready to be tweaked. 
    }

    //UIManager communication
    public void DisplayScore(int score)
    {
        uiManager.DisplayScore(score);
    }

    public void DisplayCurrency(int currency)
    {
        uiManager.DisplayCurrency(currency);
    }

    public void DisplayHealth(int health)
    {
        uiManager.DisplayHealth(health);
    }

    public void DisplayWaves(int waves, int maxWaves)
    {
        uiManager.DisplayWaves(waves, maxWaves);
    }

    public void UpdateVolume(float volume)
    {
        audioManager.SetVolume(volume);
    }

    /// <summary>
    /// checks if any objects with tag is in scene, if so they 
    /// cannot use the shop until item no longer has tag
    /// </summary>
     public void CheckIfItemHeld()
     {
        // allows only one item to be placed down at once
        GameObject unplacedItem = GameObject.FindGameObjectWithTag("Unplaced Item");
        
        if (unplacedItem != null)
        {
            holdingItem = true; 
        }
        else
        {
            holdingItem = false;
        }
     }

    public void LoseHealth()
    {
        health -= damage;
        CheckIfLoseGame();
        DisplayHealth(health);
    }

    public void WinGame()
    {
        finalScore = scoreManager.FinalizeScore(health, maxHealth);    
        uiManager.DisplayFinalScore(finalScore);
    }

    void MainMenuStar()
    {
        int oneStar = (scoreFor3Star / 3);
        int twoStar = ((scoreFor3Star / 3) * 2);

        if (finalScore == 0)
        {
            uiManager.MenuStars(0);
        }
        else if (finalScore >= 0 && finalScore <= oneStar)
        {
            uiManager.MenuStars(1);
        }
        else if (finalScore > oneStar && finalScore <= twoStar)
        {
            uiManager.MenuStars(2);
        }
        else if (finalScore > twoStar)
        {
            uiManager.MenuStars(3);
        }
    }

    public void CheckIfLoseGame()
    {
        if (health <= 0)
        {
            uiManager.GameOverScreen();
        }
    }
}
