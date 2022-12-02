using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lighter : TrapBase
{

    // probs needs health or cooldown or soemthing

     void OnTriggerEnter(Collider col)
     {

        col.gameObject.GetComponent<HamsterBase>().speed /= 2;
        col.gameObject.GetComponent<ItemEffects>().onFire = true;
        if (col.gameObject.tag == "Flammable")
        {
            col.gameObject.GetComponent<ItemEffects>().OnFire();

        }
     }
}
