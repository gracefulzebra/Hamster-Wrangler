using UnityEngine;
using UnityEngine.UI;

public class Fan : TrapBase
{
    [SerializeField] ParticleSystem windEffect;
    public float force;

    private void Update()
    {
        if (activateTrap)
        {
            UseFuel();
            windEffect.Play();
            SliderUpdate();
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (activateTrap)
        {
                Vector3 direction = transform.position - transform.parent.position;

                col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);

                //Communicates that item has interacted with the hamster and what type it is.
                itemID = "LeafBlower";
                ItemInteract(col.gameObject);         
        }
    }
}

    /*       if (GetComponentInParent<SnapToGrid>().hasItem == true)
       {
           windEffect.SetActive(false);
       }

      SliderUpdate();

       if (GetComponentInParent<SnapToGrid>().hasItem == true)
           return;  

       if (turnedOn)
       {
           if (counter < 1)
           {
               counter++;
               GameManager.instance.audioManager.LeafBlowerUse();
           }
           windEffect.SetActive(true);
           fanTimer += Time.deltaTime;
       }
       else
       {
           counter = 0;
           windEffect.SetActive(false);
           timer += Time.deltaTime;
       }

       if (timer > timerMax)
       {
           activationButton.SetActive(true);
       } 
       else
       {
           activationButton.SetActive(false);
       }

       if (fanTimer > 3)
       {
           timer = 0;
           fanTimer = 0;
           turnedOn = false;
       }
*/

