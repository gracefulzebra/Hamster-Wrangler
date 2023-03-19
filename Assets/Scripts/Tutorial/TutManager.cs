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
        switch (posCounter)
        {

            case 1:
                // when game starts 
                break;
            case 2:
                // pressed continue button
                break;
            case 3:
                // prompted to buy trap
                placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);

                contineDialouge.GetComponent<Button>().enabled = false;
                break;
            case 5:
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
            case 6:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                placementGridSquareLM.GetComponent<Renderer>().material = darkTile;

                DialougeWithNoPC();
                break;

            case 7:
                // dialouge activated 
                cantIncreasePC = false;
                contineDialouge.GetComponent<Button>().enabled = false;

                waveStartButton.GetComponent<Button>().enabled = true;
                break;

                // start wave button pressed   
                case 8:
                waveStartButton.GetComponent<Button>().enabled = false;

                // this has a fucntion that will need to be changed if poscounter changes
                if (Time.timeScale < 0.1)
                {
                    if (!callOnce)
                    {
                        DialougeWithNoPC();
                        callOnce = true;
                    }
                    tutCanUseLM = true;
                }
                break;
            // more dialouge

          
            // go to tutlawnmower script for time slowing

                // when time is zero lawnmower is useable
            case 9:
                cantIncreasePC = false;
                callOnce = false;

                // this has a fucntion that will need to be changed if poscounter changes
                if (Time.timeScale < 0.1)
                {
                    tutCanUseLM = true;
                }
                break;
                // lawnmower is pressed
            case 10:
                timer = 0;
                tutCanUseLM = true;

                Time.timeScale = 1;
                contineDialouge.GetComponent<Button>().enabled = true;
                break;
            case 11:
                // when lawnmower hits hammy
                tutCanUseLM = false;
                break;
            case 12:
                // continue button
                contineDialouge.GetComponent<Button>().enabled = false;

                GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                break;
            case 13:
                // lawnmower button is pressed

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
                // place it
            case 14:
                placementGridSquareLM.GetComponent<Renderer>().material = darkTile;
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                GameManager.instance.uiManager.DefaultShopOutline(blowTorchButton);
                blowTorchButton.GetComponent<TutButtons>().enabled = true;
                placementGridSquareBT.GetComponent<Renderer>().material.color = Color.black;

                break;
            case 15:
                // blowtorch button is pressed

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

                // can start wave
            case 16:
                GameManager.instance.uiManager.ShopButtonCantBuy(blowTorchButton);
                blowTorchButton.GetComponent<TutButtons>().enabled = false;
                placementGridSquareBT.GetComponent<Renderer>().material = lightTile;

                DialougeWithNoPC();
                break;

            case 17:
                cantIncreasePC = false;

                waveStartButton.GetComponent<Button>().enabled = true; 
                break;
            // slows time and allows activation
            case 18:
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
            case 19:
                cantIncreasePC = false;
                callOnce = false;
                break;
            case 20:
                // dialouge called
                timer = 0;
                Time.timeScale = 1;
                break;
                // time is reset 
            case 21:
                contineDialouge.GetComponent<Button>().enabled = true;
                break;
                // dialouge 
            case 22:
                contineDialouge.GetComponent<Button>().enabled = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                break;
                // go into menu
            case 23:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    NextStep();
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                break;
            case 24:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                blowTorchButton.GetComponent<TutButtons>().enabled = false;

                lawnMowerButton.GetComponent<ButtonInputs>().enabled = true;
                blowTorchButton.GetComponent<ButtonInputs>().enabled = true;
                leafBlowerButton.GetComponent<ButtonInputs>().enabled = true;
                bugZapperButton.GetComponent<ButtonInputs>().enabled = true;
                rakeButton.GetComponent<ButtonInputs>().enabled = true;

            //    GameManager.instance.currencyManager.UIOutline();
                waveStartButton.GetComponent<Button>().enabled = true;
                tutEnd = true;
                Destroy(contineDialouge.transform.parent.gameObject);
                break;
                // game done



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
