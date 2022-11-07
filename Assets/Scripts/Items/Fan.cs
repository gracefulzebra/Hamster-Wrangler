using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fan : MonoBehaviour
{

    Vector3 forceVector = new Vector3(-10f, 0, -10f);
    public GameObject hamster;
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        col.gameObject.transform.position += transform.forward * moveSpeed * Time.deltaTime;

        //GetComponent<Rigidbody>().AddRelativeForce(forceVector, ForceMode.Force); 
    }
}
