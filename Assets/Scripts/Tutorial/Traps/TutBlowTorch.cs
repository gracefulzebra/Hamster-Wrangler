using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutBlowTorch : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;

    [Header("Generic Values")]
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    bool audioOn = false;

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

    void FuelAndActivation()
    {
        if (fuelSlider != null)
        {
            ChangeSliderColour();
        }
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

                UseFuel();
                fireEffect.SetActive(true);
            }
        }
        else
        {
            fireEffect.SetActive(false);
            RechargeFuel();
            audioOn = false;
        }

        if (chargeCount == 0 && refuelTimer > rechargeDuration)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
            canUseTrap = false;
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

