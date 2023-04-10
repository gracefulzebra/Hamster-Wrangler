using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TrapBase : MonoBehaviour
{
    [Header("Fuel System")]
    protected int chargeCount;
    [SerializeField] int maxChargeCount;
    bool rechargeFuel;
    [SerializeField] protected int rechargeDuration;
    [SerializeField] protected float timeTrapActivePerCharge;
    bool startCooldown;
    [SerializeField] GameObject[] chargeCountSymbols;

    [SerializeField] protected Slider fuelSlider;
    [SerializeField] protected GameObject refuelSymbol;

    [Header("Generic Values")]
    public string itemID;
    [SerializeField] protected int damage = 10;
    protected float useFuelTimer;
    protected float refuelTimer;

    protected bool onPlacement;

    [Header("Trap Activation")]
    public bool activateTrap;
    protected bool canUseTrap;

    protected int trapInteractCounter = 0;

    GameObject background;
    GameObject fillArea;
    GameObject fill;

    public void Awake()
    {
        chargeCount = maxChargeCount;
        if (fuelSlider != null)
        {
            fuelSlider.maxValue = timeTrapActivePerCharge;
            fuelSlider.direction = Slider.Direction.RightToLeft;
            fuelSlider.value = 0;
            background = fuelSlider.transform.Find("Background").gameObject;
            fillArea = fuelSlider.transform.Find("Fill Area").gameObject;
            fill = fillArea.transform.Find("Fill").gameObject;
        }
        canUseTrap = true;
    }

    #region Fuel

    protected void UseFuel()
    {
        if (!rechargeFuel)
        {
            fuelSlider.value = useFuelTimer;
            useFuelTimer += Time.deltaTime;
            refuelTimer = 0;

            if (useFuelTimer >= timeTrapActivePerCharge)
            {
                // removes 1 charge from trap ui
                chargeCountSymbols[chargeCount - 1].SetActive(false);
                // removes 1 charge from trap
                chargeCount--;

                // trap no longer active
                activateTrap = false;
                rechargeFuel = true;
            }
        }
    }

    protected void RechargeFuel()
    {
        if (rechargeFuel)
        {
            refuelTimer += Time.deltaTime;
            fuelSlider.value = refuelTimer;
            useFuelTimer = 0;

            if (refuelTimer >= rechargeDuration)
            {
                // trap can be used
                canUseTrap = true;
                // trap no longer needs to be fueled
                rechargeFuel = false;
            }
        }
    }

    protected void ChangeSliderColour()
    {
        if (!rechargeFuel)
        {
            background.GetComponent<Image>().color = Color.green;      
            fill.GetComponent<Image>().color = Color.red;

            fuelSlider.value = 0;
            fuelSlider.maxValue = timeTrapActivePerCharge;
            fuelSlider.direction = Slider.Direction.RightToLeft;
        }
        else
        {
            background.GetComponent<Image>().color = Color.red;
            fill.GetComponent<Image>().color = Color.green;

            fuelSlider.value = 0;
            fuelSlider.maxValue = rechargeDuration;
            fuelSlider.direction = Slider.Direction.LeftToRight;
        }
    }
    public void RefuelTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            for(int i =0; i < chargeCountSymbols.Length; i++)
            {
                chargeCountSymbols[i].SetActive(enabled);
            }

            canUseTrap = true;
            chargeCount = maxChargeCount;
            refuelSymbol.SetActive(false);
            fuelSlider.value = 0;
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

