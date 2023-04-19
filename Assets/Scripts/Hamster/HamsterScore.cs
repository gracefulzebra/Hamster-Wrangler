using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterScore : MonoBehaviour
{
    private int blowerCount, zapperCount, lighterCount, mowerCount, rakeCount, environmentalCount;

    private int comboCounter = 0;
    private int comboBonusScore;

    public GameObject comboDisplayPrefab;

    private List<GameObject> interactedList = new List<GameObject>();

    public void UpdateInteracts(GameObject item, string itemID, int trapInteractCounter)
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

                case "Environmental":
                    environmentalCount++;
                    break;
            }

            comboCounter += trapInteractCounter + 1;

            if (trapInteractCounter > 0)
            {
                UpdateComboScore(trapInteractCounter);
            }
        } 
    }

    public void UpdateComboScore(int trapInteractCounter)
    {
        int comboScoreMultiplier = GameManager.instance.scoreManager.comboScore;
        comboBonusScore += comboScoreMultiplier * trapInteractCounter;
    }

    public void SendData()
    {
        GameManager.instance.scoreManager.UpdateScore(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount, comboBonusScore);
        GameManager.instance.currencyManager.UpdateCurrency(blowerCount, zapperCount, lighterCount, mowerCount, rakeCount, environmentalCount);
        GameManager.instance.waveManager.HamstersRemaining();

        GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        temp.GetComponent<ComboDisplay>().SetComboText("x" + comboCounter);
    }

    public void UpdateWaveManager()
    {
        GameManager.instance.waveManager.HamstersRemaining();
    }
}
