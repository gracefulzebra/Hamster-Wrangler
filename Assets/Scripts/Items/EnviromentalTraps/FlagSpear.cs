using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagSpear : EnvironmentalBase
{

    [SerializeField] int damage;
    [SerializeField] float range;

    bool landed;

    private void OnCollisionEnter(Collision col)
    {
        if (!landed)
        {
            CheckForHamster();
            GameManager.instance.audioManager.FlagSpearLandedAudio();
            landed = true;
        }
    }
    void CheckForHamster()
    {

        RaycastHit[] nearbyObjects = Physics.SphereCastAll(transform.position, range, Vector3.up, range, LayerMask.GetMask("Scannable"));
       
        if (nearbyObjects.Length > 0)
        {
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                AddScore();
                ItemInteract(nearbyObjects[i].transform.gameObject);
                nearbyObjects[i].transform.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
            }
            GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            temp.GetComponent<ComboDisplay>().SetComboText("EPIC KILL x " + nearbyObjects.Length);
        }
    }
}
