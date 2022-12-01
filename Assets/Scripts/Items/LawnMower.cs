using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : TrapBase
{
    

    private void OnTriggerStay(Collider collision)
    {
        itemID = "LawnMower";
        // if item is unplaced then dont run script
       if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
       if (itemBroken)
            return;
        Durability();
        ItemInteract(collision.gameObject);
        collision.gameObject.GetComponent<HamsterBase>().Kill();
    }
}
