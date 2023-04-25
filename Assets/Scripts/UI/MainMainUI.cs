using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMainUI : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject HTPMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject creditsScreen;
    [SerializeField] GameObject confirmQuitGame;
    [SerializeField] Image[] levelStars;
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] GameObject animObject;
    [SerializeField] GameObject pressAnyKeyText;
    [SerializeField] GameObject creditsButtonPos;
    [SerializeField] GameObject exitButtonPos;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject exitButton;
    Vector3 creditsStartPos;
    Vector3 exitStartPos;

    Resolution[] resolutions;


    private void Start()
    {
        InitRes();
        InitSettings();

        creditsStartPos = creditsButton.transform.position;
        exitStartPos = exitButton.transform.position;
    }

    bool once = false;
    float animTimer = 0;
    float animDuration = 1.65f;
    private void Update()
    {
        if (Input.anyKey && !once)
        {
            //playanim 
            animObject.GetComponent<Animator>().SetTrigger("StartAnimation");
            pressAnyKeyText.SetActive(false);
            once = true;
        }

        if (once)
        {
            animTimer += Time.deltaTime;
            
        }

        if(animTimer >= animDuration)
        {
            animObject.SetActive(false);
            mainMenu.SetActive(true);

            creditsButton.transform.position = Vector3.Lerp(creditsStartPos, creditsButtonPos.transform.position, ((animTimer - animDuration) / animDuration) * 2.5f);
            exitButton.transform.position = Vector3.Lerp(exitStartPos, exitButtonPos.transform.position, ((animTimer - animDuration) / animDuration) * 2.5f);
        }
    }

    private void InitRes()
    {
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
    }

    private void InitSettings()
    {
        if (GameSettings.instance.DisplayMode == 0) { fullScreenToggle.isOn = false; ToggleFullScreen(false); } else { fullScreenToggle.isOn = true; ToggleFullScreen(true); }
        SetQuality(GameSettings.instance.QualitySetting); graphicsDropdown.value = GameSettings.instance.QualitySetting;
        SetRes(GameSettings.instance.ResolutionSetting); resolutionDropdown.value = GameSettings.instance.ResolutionSetting;
    }

    void SwitchSetActive(GameObject objectToSwitch)
    {
        if (objectToSwitch.activeSelf)
        {
            objectToSwitch.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            objectToSwitch.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    void CloseCurrentUI(GameObject objectToClose)
    {
        objectToClose.SetActive(false);
    }

    public void ConfirmQuitGame()
    {
        confirmQuitGame.SetActive(true);
    }

    public void CloseConfirmQuitGame()
    {
        CloseCurrentUI(confirmQuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        levelStars[0].fillAmount = GameSettings.instance.Level1Score / GameManager.instance.scoreFor3Star;
        levelStars[1].fillAmount = GameSettings.instance.Level2Score / GameManager.instance.scoreFor3Star;
        levelStars[2].fillAmount = GameSettings.instance.Level3Score / GameManager.instance.scoreFor3Star;
        levelStars[3].fillAmount = GameSettings.instance.Level4Score / GameManager.instance.scoreFor3Star;

        GameSettings.instance.SaveSettings();

        SwitchSetActive(levelSelect);
    }

    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        GameSettings.instance.DisplayMode = isFullScreen ? 1 : 0;
        GameSettings.instance.SaveSettings();
    }

    public void SetQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        GameSettings.instance.QualitySetting = qualityLevel;
        GameSettings.instance.SaveSettings();
    }

    public void SetRes(int resolution)
    {
        Resolution _res = resolutions[resolution];
        Screen.SetResolution(_res.width, _res.height, Screen.fullScreen);
        GameSettings.instance.ResolutionSetting = resolution;
        GameSettings.instance.SaveSettings();
    }

    public void Settings()
    {
        SwitchSetActive(settings);
        GameSettings.instance.SaveSettings();
    }

    public void Credits()
    {
        SwitchSetActive(creditsScreen);
    }

    public void ExitButton()
    {
        settings.SetActive(false); levelSelect.SetActive(false); creditsScreen.SetActive(false);
        GameSettings.instance.SaveSettings();
        mainMenu.SetActive(true);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
        Time.timeScale = 1;
    }

    public void Level1()
    {
        SceneManager.LoadScene("FinnLevel");
        Time.timeScale = 1;
    }

    public void Level2()
    {
        SceneManager.LoadScene("JamieLevel");
        Time.timeScale = 1;
    }

    public void Level3()
    {
        SceneManager.LoadScene("jordan");
        Time.timeScale = 1;
    }

    public void Level4()
    {
        SceneManager.LoadScene("GnomeLevel");
        Time.timeScale = 1;
    }   
}
