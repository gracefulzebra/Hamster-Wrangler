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
    [SerializeField] ParticleSystem windEffect;
   // [SerializeField] GameObject gameManager;


    private void Update()
    {
        
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
        if (turnedOn)
        {
            print("fan has turned on");
            windEffect.Play();
            fanTimer += Time.deltaTime;
            if (fanTimer > 4)
            {
                turnedOn = false;
                fanTimer = 0;
                windEffect.Stop();
            }
        }
    }

    private void OnMouseDown()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
        if (!turnedOn)
        {
            print("fan has been clicked on");
            turnedOn = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (turnedOn && fanTimer < 4)
        {
            var direction = col.transform.position - transform.position;

            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);

            //Communicates that item has interacted with the hamster and what type it is.
            if (col.GetComponent<HamsterScore>() != null)
                col.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, "Fan");
        }
    }
}
