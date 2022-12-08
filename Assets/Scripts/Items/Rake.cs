using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rake : TrapBase
{ 

    bool pressedRake;
    float pressedRakeCooldown;
    [SerializeField] Image activationButton;


    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        timer += Time.deltaTime;
        SliderUpdate();

        if (timer > timerMax)
        {
            finishedCooldown = true;
            activationButton.GetComponent<Image>().color = new Color(activationButton.color.r, activationButton.color.g, activationButton.color.b, 1f);
        }
        else
            activationButton.GetComponent<Image>().color = new Color(activationButton.color.r, activationButton.color.g, activationButton.color.b, 0f);


        // if player pressed the userake button it would 
        // automatically throw the hamster when they walked
        // on it, this stops that
        if (pressedRake)
        {
            pressedRakeCooldown += Time.deltaTime;
            if (pressedRakeCooldown > 0.3f)
            {
                pressedRake = false;
                pressedRakeCooldown = 0;
            }
        }
    }

    public void UseRake()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
         if (timer > timerMax)
         {
            pressedRake = true;
            timer = 0;
         }
    }

   void OnTriggerStay(Collider col)
   {

        Vector3 direction = transform.position - col.transform.position;

        if (pressedRake && finishedCooldown)
        { 
            col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force / 2, ForceMode.Force);
            pressedRake = false;
            finishedCooldown = false;
            ItemInteract(col.gameObject);
        }
   }
}
