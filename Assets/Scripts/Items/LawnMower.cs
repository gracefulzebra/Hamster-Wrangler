using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth = 2;
    int health;
    bool onCooldown;
    [SerializeField] GameObject repairItemEffect;
    [SerializeField] GameObject smokeEffect;


    private void Awake()
    {
        itemID = "LawnMower";
        health = maxHealth;
    }

    private void Update()
    {

        SliderUpdate();

        Durability(health);

        if (onCooldown)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
           {
                onCooldown = false;
                repairItemEffect.SetActive(true);
            }
        }

        if (repairItem)
        {
            health = maxHealth;
            timer = 0;
            repairItemEffect.SetActive(false);

        }

        if ( health == 0)
        {
            onCooldown = true;
        }    

        if (itemBroken)
            smokeEffect.SetActive(true);
        else
            smokeEffect.SetActive(false);


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
