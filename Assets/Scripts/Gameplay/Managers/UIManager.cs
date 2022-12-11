using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject currencyDisplay;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject lmCost, lbCost, rakeCost, tarCost, lighterCost;
    // for ip3 we can use a slider thats fill is the yellow colour for the stars, so when you finish the level you can see how close to the 
    // next star you are, right now im just hard coding it 
    [SerializeField] private  List<GameObject> star1;
    [SerializeField] private Sprite starSprite;

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

    public void GameOverScreen()
    {
            Stars(0);
            gameOverScreen.SetActive(true);
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

    public void Stars(int starCount)
    {
        switch (starCount)
        {
            case 1:
                star1[0].GetComponent<Image>().sprite = starSprite;
                break;
            case 2:
                star1[0].GetComponent<Image>().sprite = starSprite;
                star1[1].GetComponent<Image>().sprite = starSprite;
                break;
            case 3:
                star1[0].GetComponent<Image>().sprite = starSprite;
                star1[1].GetComponent<Image>().sprite = starSprite;
                star1[2].GetComponent<Image>().sprite = starSprite;
                break;
            default:
                break;
        }

        //Calculation being handled in game manager. Can undo to reuse this if you want.
        /*
       if(gameManager.scoreManager.currentScore > gameManager.scoreFor3Star / 3)
       {
            star1[0].GetComponent<Image>().sprite = starSprite;
       } 
       if (gameManager.scoreManager.currentScore > (gameManager.scoreFor3Star / 3) * 2)
       {
            star1[1].GetComponent<Image>().sprite = starSprite;
       }
       if (gameManager.scoreManager.currentScore > gameManager.scoreFor3Star)
       {
            star1[2].GetComponent<Image>().sprite = starSprite;
       }
        */
    }
}
