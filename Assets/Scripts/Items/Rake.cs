using System.Collections;
using UnityEngine;

public class Rake : TrapBase
{ 
   [SerializeField] GameObject activationButton;
   [SerializeField] GameObject rakeObject;
   [SerializeField] GameObject pivotPoint;

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
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            gameObject.layer = 0;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }

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
            GameManager.instance.audioManager.PlayUsedRake();
            StartCoroutine(PlayAnimation());
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
            if (col.gameObject.name == "Hamster 1(Clone)" || col.gameObject.name == "Lawnmower(Clone)" && !col.GetComponent<SnapToGrid>().hasItem)
            {
                Rigidbody hamsterRB = col.GetComponent<Rigidbody>();
                
                hamsterRB.velocity = CalculateVel(col.transform);
            }

            if (col.gameObject.name == "Hamster 1(Clone)" && inProgress)
            {
                if (col.GetComponent<ItemEffects>().hasBeenShocked)
                {
                    // call funciton that activates bool 
                    col.GetComponent<ItemEffects>().StartLightingAOE();
                }
            }
        }     
    }

    private IEnumerator PlayAnimation()
    {
        int temp = 0;

        for (int i = 0; i < 90; i++)
        {
            float angle;
            angle = i * 0.01745329251f;
            rakeObject.transform.RotateAround(pivotPoint.transform.position, transform.right, angle);
            yield return new WaitForSeconds(0.00025f);

            temp = i;
        }
        for (int j = temp; j >= 0; j--)
        {
            float angle;
            angle = j * 0.01745329251f;
            rakeObject.transform.RotateAround(pivotPoint.transform.position, transform.right, -angle);
            yield return new WaitForSeconds(0.005f);
        }

    }
}

