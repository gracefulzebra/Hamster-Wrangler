using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using static UnityEditor.Progress;
using Unity.VisualScripting;
using UnityEngine.Networking.Types;
using System.Collections.Generic;

public class ButtonInputs : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
    [SerializeField] GameObject zapperInfo;
    [SerializeField] GameObject lighterInfo;


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
        if (Input.GetMouseButtonDown(0) && GameManager.instance.uiManager.itemDescriptionOpen == true)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Item Menu");
            Time.timeScale = 1;
            foreach (GameObject go in gameObjectArray)
            {
                GameManager.instance.uiManager.itemDescriptionOpen = false;
                go.SetActive(false);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var result in raycastResults)
            {
                if (result.gameObject.transform.name == "Image")
                {
                    GameObject unplacedItem = GameObject.FindGameObjectWithTag("Unplaced Item");
                    if (unplacedItem != null)
                    {
                        Destroy(unplacedItem);
                        GameManager.instance.holdingItem = false;
                        GameManager.instance.uiManager.RemoveShopOutline();
                    }                  
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuyItem();
    }

    public void BuyItem()
    {
        if (GameManager.instance.currencyManager.CheckPrice(gameObject.tag) == true && !GameManager.instance.holdingItem)
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
        if (GameManager.instance.uiManager.itemDescriptionOpen)
            return;

        if (!guideMenu.activeSelf)
        {
            guideMenu.SetActive(true);
            Time.timeScale = 0;
            GameManager.instance.uiManager.itemDescriptionOpen = true;
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

    public void ZapperMenu()
    {
        HelpGuide(zapperInfo);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}