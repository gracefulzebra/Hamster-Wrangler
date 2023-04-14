using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public float volume;

    [SerializeField] GameObject audioObject;

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
    [SerializeField] private AudioClip lbActive;

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
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        StartCoroutine(DeleteAudio(audio));
        switch (Random.Range(1, 5)) //Max exclusive
        {

            case 1:
                 audio.GetComponent<AudioSource>().PlayOneShot(hamsterDeath1);
                break;

            case 2:
                audio.GetComponent<AudioSource>().PlayOneShot(hamsterDeath2);
                break;

            case 3:
                audio.GetComponent<AudioSource>().PlayOneShot(hamsterDeath3);
                break;

            case 4:
                audio.GetComponent<AudioSource>().PlayOneShot(hamsterDeath4);
                break;
        }
    }

    public void PlayUsedRake()
    {     
            GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
            audio.GetComponent<AudioSource>().PlayOneShot(rake1);
            StartCoroutine(DeleteAudio(audio));
    }

    public void LighterOn()
    {
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        audio.GetComponent<AudioSource>().PlayOneShot(lighterOn);
        StartCoroutine(DeleteAudio(audio));
    }


    public void ItemPlacedAudio()
    {
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        audio.GetComponent<AudioSource>().PlayOneShot(trapPlaced);
        StartCoroutine(DeleteAudio(audio));
    }


    // adds to last that is used in lawnmower script
    public List<GameObject> lmRunList;
    public void LawnMowerRunAudio()
    {
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        audio.GetComponent<AudioSource>().PlayOneShot(lmRun);
        lmRunList.Add(audio);
        StartCoroutine(DeleteAudio(audio));
    }

    public void LawnMowerExplodeAudio()
    {
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        audio.GetComponent<AudioSource>().PlayOneShot(lmExplode);
        StartCoroutine(DeleteAudio(audio));
    }

    public List<GameObject> lbSoundList;
    public void LeafBlowerUse()
    {
        GameObject audio = Instantiate(audioObject, Vector3.zero, Quaternion.identity);
        lbSoundList.Add(audio);
        audio.GetComponent<AudioSource>().PlayOneShot(lmRun);
        StartCoroutine(DeleteAudio(audio));
    }

    float deleteAudio = 10;

    IEnumerator DeleteAudio(GameObject audio)
    {
        yield return new WaitForSeconds(deleteAudio);
        Destroy(audio);
    }
}
