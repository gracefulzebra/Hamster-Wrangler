using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{

    int maxItemHealth = 2;
    int itemHealth;

    float cooldown;
    public string itemID;
    protected bool itemBroken;

    public void Awake()
    {
        itemHealth = maxItemHealth;
    }

    public void Durability()
    {
        itemHealth--;
        //print(itemHealth);
         if (itemHealth <= maxItemHealth / 2)
         {
           //print("half health");
         }
        if (itemHealth <= 0)
        {
            ItemBreak();
        }
    }

    void ItemBreak()
    {
        itemBroken = true;
        //print(itemBroken);
    }

    void RepairItem()
    {
        //print(itemBroken);
        if (itemBroken)
        {
           // itemHealth = maxItemHealth;
            //itemBroken = false;

            //print(itemHealth);
            //print(itemBroken);

        }
    }

    private void OnMouseDown()
    {
        RepairItem();
    }

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID);
    }
}

