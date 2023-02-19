using System.Collections;
using UnityEngine;

public class LawnMower : TrapBase
{

    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject smokeEffect;

    [Header("Generic Values")]
    [SerializeField] float lawnmowerDestroyDelay;
    [SerializeField] float lawnmowerSpd;
    [SerializeField] float lawnmowerExplodeDelay;
    int counter = 0;
    bool willExplode;

    [Header("References")]
    GameObject gridRefObject;
    GridGenerator gridRef;
    [SerializeField] GameObject explosion;
    Node nodeHit;


    private void Start()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
        itemID = "LawnMower";
    }

    private void Update()
    {
        if (activateTrap)
        {
            if (counter < 1)
            {
                // finds the cloest node for the player and makes it placeable
                nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
                nodeHit.placeable = true;
                counter++;
            }
            ActivateLawnmower();
        }
    }

 /*   IEnumerator DestroyLawnmower()
    {
        yield return new WaitForSeconds(lawnmowerDestroyDelay);
        Destroy(gameObject.transform.parent.gameObject);
    }*/

    public void ActivateLawnmower()
    {
        smokeEffect.SetActive(true);
        activateTrap = true;
        transform.parent.Translate(Vector3.forward * lawnmowerSpd * Time.deltaTime);
    }

    void LawnmowerExplode()
    {
        nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
        Vector3 explosionPos = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.5f, nodeHit.worldPosition.z); ;
        Instantiate(explosion, explosionPos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider col)
    {
        // if item is unplaced then dont run script
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (!activateTrap)
            return;

        if (col.gameObject.name == "Hamster 1(Clone)")
        {
                ItemInteract(col.gameObject);
                col.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
        }

        if (col.gameObject.layer == 7) //|| collision.gameObject.tag == "Placed Item")
        {
            if (willExplode)
            {
                LawnmowerExplode();
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }

        if (col.gameObject.name == "Rake(Clone)")
        {
                Destroy(gameObject.transform.parent.gameObject);
                LawnmowerExplode();
                //do explosion
        }     
    }

    private void OnTriggerStay(Collider col)
    {
        // if item is unplaced then dont run script
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (col.gameObject.name == "Lighter Hitbox")
        {
            if (col.gameObject.GetComponent<TrapBase>().activateTrap)
            {
                willExplode = true;
                activateTrap = true;
                StartCoroutine(DelayLawnMowerExplode());
                fireEffect.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator DelayLawnMowerExplode()
    {
        yield return new WaitForSeconds(lawnmowerExplodeDelay);
        LawnmowerExplode();
    }
}

// old destroy and repair code
/*
   int maxHealth = 2;
        health = maxHealth;
    int health;
    int breakCounter;
    int repairCounter;
    bool onCooldown;

        if (health == 0)
        {
            repairCounter = 0;
            onCooldown = true;
            if (breakCounter < 1)
            {
                breakCounter++;
            
                gameManager.audioManager.LawnMowerBreak();
            }
        }

        if (onCooldown)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                onCooldown = false;
                repairItemEffect.SetActive(true);
            }
        }

        if (repairItem)
        {
            health = maxHealth;
            timer = 0;
            breakCounter = 0;
            repairItemEffect.SetActive(false);
            if (repairCounter < 1)
            {
                repairCounter++;
                gameManager.audioManager.LawnMowerRepair();
            }
        }
    void SmokeEffect()
    {
        if (itemBroken)
            smokeEffect.SetActive(true);
        else
            smokeEffect.SetActive(false);
    }
*/