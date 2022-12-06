using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


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

    public bool holdingItem = false;
    public GameObject placementConfirmation;
    public float health;

    [SerializeField] private int startingCurrency;

    //Manager references
    public ScoreManager scoreManager { get; private set; } //Not currently in use in GameManager
    public CurrencyManager currencyManager { get; private set; } //Not currently in use in GameManager
    public UIManager uiManager { get; private set; }
    public WaveManager waveManager { get; private set; }
    public AnimationManager animationManager { get; private set; }
    public AudioManager audioManager { get; private set; }


    private void Awake()
    {
        InitialiseSystems();
        //   holdingItem = false;
    }

    private void InitialiseSystems()
    {
        uiManager = GetComponent<UIManager>();

        scoreManager = GetComponent<ScoreManager>();
        scoreManager.InitializeScore();

        currencyManager = GetComponent<CurrencyManager>();
        currencyManager.InitializeCurrency(startingCurrency);

        waveManager = GetComponent<WaveManager>();

        animationManager = GetComponent<AnimationManager>();

        audioManager = GetComponent<AudioManager>();
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
}
