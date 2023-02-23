//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tar : TrapBase
{
    [SerializeField] float slowSpeed;

    private void Start()
    {
      //  itemID = "Tar";
    }
    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<HamsterBase>().speed /= slowSpeed;
        col.gameObject.tag = "Flammable";
        ItemInteract(col.gameObject);
    }
    private void OnTriggerExit(Collider col)
    {
        col.gameObject.GetComponent<HamsterBase>().speed *= slowSpeed;
  //      col.gameObject.GetComponent<ItemEffects>().LeftTarArea();
    }
}
