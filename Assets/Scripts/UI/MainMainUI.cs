using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMainUI : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject HTPMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject confirmQuitGame;

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
        SwitchSetActive(levelSelect);
    }

    public void Settings()
    {
        SwitchSetActive(settings);
        GameSettings.instance.SaveSettings();
    }
    
    public void ExitButton()
    {
        transform.parent.gameObject.SetActive(false);
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
