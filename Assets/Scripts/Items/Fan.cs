using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : TrapBase
{
    
    Vector3 pushForce = new Vector3(-5f, 0f, -5f);
    bool turnedOn;
    float fanTimer;
    [SerializeField] ParticleSystem windEffect;

    private void Update()
    {        
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;  
   
        SliderUpdate();

        if (turnedOn)
        {
            fanTimer += Time.deltaTime;
        }
        else
            cooldown += Time.deltaTime;

        if (fanTimer > 3)
        {
            cooldown = 0;
            fanTimer = 0;
            turnedOn = false;
        }
    }

    public void UseFan()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
        if (!turnedOn && cooldown > cooldownMax)
        { 
            turnedOn = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (turnedOn)
        {
            if (fanTimer < 3f)
            {
                cooldown = 0;

                Vector3 direction = transform.position - transform.parent.position;

                col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);

                //Communicates that item has interacted with the hamster and what type it is.
                itemID = "LeafBlower";
                ItemInteract(col.gameObject);         
            }
        }
    }
}
