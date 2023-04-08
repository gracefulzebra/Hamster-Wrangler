using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject windEffect;
    [SerializeField] GameObject flameThrowerEffect;

    [Header("Generic Values")]
    [SerializeField] float force;
    [SerializeField] float leafblowerDuration;

    
    float maxForce;
    bool audioOn = false;

    [Header("Flamethrower")]
    public bool flameThrower;
    float leafblowerTimer;
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    [Header("Overcharge")]
    public bool overCharge;
    [SerializeField] float overChargeForceMultiplication;
    [SerializeField] GameObject lightningEffect;



    private void Start()
    {
        maxForce = force;
        itemID = "LeafBlower";

        //Physics.IgnoreLayerCollision(0, 9);
    }
   
    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }

        // logic doesnt work - update fuel cant be called
   //     if (activateTrap)
      //  {
        //    canUseTrap = false;
            if (chargeCount != 0)
            {
                UpdateFuel();
                leafblowerTimer += Time.deltaTime;
                windEffect.SetActive(true);
                if (leafblowerTimer > leafblowerDuration)
                {
                    canUseTrap = true;
                    activateTrap = false;
                    audioOn = false;
                    leafblowerTimer = 0f;
                }           
           // }
        }
        else
        {
            windEffect.SetActive(false);
            // when trap is decativated it ensures it doesnt wake up as flamethrower
            flameThrower = false;
            flameThrowerEffect.SetActive(false);
        }

        if (chargeCount == 0)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
        }
       // LeafblowerActivation();
       // OutOfFuel();
    }




    void LeafblowerActivation()
    {


        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.LeafBlowerUse();
                audioOn = true;
            }

            // used so leafblower cannot be activated if already on
            canUseTrap = false;
            leafblowerTimer += Time.deltaTime;
            
            SliderUpdate();
            UseFuel();
            windEffect.SetActive(true);
            

            if (leafblowerTimer > leafblowerDuration)
            {
                canUseTrap = true;
                activateTrap = false;
                audioOn = false;
                leafblowerTimer = 0f;
            }

            if (flameThrower)
            {
                flameThrowerEffect.SetActive(true);
            }
            else
            {
                flameThrowerEffect.SetActive(false);
            }

            if (overCharge)
            {
                StartCoroutine(StopOverCharge());
                lightningEffect.SetActive(true);
            }
        }
        else
        {
            windEffect.SetActive(false);
            // when trap is decativated it ensures it doesnt wake up as flamethrower
            flameThrower = false;
            flameThrowerEffect.SetActive(false);
        }

        if (overCharge)
        {
            lightningEffect.SetActive(true);
        }
    }

    IEnumerator StopOverCharge()
    {
        yield return new WaitForSeconds(4f);
        overCharge = false;
        lightningEffect.SetActive(false);
    }

    void OutOfFuel()
    {
        if (hasFuel == false)
        {
            canUseTrap = true;
            activateTrap = false;
            refuelSymbol.SetActive(true);
            // resets timer
            leafblowerTimer = 0f;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
       if (activateTrap)
       {
        // for hamster collisions
         if (col.transform.CompareTag("Hamster"))
         {
            Vector3 direction = transform.position - transform.parent.position;
       
                if (overCharge)
                {
                    col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force * overChargeForceMultiplication, ForceMode.Force);
                    //ItemInteract(col.gameObject);
                }
                else
                {
                    col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force , ForceMode.Force);
                }

                //Communicates that item has interacted with the hamster and what type it is.
                ItemInteract(col.gameObject);

               if (flameThrower)
               {
                   col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                   //ItemInteract(col.gameObject);
               } 
         }
            // for none hamster collision
            else
            {
                // for leafblower + lighter interaction
              if (col.GetComponent<TrapBase>() != null)
              {
                if (col.gameObject.GetComponent<TrapBase>().itemID == "Lighter" && col.gameObject.GetComponent<TrapBase>().activateTrap)
                {
                    IncrementTrapInteracts(col.gameObject);
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
}

