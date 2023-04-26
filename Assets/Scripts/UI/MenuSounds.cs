using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{

    [SerializeField] AudioSource audioSourceObject;

    public void StartMusic()
    {
        audioSourceObject.Play();
    }

    public void HamsterSlice()
    {
        GameManager.instance.audioManager.WindmillAudio();
    }

    public void HamsterSqueal()
    {
        GameManager.instance.audioManager.PlayHamsterDeathAudio();
    }
}
