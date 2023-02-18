using UnityEngine;

public class BugZapper : TrapBase
{

    float distance;
    public GameObject[] hamsterNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

// call this in item effects and tick a bool to enable 
        distance = (transform.position - hamsterNo[0].transform.position).magnitude;

        if (distance < 5)
        {
            print("within distanc eof deadly cube");
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
        if (activateTrap)
        {
            if (col.gameObject.name == "Hamster 1(Clone)")
            {
                col.gameObject.GetComponent<ItemEffects>().BeenElectrocuted(damage);
            }
            activateTrap = false;
        }
    }
}
