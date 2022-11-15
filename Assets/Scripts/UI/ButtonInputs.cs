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
    [SerializeField] SnapToGrid objectToSnap;

    void SpawnItem(GameObject itemToSpawn)
    {
        // turns off all other buttons
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("ShopItem");

        foreach (GameObject go in gameObjectArray)
        {
            go.GetComponent<Button>().enabled = false;
        }
        // used for colour change
        objectToSnap.itemHeld = gameObject;
        gameObject.GetComponent<Image>().color = Color.yellow;
        // resets bool when clicked - clean this up 
        objectToSnap.holdingItem = false;
        // if not holdign item 
        if (!objectToSnap.holdingItem)
        {
            Vector3 spawnPos = new Vector3(0f, 100f, 0f);
            Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
            objectToSnap.hasItem = true;
            objectToSnap.holdingItem = true;
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

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
 