using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PauseAudioObject : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
         if (Time.timeScale == 0)
        {
            audioSource.Pause();
        }
         else
        {
            audioSource.UnPause();
        }
    }
}
