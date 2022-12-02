using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth;
    int health = 2;

    private void OnTriggerStay(Collider collision)
    {
        itemID = "LawnMower";
        // if item is unplaced then dont run script
       if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
       if (itemBroken)
            return;
        Durability(health);
        ItemInteract(collision.gameObject);
        collision.gameObject.GetComponent<HamsterBase>().Kill();
    }
}
