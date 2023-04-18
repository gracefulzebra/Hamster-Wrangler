using System.Collections;
using System.Linq;
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

    [Header("Flamethrower")]
    public bool flameThrower;
    float leafblowerTimer;
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    [Header("Overcharge")]
    public bool overCharge;
    [SerializeField] float overChargeForceMultiplication;
    [SerializeField] GameObject lightningEffect;

    [SerializeField] GameObject mat;

    private void Start()
    {
        maxForce = force;
        itemID = "LeafBlower";
    }
   
    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }
        FuelAndActivation();
    }

    GameObject blowerNoiseObject;
    void FuelAndActivation()
    {
        if (activateTrap)
        {
            if (chargeCount != 0)
            {
                canUseTrap = false;
                if (!audioOn)
                {
                    GameManager.instance.audioManager.LeafBlowerUse();
                     blowerNoiseObject = GameManager.instance.audioManager.lbSoundList.LastOrDefault();
                     audioOn = true;
                }
                UseFuel1();
                windEffect.SetActive(true);
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
            RechargeFuel1();
            if (blowerNoiseObject != null)
            {
                Destroy(blowerNoiseObject);
            }
            audioOn = false;
        }

        if (chargeCount == 0 && refuelTimer > rechargeDuration)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
            canUseTrap = false;
        }

        if (overCharge)
        {
            lightningEffect.SetActive(true);
        }
    }

    protected void UseFuel1()
    {
        if (!rechargeFuel)
        {
            useFuelTimer += Time.deltaTime;
            refuelTimer = 0;

            if (useFuelTimer >= timeTrapActivePerCharge)
            {
                // removes 1 charge from trap
                chargeCount--;

                mat.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                mat.GetComponent<Renderer>().material.color = customColor;

                activateTrap = false;
                rechargeFuel = true;
            }
        }
    }

    protected void RechargeFuel1()
    {
        if (rechargeFuel)
        {
            refuelTimer += Time.deltaTime;
            useFuelTimer = 0;
            if (refuelTimer >= rechargeDuration)
            {
                mat.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                mat.GetComponent<Renderer>().material.color = Color.white;
                // trap can be used
                canUseTrap = true;
                // trap no longer needs to be fueled
                rechargeFuel = false;
            }
        }
    }

    IEnumerator StopOverCharge()
    {
        yield return new WaitForSeconds(4f);
        overCharge = false;
        lightningEffect.SetActive(false);
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

/*
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
}*/