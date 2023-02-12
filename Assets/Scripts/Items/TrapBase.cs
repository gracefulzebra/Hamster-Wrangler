using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrapBase : MonoBehaviour
{

    public float fuelUsage;
    float currentFuel;
    public float maxFuel;
    public bool hasFuel;
    float timer;
    public string itemID;
    [SerializeField] Slider fuelSlider;
    [SerializeField] GameObject refuelSymbol;
    [SerializeField] GameObject useItemSymbol;
    public bool activateTrap;

    public void Awake()
    {
        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
        }
        currentFuel = maxFuel;
        hasFuel = true;
        SliderUpdate();
    }

    public void RefuelTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            currentFuel = maxFuel;
            hasFuel = true;
        }
    }

    public void SliderUpdate()
    {
        if (fuelSlider != null)
        {
            fuelSlider.value = currentFuel;
        }
    }

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID);
    }

    public void ActivateTrap()
    {
        if (activateTrap == false)
        {
            activateTrap = true;
        }
        else
        {
            activateTrap = false;
        }   
    }

   public void UseFuel()
    {
        print(currentFuel);
        if (hasFuel)
        {
            if (currentFuel <= 0)
            {
                hasFuel = false;
            }
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
            currentFuel -= fuelUsage/2;
            timer = 0;
            }
        }
    }

    void RefillFuel()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            hasFuel = true;
            RefuelTrap();
        }
    }
}

