using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private void OnTriggerStay(Collider col)
    {
       Vector3 hamsterPos = col.gameObject.transform.position;
       col.gameObject.GetComponent<Rigidbody>().AddRelativeForce(hamsterPos, ForceMode.Force);
     //   col.gameObject.GetComponent<HamsterBase>().spd = 0.05f;

    }
}
