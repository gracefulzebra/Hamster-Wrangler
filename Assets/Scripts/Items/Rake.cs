using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rake : TrapBase
{ 
    /*
    bool pressedRake;
    float pressedRakeCooldown;
    [SerializeField] GameObject activationButton;

    private void Start()
    {
        itemID = "Rake";
    }
    private void Update()
    {
        SliderUpdate();

  /*      if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        timer += Time.deltaTime;

        if (timer > timerMax)
        {
            finishedCooldown = true;
            activationButton.SetActive(true);
        }
        else
            activationButton.SetActive(false);
    


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
      /*   if (timer > timerMax)
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
            GameManager.instance.audioManager.PlayUsedRake();
            col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force / 2, ForceMode.Force);
            pressedRake = false;
            finishedCooldown = false;
            ItemInteract(col.gameObject);
        }
   }
*/
}
 
