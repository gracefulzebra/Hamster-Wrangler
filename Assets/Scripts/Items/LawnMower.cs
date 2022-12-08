using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth = 2;
    int health;
    bool onCooldown;
    [SerializeField] GameObject itemBrokenEffect;


    private void Awake()
    {
        itemID = "LawnMower";
        health = maxHealth;
        gameManagerObject = GameObject.Find("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    private void Update()
    {
       if(onCooldown)
       {
         timer += Time.deltaTime;

         if (timer > timerMax)
         {
             onCooldown = false;     
         }
       }

        SliderUpdate();

        if (repairItem)
        {
            health = maxHealth;
            timer = 0;
        }

        Durability(health);
        
        if( health == 0)
        {
            itemBrokenEffect.SetActive(true);
            onCooldown = true;
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
