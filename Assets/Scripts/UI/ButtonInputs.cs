using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ButtonInputs : MonoBehaviour, IPointerDownHandler
{
   
    [Header("Shop Items")]
    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject bugZapper;
    [SerializeField] GameObject lighter;

    [SerializeField] GameObject itemToSpawn;

    [Header("Trap Info")]
    [SerializeField] GameObject lawnMowerInfo;
    [SerializeField] GameObject leafBlowerInfo;
    [SerializeField] GameObject rakeInfo;
    [SerializeField] GameObject tarInfo;
    [SerializeField] GameObject lighterInfo;

    [SerializeField] GameObject HTPMenuIngame;

    bool desOpen;

    void Start()
    {
        switch (gameObject.tag)
        {
            case "LawnMower":
                itemToSpawn = lawnMower;
                break;
            case "LeafBlower":
                itemToSpawn = leafBlower;
                break;
            case "Rake":
                itemToSpawn = rake;
                break;
            case "BugZapper":
                itemToSpawn = bugZapper;
                break;
            case "Lighter":
                itemToSpawn = lighter;
                break;
        }
    }

    private void Update()
    {
      
        // CHANGE THIS 
        if (Input.GetMouseButton(0) && desOpen == true)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Item Menu");
            Time.timeScale = 1;
            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            desOpen = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuyItem();
    }

    public void BuyItem()
    {
        if (GameManager.instance.currencyManager.CheckPrice(gameObject.tag) == true)
        {
            GameManager.instance.uiManager.ShopButtonOutline(gameObject);
            GameManager.instance.uiManager.highLightedButton = gameObject;

            GameManager.instance.holdingItem = true;

            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
        }
    }

    void HelpGuide(GameObject guideMenu)
    {
        if (desOpen == true)
        {
            guideMenu.SetActive(false);
            Time.timeScale = 1;
            return;
        }

        if (!guideMenu.activeSelf)
        {
            guideMenu.SetActive(true);
            Time.timeScale = 0;
            desOpen = true;
        }
        else 
        {
            guideMenu.SetActive(false);
            Time.timeScale = 1;
            desOpen = false;
        }    
    }
 
    public void LawnmowerMenu()
    {
        HelpGuide(lawnMowerInfo);
    }

    public void LeafblowerMenu()
    {
        HelpGuide(leafBlowerInfo);
    }

    public void RakeMenu()
    {
        HelpGuide(rakeInfo);
    }

    public void TarMenu()
    {
        HelpGuide(tarInfo);
    }

    public void LighterMenu()
    {
        HelpGuide(lighterInfo);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void HTPIngame()
    {
        HTPMenuIngame.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}