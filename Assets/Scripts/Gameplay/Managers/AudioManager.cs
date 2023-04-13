using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public float volume;

    [Header("Hamster Audio")]
    [SerializeField] private AudioClip hamsterDeath1;
    [SerializeField] private AudioClip hamsterDeath2;
    [SerializeField] private AudioClip hamsterDeath3;
    [SerializeField] private AudioClip hamsterDeath4;

    [Header("Lighter")]
    [SerializeField] private AudioClip lighterOn;

    [Header("LawnMower")]
    [SerializeField] private AudioClip lmRun;
    [SerializeField] private AudioClip lmExplode;

    [Header("Rake")]
    [SerializeField] private AudioClip rake1;
    [SerializeField] private AudioClip rake2;
    [SerializeField] private AudioClip rake3;

    [Header("LeafBlower")]
    [SerializeField] private AudioClip lfBlower;

    [SerializeField] private AudioClip trapPlaced;

    [SerializeField] private AudioClip music;

    [SerializeField] AudioMixer audioMixer;

    public const string MIXER_MUSIC = "Music";
   public const string MIXER_SFX = "SFX";

    // these need to be in here for some reason? mayeb caus ein awake
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    private void Awake()
    {
       // audioSource = GetComponent<AudioSource>();

      musicSlider.onValueChanged.AddListener(SetMusicVolume);
      sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // spawn in objects with audio source, pass in what noise wen want, and then delete 
        // need to have aduio manager delete the instances 
    }

    private void Start()
    {
       musicSlider.value = PlayerPrefs.GetFloat(GameManager.MUSIC_KEY, 1f);
       sfxSlider.value = PlayerPrefs.GetFloat(GameManager.SFX_KEY, 1f);
    }

    void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    public void SetVolume(float volumeLevel)
    {
        volume = volumeLevel / 5;
        audioSource.volume = volume;
    }

    public void PlayHamsterDeathAudio()
    {
        return;
        switch(Random.Range(1, 5)) //Max exclusive
        {
            case 1:
                audioSource.PlayOneShot(hamsterDeath1, volume);
                break;

            case 2:
                audioSource.PlayOneShot(hamsterDeath2, volume);
                break;

            case 3:
                audioSource.PlayOneShot(hamsterDeath3, volume);
                break;

            case 4:
                audioSource.PlayOneShot(hamsterDeath4, volume);
                break;
        }
    }

    public void PlayUsedRake()
    {
        return;
        switch (Random.Range(1, 4)) //Max exclusive
        {
            case 1:
                audioSource.PlayOneShot(rake1, volume);
                break;

            case 2:
                audioSource.PlayOneShot(rake2, volume);
                break;

            case 3:
                audioSource.PlayOneShot(rake3, volume);
                break;
        }
    }

    public void LighterOn()
    {
        //audioSource.PlayOneShot(lighterOn, volume * 2);
        
    }

    public void ItemPlacedAudio()
    {
      //  audioSource.PlayOneShot(trapPlaced, volume / 1.5f);
    }

    public void LawnMowerRunAudio()
    {
        //audioSource.PlayOneShot(lmRun, volume / 2f);
    }

    public void LawnMowerExplodeAudio()
    {
      //  audioSource.PlayOneShot(lmExplode, volume / 1.5f);
    }

    public void LeafBlowerUse()
    {
     //   audioSource.PlayOneShot(lfBlower, volume / 2f);
    }

    public void PlayMusic()
    {
      //  audioSource.PlayOneShot(music, volume / 8);
    }//
}
