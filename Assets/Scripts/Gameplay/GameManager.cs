using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool holdingItem;
    [SerializeField] GameObject mainMenu;
    public bool mainMenuActive;
    public GameObject placementConfirmation;

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
}
