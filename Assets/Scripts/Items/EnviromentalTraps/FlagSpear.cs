using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class FlagSpear : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField] int scorePerKill;
    [SerializeField] GameObject comboDisplayPrefab;

    bool landed;


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
                nearbyObjects[i].transform.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
                //   ItemInteract(targetObject);
            }
            GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            temp.GetComponent<ComboDisplay>().SamComboTest("EPIC KILL x " + nearbyObjects.Length);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        landed = true;
        if (col.transform.GetComponent<HamsterBase>() != null)
        {
            col.transform.GetComponent<HamsterBase>().Kill();
        }
    }
}
