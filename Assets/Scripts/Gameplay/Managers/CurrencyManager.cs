using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private static int currency;
    [SerializeField] private int blowerCost, mowerCost, lighterCost, tarCost, rakeCost;
    

    //To be attached to the game manager
    //Updated at the start of every level
    public void InitializeCurrency(int startingCurrency)
    {
        currency = startingCurrency;
    }

    /// <summary>
    /// Checks to see if item can be bought 
    /// with current currency
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns> 
    /// true : If item can be bought then updates currency to show item has been purchased
    /// false : If item cannot be bought returns false and returns.
    /// </returns>
    public bool TryBuy(string itemID)
    {
        switch (itemID)
        {
            case "LeafBlower":
                if (currency - blowerCost < 0) { return false; }
                else { PurchaseItem(blowerCost); return true; }

            case "Tar":
                if (currency - tarCost < 0) { return false; }
                else { PurchaseItem(tarCost); return true; }

            case "Lighter":
                if (currency - lighterCost < 0) { return false; }
                else { PurchaseItem(lighterCost); return true; }

            case "LawnMower":
                if (currency - mowerCost < 0) { return false; }
                else { PurchaseItem(mowerCost); return true; }

            case "Rake":
                if (currency - rakeCost < 0) { return false; }
                else { PurchaseItem(rakeCost); return true; }
        }
        //Only runs if itemID param passes without matching a preset itemID.
        Debug.Log("ItemID not found"); 
        return false;
    }

    private void PurchaseItem(int itemCost)
    {
        currency -= itemCost;
        UpdateCurrencyDisplay();
    }

    private void UpdateCurrencyDisplay()
    {
        GetComponent<GameManager>().DisplayCurrency(currency);
    }
    
}
