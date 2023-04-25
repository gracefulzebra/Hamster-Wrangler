using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

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


    [SerializeField] GameObject binOpen;
    [SerializeField] GameObject binClosed;


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
        if (Input.GetMouseButtonDown(0) && GameManager.instance.uiManager.itemDescriptionOpen == true)
        {
                    GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Item Menu");
            foreach (GameObject go in gameObjectArray)
            {
                GameManager.instance.uiManager.itemDescriptionOpen = false;
                go.SetActive(false);         
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuyItem();
    }

    // its just way smoother if this stays in here cause iof how pointerevents pass data 
    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject unplacedItem = GameObject.FindGameObjectWithTag("Unplaced Item");
        if (unplacedItem != null)
        {
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var result in raycastResults)
                {
                    if (result.gameObject.transform.name == "Delete Trap")
                    {

                        Destroy(unplacedItem);
                        GameManager.instance.holdingItem = false;
                        GameManager.instance.uiManager.RemoveShopOutline();
                    }       
                }
            }
        }
        else
        {
            if (GameManager.instance.waveManager.waveCompleted && gameObject.transform.name == "Delete Trap")
            {
                if (!GameManager.instance.currencyManager.deleteItemMode)
                {
                    GameManager.instance.currencyManager.deleteItemMode = true;
                    binClosed.SetActive(false);
                    binOpen.SetActive(true);
                }
                else
                {
                    GameManager.instance.currencyManager.deleteItemMode = false;
                    binClosed.SetActive(true);
                    binOpen.SetActive(false);
                }
            }
        }
    }
    public void BuyItem()
    {
        if (GameManager.instance.currencyManager.CheckPrice(gameObject.tag) == true && !GameManager.instance.holdingItem)
        {
            GameManager.instance.currencyManager.deleteItemMode = false;
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
            GameManager.instance.uiManager.itemDescriptionOpen = true;
          //  gameObject.GetComponent<ButtonInputs>().enabled = false;
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
        GameSettings.instance.SaveSettings();
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        GameSettings.instance.SaveSettings();
        Application.Quit();
    }

    public void MainMenu()
    {
        GameSettings.instance.SaveSettings();
        SceneManager.LoadScene("MainMenu");
    }

}