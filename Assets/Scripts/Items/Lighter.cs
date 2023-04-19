using System.Linq;
using UnityEngine;

public class Lighter : TrapBase
{

    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;

    [Header("Generic Values")]
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    [SerializeField] Animator animator;
    [SerializeField] GameObject mat;


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

    GameObject blowTorchObject;
    void FuelAndActivation()
    {
        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.BlowtorchActive();
                blowTorchObject = GameManager.instance.audioManager.blowTorchList.LastOrDefault();
                audioOn = true;
            }

            if (chargeCount != 0)
            {
                canUseTrap = false;   
                UseFuel();
                fireEffect.SetActive(true);
            }
        }
        else
        {
            fireEffect.SetActive(false);
            RechargeFuel();
            if (blowTorchObject != null)
            {
                Destroy(blowTorchObject);
            }
            audioOn = false;
        }
        
        if (chargeCount == 0 && refuelTimer > rechargeDuration)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
            canUseTrap = false;
        }
    }

    protected void UseFuel()
    {
        if (!rechargeFuel)
        {
            useFuelTimer += Time.deltaTime;
            refuelTimer = 0;

            if (useFuelTimer >= timeTrapActivePerCharge)
            {
                // removes 1 charge from trap
                chargeCount--;

                animator.SetTrigger("Deactivate");
                mat.GetComponent<Renderer>().material.color = customColor;

                activateTrap = false;
                rechargeFuel = true;
            }
        }
    }

    protected void RechargeFuel()
    {
        if (rechargeFuel)
        {
            refuelTimer += Time.deltaTime;
            useFuelTimer = 0;
            if (refuelTimer >= rechargeDuration)
            {
                animator.SetTrigger("Activate");
                mat.GetComponent<Renderer>().material.color = Color.white;

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

