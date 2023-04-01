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
    [SerializeField] private  List<GameObject> starsInGame;
    [SerializeField] private List<GameObject> starsLevel1;
    [SerializeField] private List<GameObject> starsLevel2;
    [SerializeField] private Sprite starSprite;

    [SerializeField] private GameObject IngameHTP;
  
    // for buttonInputs
    [SerializeField] Sprite itemSelected;
    [SerializeField] Sprite itemUnselected;
    [SerializeField] Sprite itemCantBuy;
    GameObject previousButton;
    public GameObject highLightedButton;

    public bool mainMenuActive;
    public bool itemDescriptionOpen;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            //StartCoroutine(UpdateAudio());
            UpdateItemCosts();
            wavesDisplay.GetComponent<TextMeshProUGUI>().text = "0/3";
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

    /// <summary>
    /// called to change outline based on currency when item is bought
    /// </summary>
    public void UpdateUIOnPurchase()
    {
        for (int i = 0; i < GameManager.instance.currencyManager.shopItems.Length; i++)
        {
            if (!GameManager.instance.currencyManager.CheckValidPurchase(GameManager.instance.currencyManager.itemCosts[i]))
            {
                GameManager.instance.uiManager.ShopButtonCantBuy(GameObject.FindGameObjectWithTag(GameManager.instance.currencyManager.shopItems[i]));
            }
        }
    }

    /// <summary>
    /// called to change outline based on currency when hamster is killed
    /// </summary>
    public void UpdateUIOnHamsterDeath()
    {
        for (int i = 0; i < GameManager.instance.currencyManager.shopItems.Length; i++)
        {
            if (GameManager.instance.currencyManager.CheckValidPurchase(GameManager.instance.currencyManager.itemCosts[i]))
            {
                GameManager.instance.uiManager.DefaultShopOutline(GameObject.FindGameObjectWithTag(GameManager.instance.currencyManager.shopItems[i]));
            }
        }
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
            wavesDisplay.GetComponent<TextMeshProUGUI>().text = waves + "/" + maxWaves;
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
                starsInGame[0].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            case 2:
                starsInGame[0].GetComponent<Image>().sprite = starSprite;
                starsInGame[1].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            case 3:
                starsInGame[0].GetComponent<Image>().sprite = starSprite;
                starsInGame[1].GetComponent<Image>().sprite = starSprite;
                starsInGame[2].GetComponent<Image>().sprite = starSprite;
                gameOverScreen.SetActive(true);
                break;
            default:
                gameOverScreen.SetActive(true);
                break;
        }
    }

   public void MenuStars(int starCount)
   {
        List<GameObject> menuStars = null;
        switch (GameManager.level)
            {
            case (1):
                menuStars = starsLevel1;
                break;
                    case (2):
                menuStars = starsLevel2;
                break;
        }
        switch (starCount)
        {
                         
            case 1:
                menuStars[0].GetComponent<Image>().sprite = starSprite;
                DontDestroyOnLoad(menuStars[0]);
                break;
            case 2:
                menuStars[0].GetComponent<Image>().sprite = starSprite;
                menuStars[1].GetComponent<Image>().sprite = starSprite;
                DontDestroyOnLoad(menuStars[0]);
                DontDestroyOnLoad(menuStars[1]);
                break;
            case 3:
                menuStars[0].GetComponent<Image>().sprite = starSprite;
                menuStars[1].GetComponent<Image>().sprite = starSprite;
                menuStars[2].GetComponent<Image>().sprite = starSprite;
                DontDestroyOnLoad(menuStars[0]); DontDestroyOnLoad(menuStars[1]); DontDestroyOnLoad(menuStars[2]);
                break;

 
        }
   }

    public void RemoveShopOutline()
    {
        highLightedButton.GetComponent<Image>().sprite = itemUnselected;

        highLightedButton = null;
    }

    public void DefaultShopOutline(GameObject theButton)
    {
        theButton.GetComponent<Image>().sprite = itemUnselected;
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
