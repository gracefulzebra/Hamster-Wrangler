using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Rake : TrapBase
{ 
   [SerializeField] GameObject activationButton;

    public Transform target;

    public float height;
    public float gravity;

    [SerializeField] private int rakeActivationCounter;
    [SerializeField] private float rakeActivationDuration = 0.25f;
    [SerializeField] private float rakeFlingDelay = 2;
    bool inProgress = false;

    private bool rakeEnabled = false;

    private void Start()
    {
        itemID = "Rake";
    }
    private void Update()
    {
        if (activateTrap && !inProgress)
        {
            activateTrap = false;
            StartCoroutine(Fling());
        }
    }

    IEnumerator Fling()
    {
        inProgress = true;
        for(int counter = 0; counter < rakeActivationCounter; counter++)
        {
            rakeEnabled = true;
           
            yield return new WaitForSeconds(rakeActivationDuration);
            rakeEnabled = false;

            yield return new WaitForSeconds(rakeFlingDelay);
        }
        inProgress = false;
        
    } 

    private Vector3 CalculateVel(Transform currentPos)
    {
        float displacementY = target.position.y - currentPos.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - currentPos.position.x, 0, target.position.z - currentPos.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        return 2 * velocityXZ + velocityY * -Mathf.Sign(gravity);
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        if (rakeEnabled)
        {           
            if (col.gameObject.name == "Hamster 1(Clone)")
            {
                Rigidbody hamsterRB = col.GetComponent<Rigidbody>();
                
                hamsterRB.velocity = CalculateVel(col.transform);
            }
        }
        
    }
}

