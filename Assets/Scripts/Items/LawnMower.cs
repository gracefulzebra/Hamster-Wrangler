using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth = 2;
    int health;

    private void Awake()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (repairItem)
        {
            health = maxHealth;
        }
       Durability(health);
    }

    private void OnTriggerStay(Collider collision)
    {
        itemID = "LawnMower";
        // if item is unplaced then dont run script
       if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (itemBroken)
            return;  
        health--;
        ItemInteract(collision.gameObject);
        collision.gameObject.GetComponent<HamsterBase>().Kill();
    }
}
