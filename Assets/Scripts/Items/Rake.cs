using UnityEngine;

public class Rake : TrapBase
{ 
        bool pressedRake = false;
        public float force;


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

        var direction = col.transform.position - transform.position;

      //  col.gameObject.GetComponent<Rigidbody>().AddForce(direction * 35, ForceMode.Force);

        if (pressedRake)
            { 
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * force / 4, ForceMode.Force);
                pressedRake = false;
              ItemInteract(col.gameObject);
             }
        }
}
