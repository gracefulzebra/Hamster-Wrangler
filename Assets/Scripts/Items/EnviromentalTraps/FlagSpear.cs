using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpear : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] float range;

    List<GameObject> abusedHamster;

    bool landed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (landed)
        {
            CheckForHamster();
        }
    }

    void CheckForHamster()
    {

        RaycastHit[] nearbyObjects = Physics.SphereCastAll(transform.position, range, Vector3.up, range, LayerMask.GetMask("Scannable"));
       
        if (nearbyObjects.Length > 0)
        {
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (abusedHamster.Contains(nearbyObjects[i].transform.gameObject))
                    return;

                abusedHamster.Add(nearbyObjects[i].transform.gameObject);
                nearbyObjects[i].transform.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
                //ItemInteract(targetObject);
                print("hamster");
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.GetComponent<HamsterBase>() != null)
        {
            col.transform.GetComponent<HamsterBase>().Kill();
        }
        landed = true;
    }
}
