using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private static int currency;
    [SerializeField] private int hamsterCost = 100;
    [SerializeField] private int blowerCost, mowerCost, lighterCost, tarCost, rakeCost;

    //To be attached to the game manager
    //Updated at the start of every level
    public void InitializeCurrency(int startingCurrency)
    {
        currency = startingCurrency;
        UpdateCurrencyDisplay();
    }

    /// <summary>
    /// Checks to see if item can be bought 
    /// with current currency
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns> 
    /// true : If item can be bought
    /// false : If item cannot be bought
    /// </returns>
    public bool CheckPrice(string itemID)
    {
        switch (itemID)
        {
            case "LeafBlower":
                return CheckValidPurcase(blowerCost);

            case "Tar":
                return CheckValidPurcase(tarCost);

            case "Lighter":
                return CheckValidPurcase(lighterCost);

            case "LawnMower":
                return CheckValidPurcase(mowerCost);

            case "Rake":
                return CheckValidPurcase(rakeCost);

            default:
                Debug.Log("Invalid itemID");
                return false;
        }
    }

    /// <summary>
    /// Checks to see if item can be bought 
    /// with current currency and then purchases it.
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
                if (CheckValidPurcase(blowerCost)) { PurchaseItem(blowerCost); return true; }
                else { return false; }

            case "Tar":
                if (CheckValidPurcase(tarCost)) { PurchaseItem(tarCost); return true; }
                else { return false; }

            case "Lighter":
                if (CheckValidPurcase(lighterCost)) { PurchaseItem(lighterCost); return true; }
                else { return false; }

            case "LawnMower":
                if (CheckValidPurcase(mowerCost)) { PurchaseItem(mowerCost); return true; }
                else { return false; }

            case "Rake":
                if (CheckValidPurcase(rakeCost)) { PurchaseItem(rakeCost); return true; }
                else { return false; }

            default:
                Debug.Log("Invalid itemID");
                return false;
        }
    }

    private bool CheckValidPurcase(int cost)
    {
        return currency - cost >= 0;
    }

    private void PurchaseItem(int itemCost)
    {
        currency -= itemCost;
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrency(int blowerCount, int tarCount, int lighterCount, int mowerCount, int rakeCount)
    {
        int total = (blowerCount + tarCount + lighterCount + mowerCount + rakeCount) - 1;
        float currencyRewarded = 0;

        switch (total)
        {
            case 0:
                currencyRewarded = hamsterCost;
                break;

            case 1:
                currencyRewarded = hamsterCost * 2;
                break;

            case 2:
                currencyRewarded = hamsterCost * 2.75f;
                break;

            case 3:
                currencyRewarded = hamsterCost * 3.25f;
                break;

            case 4:
                currencyRewarded = hamsterCost * 3.5f;
                break;

            case >5:
                currencyRewarded = (hamsterCost * 3.5f) + ((total - 5) * 0.25f);
                break;
        }

        currency += (int)currencyRewarded;
        UpdateCurrencyDisplay();
    }

    private void UpdateCurrencyDisplay()
    {
        GetComponent<GameManager>().DisplayCurrency(currency);
    }
    
}
