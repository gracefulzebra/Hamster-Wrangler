using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Lighter : TrapBase
{

    public bool lighterOn;
    int counter;
    public float lighterCooldown;
    public float lighterTimerMax;
    [SerializeField] GameObject fireEffect;

    private void Start()
    {
        lighterOn = false;
        itemID = "Lighter";
    }

    private void Update()
    {
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
    }

    void OnTriggerStay(Collider col)
    {
         if (lighterOn)
         {
            if (lighterCooldown < timerMax)
            {
                if (col.gameObject.tag == "Flammable")
                {
                    col.gameObject.GetComponent<ItemEffects>().OnFire();
                    ItemInteract(col.gameObject);
                }
            }
         }
    }
}
