using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LawnMower : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject smokeEffect;

    [Header("Adjustabele")]
    [SerializeField] int noOfHamstersBeforeReduction;
    [SerializeField] int limitDamageReduction;
    [SerializeField] float lawnmowerSpd;
    [SerializeField] int dmgReductionPerHit;
    float lawnmowerDestroyDelay;
    int hamsterCounter;

    int counter = 0;
    bool willExplode;

    [Header("References")]
    GameObject gridRefObject;
    GridGenerator gridRef;
    [SerializeField] GameObject explosion;
    Node nodeHit;
    [SerializeField] Animator animator;

    [Header("Explosion")]
    [SerializeField] int explosiondamage;
    [SerializeField] float explosionRange;
    [SerializeField] private LayerMask scannableMask;
    [SerializeField] float lawnmowerExplodeDelay;


    private void Start()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
        itemID = "LawnMower";

        //Physics.IgnoreLayerCollision(0, 9);
    }

    private void Update()
    {
        // this is weird! need to turn gravity off for placement but need it for lawnmower launch
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }

        if (activateTrap)
        {
            if (counter < 1)
            {
                canUseTrap = false;
                // finds the cloest node for the player and makes it placeable
                nodeHit = gridRef.GetNodeFromWorldPoint(transform.position);
                nodeHit.placeable = true;
                counter++;
            }
        }
    }

    GameObject lmRunObject;
    public void ActivateLawnmower()
    {
        if (!audioOn)
        {
            GameManager.instance.audioManager.LawnMowerRunAudio();
            // assigns to gameobject that is destroyed when hits wall
            lmRunObject = GameManager.instance.audioManager.lmRunList.Last();
            audioOn = true;
        }

        animator.SetTrigger("Start");
        //sets lawn mower to ignore raycast
        transform.parent.gameObject.layer = 2;
        smokeEffect.SetActive(true);
        transform.parent.Translate(Vector3.forward * lawnmowerSpd * Time.deltaTime);
    }

    IEnumerator DelayLawnMowerExplode()
    {
        yield return new WaitForSeconds(lawnmowerExplodeDelay);
        LawnmowerExplode();
    }

    void LawnmowerExplode()
    {
        Destroy(lmRunObject);
        GameManager.instance.audioManager.LawnMowerExplodeAudio();

        RaycastHit[] nearbyObjects = Physics.SphereCastAll(transform.position, explosionRange, Vector3.up, explosionRange, scannableMask);

        if (nearbyObjects.Length > 0)
        {
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                Explosion(nearbyObjects[i].transform.gameObject);
            }
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }

    void Explosion(GameObject targetObject)
    {
        if (targetObject.transform.CompareTag("Hamster"))
        {
            ItemInteract(targetObject);
            targetObject.GetComponent<ItemEffects>().InExplosionRadius(explosiondamage);
        }
    }

    List<GameObject> noOfHamsters = new List<GameObject>();

    private void OnTriggerStay(Collider col)
    {
        // if item is unplaced then dont run script
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (col.GetComponent<TrapBase>() != null)
        { 
            if (col.gameObject.GetComponent<TrapBase>().itemID == "Lighter" && col.gameObject.GetComponent<TrapBase>().activateTrap)
            {
                if (!synergyDisplay)
                {
                    synergyDisplay = true;
                    GameObject temp = Instantiate(comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
                    temp.GetComponent<ComboDisplay>().SetComboText("LAWNSPLOSION!");
                }

                IncrementTrapInteracts(col.gameObject);
                willExplode = true;
                activateTrap = true;
                StartCoroutine(DelayLawnMowerExplode());
            }
            if(col.gameObject.GetComponent<TrapBase>().itemID == "LeafBlower" && col.gameObject.GetComponent<TrapBase>().activateTrap && col.gameObject.GetComponentInChildren<Fan>().flameThrower)
            {
                IncrementTrapInteracts(col.gameObject);
                willExplode = true;
                activateTrap = true;
                StartCoroutine(DelayLawnMowerExplode());
            }
        
        }

            if (!activateTrap)
            return;

        if (col.CompareTag("Hamster"))
        {
            // reduces damage of hamster per hit
            if (!noOfHamsters.Contains(col.gameObject))
            {
                hamsterCounter++;
                ItemInteract(col.gameObject);
                col.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
                if (damage > limitDamageReduction && hamsterCounter >= noOfHamstersBeforeReduction)
                {
                    damage -= dmgReductionPerHit;
                }
                noOfHamsters.Add(col.gameObject);
            }
        }

        // obstacle
        if (col.gameObject.layer == 7)
        {
            Destroy(lmRunObject);
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

        // on ground
        if (col.gameObject.layer == 6 && activateTrap)
        {
                ActivateLawnmower();    
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