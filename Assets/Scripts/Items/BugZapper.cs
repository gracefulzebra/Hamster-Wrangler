using System;
using System.Collections.Generic;
using UnityEngine;

public class BugZapper : TrapBase
{

    [SerializeField] int chargeCount;
    [SerializeField] float cooldownTimer;
    [SerializeField] float cooldownTimerMax;
    bool startCooldown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // THIS ALL NEEDS TO BE REWORKED AS IF YOU CLICK THE BUTTON IT AUTO CLICKS OFF AND THE TRAP DOESNT ORK 
    // Update is called once per frame
    void Update()
    {
        if(activateTrap)
        {
            canUseTrap = false;
            startCooldown = true;
            chargeCount--;
            activateTrap = false;
        }

        if (startCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldownTimerMax)
            {
                canUseTrap = true;
                activateTrap = false;
                startCooldown = false;
                cooldownTimer = 0;
                if (chargeCount ==0)
                {
                    refuelSymbol.SetActive(true);   
                }
            }
        }

        if (chargeCount == 0)
        {
            canUseTrap = false;
            activateTrap = false;
        }
    }

    public void ReactiveTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            canUseTrap = true;
            chargeCount = 2;
            refuelSymbol.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
        if (activateTrap && chargeCount !=0)
        {
            print("ja");
            if (col.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().BeenElectrocuted(damage);
                col.gameObject.GetComponent<ItemEffects>().BugZapperDistance();
            }
        }
    }
}


//   GameObject hamster = GameObject.Find("Hamster 1(Clone)");
//   hamsterNo.Add(hamster);


// call this in item effects and tick a bool to enable 
