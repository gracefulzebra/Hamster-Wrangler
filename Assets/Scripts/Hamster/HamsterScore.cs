using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterScore : MonoBehaviour
{
    ScoreManager scoreManager;
    private int blowerCount, tarCount, lighterCount, mowerCount, rakeCount;

    List<GameObject> interactedList = new List<GameObject>();

    private void Awake()
    {
        scoreManager = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
    }

    public void UpdateInteracts(GameObject item, string itemID)
    {
        if (interactedList.Contains(item))
        {
            interactedList.Add(item);

            switch (itemID)
            {
                case "LeafBlower":
                    blowerCount++;
                    break;

                case "Tar":
                    tarCount++;
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

    public void SendScore()
    {
        scoreManager.UpdateScore(blowerCount, tarCount, lighterCount, mowerCount, rakeCount);
    }


}
