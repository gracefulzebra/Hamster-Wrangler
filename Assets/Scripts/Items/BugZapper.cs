using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BugZapper : TrapBase
{

    [Header("Misc")]
    [SerializeField] ParticleSystem activeEffect;
    [SerializeField] Slider rechargeSlider;
    
    [Header("Activation")]
    int trapActivatrionCounter;
    bool activatedTrap;

    [Header("Line Renderer")]
    [SerializeField] private LayerMask scannableMask;
    private int objectsShockedCounter = 0;
    [SerializeField] private int maxObjectsShocked;
    private List<GameObject> shockedObjects;
    private LineRenderer lR;
    [SerializeField] LineRenderer lrPrefab;

    [Header("Adjustable Value")]
    [SerializeField] private float hamsterShockRadius;
    [SerializeField] float lightingAOERange;
    [SerializeField] float shockDuration;

    [SerializeField] GameObject mat;

    private void Start()
    {
        itemID = "BugZapper";
      //  rechargeSlider.maxValue = cooldownTimerMax;
      //  rechargeSlider.gameObject.SetActive(false);


        shockedObjects = new List<GameObject>();
        hamsterShockRadius += 0.5f;

        //Physics.IgnoreLayerCollision(0, 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }
        FuelAndActivation();
    }

    void FuelAndActivation()
    {
      
        ChangeSliderColour();

        if (activateTrap)
        {
            if (!GetComponentInParent<SnapToGrid>().hasItem && chargeCount != 0)
            {
                canUseTrap = false;
                //   startCooldown = true;
                activatedTrap = true;

                ScanForFirstTarget();
                if (trapActivatrionCounter == 0)
                {
                    trapActivatrionCounter++;
                }
            }
        }
        if (activatedTrap)
        {
            UseFuel1();
            if (useFuelTimer > timeTrapActivePerCharge)
            {
                activatedTrap = false;
            }
        }
        else if (!canUseTrap && !activatedTrap)
        {
            RechargeFuel1();
            trapActivatrionCounter = 0;
        }

        if (chargeCount == 0 && refuelTimer > rechargeDuration)
        {
            canUseTrap = false;
            activateTrap = false;
            refuelSymbol.SetActive(true);
        }
    }
    protected void UseFuel1()
    {
        if (!rechargeFuel)
        {
            useFuelTimer += Time.deltaTime;
            refuelTimer = 0;

            if (useFuelTimer >= timeTrapActivePerCharge)
            {
                mat.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                activeEffect.Stop();
                activateTrap = false;
                rechargeFuel = true;
            }
        }
    }

    protected void RechargeFuel1()
    {
        if (rechargeFuel)
        {
            refuelTimer += Time.deltaTime;
            useFuelTimer = 0;
            if (refuelTimer >= rechargeDuration)
            {
                activeEffect.Play();
                mat.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                // trap can be used
                canUseTrap = true;
                // trap no longer needs to be fueled
                rechargeFuel = false;
            }
        }
    }
    // for refueling
    public void ReactiveTrap()
    {
        if (GameManager.instance.currencyManager.RepairItemCost() == true)
        {
            canUseTrap = true;
            chargeCount = 2;
            refuelSymbol.SetActive(false);
        }
    }

    private void ScanForFirstTarget()
    {
        shockedObjects.Clear();

        activateTrap = false;
        objectsShockedCounter = 0;
        ResetInteracts();

        initializeLineRenderer();

        Vector3 currentPos = transform.position;

        //Find all objects in a radius with the scannable layerMask
        RaycastHit[] nearbyObjects = Physics.SphereCastAll(currentPos, hamsterShockRadius, Vector3.up, hamsterShockRadius, scannableMask);

        if(nearbyObjects.Length > 0)
        {
            objectsShockedCounter++;

            RaycastHit closestObject;

            closestObject = nearbyObjects[0];

            for(int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].distance < closestObject.distance)
                {
                   closestObject = nearbyObjects[i];
                }
            }
        
            shockedObjects.Add(closestObject.transform.gameObject);
            PerformZapEffect(closestObject.transform.gameObject); //Perform necessary effects for object type
            ScanForNextTarget(closestObject.transform.position); //Scan for nextObject
        }
        StartCoroutine(shockVisual());
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, hamsterShockRadius);
    }
    private void ScanForNextTarget(Vector3 scanningPos)
    {
        if (objectsShockedCounter >= maxObjectsShocked)
            return;
        
        objectsShockedCounter++;

        //Find all objects in a radius with the scannable layerMask
        RaycastHit[] nearbyObjects = Physics.SphereCastAll(scanningPos, hamsterShockRadius, Vector3.up, hamsterShockRadius, scannableMask);

        //If there are objects scanned : Find closest
        if (nearbyObjects.Length > 0)
        {
            RaycastHit closestObject = new RaycastHit();

            closestObject.distance = 1000;

            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (!shockedObjects.Contains(nearbyObjects[i].transform.gameObject)) //Check 
                {
                    if (nearbyObjects[i].distance < closestObject.distance)
                    {
                        closestObject = nearbyObjects[i];
                    }
                }                          
            }

            if (closestObject.distance == 1000)
                return;

            if (shockedObjects.Contains(closestObject.transform.gameObject))
                return;
            

            shockedObjects.Add(closestObject.transform.gameObject);
            PerformZapEffect(closestObject.transform.gameObject);
            ScanForNextTarget(closestObject.transform.position);
        }
    }

    private void PerformZapEffect(GameObject targetObject)
    {
        if(targetObject.CompareTag("Hamster"))
        {
            targetObject.GetComponent<ItemEffects>().BeenElectrocuted(shockDuration, damage, hamsterShockRadius, lightingAOERange);
            targetObject.GetComponent<HamsterScore>().UpdateInteracts(this.gameObject, itemID, trapActivatrionCounter);
        }
        else if(targetObject.name == "Leafblower(Clone)")
        {
            IncrementTrapInteracts(targetObject);
            targetObject.GetComponentInChildren<Fan>().IncrementTrapInteracts(this.gameObject);
            targetObject.GetComponentInChildren<Fan>().overCharge = true;
        }
    }

    private void initializeLineRenderer()
    {
        lR = new LineRenderer();
        lR = Instantiate(lrPrefab, this.transform);
        lR.positionCount = 0;
    }

    private IEnumerator shockVisual()
    {
        Vector3[] linePoints;

        linePoints = new Vector3[shockedObjects.Count + 1];
        linePoints[0] = transform.position;
        
        for(int i = 0; i < shockedObjects.Count; i++)
        {

            linePoints[i + 1] = shockedObjects[i].transform.position; 
        }
        
        lR.positionCount = linePoints.Length;
        lR.SetPositions(linePoints);

        yield return new WaitForSeconds(0.2f);

        Destroy(lR);

     
    }
}