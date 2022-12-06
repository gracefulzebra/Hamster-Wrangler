using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth = 2;
    int health;
    [SerializeField] GameObject itemBrokenEffect;


    private void Awake()
    {
        itemID = "LawnMower";
        health = maxHealth;
    }

    private void Update()
    {
        if (repairItem)
        {
            health = maxHealth;
        }
       Durability(health);
        
        if( health == 0)
        {
            itemBrokenEffect.SetActive(true);
        }    
        else
        {
            itemBrokenEffect.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
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
