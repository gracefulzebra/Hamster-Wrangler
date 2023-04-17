using UnityEngine;

public class Lighter : TrapBase
{

    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;

    [Header("Generic Values")]
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    [SerializeField] Animator animator;

    private void Start()
    {
        itemID = "Lighter";
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


    void FuelAndActivation()
    {
        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.LighterOn();
                audioOn = true;
            }

            if (chargeCount != 0)
            {
                canUseTrap = false;   
                UseFuel1();
                fireEffect.SetActive(true);
            }
        }
        else
        {
            fireEffect.SetActive(false);
            RechargeFuel1();
                audioOn = false;
        }
        
        if (chargeCount == 0 && refuelTimer > rechargeDuration)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
            canUseTrap = false;
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
                animator.SetTrigger("Deactivate");
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
                animator.SetTrigger("Activate");
                // trap can be used
                canUseTrap = true;
                // trap no longer needs to be fueled
                rechargeFuel = false;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (activateTrap)
        {        
            if (col.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                ItemInteract(col.gameObject);
            }  
        }
    }
}
/*
            if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;

        // if lighter isnt on, starts timer to activate it 
        if (!lighterOn)
        {
            counter = 0;
            timer += Time.deltaTime;
            lighterCooldown = 0f;
            fireEffect.SetActive(false);
        }
        // if timer is on, timer starts till it turn off
        else
{
    if (counter < 1)
    {
        counter++;
        gameManager.audioManager.LighterOn();
    }
    fireEffect.SetActive(true);
    lighterCooldown += Time.deltaTime;
}

if (timer > timerMax)
{
    lighterOn = true;
    timer = 0;
}

if (lighterCooldown > lighterTimerMax)
{
    lighterOn = false;
}
*/

