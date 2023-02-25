using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BugZapper : TrapBase
{

    [SerializeField] GameObject activationEffect;
    [SerializeField] int chargeCount;
    float cooldownTimer;
    [SerializeField] float cooldownTimerMax;
    [SerializeField] float hamsterShockRadius;
    [SerializeField] Slider rechargeSlider;
    int trapActivatrionCounter;

    bool startCooldown;

    private void Start()
    {
        itemID = "BugZapper";
        rechargeSlider.maxValue = cooldownTimerMax;
        rechargeSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(activateTrap)
        {
            canUseTrap = false;
            startCooldown = true;
            // for weird zap effect

            if (trapActivatrionCounter == 0)
            {
                trapActivatrionCounter++;
                activationEffect.SetActive(true);
                StartCoroutine(Unactivate());
            }
        }
        CooldownTimer();
    }

    void CooldownTimer()
    {

        if (startCooldown)
        {

            rechargeSlider.gameObject.SetActive(true);
            rechargeSlider.value = cooldownTimer;
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer > cooldownTimerMax)
            {

                canUseTrap = true;
                startCooldown = false;
                activateTrap = false;

                cooldownTimer = 0;
                trapActivatrionCounter = 0;

                chargeCount--;
                rechargeSlider.gameObject.SetActive(false);
            }
        }

        if (chargeCount == 0)
        {
            canUseTrap = false;
            activateTrap = false;
            refuelSymbol.SetActive(true);
        }
    }

    // for zap effecr
    IEnumerator Unactivate()
    {
        yield return new WaitForSeconds(0.2f);
        activationEffect.SetActive(false);
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

    private void OnTriggerStay(Collider col)
    {
        if (GetComponentInParent<SnapToGrid>().hasItem)
            return;

        // if the item is active
        if (activateTrap && chargeCount !=0)
        {
            if (col.CompareTag("Hamster"))
            {
                col.gameObject.GetComponent<ItemEffects>().BeenElectrocuted();
                activateTrap = false;
            }
        }
    }
}