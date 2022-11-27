using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool holdingItem;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject scoreDisplay;
    public bool mainMenuActive;
    public GameObject placementConfirmation;
    float health;
    float score;
    float money;


    private void Update()
    {
        MainMenu();
       
        if(holdingItem)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Unplaced Item");

            foreach (GameObject x in gameObjectArray)
            {
                placementConfirmation.transform.position = x.transform.position;
            }
        }
        else
        {
            placementConfirmation.transform.position = new Vector3(0, 100, 0);
        }
    }

    void MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuActive)
        {
            mainMenu.SetActive(true);
            mainMenuActive = true;
            Time.timeScale = 0;
            holdingItem = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuActive)
        {
            mainMenu.SetActive(false);
            mainMenuActive = false;
            Time.timeScale = 1;
            holdingItem = false;
        }
    }

    void loseGame()
    {
        if(health <= 0)
        {
            deathScreen.SetActive(true);
        }
    }

    public void DisplayScore(int score)
    {
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

}
