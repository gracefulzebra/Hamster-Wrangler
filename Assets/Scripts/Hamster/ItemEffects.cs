using System.Collections;
using System.Linq;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{

    [Header("Blow Torch")]
    [SerializeField] GameObject fireEffect;
    public bool onFire = false;
    private int burnIndex = 0;

    [Header("Bug Zapper")]
    float hamsterDistance;
    public bool hasBeenShocked;
    [SerializeField] float LightningAOEDistance;
    int electricDamage;
    float shockDur;
    float hamsterShockRad;
    bool canLightingAOE;
    [SerializeField] GameObject lightningEffect;
    [SerializeField] GameObject lightningStrikeEffect;
    float hamsterLightningAOERange;
    HamsterAnimation animationController;

    [SerializeField] LineRenderer lineRenderer;

    private void Awake()
    {
        animationController = GetComponent<HamsterAnimation>();
    }

    #region Fire

    GameObject onFireNoiseObject;
    bool burningAudioOn;

    ///<summary>
    /// called when hamster interacts with fire 
    ///</summary>
    public void OnFire(int burnDamage, float burnDuration, int burnAmount)
    {      
        fireEffect.SetActive(true);

        if (onFire)
        {
            burnIndex = 0;
        }
        else
        {
            StartCoroutine(burnToDeath(burnDamage, burnDuration, burnAmount));
        }
    }

    ///<summary>
    /// called when player is on fire to damage hamster
    ///</summary>
    IEnumerator burnToDeath(int burnDamage, float burnDuration, int burnAmount)
    {
        if (!burningAudioOn)
        {
            burningAudioOn = true;
        }
        onFire = true;
        for (burnIndex = 0; burnIndex < burnAmount; burnIndex++)
        {
            yield return new WaitForSeconds(burnDuration);
            GetComponent<HamsterBase>().TakeDamage(burnDamage);
        }     
        burningAudioOn = false;
        onFire = false;
        fireEffect.SetActive(false);
    }

    #endregion Fire

    #region BugZapper

    ///<summary>
    /// called when hamster has been hit by bugzapper
    ///</summary>
    public void BeenElectrocuted(float shockDuration, int electricDmg, float hamsterShockRadius, float lightningAOERange)
    {
        shockDur = shockDuration;
        electricDamage = electricDmg;
        hamsterShockRad = hamsterShockRadius;
        hamsterLightningAOERange = lightningAOERange;

        ElectricDamage();
    }
   
    public void ElectricDamage()
    {
        hasBeenShocked = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<HamsterBase>().speed = 0;
        GetComponent<HamsterBase>().TakeDamage(electricDamage);
        lightningEffect.SetActive(true);
        StartCoroutine(TurnOffShock());
    }

    public void StartLightingAOE()
    {
        // next time hamster collides with object itll call FinishLightningAOE()
        canLightingAOE = true;
    }

    // finds hamster in radius and shocks them
    void FinishLightningAOE()
    {
        GameObject temp = Instantiate(GetComponent<HamsterScore>().comboDisplayPrefab, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        temp.GetComponent<ComboDisplay>().SetComboText("HAMST-THOR!");

        Instantiate(lightningStrikeEffect, transform.position, Quaternion.identity);
        GameManager.instance.audioManager.HamsThorSound();

        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Hamster");

        foreach (GameObject hamster in gameObjectArray)
        {
            hamsterDistance = (transform.position - hamster.transform.position).magnitude;
            // make public bool somewhere, will need to be read in from bugzapper for contiuinity probs
            if (hamsterDistance < hamsterLightningAOERange)
            {
                hamster.GetComponent<ItemEffects>().ElectricDamage();
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(gameObject.transform.position, Vector3.one * hamsterShockRad);
    }

    ///<summary>
    /// called once hamster has been electrocuted and values need to be reset
    ///</summary>
    IEnumerator TurnOffShock()
    {
        animationController.SetShockedTrigger();
        yield return new WaitForSeconds(shockDur);
        hasBeenShocked = false;
        lightningEffect.SetActive(false);
        if (GetComponent<HamsterBase>().currentHealth > 0)
        {
            GetComponent<HamsterBase>().speed = GetComponent<HamsterBase>().maxSpeed;
        }
    }

    #endregion BugZapper

    bool dmgCounter;
    public void InExplosionRadius(int explosionDamage)
    {
        if (!dmgCounter)
        {
            dmgCounter = true;
            GetComponent<HamsterBase>().TakeDamage(explosionDamage);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // when coliddes with anythign finds hamster in radius 
        if (canLightingAOE && !col.transform.CompareTag("Hamster"))
        {
            FinishLightningAOE();
            canLightingAOE = false;
        }
    }
}
