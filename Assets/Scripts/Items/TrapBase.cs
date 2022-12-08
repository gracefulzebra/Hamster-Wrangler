using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapBase : MonoBehaviour
{
    public float repairCooldown;
    public float repairCooldownMax;
    public float timer;
    public float timerMax;
    public bool finishedCooldown;
    protected string itemID;
    protected bool itemBroken;
    protected bool repairItem;
    public float force;
    GameObject gameManagerObject;
    GameManager gameManager;
    [SerializeField] Slider cooldownSlider;

    private void Awake()
    {
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        repairItem = false;
        cooldownSlider.maxValue = timerMax;
    }

    private void Update()
    {
        repairCooldown += Time.deltaTime;

        if (repairCooldown > 10f)
        {
            //  RepairItem();
            repairCooldown = 0f;
        }
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

    // read in items maxhealth, the logic in this is flawed
    public void RepairItem()
    {
        // gameManager.currencyManager.RepairItemCost();
         if (itemBroken)
         {
            itemBroken = false;
            StartCoroutine(ItemRepair());
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
}

