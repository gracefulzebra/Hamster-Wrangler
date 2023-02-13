using UnityEngine;

public class Lighter : TrapBase
{

    [SerializeField] ParticleSystem fireEffect;

    private void Start()
    {
        itemID = "Lighter";
    }

    private void Update()
    {
        if (activateTrap)
        {
            UseFuel();
           // fireEffect.Play();
            SliderUpdate();
        }

        if (hasFuel == false)
        {
            refuelSymbol.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (activateTrap)
         {
            if (col.gameObject.tag == "Flammable")
            {
                col.gameObject.GetComponent<ItemEffects>().OnFire();
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

