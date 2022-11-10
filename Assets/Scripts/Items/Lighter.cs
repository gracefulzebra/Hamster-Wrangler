using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
     void OnTriggerEnter(Collider col)
     {
        if (col.gameObject.tag == "Flammable")
        {
            col.gameObject.GetComponent<Renderer>().material.color = Color.red;
            print("IM BURNING FUCKKKK");
        }
     }
}
