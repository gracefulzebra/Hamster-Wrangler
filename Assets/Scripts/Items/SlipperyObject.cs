//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        // col.gameObject.GetComponent<HamsterMovement>().speed /= 2;
        col.gameObject.tag = "Flammable";
    }
    private void OnTriggerExit(Collider col)
    {
        // col.gameObject.GetComponent<HamsterMovement>().speed *= 2;
         col.gameObject.GetComponent<ItemEffects>().LeftTarArea();
    }
}
