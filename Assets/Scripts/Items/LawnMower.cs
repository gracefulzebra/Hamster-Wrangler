using System.Collections;
using UnityEngine;

public class LawnMower : TrapBase
{

    int maxHealth = 2;
    int health;
    int breakCounter;
    int repairCounter;
    bool onCooldown;
    [SerializeField] GameObject repairItemEffect;
    [SerializeField] GameObject smokeEffect;
    [SerializeField] float lawnmowerDestroyDelay;
    [SerializeField] bool activateLawnmower;

    private void Start()
    {
        itemID = "LawnMower";
        health = maxHealth;
    }

    private void Update()
    {


        SliderUpdate();

        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

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

        SmokeEffect();

        if (activateLawnmower)
        {
            MoveForward();
            StartCoroutine(DestroyLawnmower());
        }
    }

    IEnumerator DestroyLawnmower()
    {
        yield return new WaitForSeconds(lawnmowerDestroyDelay);
        Destroy(gameObject.transform.parent.gameObject);
    }

    void SmokeEffect()
    {
        if (itemBroken)
            smokeEffect.SetActive(true);
        else
            smokeEffect.SetActive(false);
    }


    private void OnTriggerStay(Collider collision)
    {
        // if item is unplaced then dont run script
       if (GetComponentInParent<SnapToGrid>().hasItem)
            return;
        if (itemBroken)
            return;  
        health--;
        Durability(health);
        ItemInteract(collision.gameObject);
        collision.gameObject.GetComponent<HamsterBase>().Kill();

        if (activateLawnmower && collision.gameObject.GetComponent<TrapBase>().itemID == "Rake")
        {
            Destroy(gameObject.transform.parent);
            //do explosion
        }
    }

    void MoveForward()
    {
        transform.parent.Translate(Vector3.forward * 5 * Time.deltaTime);
    }
}
