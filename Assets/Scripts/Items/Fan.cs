using UnityEngine;
using UnityEngine.UI;

public class Fan : TrapBase
{
    [SerializeField] GameObject windEffect;
    [SerializeField] GameObject flameThrowerEffect;
    public float force;
    public bool flameThrower;

    float timeActive;

    private void Start()
    {
        itemID = "LeafBlower";
    }
   
    private void Update()
    {
        if (activateTrap && !flameThrowerEffect)
        {
            UseFuel();
            windEffect.SetActive(true);
            SliderUpdate();
        }
        else if (activateTrap && flameThrowerEffect)
        {
            UseFuel();
            flameThrowerEffect.SetActive(true);
            SliderUpdate();
        }

        if (activateTrap == false)
        {
            windEffect.SetActive(false);
            // when trap is decativated it ensures it doesnt wake up as lawnmower
            flameThrower = false;
        }

        if (hasFuel == false)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
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
                   col.gameObject.GetComponent<ItemEffects>().OnFire();
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

