using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutLawnmower : TrapBase
{

    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;
    [SerializeField] GameObject smokeEffect;

    [Header("Generic Values")]
    [SerializeField] float lawnmowerSpd;
    [SerializeField] int dmgReductionPerHit;
    int counter = 0;
    bool willExplode;

    [Header("References")]
    GameObject gridRefObject;
    GridGenerator gridRef;
    [SerializeField] GameObject explosion;
    Node nodeHit;

    [Header("Explosion")]
    [SerializeField] float explosionRange;
    [SerializeField] private LayerMask scannableMask;
    [SerializeField] float lawnmowerExplodeDelay;
    [SerializeField] Animator animator;


    private void Start()
    {
        gridRefObject = GameObject.Find("OliverGriddy");
        gridRef = gridRefObject.GetComponent<GridGenerator>();
        itemID = "LawnMower";
    }

    private void Update()
    {
        // this is weird! need to turn gravity off for placement but need it for lawnmower launch
        if (GetComponentInParent<BaseSnapToGrid>().hasItem == false && !onPlacement)
        {
            transform.parent.gameObject.layer = 0;
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


        // step 10ish
        GameObject hamster = GameObject.FindGameObjectWithTag("Hamster");
       
        if (hamster != null && TutManager.tutInstance.posCounter == 9| TutManager.tutInstance.posCounter == 10)
        {
            float hamsterDistance = (transform.position - hamster.transform.position).magnitude;
            // make public bool somewhere, will need to be read in from bugzapper for contiuinity probs
            if (hamsterDistance < 4)
            {
                // only works for poscounter10
                TutManager.tutInstance.LerpTimeDown();
            }
        }         
    }


    GameObject lmRunObject;
    bool once = false;
    public void ActivateLawnmower()
    {
        if (!audioOn)
        {
            GameManager.instance.audioManager.LawnMowerRunAudio();
            // assigns to gameobject that is destroyed when hits wall
            lmRunObject = GameManager.instance.audioManager.lmRunList.Last();
            audioOn = true;
        }

        if (!once)
        {
            animator.SetTrigger("Start");
            once = true;
        }
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
            targetObject.GetComponent<ItemEffects>().InExplosionRadius(damage);
        }
    }

    GameObject interactedHamster = null;

    private void OnTriggerStay(Collider col)
    {
        // if item is unplaced then dont run script
        if (GetComponentInParent<BaseSnapToGrid>().hasItem)
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
                willExplode = true;
                activateTrap = true;
                StartCoroutine(DelayLawnMowerExplode());
            }

        }
        // checks if lighter is on
        if (!activateTrap)
            return;

        if (col.CompareTag("Hamster"))
        {
            // reduces damage of hamster per hit
            if (interactedHamster != col.gameObject)
            {
                ItemInteract(col.gameObject);
                col.gameObject.GetComponent<HamsterBase>().TakeDamage(damage);
                damage -= dmgReductionPerHit;
                interactedHamster = col.gameObject;
            }
        

        if (TutManager.tutInstance.posCounter == 11)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
            if (TutManager.tutInstance.posCounter == 19)
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
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

