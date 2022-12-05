using Unity.VisualScripting;
using UnityEngine;

public class Rake : TrapBase
{ 

        bool pressedRake = false;
        public float force;

    private void Awake()
    {
        cooldownFinish = 7f;
    }

    private void Update()
    {
         cooldown += Time.deltaTime;

        if (cooldown > cooldownFinish)
        {
            finishedCooldown = true;
            cooldown = 0;
        }
    }

    void OnMouseDown()
    {
        pressedRake = true;
          //  print(pressedRake);
    }

        ///<summary>
        ///Throws hamster in air if standing on it 
        ///</summary>
        void ThrowObject()
        {

        }

        void OnTriggerStay(Collider col)
        {

        //var direction = col.transform.position - transform.position;
        Vector3 direction = transform.position - transform.parent.position;

        //  col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);

        if (pressedRake && finishedCooldown)
        { 
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
                col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force * 2, ForceMode.Force);
                pressedRake = false;
                finishedCooldown = false;
              ItemInteract(col.gameObject);

        }
        }
}
