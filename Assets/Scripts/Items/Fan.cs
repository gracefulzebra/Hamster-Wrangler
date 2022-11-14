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
        var direction = col.transform.position-transform.position;
 
        col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);
    }
}
