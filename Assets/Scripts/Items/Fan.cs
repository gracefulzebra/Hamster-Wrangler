using UnityEngine;
using UnityEngine.UI;

public class Fan : TrapBase
{
    
    bool turnedOn;
    int counter;
    public float fanTimer;
    [SerializeField] GameObject windEffect;
    [SerializeField] GameObject activationButton;

    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
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
                gameManager.audioManager.LeafBlowerUse();
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
    }


    public void UseFan()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;
            turnedOn = true;
            timer = 0;
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
