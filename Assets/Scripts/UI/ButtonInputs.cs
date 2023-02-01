using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonInputs : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameManager gameManager;

    [Header("Shop Items")]
    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject tar;
    [SerializeField] GameObject lighter;
    GameObject[] temp;

    [Header("Trap Info")]
    [SerializeField] GameObject lawnMowerInfo;
    [SerializeField] GameObject leafBlowerInfo;
    [SerializeField] GameObject rakeInfo;
    [SerializeField] GameObject tarInfo;
    [SerializeField] GameObject lighterInfo;
    [SerializeField] GameObject itemToSpawn;


    [Header("Main Menu")]
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject HTPMenu;
    [SerializeField] GameObject HTPMenuIngame;
    [SerializeField] GameObject settings;

    [Header("Open/Close Shop")]
    [SerializeField] GameObject openMenu;
    [SerializeField] GameObject closeMenu;

    [SerializeField] Sprite itemUnselected;
    [SerializeField] Sprite itemSelected;


    void Awake()
    {
        if (gameManager != null)
        gameManager.holdingItem = false;
    }
    void Start()
    {
        if(gameObject.tag == "LawnMower")
        {
            itemToSpawn = lawnMower;
        }
        if (gameObject.tag == "LeafBlower")
        {
            itemToSpawn = leafBlower;
        }
        if (gameObject.tag == "Rake")
        {
            itemToSpawn = rake;
        }
        if (gameObject.tag == "Tar")
        {
            itemToSpawn = tar;
        }
        if (gameObject.tag == "Lighter")
        {
            itemToSpawn = lighter;
        }
    }

    private void Update()
    {
        if (gameManager != null)
        {
            if (!gameManager.holdingItem)
            {
               GetComponent<Image>().sprite = itemUnselected;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameManager.currencyManager.CheckPrice(gameObject.tag) == true)
        {
            gameManager.uiManager.RemoveShopOutline(gameObject);
            gameManager.uiManager.ShopButtonOutline(gameObject);
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
/*
    public void SpawnLawnMower()
    {
        if (gameManager.currencyManager.CheckPrice("LawnMower") == true)
        {
            SpawnItem(lawnMower);
        }     
    }

    public void SpawnLeafBlower()
    {
        if (gameManager.currencyManager.CheckPrice("LeafBlower") == true)
        {
            SpawnItem(leafBlower);
        }
    }

    public void SpawnRake()
    {
        if (gameManager.currencyManager.CheckPrice("Rake") == true)
        {
            SpawnItem(rake);
        }
    }

    public void SpawnTar()
    {
        if (gameManager.currencyManager.CheckPrice("Tar") == true)
        {
            SpawnItem(tar);
        }
    }

    public void SpawnLighter()
    {
        if (gameManager.currencyManager.CheckPrice("Lighter") == true)
        {
            SpawnItem(lighter);
        }
    }
    void SpawnItem(GameObject itemToSpawn)
    {
        if (!gameManager.holdingItem)
        {
            //    ItemSpawn(itemToSpawn);
            gameManager.holdingItem = true;
        }
        if (gameManager.holdingItem)
        {
            temp = GameObject.FindGameObjectsWithTag("Unplaced Item");
            foreach (GameObject x in temp)
            {
                Destroy(x);
            }
            //  ItemSpawn(itemToSpawn);
        }
    }

    /*  void ItemSpawn(GameObject itemToSpawn)
      {
          gameManager.uiManager.RemoveShopOutline(gameObject);
          gameManager.uiManager.ShopButtonOutline(gameObject);
          Vector3 spawnPos = new Vector3(0f, 100f, 0f);
          Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
      }
    */

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
        gameManager.animationManager.OpenShopAnimation();
        closeMenu.SetActive(false);
    }

    public void CollapseShop()
    {
        closeMenu.SetActive(true);
        gameManager.animationManager.CloseShopAnimation();
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