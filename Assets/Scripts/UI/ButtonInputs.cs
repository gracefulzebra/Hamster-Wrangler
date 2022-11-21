using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonInputs : MonoBehaviour
{

    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject tar;
    [SerializeField] GameObject lighter;
    [SerializeField] GameObject hamster;
    [SerializeField] SnapToGrid objectToSnap;
    [SerializeField] GameManager gameManager;


     void Awake()
     {
        gameManager.holdingItem = false;
     }


    void SpawnItem(GameObject itemToSpawn)
    {
      if (!gameManager.holdingItem)
        {
        // used for colour change
        objectToSnap.buttonRef = gameObject;
        gameObject.GetComponent<Image>().color = Color.yellow; 
        Vector3 spawnPos = new Vector3(0f, 100f, 0f);
        Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
        objectToSnap.hasItem = true;
        gameManager.holdingItem = true;
        }
    }

    public void SpawnLawnMower()
    {     
         SpawnItem(lawnMower);
    }

    public void SpawnLeafBlower()
    {
            SpawnItem(leafBlower);
    }

    public void SpawnRake()
    {
            SpawnItem(rake);
    }

    public void SpawnTar()
    {
        SpawnItem(tar);
    }

    public void SpawnLighter()
    {
        SpawnItem(lighter);
    }

    public void SpawnHamster()
    {

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
/*
// turns off all other buttons
GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ShopItem");

foreach (GameObject go in gameObjectArray)
{
    go.GetComponent<Button>().enabled = false;
}*/