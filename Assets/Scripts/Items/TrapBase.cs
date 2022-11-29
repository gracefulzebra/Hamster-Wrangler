using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{

    int itemHealth;
    int maxItemHealth;

    float cooldown;
    string itemID;
    public bool itemBroken;

    void Awake()
    {
        itemHealth = maxItemHealth;
    }

    private void Update()
    {
       
    }

    public void Durability()
    {
        itemHealth -= 1;

        if (itemHealth < maxItemHealth / 2)
            print("half health");
        if (itemHealth <= 0)
        {
            ItemBreak();
        }
    }

    void ItemBreak()
    {
        itemBroken = true;
    }

    void RepairItem()
    {
       
    }

    //"clean" nice
    void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID);
    }
}
