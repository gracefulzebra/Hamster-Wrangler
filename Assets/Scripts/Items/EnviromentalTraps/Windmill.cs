using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : EnvironmentalBase
{

    private void OnTriggerEnter(Collider col)
    {
            if (col.CompareTag("Hamster"))
            {
                AddScore();
                ItemInteract(col.gameObject);
                GameManager.instance.audioManager.WindmillAudio();
                col.transform.GetComponent<HamsterBase>().Kill();
            }
    }
}
