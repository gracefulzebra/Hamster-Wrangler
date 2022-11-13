using UnityEngine;

public class Rake : MonoBehaviour
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
            if (pressedRake)
            { 
                col.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Force);
                pressedRake = false;
            }
        }
}
