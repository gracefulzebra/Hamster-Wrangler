using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LawnMower : TrapBase
{

    [SerializeField] GameObject smokeEffect;
    [SerializeField] float lawnmowerDestroyDelay;
    [SerializeField] float lawnmowerSpd;
    [SerializeField] GameObject explosion;
    GameObject gridRefObject;
    GridGenerator gridRef;

    Node nodeHit;

    int counter;

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
            MoveForward();
            StartCoroutine(DestroyLawnmower());
        }
    }

    void MoveForward()
    {
        transform.parent.Translate(Vector3.forward * lawnmowerSpd * Time.deltaTime);
    }

    IEnumerator DestroyLawnmower()
    {
        yield return new WaitForSeconds(lawnmowerDestroyDelay);
        Destroy(gameObject.transform.parent.gameObject);
    }

   public void ActivateLawnmower()
   {
        smokeEffect.SetActive(true);
        StartCoroutine(DestroyLawnmower());
        activateTrap = true;
   }

    void LawnmowerExplode()
    {
        nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
        Vector3 explosionPos = new Vector3(nodeHit.worldPosition.x, nodeHit.worldPosition.y - 0.5f, nodeHit.worldPosition.z); ;
        Instantiate(explosion, explosionPos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // if item is unplaced then dont run script
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (!activateTrap)
            return;

        if (collision.gameObject.layer == 7) //|| collision.gameObject.tag == "Placed Item")
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        if (collision.gameObject.name == "Hamster 1(Clone)")
        {
            ItemInteract(collision.gameObject);
            collision.gameObject.GetComponent<HamsterBase>().Kill();
        }

        if (collision.gameObject.name == "Rake(Clone)")
        {
                Destroy(gameObject.transform.parent.gameObject);
                LawnmowerExplode();
                //do explosion
        }
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