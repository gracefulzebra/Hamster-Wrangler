using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : TrapBase
{
    
    Vector3 pushForce = new Vector3(-5f, 0f, -5f);
    public bool turnedOn;
    float fanTimer;
    [SerializeField] ParticleSystem windEffect;
    [SerializeField] GameObject itemCooldown;
    public float force;

    private void Awake()
    {
        cooldownFinish = 3.5f;
    }

    private void Update()
    {        
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;  

        cooldown += Time.deltaTime;

        if (cooldown > cooldownFinish)
            itemCooldown.SetActive(true);

        if (turnedOn)
        {
            fanTimer += Time.deltaTime;
        }
        
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
        if (!turnedOn && cooldown > cooldownFinish)
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
                Vector3 direction = transform.position - transform.parent.position;

                col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);

                //Communicates that item has interacted with the hamster and what type it is.
                itemID = "LeafBlower";
                ItemInteract(col.gameObject);
              
                itemCooldown.SetActive(false);
                cooldown = 0;
            }
        }
    }
}
