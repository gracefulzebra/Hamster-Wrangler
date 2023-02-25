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
   public bool flameThrower;
    float leafblowerTimer;
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    bool audioOn = false;

    private void Start()
    {
        itemID = "LeafBlower";
    }
   
    private void Update()
    {
        LeafblowerActivation();
        OutOfFuel();
    }

    void LeafblowerActivation()
    {
        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.LeafBlowerUse();
                audioOn = true;
            }

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
                audioOn = false;
                leafblowerTimer = 0f;
            }

            if (flameThrower)
            {
                flameThrowerEffect.SetActive(true);
            }
            else
            {
                flameThrowerEffect.SetActive(false);
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

    void OutOfFuel()
    {
        if (hasFuel == false)
        {
            canUseTrap = true;
            activateTrap = false;
            refuelSymbol.SetActive(true);
            // resets timer
            leafblowerTimer = 0f;
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
                   //ItemInteract(col.gameObject);
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

