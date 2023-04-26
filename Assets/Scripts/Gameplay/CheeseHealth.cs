using System.Collections;
using UnityEngine;

public class CheeseHealth : MonoBehaviour
{
    GameObject gridRefObject;
    GridGenerator gridRef;

    [SerializeField] GameObject[] cheeseSections;
    [SerializeField] ParticleSystem cheeseCrumbs;
    int counter;

    Node nodeHit;

    private void Awake()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
    }
    private void Start()
    {
        nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
        nodeHit.placeable = false;
    }

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