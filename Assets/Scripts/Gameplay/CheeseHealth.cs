using System.Collections;
using UnityEngine;

public class CheeseHealth : MonoBehaviour
{
    [SerializeField] GameObject[] cheeseSections;
    [SerializeField] ParticleSystem cheeseCrumbs;
    int counter;

    private void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<HamsterBase>() != null)
        {
            col.GetComponent<HamsterBase>().Despawn();
            GameManager.instance.LoseHealth();
            cheeseCrumbs.Play();
            Destroy(cheeseSections[counter]);
            counter++;

            if (counter >= cheeseSections.Length)
            {
                Destroy(gameObject);
            }
        }      
    }
}