using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutManager : MonoBehaviour
{
    public int posCounter;
    float timer;

    public float textDelay;

    public static TutManager tutInstance;

    [SerializeField] Material darkTile;
    [SerializeField] Material lightTile;

    [SerializeField] GameObject placementGridSquareLM;
    [SerializeField] GameObject placementGridSquareBT;

    [SerializeField] Button contineDialouge;
    [SerializeField] Button waveStartButton;

    [SerializeField] GameObject lawnMowerButton;
    [SerializeField] GameObject blowTorchButton;
    [SerializeField] GameObject leafBlowerButton;
    [SerializeField] GameObject bugZapperButton;
    [SerializeField] GameObject rakeButton;

    public bool tutEnd;
    public bool tutCanUseLM;
    public bool tutCanUseBT;

    public bool lmPlaceable;
    public bool btPlaceable;


    bool cantIncreasePC;
    bool callOnce;

    private void Awake()
    {
        if (tutInstance == null)
        {
            tutInstance = this;
        }
        if (tutInstance != this)
        {
            Destroy(gameObject);
        }
    }

        // Start is called before the first frame update
    void Start()
    {
        GetComponent<TriggerDialogue>().DialogueTrigger();
    }
    
    public void NextStep()
    {
        if (!cantIncreasePC)
            posCounter++;
    }

    private void Update()
    {
        if (tutEnd)
            return;

        if ( posCounter == 12 || posCounter == 21)
        {
            GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);
            GameManager.instance.uiManager.ShopButtonCantBuy(blowTorchButton);
            GameManager.instance.uiManager.ShopButtonCantBuy(leafBlowerButton);
            GameManager.instance.uiManager.ShopButtonCantBuy(bugZapperButton);
            GameManager.instance.uiManager.ShopButtonCantBuy(rakeButton);
        }

        if (!FindObjectOfType<DialogueManager>().sentencePrinting)
        {
            switch (posCounter)
            {

                case 1:
                    // when game starts 
                    break;
                case 2:
                    // pressed continue button
                    break;
                case 3:
                    // pressed continue button
                    break;
                case 4:
                    // prompted to buy trap
                    placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                    lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                    GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);

                    contineDialouge.GetComponent<Button>().enabled = false;
                    break;
                case 6:
                    // changes highlight of grid square
                    if (lmPlaceable)
                    {
                        placementGridSquareLM.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    else
                    {
                        placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                    }
                    break;
                    // poscounter +2 when buy trap, tutbuttons always increase, then dialouge 
                case 7:
                    lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                    GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                    placementGridSquareLM.GetComponent<Renderer>().material = darkTile;

                    DialougeWithNoPC();
                    break;
                    
                // poscounter +2 when placed, then dialouge plays
                case 8:
                    cantIncreasePC = false;
                    contineDialouge.GetComponent<Button>().enabled = false;

                    waveStartButton.GetComponent<Button>().enabled = true;
                    break;

                // start wave button pressed   
                case 9:
                    waveStartButton.GetComponent<Button>().enabled = false;

                    // this has a fucntion that will need to be changed if poscounter changes
                    if (Time.timeScale < 0.1)
                    {
                        if (!callOnce)
                        {
                            DialougeWithNoPC();
                            callOnce = true;
                        }
                    }
                    break;
                // pressign lawnmower 
                case 10:
                    cantIncreasePC = false;
                    callOnce = false;

                    tutCanUseLM = true;
                    break;
                    // diague 
                case 11:
                    timer = 0;
                    Time.timeScale = 1;
                    tutCanUseLM = false;
                    break;
                    // on contact with hammy
                case 12:      
                    contineDialouge.GetComponent<Button>().enabled = true;
  
                    break;
                    // presses dialouge button
                case 13:
                    contineDialouge.GetComponent<Button>().enabled = false;

                    GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);
                    lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                    placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                    break;
                    // pressed buy item
                case 14:
                    // changes highlight of grid square
                    if (lmPlaceable)
                    {
                        placementGridSquareLM.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    else
                    {
                        placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                    }
                    break;
                    // item placed
                case 15:
                    placementGridSquareLM.GetComponent<Renderer>().material = darkTile;
                    lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                    GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                    GameManager.instance.uiManager.DefaultShopOutline(blowTorchButton);
                    blowTorchButton.GetComponent<TutButtons>().enabled = true;
                    placementGridSquareBT.GetComponent<Renderer>().material.color = Color.black;
                    break;
                // pressed buy item
                case 16:
                    // changes highlight of grid square
                    if (btPlaceable)
                    {
                        placementGridSquareBT.GetComponent<Renderer>().material.color = Color.yellow;
                    }
                    else
                    {
                        placementGridSquareBT.GetComponent<Renderer>().material.color = Color.black;
                    }
                    break;
                // item placed
                case 17:
                    GameManager.instance.uiManager.ShopButtonCantBuy(blowTorchButton);
                    blowTorchButton.GetComponent<TutButtons>().enabled = false;
                    placementGridSquareBT.GetComponent<Renderer>().material = lightTile;

                    DialougeWithNoPC();
                    break;
                // item placed
                case 18:
                    cantIncreasePC = false;

                    waveStartButton.GetComponent<Button>().enabled = true;
                    break;
                // slows time and allows activation
                case 19:
                    waveStartButton.GetComponent<Button>().enabled = false;

                    if (Time.timeScale > 0.1)
                    {
                        cantIncreasePC = false;
                    }
                    else
                    {
                        if (!callOnce)
                        {
                            DialougeWithNoPC();
                            callOnce = true;
                        }
                        tutCanUseBT = true;
                    }
                    break;
                    //hammy death
                case 20:
                    cantIncreasePC = false;
                    callOnce = false;
                    break;
                    //dialouge
                case 21:
 
                    timer = 0;
                    Time.timeScale = 1;       
                    break;
                    // dilaouge continues
                case 22:
                    contineDialouge.GetComponent<Button>().enabled = true;

                    break;
                case 23:
                    contineDialouge.GetComponent<Button>().enabled = false;

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        FindObjectOfType<DialogueManager>().DisplayNextSentence();
                    }
                    break;
                case 24:
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        NextStep();
                        FindObjectOfType<DialogueManager>().DisplayNextSentence();
                    }
                    break;
                case 25:
                    lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                    blowTorchButton.GetComponent<TutButtons>().enabled = false;

                    lawnMowerButton.GetComponent<ButtonInputs>().enabled = true;
                    blowTorchButton.GetComponent<ButtonInputs>().enabled = true;
                    leafBlowerButton.GetComponent<ButtonInputs>().enabled = true;
                    bugZapperButton.GetComponent<ButtonInputs>().enabled = true;
                    rakeButton.GetComponent<ButtonInputs>().enabled = true;

                    GameManager.instance.uiManager.UpdateUIOnHamsterDeath();
                    waveStartButton.GetComponent<Button>().enabled = true;
                    tutEnd = true;
                    Destroy(contineDialouge.transform.parent.gameObject);
                    break;                
            }
        }
    }


    void DialougeWithNoPC()
    {
        if (!cantIncreasePC)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            cantIncreasePC = true;
        }
    }

    public void LerpTimeDown()
    {
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1, 0, timer / 1f);
    }
}
