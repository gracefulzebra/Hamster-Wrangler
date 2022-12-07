using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject deathScreen;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject currencyDisplay;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject lmCost, lbCost, rakeCost, tarCost, lighterCost;

    public bool mainMenuActive;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        StartCoroutine(UpdateAudio());
        UpdateItemCosts();
    }

    private void Update()
    {
        MainMenu();
    }

   void MainMenu()
   {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuActive)
        {
            mainMenu.SetActive(true);
            mainMenuActive = true;
            Time.timeScale = 0;
            GetComponent<GameManager>().holdingItem = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuActive)
        {
            mainMenu.SetActive(false);
            mainMenuActive = false;
            Time.timeScale = 1;
            GetComponent<GameManager>().holdingItem = false;
        }
   }

  public void MainMenuButton()
    {
        mainMenu.SetActive(false);
        mainMenuActive = false;
        Time.timeScale = 1;
        GetComponent<GameManager>().holdingItem = false;
    }
 

    IEnumerator UpdateAudio()
    {
        for(; ;)
        {
            gameManager.UpdateVolume(slider.value);
            yield return new WaitForSeconds(.5f);
        }
    }

    void loseGame()
    {
        if (GetComponent<GameManager>().health <= 0)
        {
            deathScreen.SetActive(true);
        }
    }

    public void DisplayScore(int score)
    {
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void DisplayCurrency(int currency)
    {
        currencyDisplay.GetComponent<TextMeshProUGUI>().text = "" + currency;
    }


   public void UpdateItemCosts()
    {
        lmCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.mowerCost;
        lbCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.blowerCost;
        rakeCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.rakeCost;
        tarCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.tarCost;
        lighterCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.lighterCost;
    }
}
