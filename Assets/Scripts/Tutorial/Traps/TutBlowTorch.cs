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
