using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GnomeKing : TrapBase
{

    Vector3 john;

    // Start is called before the first frame update
    void Start()
    {
        john =new Vector3( 0, 90,0);
    }

    // Update is called once per frame
    void Update()
    {

       // if round over use canusetrap

       if (activateTrap)
       {
            canUseTrap = false;
       }
    }

     IEnumerator GnomeSpin()
     {
        
        yield return gameObject.transform.eulerAngles = john;

     }

    private void OnTriggerStay(Collider col)
    {
        if (!activateTrap)
            return;
        if (col.CompareTag("Hamster"))
        {
            col.GetComponent<HamsterBase>().Kill();
        }
    }
}
