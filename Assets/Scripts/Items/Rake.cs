using Unity.VisualScripting;
using UnityEngine;

public class Rake : TrapBase
{ 

    public bool pressedRake;
    public float force;
    [SerializeField] GameObject itemBrokenEffect;


    private void Awake()
    {
        cooldownFinish = 7f;
        pressedRake = false;
    }

    private void Update()
    {

        cooldown += Time.deltaTime;

        if (cooldown > cooldownFinish)
        {
            itemBrokenEffect.SetActive(false);
            finishedCooldown = true;  
        }
    }

    public void UseRake()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (cooldown > cooldownFinish)
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
            itemBrokenEffect.SetActive(true);
        }
   }
}
