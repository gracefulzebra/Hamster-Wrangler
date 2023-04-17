using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CurrencyManager : MonoBehaviour
{
    private static int currency;
    [SerializeField] private int hamsterCost = 100;
    [SerializeField] public int blowerCost, mowerCost, lighterCost, zapperCost, rakeCost, repairCost;
    [SerializeField] int valueDividedByPrice;

    public string[] shopItems;
    public int[] itemCosts;

    private void Start()
    {
        shopItems = new string[] { "LeafBlower", "BugZapper", "Lighter", "LawnMower", "Rake"};
        itemCosts = new int[] { blowerCost, zapperCost, lighterCost, mowerCost, rakeCost};
    }

    /// <summary>
    /// Increments currency by a value, can pass negative values to reduce it
    /// </summary>
    /// <param name="amount"></param>
    public void IncrementCurrency(int amount)
    {
        currency += amount;
        GameManager.instance.uiManager.UpdateUIOnHamsterDeath();
        UpdateCurrencyDisplay();
    }

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
                //Debug.Log("Invalid itemID");
                return false;
        }
    }

    public void ChangeCurrencyTut(string itemID)
    {
        switch (itemID)
        {
            case "LawnMower":
                currency -= mowerCost;
                break;
            case "Lighter":
                currency -= lighterCost;
                break;
        }
        UpdateCurrencyDisplay();
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

    public void SellItem(string itemID)
    {
        switch (itemID)
        {
            case "LeafBlower":
                currency += blowerCost / valueDividedByPrice;
              break;

            case "BugZapper":
                currency += zapperCost / valueDividedByPrice;
                break;

            case "LawnMower":
                currency += mowerCost / valueDividedByPrice;
                break;

            case "Lighter":
                currency += lighterCost / valueDividedByPrice;
                break;

            case "Rake":
                currency += rakeCost / valueDividedByPrice;
                break;
        }
        UpdateCurrencyDisplay();
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

    public bool CheckValidPurchase(int cost)
    {
        return currency - cost >= 0;
    }

    private void PurchaseItem(int itemCost)
    {
        currency -= itemCost;
        UpdateCurrencyDisplay();
        GameManager.instance.uiManager.UpdateUIOnPurchase();
    }

    public void UpdateCurrency(int blowerCount, int zapperCount, int lighterCount, int mowerCount, int rakeCount, int environmentalCount)
    {
        int total = (blowerCount + zapperCount + lighterCount + mowerCount + rakeCount + environmentalCount) - 1;
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
        GameManager.instance.uiManager.UpdateUIOnHamsterDeath();
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
/*
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
  */