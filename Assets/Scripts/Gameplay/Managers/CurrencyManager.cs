using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CurrencyManager : MonoBehaviour
{
    private static int currency;
    [SerializeField] private int hamsterCost = 100;
    [SerializeField] public int blowerCost, mowerCost, lighterCost, zapperCost, rakeCost, repairCost;

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
                return CheckValidPurchase(blowerCost);

            case "BugZapper":
                return CheckValidPurchase(zapperCost);

            case "Lighter":
                return CheckValidPurchase(lighterCost);

            case "LawnMower":
                return CheckValidPurchase(mowerCost);

            case "Rake":
                return CheckValidPurchase(rakeCost);

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
                if (CheckValidPurchase(blowerCost)) {PurchaseItem(blowerCost); return true; }
                else { return false; }

            case "BugZapper":
                if (CheckValidPurchase(zapperCost)) { PurchaseItem(zapperCost); return true; }
                else { return false; }

            case "Lighter":
                if (CheckValidPurchase(lighterCost)) { PurchaseItem(lighterCost); return true; }
                else { return false; }

            case "LawnMower":
                if (CheckValidPurchase(mowerCost)) { PurchaseItem(mowerCost); return true; }
                else { return false; }

            case "Rake":
                if (CheckValidPurchase(rakeCost)) { PurchaseItem(rakeCost); return true; }
                else { return false; }

            default:
                Debug.Log("Invalid itemID");
                return false;
        }
       
    }

    // FIX THIS FUCKER!!! THIS IS JUST 5 IF STATEMENTS IF THEY FIN DOUT ABOUT HIS YOU ARE TOAST
    public void UIOutline()
    {

         if (!CheckValidPurchase(blowerCost)) 
         { 
            GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag("LeafBlower")); 
         }
          
         if (!CheckValidPurchase(zapperCost)) 
         {
            GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag("BugZapper")); 
         }
                 
         if (!CheckValidPurchase(lighterCost)) 
         {
            GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag("Lighter")); 
         }

         if (!CheckValidPurchase(mowerCost)) 
         { 
            GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag("LawnMower")); 
         }

         if (!CheckValidPurchase(rakeCost))
         {
            GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag("Rake"));
         }   
    }


    public bool RepairItemCost()
    {
        if (CheckValidPurchase(repairCost))
        {
            PurchaseItem(repairCost);
            return true;
        }
        else
            return false;
    }

    private bool CheckValidPurchase(int cost)
    {
        return currency - cost >= 0;
    }

    private void PurchaseItem(int itemCost)
    {
        currency -= itemCost;
        UpdateCurrencyDisplay();
        UIOutline();
    }

    public void UpdateCurrency(int blowerCount, int zapperCost, int lighterCount, int mowerCount, int rakeCount)
    {
        int total = (blowerCount + zapperCost + lighterCount + mowerCount + rakeCount) - 1;
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

// not used testign for cannotbuyshopoutline
    public bool CheckPrice(int itemPrice)
    {
        return (itemPrice >= currency);
    }
}
