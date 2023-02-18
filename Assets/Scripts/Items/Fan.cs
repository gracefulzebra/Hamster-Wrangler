using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Fan : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject windEffect;
    [SerializeField] GameObject flameThrowerEffect;

    [Header("Generic Values")]
    [SerializeField] float force;
    [SerializeField] float leafblowerDuration;
    bool trapInUse;


    [Header("Flamethrower")]
    bool flameThrower;
    float leafblowerTimer;
    private float burnDuration = 2f; //Time between instances of burn damage
    private int burnAmount = 5; //Amount of instances of burn damage

    private void Start()
    {
        itemID = "LeafBlower";
    }
   
    private void Update()
    {
        if (trapInUse)
        {
            activateTrap = false;
        }
        else
        {
            if (activateTrap)
            {
                // used so lawnmower cannot be activated 
                trapInUse = true;
                leafblowerTimer += Time.deltaTime;

                SliderUpdate();
                UseFuel();
                windEffect.SetActive(true);

                if (leafblowerTimer > leafblowerDuration)
                {
                    trapInUse = false;
                    activateTrap = false;
                    leafblowerTimer = 0f;
                }

                if (flameThrower)
                {
                    flameThrowerEffect.SetActive(true);
                }
            }
        }
    
        if (activateTrap == false)
        {
            windEffect.SetActive(false);
            // when trap is decativated it ensures it doesnt wake up as lawnmower
            flameThrower = false;
            flameThrowerEffect.SetActive(false);
        }

        if (hasFuel == false)
        {
            trapInUse = false;
            activateTrap = false;
            refuelSymbol.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
       if (activateTrap)
       {
        // inside this if is effects that shoudl only effect the hamster
         if (col.gameObject.name == "Hamster 1(Clone)")
         {
           if (activateTrap)
           {
            Vector3 direction = transform.position - transform.parent.position;

            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);

            //Communicates that item has interacted with the hamster and what type it is.
            ItemInteract(col.gameObject);

               if (flameThrower)
               {
                   col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                   ItemInteract(col.gameObject);
               } 
           }
 
         }
            else
            {
                // for leafblower + lighter interaction
                if (col.gameObject.GetComponent<TrapBase>().itemID == "Lighter" && col.gameObject.GetComponent<Lighter>().activateTrap)
                {
                    flameThrower = true;
                }
                else
                {
                    flameThrower = false;
                }
            }
       }
    }
}

    /*       if (GetComponentInParent<SnapToGrid>().hasItem == true)
       {
           windEffect.SetActive(false);
       }

      SliderUpdate();

       if (GetComponentInParent<SnapToGrid>().hasItem == true)
           return;  

       if (turnedOn)
       {
           if (counter < 1)
           {
               counter++;
               GameManager.instance.audioManager.LeafBlowerUse();
           }
           windEffect.SetActive(true);
           fanTimer += Time.deltaTime;
       }
       else
       {
           counter = 0;
           windEffect.SetActive(false);
           timer += Time.deltaTime;
       }

       if (timer > timerMax)
       {
           activationButton.SetActive(true);
       } 
       else
       {
           activationButton.SetActive(false);
       }

       if (fanTimer > 3)
       {
           timer = 0;
           fanTimer = 0;
           turnedOn = false;
       }
*/

