using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInputs : MonoBehaviour, IPointerDownHandler
{
   
    [Header("Shop Items")]
    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject tar;
    [SerializeField] GameObject lighter;

    [SerializeField] GameObject itemToSpawn;

    [Header("Trap Info")]
    [SerializeField] GameObject lawnMowerInfo;
    [SerializeField] GameObject leafBlowerInfo;
    [SerializeField] GameObject rakeInfo;
    [SerializeField] GameObject tarInfo;
    [SerializeField] GameObject lighterInfo;

    [Header("Main Menu")]
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject HTPMenu;
    [SerializeField] GameObject HTPMenuIngame;
    [SerializeField] GameObject settings;

    [Header("Open/Close Shop")]
    [SerializeField] GameObject openMenu;
    [SerializeField] GameObject closeMenu;

    void Awake()
    {

    }

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
            case "Tar":
                itemToSpawn = tar;
                break;
            case "Lighter":
                itemToSpawn = lighter;
                break;
        }
    }

    private void Update()
    {
        if (!GameManager.instance.holdingItem && GetComponent<Image>().sprite.name == "Selected Trapbox")
        {
            GetComponent<Image>().sprite = GameManager.instance.uiManager.itemUnselected;
        }

        if (Input.GetMouseButtonUp(0) && GameManager.instance.holdingItem)
        {
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Unplaced Item");

            foreach (GameObject go in gameObjectArray)
            {
                Destroy(go);
            }
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
            GameManager.instance.holdingItem = true;
            GameManager.instance.uiManager.ShopButtonOutline(gameObject);
            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
        }
    }

    void HelpGuide(GameObject guideMenu)
    {
        if (!guideMenu.activeSelf)
        {
            guideMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            guideMenu.SetActive(false);
            Time.timeScale = 1;
        }
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        SwitchSetActive(levelSelect);
    }

    public void HTP()
    {
        SwitchSetActive(HTPMenu);
    }

    public void HTPIngame()
    {
        HTPMenuIngame.SetActive(true);
    }

    public void Settings()
    {
        SwitchSetActive(settings);
    }

    public void ExitButton()
    {
        transform.parent.gameObject.SetActive(false);
        if (mainMenu != null)
            mainMenu.SetActive(true);
    }

    public void Level1()
    {
        SceneManager.LoadScene("OpenLevel");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenShop()
    {
        openMenu.SetActive(true);
        GameManager.instance.animationManager.OpenShopAnimation();
        closeMenu.SetActive(false);
    }

    public void CollapseShop()
    {
        closeMenu.SetActive(true);
        GameManager.instance.animationManager.CloseShopAnimation();
        StartCoroutine(CloseMenu());
    }
    WaitForSeconds delay = new WaitForSeconds(0.19f);
    IEnumerator CloseMenu()
    {
        yield return delay;
        openMenu.SetActive(false);
    }
}
/*
// turns off all other buttons
GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ShopItem");

foreach (GameObject go in gameObjectArray)
{
    go.GetComponent<Button>().enabled = false;
}*/