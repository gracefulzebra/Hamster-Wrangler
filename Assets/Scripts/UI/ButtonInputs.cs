using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInputs : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
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

    [SerializeField] GameObject menuToOpen;

    [SerializeField] GameObject HTPMenuIngame;


    IEnumerator stopTrapGuide;

    void Start()
    {
        switch (gameObject.tag)
        {
            case "LawnMower":
                itemToSpawn = lawnMower;
                menuToOpen = lawnMowerInfo;
                break;
            case "LeafBlower":
                itemToSpawn = leafBlower;
                menuToOpen = leafBlowerInfo;
                break;
            case "Rake":
                itemToSpawn = rake;
                menuToOpen = rakeInfo;
                break;
            case "BugZapper":
                itemToSpawn = bugZapper;
                menuToOpen = zapperInfo;
                break;
            case "Lighter":
                itemToSpawn = lighter;
                menuToOpen = lighterInfo;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        BuyItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
     //   stopTrapGuide = ActivateInformationTab();
      //  StartCoroutine(stopTrapGuide);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      //  StopCoroutine(stopTrapGuide);
    }

    IEnumerator ActivateInformationTab()
    {   
        yield return new WaitForSeconds(2f);
        if (gameObject == GameObject.FindGameObjectWithTag("LawnMower"))
        {
            LawnmowerMenu();
        }
        if (gameObject == GameObject.FindGameObjectWithTag("LeafBlower"))
        {
            LeafblowerMenu();
        }
        if (gameObject == GameObject.FindGameObjectWithTag("Rake"))
        {
            RakeMenu();
        }
        if (gameObject == GameObject.FindGameObjectWithTag("BugZapper"))
        {
            ZapperMenu();
        }
        if (gameObject == GameObject.FindGameObjectWithTag("Lighter"))
        {
            LighterMenu();
        }
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
       /* else 
        {
            guideMenu.SetActive(false);
            Time.timeScale = 1;
            GameManager.instance.uiManager.itemDescriptionOpen = false;
        } */   
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