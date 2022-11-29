using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : TrapBase
{
   

    private void OnTriggerStay(Collider collision)
    {
        // if item is unplaced then dont run script
       if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
        // if item is broken then dont run script
       if (itemBroken)
           return;
        collision.gameObject.GetComponent<HamsterBase>().Kill();
        Durability();
    }
}
