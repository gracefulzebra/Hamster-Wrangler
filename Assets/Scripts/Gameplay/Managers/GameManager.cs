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

    //Manager references
    ScoreManager scoreManager; //Not currently in use in GameManager
    CurrencyManager currencyManager; //Not currently in use in GameManager
    UIManager uiManager;
    WaveManager waveManager; //Not currently in use in GameManager

    private void Awake()
    {
        InitialiseSystems();
    }

    private void InitialiseSystems()
    {
        scoreManager = GetComponent<ScoreManager>();
        currencyManager = GetComponent<CurrencyManager>();
        uiManager = GetComponent<UIManager>();
        waveManager = GetComponent<WaveManager>();
    }

    public void StartWave()
    {
        //Called from UI button to start wave
    }

   
    private void Update()
    {
        
        //See if we can move this code

        // allows only one item to be placed down at once
        GameObject unplacedItem = GameObject.FindGameObjectWithTag("Unplaced Item");
            
            if (unplacedItem != null)
            {
                holdingItem = true;
                //  placementConfirmation.transform.position = x.transform.position;
            }
            else
            { 
                holdingItem = false;
            }
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

     
}
