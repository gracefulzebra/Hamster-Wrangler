using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Item Effects")]
    WaitForSeconds delay = new WaitForSeconds(2);

    ///<summary>
    ///Starts courtine that will kill hamster in 2 seconds 
    ///</summary>
    public void OnFire()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        StartCoroutine(burnToDeath());
    }

    IEnumerator burnToDeath()
    {
        yield return delay;
        Kill();
    }
     
    ///<summary>
    /// when player is no longer in tar the flammablke affect will wear off 
    ///</summary>
    public void LeftTarArea()
    {
        StartCoroutine(RemoveTar());
    }

    IEnumerator RemoveTar()
    {
        yield return delay;
        gameObject.tag = "Untagged";
    }

    void OnCollisionEnter(Collision col)
    {
        // if a hamster is onfire he can set other hamster on fire
        if (gameObject.tag == "Flammable" && col.gameObject.name == "Hamster (1)")
        {
            print("in if");
            col.gameObject.GetComponent<ItemEffects>().OnFire();
        }
    }

    ///<summary>
    ///Destroys the current Hamster with no delay
    ///</summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
