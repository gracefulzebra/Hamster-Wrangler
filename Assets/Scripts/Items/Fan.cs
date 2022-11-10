using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : MonoBehaviour
{

    Vector3 pushForce = new Vector3(-5f, 0f, -5f);

    private void OnTriggerStay(Collider col)
    {
     //  Vector3 hamsterPos = col.gameObject.transform.position;
     //  col.gameObject.GetComponent<Rigidbody>().AddRelativeForce(pushForce, ForceMode.Force);
        //   col.gameObject.GetComponent<HamsterBase>().spd = 0.05f;

        // Get direction from your postion toward the object you wish to push
        var direction = col.transform.position-transform.position;
 
        col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);
    }
}
