using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] private GameObject finalScoreDisplay;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject currencyDisplay;
    [SerializeField] private GameObject healthDisplay;
    [SerializeField] private GameObject wavesDisplay;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject lmCost, lbCost, rakeCost, tarCost, lighterCost;
    // for ip3 we can use a slider thats fill is the yellow colour for the stars, so when you finish the level you can see how close to the 
    // next star you are, right now im just hard coding it 
    [SerializeField] private  List<GameObject> star1;
    [SerializeField] private Sprite starSprite;

    [SerializeField] private GameObject IngameHTP;
  
    // for buttonInputs
    [SerializeField] Sprite itemSelected;
    [SerializeField] Sprite itemUnselected;
    [SerializeField] Sprite itemCantBuy;
    GameObject previousButton;
    public GameObject highLightedButton;

    public bool mainMenuActive;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            StartCoroutine(UpdateAudio());
            UpdateItemCosts();
            wavesDisplay.GetComponent<TextMeshProUGUI>().text = "0 / 3";
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        MainMenu();
    }

   void MainMenu()
   {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuActive)
        {
            mainMenu.SetActive(true);
            mainMenuActive = true;
            Time.timeScale = 0;
            gameManager.holdingItem = true;
            IngameHTP.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainMenuActive)
        {
            mainMenu.SetActive(false);
            mainMenuActive = false;
            Time.timeScale = 1;
            gameManager.holdingItem = false;
            IngameHTP.SetActive(false);
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
        if (scoreDisplay != null)
        scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    public void DisplayCurrency(int currency)
    {
        if (scoreDisplay != null)
            currencyDisplay.GetComponent<TextMeshProUGUI>().text = "" + currency;
    }

    public void DisplayHealth(int health)
    {
        if (scoreDisplay != null)
            healthDisplay.GetComponent<TextMeshProUGUI>().text = "" + health;
    }

    public void DisplayWaves(int waves, int maxWaves)
    {
        if (scoreDisplay != null)
            wavesDisplay.GetComponent<TextMeshProUGUI>().text = waves + " / " + maxWaves;
    }

    public void DisplayFinalScore(int finalScore)
    {
        finalScoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score : " + finalScore;
    }

    public void UpdateItemCosts()
    {
        if (lmCost != null)
        {
            lmCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.mowerCost;
            lbCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.blowerCost;
            rakeCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.rakeCost;
            tarCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.zapperCost;
            lighterCost.GetComponent<TextMeshProUGUI>().text = "" + gameManager.currencyManager.lighterCost;
        } 
    }

    public void Stars(int starCount)
    {
        switch (starCount)
        {
            case 1:
                star1[0].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            case 2:
                star1[0].GetComponent<Image>().sprite = starSprite;
                star1[1].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            case 3:
                star1[0].GetComponent<Image>().sprite = starSprite;
                star1[1].GetComponent<Image>().sprite = starSprite;
                star1[2].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            default:
                gameOverScreen.SetActive(true);
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


   public void MenuStars(int starCount)
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
        }
    }

    public void RemoveShopOutline()
    {
        highLightedButton.GetComponent<Image>().sprite = itemUnselected;

        highLightedButton = null;
    }

    public void ShopButtonOutline(GameObject theButton)
    {
        theButton.GetComponent<Image>().sprite = itemSelected;
    }

    public void ShopButtonCantBuy(GameObject theButton)
    {
        theButton.GetComponent<Image>().sprite = itemCantBuy;
    }
}
