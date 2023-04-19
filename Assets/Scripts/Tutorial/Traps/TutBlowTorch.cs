using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutBlowTorch : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;

    [Header("Generic Values")]
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    [SerializeField] Animator animator;
    [SerializeField] GameObject mat;

    GameObject hamster;

    private void Start()
    {
        itemID = "Lighter";
    }

    private void Update()
    {
        if (GetComponentInParent<BaseSnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }
        FuelAndActivation();

        // step 18ish
        if (hamster == null)
        {
            hamster = GameObject.FindGameObjectWithTag("Hamster");
        }
        else if (hamster != null && TutManager.tutInstance.posCounter == 19 || TutManager.tutInstance.posCounter == 20)
        {
            float hamsterDistance = (transform.position - hamster.transform.position).magnitude;
            // make public bool somewhere, will need to be read in from bugzapper for contiuinity probs
            if (hamsterDistance < 3.5f)
            {
                // only works for poscounter18
                TutManager.tutInstance.LerpTimeDown();
            }
        }
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
        if (GetComponentInParent<BaseSnapToGrid>().hasItem)
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
    private void Update()
    {
        if (GetComponentInParent<BaseSnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }
        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.LighterOn();
                audioOn = true;
            }

            UseFuel();
            fireEffect.SetActive(true);
            SliderUpdate();
        }
        else
        {
            fireEffect.SetActive(false);
        }

        if (hasFuel == false)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
        }

        // step 18ish
        if (hamster == null)
        {
            hamster = GameObject.FindGameObjectWithTag("Hamster");

        } else if (hamster != null && TutManager.tutInstance.posCounter == 19 || TutManager.tutInstance.posCounter == 20)
        {
            float hamsterDistance = (transform.position - hamster.transform.position).magnitude;
            // make public bool somewhere, will need to be read in from bugzapper for contiuinity probs
            if (hamsterDistance < 3.5f)
            {
                // only works for poscounter18
                TutManager.tutInstance.LerpTimeDown();
            }
        }    
    }

    void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<BaseSnapToGrid>().hasItem)
            return;
        if (activateTrap)
        {
            if (col.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                ItemInteract(col.gameObject);
            }
        }
    }*/

