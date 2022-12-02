using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gameManager;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject currencyDisplay;
    public bool mainMenuActive;

    
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
         //   gameManager.holdingItem = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuActive)
        {
            mainMenu.SetActive(false);
            mainMenuActive = false;
            Time.timeScale = 1;
          //  gameManager.holdingItem = false;
        }
   }

  /*  void loseGame()
    {
        if (gameManager.health <= 0)
        {
            deathScreen.SetActive(true);
        }
    }*/

    public void DisplayScore(int score)
    {
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void DisplayCurrency(int currency)
    {
        currencyDisplay.GetComponent<TextMeshProUGUI>().text = "Currency : " + currency;
    }


}
