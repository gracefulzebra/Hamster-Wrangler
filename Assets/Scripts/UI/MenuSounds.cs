using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceObject;

    public void StartMainMenuMusic()
    {
        audioSourceObject.Play();
    }

    public void MainMenuSlice()
    {
        GameManager.instance.audioManager.WindmillAudio();
    }
}
