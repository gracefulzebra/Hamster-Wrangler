using UnityEngine;
using UnityEngine.UI;

public class Fan : TrapBase
{
    
    Vector3 pushForce = new Vector3(-5f, 0f, -5f);
    bool turnedOn;
    float fanTimer;
    [SerializeField] ParticleSystem windEffect;
    [SerializeField] Image activationButton;

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
            timer += Time.deltaTime;

        if (timer > timerMax)
        {
            activationButton.GetComponent<Image>().color = new Color(activationButton.color.r, activationButton.color.g, activationButton.color.b, 1f);
        } 
        else
        {
            activationButton.GetComponent<Image>().color = new Color(activationButton.color.r, activationButton.color.g, activationButton.color.b, 0f);

        }

        if (fanTimer > 3)
        {
            timer = 0;
            fanTimer = 0;
            turnedOn = false;
        }
    }

    public void UseFan()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
        if (!turnedOn && timer > timerMax)
        { 
            turnedOn = true;
            timer = 0;
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
            }
        }
    }
}
