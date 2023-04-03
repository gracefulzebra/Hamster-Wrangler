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
    }
    
    public void ExitButton()
    {
        transform.parent.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Level1()
    {
        SceneManager.LoadScene("TutorialLevel");
        Time.timeScale = 1;
    }

    public void Level2()
    {
        SceneManager.LoadScene("FinnLevel");
        Time.timeScale = 1;
    }
}
