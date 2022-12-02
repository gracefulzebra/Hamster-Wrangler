using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class ButtonInputs : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [Header("Shop Items")]
    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject tar;
    [SerializeField] GameObject lighter;
    [SerializeField] SnapToGrid objectToSnap;

    [Header("Trap Info")]
    [SerializeField] GameObject lawnMowerInfo;
    [SerializeField] GameObject leafBlowerInfo;
    [SerializeField] GameObject rakeInfo;
    [SerializeField] GameObject tarInfo;
    [SerializeField] GameObject lighterInfo;


    [Header("Open/Close Shop")]
    [SerializeField] GameObject openMenu;
    [SerializeField] GameObject closeMenu;

    public bool menuOpen;


     void Awake()
     {
        if (gameManager != null)
        gameManager.holdingItem = false;
     }

    void SpawnItem(GameObject itemToSpawn)
    {
      if (!gameManager.holdingItem)
      {
        // used for colour change
        objectToSnap.buttonRef = gameObject;
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
        objectToSnap.hasItem = true;
        gameManager.holdingItem = true;
      }     
    }

    void HelpGuide(GameObject guideMenu)
    {
        if (!menuOpen)
        {
            guideMenu.SetActive(true);
            menuOpen = true;
            Time.timeScale = 0;
        }
        else 
        {
            guideMenu.SetActive(false);
            menuOpen = false;
            Time.timeScale = 1;
        }
    }


    public void SpawnLawnMower()
    {
        // this wont work because you cna unconfirm placement, if that happens mayeb have return currancy fucntion
        if (gameManager.currencyManager.TryBuy("LawnMower") == true)
         SpawnItem(lawnMower);
    }

    public void SpawnLeafBlower()
    {
        if (gameManager.currencyManager.TryBuy("LeafBlower") == true)
            SpawnItem(leafBlower);
    }

    public void SpawnRake()
    {
        if (gameManager.currencyManager.TryBuy("Rake") == true)
            SpawnItem(rake);
    }

    public void SpawnTar()
    {
        if (gameManager.currencyManager.TryBuy("Tar") == true)
            SpawnItem(tar);
    }

    public void SpawnLighter()
    {
        if (gameManager.currencyManager.TryBuy("Lighter") == true)
            SpawnItem(lighter);
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