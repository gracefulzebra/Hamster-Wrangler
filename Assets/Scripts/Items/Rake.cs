using Unity.VisualScripting;
using UnityEngine;

public class Rake : TrapBase
{ 

    bool pressedRake;
    float pressedRakeCooldown;
    float pressedRakeCooldownMax;

    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        cooldown += Time.deltaTime;
        SliderUpdate();

        if (cooldown > cooldownMax)
        {
            finishedCooldown = true;  
        }

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
         if (cooldown > cooldownMax)
         {
            pressedRake = true;
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
            cooldown = 0;
            ItemInteract(col.gameObject);
        }
   }
}
