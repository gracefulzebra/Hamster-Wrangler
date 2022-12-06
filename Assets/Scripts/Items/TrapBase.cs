using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{

   // int maxItemHealth = 2;
  //  int itemHealth;

    public float cooldown;
    public float cooldownFinish;
    public bool finishedCooldown;
    protected string itemID;
    protected bool itemBroken;
    protected bool repairItem;
    [SerializeField] GameObject gameManagerObject;
    [SerializeField] GameManager gameManager;


    private void Awake()
    {
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        repairItem = false;
    }

    private void Update()
    {
        cooldown += Time.deltaTime;

        if (cooldown > 10f)
        {
          //  RepairItem();
            cooldown = 0f;
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

