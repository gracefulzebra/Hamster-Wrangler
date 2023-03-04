using UnityEngine;
using UnityEngine.UI;

public class TrapBase : MonoBehaviour
{
    [Header("Fuel System")]
    public float fuelUsage;
    protected float currentFuel;
    public float maxFuel;
    protected bool hasFuel;
    [SerializeField] Slider fuelSlider;
    [SerializeField] protected GameObject refuelSymbol;

    [Header("Generic Values")]
    public string itemID;
    [SerializeField] protected int damage = 10;
    float timer;
    protected bool onPlacement;

    [Header("Trap ID")]
    protected string rakeID;
    protected string lighterID;
    protected string zapperID;
    protected string mowerID;
    protected string blowerID;

    [Header("Trap Activation")]
    public bool activateTrap;
    protected bool canUseTrap;


    public void Awake()
    {

        rakeID = "Rake";
        lighterID = "Lighter";
        zapperID = "BugZapper";
        mowerID = "LawnMower";
        blowerID = "LeafBlower";

        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
            currentFuel = maxFuel;
            hasFuel = true;
            SliderUpdate();
        }
        canUseTrap = true;
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

        // this fucntion is called in snaptogrid 
    public void ActivateTrap()
    {
       if (canUseTrap)
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

            if (timer > 0.5f)
            {
            currentFuel -= fuelUsage/2;
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
}

