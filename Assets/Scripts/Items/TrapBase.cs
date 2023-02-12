using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrapBase : MonoBehaviour
{

    public float fuelUsage;
    public float currentFuel;
    public float maxFuel;
    public bool hasFuel;
    public float timer;
    public float timerMax;
    public bool finishedCooldown;
    public string itemID;
    protected bool itemBroken;
    protected bool repairItem;
    public float force;
    protected GameObject gameManagerObject;
    protected GameManager gameManager;
    [SerializeField] Slider cooldownSlider;

    public void Awake()
    {
        repairItem = false;
        if (cooldownSlider != null)
        {
            cooldownSlider.maxValue = timerMax;
        }
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
    }

    // read in items health
    public void Durability(int itemHealth)
    {
        if (itemHealth <= 0 && itemBroken == false)
        {
            ItemBreak();
        }
    }

    void ItemBreak()
    {
        itemBroken = true;
    }

    public void RepairItem()
    {
        if (itemBroken)
        {
            if (gameManager.currencyManager.RepairItemCost() == true)
            {
                itemBroken = false;
                StartCoroutine(ItemRepair());
            }
        }
    }

    public void SliderUpdate()
    {
        cooldownSlider.value = timer;
    }

    WaitForSeconds delay = new WaitForSeconds(.5f);
    IEnumerator ItemRepair()
    {
        repairItem = true;
        yield return delay;
        repairItem = false;
    }

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID);
    }


    void UseFuel()
    {
       if (hasFuel)
       {

            if (currentFuel <= 0)
            {
                hasFuel = false;
            }
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            currentFuel -= fuelUsage;
            timer = 0;
        }
       }
    }

    void RefillFuel()
    {
        if (gameManager.currencyManager.RepairItemCost() == true)
        {
            hasFuel = false;
            StartCoroutine(ItemRepair());
        }
    }
}

