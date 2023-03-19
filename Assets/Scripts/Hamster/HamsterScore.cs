using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterScore : MonoBehaviour
{
    private int blowerCount, zapperCount, lighterCount, mowerCount, rakeCount;

    private int comboBonusScore;

    private List<GameObject> interactedList = new List<GameObject>();

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

    public void UpdateComboScore(int trapInteractCounter)
    {
        
    }

    public void SendData()
    {
        GameManager.instance.scoreManager.UpdateScore(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount, comboBonusScore);
        GameManager.instance.currencyManager.UpdateCurrency(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount);
        GameManager.instance.waveManager.HamstersRemaining();
    }

    public void UpdateWaveManager()
    {
        GameManager.instance.waveManager.HamstersRemaining();
    }


}
