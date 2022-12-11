using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Lighter : TrapBase
{

    public bool lighterOn;
    public float lighterCooldown;
    [SerializeField] GameObject fireEffect;

    private void Start()
    {
        lighterOn = false;
    }

    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;

        // if lighter isnt on, starts timer to activate it 
        if (!lighterOn)
        {
            timer += Time.deltaTime;
            lighterCooldown = 0f;
            fireEffect.SetActive(false);
        }
        // if timer is on, timer strats till it turn off
        else
        {
            fireEffect.SetActive(true);
            lighterCooldown += Time.deltaTime;
        }

        if (timer > timerMax)
        {
            lighterOn = true;
            timer = 0;
        }

        if (lighterCooldown > timerMax)
        {
            lighterOn = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
         if (lighterOn)
         {
            if (lighterCooldown < timerMax)
            {
                if (col.gameObject.tag == "Flammable")
                {
                    col.gameObject.GetComponent<ItemEffects>().OnFire();
                }
            }
         }
    }
}
