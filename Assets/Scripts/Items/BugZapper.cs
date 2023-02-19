using System.Collections.Generic;
using UnityEngine;

public class BugZapper : TrapBase
{

    float distance;
    [SerializeField] int chargeCount;
    public List<GameObject> hamsterNo = new List<GameObject>();
    [SerializeField] float cooldownTimer;
    [SerializeField] float cooldownTimerMax;
    bool startCooldown;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activateTrap)
        {
            canUseTrap = false;
            chargeCount--;
            activateTrap = false;
            startCooldown = true;
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
            if (col.gameObject.name == "Hamster 1(Clone)")
            {
                col.gameObject.GetComponent<ItemEffects>().BeenElectrocuted(damage);
            }
            activateTrap = false;
        }
    }
}


//   GameObject hamster = GameObject.Find("Hamster 1(Clone)");
//   hamsterNo.Add(hamster);


// call this in item effects and tick a bool to enable 
//  distance = (transform.position - hamsterNo[0].transform.position).magnitude;

// if (distance < 5)
// {
//    print("within distanc eof deadly cube");
//// }