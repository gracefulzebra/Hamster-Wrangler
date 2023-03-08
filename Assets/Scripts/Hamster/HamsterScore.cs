using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterScore : MonoBehaviour
{
    private GameManager manager;
    private int blowerCount, zapperCount, lighterCount, mowerCount, rakeCount;

    private List<GameObject> interactedList = new List<GameObject>();

    private void Awake()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void UpdateInteracts(GameObject item, string itemID)
    {
        
        if (!interactedList.Contains(item))
        {           
            interactedList.Add(item);

            switch (itemID)
            {
                case "LeafBlower":
                    blowerCount++;
                    break;

                case "BugZapper":
                    zapperCount++;
                    break;

                case "Lighter":
                    lighterCount++;
                    break;

                case "LawnMower":
                    mowerCount++;
                    break;

                case "Rake":
                    rakeCount++;
                    break;
            }
        }  
    }

    public void SendData()
    {
        manager.scoreManager.UpdateScore(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount);
        manager.currencyManager.UpdateCurrency(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount);
        manager.waveManager.HamstersRemaining();
    }

    public void UpdateWaveManager()
    {
        manager.waveManager.HamstersRemaining();
    }


}
