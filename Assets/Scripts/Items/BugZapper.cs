using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugZapper : TrapBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
        if (activateTrap)
        {
            if (col.gameObject.name == "Hamster 1(Clone)")
            {
                col.gameObject.GetComponent<ItemEffects>().BeenElectrocuted(damage);
            }
            activateTrap = false;
        }
    }
}
