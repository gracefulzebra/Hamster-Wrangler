using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool holdingItem;
    public GameObject placementConfirmation;
    [SerializeField] GameObject scoreDisplay;
    [SerializeField] GameObject currencyDisplay;
    public float health;

    private void Awake()
    {
        holdingItem = false;
    }

    private void Update()
    {
      
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

    //To be replaced with a UI manager
    public void DisplayScore(int score)
    {
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void DisplayCurrency(int currency)
    {
        currencyDisplay.GetComponent<TextMeshProUGUI>().text = "Currency : " + currency;
    }

    // link up managers, get audio, get aniamtions, quality of life, placement working smoothing, durablity, menu/ ui, 
}
