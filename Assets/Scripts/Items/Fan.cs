using UnityEngine;

public class Fan : TrapBase
{
    [Header("Particle Effects")]
    [SerializeField] GameObject windEffect;
    [SerializeField] GameObject flameThrowerEffect;

    [Header("Generic Values")]
    [SerializeField] float force;
    [SerializeField] float leafblowerDuration;

    [Header("Flamethrower")]
    bool flameThrower;
    float leafblowerTimer;
    private float burnDuration = 2f; //Time between instances of burn damage
    private int burnAmount = 5; //Amount of instances of burn damage

    private void Start()
    {
        itemID = "LeafBlower";
    }
   
    private void Update()
    {
        LeafblowerActivation();

        if (hasFuel == false)
        {
            canUseTrap = true;
            activateTrap = false;
            refuelSymbol.SetActive(true);
            // resets timer
            leafblowerTimer = 0f;
        }
    }

    void LeafblowerActivation()
    {
        if (activateTrap)
        {
            // used so leafblower cannot be activated if already on
            canUseTrap = false;
            leafblowerTimer += Time.deltaTime;

            SliderUpdate();
            UseFuel();
            windEffect.SetActive(true);

            if (leafblowerTimer > leafblowerDuration)
            {
                canUseTrap = true;
                activateTrap = false;
                leafblowerTimer = 0f;
            }

            if (flameThrower)
            {
                flameThrowerEffect.SetActive(true);
            }
        }
        else
        {
            windEffect.SetActive(false);
            // when trap is decativated it ensures it doesnt wake up as lawnmower
            flameThrower = false;
            flameThrowerEffect.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
       if (activateTrap)
       {
        // for hamster collisions
         if (col.gameObject.name == "Hamster 1(Clone)")
         {
            Vector3 direction = transform.position - transform.parent.position;

            col.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);

            //Communicates that item has interacted with the hamster and what type it is.
            ItemInteract(col.gameObject);

               if (flameThrower)
               {
                   col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                  // ItemInteract(col.gameObject);
               } 
         }
            // for none hamster collision
            else
            {
                // for leafblower + lighter interaction
                if (col.gameObject.GetComponent<TrapBase>().itemID == "Lighter" && col.gameObject.GetComponent<Lighter>().activateTrap)
                {
                    flameThrower = true;
                }
                else
                {
                    flameThrower = false;
                }
            }
       }
    }
}

