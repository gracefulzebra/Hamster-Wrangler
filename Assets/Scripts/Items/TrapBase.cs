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
    protected bool rechargeFuel;
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
    protected bool audioOn;

    protected bool synergyDisplay = false;
    [SerializeField] protected GameObject comboDisplayPrefab;

    protected int trapInteractCounter = 0;

    protected Color customColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    public void Awake()
    {
        chargeCount = maxChargeCount;
        canUseTrap = true;
    }

    #region Fuel

    public void RefuelTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            GameManager.instance.audioManager.RefuelAudio();
            canUseTrap = true;
            chargeCount = maxChargeCount;
            refuelSymbol.SetActive(false);
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

