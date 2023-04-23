using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfWater : EnvironmentalBase
{
    [SerializeField] ParticleSystem waterSplash;


    IEnumerator DeleteWater(ParticleSystem water)
    {
        yield return new WaitForSeconds(2f);
        Destroy(water);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Hamster"))
        {
            ParticleSystem waterSplashObject = Instantiate(waterSplash, col.transform.position, Quaternion.LookRotation(transform.up));
            print(col.transform.position);
            StartCoroutine(DeleteWater(waterSplashObject));

            AddScore();
            ItemInteract(col.gameObject);
            GameManager.instance.audioManager.WaterSplashAudio();
            col.GetComponent<HamsterBase>().WaterDeath();
        }
    }
}
