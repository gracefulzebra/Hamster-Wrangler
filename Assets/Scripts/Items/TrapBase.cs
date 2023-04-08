using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TrapBase : MonoBehaviour
{
    [Header("Fuel System")]
    protected float currentFuel;
    public float maxFuel;
    protected bool hasFuel;
    public float fuelUsage;


   public  int chargeCount;
    public bool rechargeFuel;
    public int rechargeDuration;
    public float timeTrapActivePerCharge;
    public bool startCooldown;
    [SerializeField] GameObject[] chargeCountSymbols;




    [SerializeField] Slider fuelSlider;
    [SerializeField] protected GameObject refuelSymbol;

    [Header("Generic Values")]
    public string itemID;
    [SerializeField] protected int damage = 10;
    public float timer;
    protected bool onPlacement;

    [Header("Trap Activation")]
    public bool activateTrap;
    protected bool canUseTrap;


    protected int trapInteractCounter = 0;


    // timer needs to be changed 
    protected void UpdateFuel()
    {
        // overall bool to make sure this doesnt run forever
        if (!startCooldown)
            return;

        // timer keeps track of time
            timer += Time.deltaTime;


        // updates sldier based on time
        if (fuelSlider != null)
        {
            fuelSlider.value = timer;
        }

        // if trap isnt out of fuel
        if (!rechargeFuel)
        {
            timer += Time.deltaTime;
            fuelSlider.maxValue = timeTrapActivePerCharge;
            if (timer >= timeTrapActivePerCharge)
                {
                    timer = 0;
                // trap needs to be recharged
                    rechargeFuel = true;
                // trap no longer active
                    activateTrap = false;
                }
        }
        // if traps needs to be refuels
        else
        {
            fuelSlider.maxValue = rechargeDuration;

            if (timer >= rechargeDuration)
            {             
                timer = 0;
                // trap no longer needs to be fuel
                rechargeFuel = false;
                // trap can be used
                canUseTrap = true;
                // removes 1 charge from trap ui
           //     chargeCountSymbols[chargeCount].SetActive(false);
                // removes 1 charge from trap
                chargeCount--;
                // this whole function returns
                startCooldown = false;               
            }
        }       
    }

    public void Awake()
    {

        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
            currentFuel = maxFuel;
            hasFuel = true;
            SliderUpdate();
        }
        canUseTrap = true;
    }

    #region Fuel

    public void SliderUpdate()
    {
        if (fuelSlider != null)
        {
            fuelSlider.value = currentFuel;
        }
    }

    public void UseFuel()
    {
        if (hasFuel)
        {
            if (currentFuel <= 0)
            {
                hasFuel = false;
            }
            timer += Time.deltaTime;

            // every half a second, fuel is removed from current fuel whioch updates the slider - so the step size isnt so big
            if (timer > 0.5f)
            {
                currentFuel -= fuelUsage/ 2;
                timer = 0;
            }
        }
    }

    public void RefuelTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            currentFuel = maxFuel;
            hasFuel = true;
            refuelSymbol.SetActive(false);
            SliderUpdate();
        }
    }

    #endregion

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
        {
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID, trapInteractCounter);
        }      
    }

        // this fucntion is called in snaptogrid 
    public void ActivateTrap()
    {
       if (canUseTrap)
       {
            if (activateTrap == false)
            {
                //starts cycle of activating and recharging
                startCooldown = true;
                // sets trap to beign active
                activateTrap = true;
            }
            else
            {
                activateTrap = false;
            }
       }    
    }




    private List<GameObject> interactedList = new List<GameObject>();
    public void IncrementTrapInteracts(GameObject interactingObject)
    {
        if (!interactedList.Contains(interactingObject))
        {
            interactedList.Add(interactingObject);
            //print("interacts incremented");
            trapInteractCounter++;
        }    
    }

    protected void ResetInteracts()
    {
        trapInteractCounter = 0;
    }
}

