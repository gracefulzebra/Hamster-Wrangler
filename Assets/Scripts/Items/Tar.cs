//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tar : TrapBase
{
    private void Start()
    {
        itemID = "Tar";
    }
    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<HamsterBase>().speed /= 2;
        col.gameObject.tag = "Flammable";
        ItemInteract(col.gameObject);
    }
    private void OnTriggerExit(Collider col)
    {
        col.gameObject.GetComponent<HamsterBase>().speed *= 2;
        col.gameObject.GetComponent<ItemEffects>().LeftTarArea();
    }
}
