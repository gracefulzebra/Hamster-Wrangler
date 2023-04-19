using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfWater : EnvironmentalBase
{
    [SerializeField] ParticleSystem waterSplash;

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Hamster"))
        {
            Instantiate(waterSplash, col.transform.position, Quaternion.identity);
            AddScore();
            ItemInteract(col.gameObject);
            GameManager.instance.audioManager.WaterSplashAudio();
            col.GetComponent<HamsterBase>().Kill();
        }
    }
}
