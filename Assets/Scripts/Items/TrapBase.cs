using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBase : MonoBehaviour
{

   // int maxItemHealth = 2;
  //  int itemHealth;

    float cooldown;
    public string itemID;
    protected bool itemBroken;

    public void Awake()
    {
    }

    // read in items health
    public void Durability(int itemHealth)
    {
        itemHealth--;
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

    // read in items maxhealth, the logic in this is flawed
    void RepairItem(int maxHealth)
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
      //  RepairItem();
    }

    public void ItemInteract(GameObject col)
    {
        if (col.GetComponent<HamsterScore>() != null)
            col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID);
    }
}

