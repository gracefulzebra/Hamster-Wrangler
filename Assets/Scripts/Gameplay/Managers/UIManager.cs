using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gameManager;
    public bool mainMenuActive;

    /*
    private void Update()
    {
        MainMenu();
    }

   void MainMenu()
   {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuActive)
        {
            mainMenu.SetActive(true);
            mainMenuActive = true;
            Time.timeScale = 0;
            gameManager.holdingItem = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuActive)
        {
            mainMenu.SetActive(false);
            mainMenuActive = false;
            Time.timeScale = 1;
            gameManager.holdingItem = false;
        }
   }

    void loseGame()
    {
        if (gameManager.health <= 0)
        {
            deathScreen.SetActive(true);
        }
    }

    */
}
