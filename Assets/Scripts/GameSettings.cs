using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Audio;


public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public AudioMixer masterMixer;
    
    private float level1Score;
    public float Level1Score{ get { return level1Score; } set { level1Score = value; } }
    
    private float level2Score;
    public float Level2Score{ get { return level2Score; } set { level2Score = value; } }

    private float level3Score;
    public float Level3Score{ get { return level3Score; } set { level3Score = value; } }
    
    private float level4Score;
    public float Level4Score{ get { return level4Score; } set { level4Score = value; } }

    int displayMode;
    public int DisplayMode { get => displayMode; set => displayMode = value; }

    int qualitySetting;
    public int QualitySetting { get => qualitySetting; set => qualitySetting = value; }

    private float sfxVolume;
    public float SfxVolume { get => sfxVolume; set => sfxVolume = value; }

    private float musicVolume;
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }

    private const string DEFAULT_VALUE = "masterVolume=10,musicVolume=0.5,sfxVolume=0.5,quality=2,displayMode=0,level1Score=0,level2Score=0,level3Score=0,level4Score=0";

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        LoadSettings();
    }

    public void SaveSettings()
    {
        bool result;
        float sfxVol;
        float musicVol;
        result = masterMixer.GetFloat("SFX", out sfxVol);
        if (!result)
            return;
        result = masterMixer.GetFloat("Music", out musicVol);
        if (!result)
            return;
        MusicVolume = Mathf.Pow(10, musicVol / 20); 
        SfxVolume = Mathf.Pow(10, sfxVol / 20); 
        string text = "masterVolume=" + "10" + ",musicVolume=" + MusicVolume + ",sfxVolume=" + SfxVolume + ",quality=" + QualitySetting + ",displayMode=" + DisplayMode + ",level1Score=" + level1Score + ",level2Score=" + level2Score + ",level3Score=" + level3Score + ",level4Score=" + level4Score;

        //Debug.Log(text);
        File.WriteAllText(Application.dataPath + "/Resources/options.txt", text);
    }

    private void LoadSettings()
    {
        string saveString;
        // Get file as String
        try
        {
            saveString = File.ReadAllText(Application.dataPath + "/Resources/options.txt");
        }
        catch
        {
            SetDefaultSettings();
            LoadSettings();
            return;
        }
        // Remove Whitespace
        saveString = Regex.Replace(saveString, @"\s+", "");

        //Debug.Log("Save String: " + saveString);
        char[] listOfChar = saveString.ToCharArray();

        if (listOfChar.Length <= 1 || listOfChar == null || listOfChar.Length == 0)
        {
            SetDefaultSettings();
            LoadSettings();
            return;
        }

        List<string> values = new List<string>();
        string tempString = "";
        foreach (char character in listOfChar)
        {
            if (character == ',' || character == '=')
            {
                values.Add(tempString);
                tempString = "";
            }
            else
                tempString = tempString + character;
        }
        values.Add(tempString);

        // Master Volume
        if (values[0] == "masterVolume")
            masterMixer.SetFloat("MasterVolume", float.Parse(values[1]));
        else
            Debug.Log("MasterVolume not found in Game Settings.");

        if (values[2] == "musicVolume")
        {
            MusicVolume = float.Parse(values[3]);
            masterMixer.SetFloat("Music", Mathf.Log10(MusicVolume) * 20);
            //print(MusicVolume);
        }
        else
            Debug.Log("musicVolume not found in Game Settings.");
        if (values[4] == "sfxVolume")
        {
            SfxVolume = float.Parse(values[5]);
            masterMixer.SetFloat("SFX", Mathf.Log10(SfxVolume) * 20);
            //print(SfxVolume);    
        }
        else
            Debug.Log("SFXVolume not found in Game Settings.");
        if (values[6] == "quality")
            QualitySetting = int.Parse(values[7]);
        else
        {
            Debug.Log("Quality Setting not found in Game Settings.");
            QualitySetting = 0;

        }
        if (values[8] == "displayMode")
            DisplayMode = int.Parse(values[9]);
        else
        {
            Debug.Log("Display mode not found in Game Settings.");
            DisplayMode = 0;
        }
        if (values[10] == "level1Score")
            level1Score = float.Parse(values[11]);
        else
        {
            Debug.Log("Level 1 Score not found in Game Settings.");
            level1Score = 0;
        }
        if (values[12] == "level2Score")
            level2Score = float.Parse(values[13]);
        else
        {
            Debug.Log("Level 2 Score not found in Game Settings.");
            level2Score = 0;
        }
        if (values[14] == "level3Score")
            level3Score = float.Parse(values[15]);
        else
        {
            Debug.Log("Level 3 Score not found in Game Settings.");
            level3Score = 0;
        }
        if (values[16] == "level4Score")
            level4Score = float.Parse(values[17]);
        else
        {
            Debug.Log("Level 4 Score not found in Game Settings.");
            level4Score = 0;
        }
    }

    public void SetDefaultSettings()
    {
        File.WriteAllText(Application.dataPath + "/Resources/options.txt", DEFAULT_VALUE);
    }
}
