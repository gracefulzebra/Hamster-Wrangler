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
    [SerializeField] GameObject itemCooldown;
    // [SerializeField] GameObject gameManager;

    private void Awake()
    {
        cooldownFinish = 7;
    }

    void VisualItemCooldown()
    {
        if (!turnedOn)
        {
            itemCooldown.SetActive(true);
        } 
        else
            itemCooldown.SetActive(false);
    }



    private void Update()
    {
        
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;

        cooldown += Time.deltaTime;

        VisualItemCooldown();

        if (turnedOn)
        {
            //print("fan has turned on");
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
        if (!turnedOn && cooldown > cooldownFinish)
        {
            //print("fan has been clicked on");
            turnedOn = true;
            cooldown = 0;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (turnedOn && fanTimer < 4)
        {
            Vector3 direction = transform.position - transform.parent.position;

            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);

            //Communicates that item has interacted with the hamster and what type it is.
            itemID = "LeafBlower";
            ItemInteract(col.gameObject);
        }
    }
}
