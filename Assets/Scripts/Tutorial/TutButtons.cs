using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutButtons : MonoBehaviour, IPointerDownHandler
{
    [Header("Shop Items")]
    [SerializeField] GameObject lawnMower;
    [SerializeField] GameObject leafBlower;
    [SerializeField] GameObject rake;
    [SerializeField] GameObject bugZapper;
    [SerializeField] GameObject lighter;

    [SerializeField] GameObject itemToSpawn;

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


            TutManager.tutInstance.NextStep();
        }
    }
}
