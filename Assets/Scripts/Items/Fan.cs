using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : MonoBehaviour
{

    Vector3 pushForce = new Vector3(-5f, 0f, -5f);
    bool turnedOn;
    float fanTimer;


    private void Update()
    {
        if (turnedOn)
        {
            fanTimer += Time.deltaTime;
            if (fanTimer > 4)
            {
                turnedOn = false;
                fanTimer = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        if(!turnedOn)
        turnedOn = true;
    }

    private void OnTriggerStay(Collider col)
    {
        if (turnedOn && fanTimer < 4)
        {
            var direction = col.transform.position - transform.position;

            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);
        }
    }
}
