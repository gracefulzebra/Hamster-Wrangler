using UnityEngine;

public class Lighter : TrapBase
{

    [Header("Particle Effects")]
    [SerializeField] GameObject fireEffect;

    [Header("Generic Values")]
    [SerializeField] private float burnDuration; //Time between instances of burn damage
    [SerializeField] private int burnAmount; //Amount of instances of burn damage

    bool audioOn = false;

    private void Start()
    {
        itemID = "Lighter";

        //Physics.IgnoreLayerCollision(0, 9);
    }

    private void Update()
    {
        if (GetComponentInParent<SnapToGrid>().hasItem == false && !onPlacement)
        {
            onPlacement = true;
            GetComponentInParent<Rigidbody>().useGravity = true;
        }
        if (activateTrap)
        {
            if (!audioOn)
            {
                GameManager.instance.audioManager.LighterOn();
                audioOn = true;
            }

            UseFuel();
            fireEffect.SetActive(true);
            SliderUpdate();
        }
        else
        {
            fireEffect.SetActive(false);
        }

        if (hasFuel == false)
        {
            refuelSymbol.SetActive(true);
            activateTrap = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (activateTrap)
        {        
            if (col.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().OnFire(damage, burnDuration, burnAmount);
                ItemInteract(col.gameObject);
            }  
        }
    }
}
/*
            if (GetComponentInParent<SnapToGrid>().hasItem == true)
            return;

        // if lighter isnt on, starts timer to activate it 
        if (!lighterOn)
        {
            counter = 0;
            timer += Time.deltaTime;
            lighterCooldown = 0f;
            fireEffect.SetActive(false);
        }
        // if timer is on, timer starts till it turn off
        else
{
    if (counter < 1)
    {
        counter++;
        gameManager.audioManager.LighterOn();
    }
    fireEffect.SetActive(true);
    lighterCooldown += Time.deltaTime;
}

if (timer > timerMax)
{
    lighterOn = true;
    timer = 0;
}

if (lighterCooldown > lighterTimerMax)
{
    lighterOn = false;
}
*/

