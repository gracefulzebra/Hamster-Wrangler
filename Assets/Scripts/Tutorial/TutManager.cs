using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutManager : MonoBehaviour
{
    public int posCounter;
    float timer;
    public bool unfreezeTime;

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
            // do all dialouge

            // player can buy item
            case 5:
                placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);

                contineDialouge.GetComponent<Button>().enabled = false;
                break;
          // more dialogue after trap is placed
            case 7:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                contineDialouge.GetComponent<Button>().enabled = true;

                placementGridSquareLM.GetComponent<Renderer>().material = darkTile;

                DialougeWithNoPC();

                break;

                // can play 
                case 8:
                cantIncreasePC = false;
                contineDialouge.GetComponent<Button>().enabled = false;

                waveStartButton.GetComponent<Button>().enabled = true;
                break;
            // more dialouge

          
            // go to tutlawnmower script for time slowing

                // when time is zero lawnmower is useable
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
                    tutCanUseLM = true;              
                }
                break;
            case 10:
                cantIncreasePC = false;

                // this has a fucntion that will need to be changed if poscounter changes
                if (Time.timeScale < 0.1)
                {
                    tutCanUseLM = true;
                }
                break;
            // trap is activated
            case 11:
                callOnce = false;
                timer = 0;
                tutCanUseLM = true;

                Time.timeScale = 1;
                contineDialouge.GetComponent<Button>().enabled = true;
                break;

            //dialouge

            // allows lawnmoer and stuff to be palced
            case 13:
                tutCanUseLM = false;
                contineDialouge.GetComponent<Button>().enabled = false;

                GameManager.instance.uiManager.DefaultShopOutline(lawnMowerButton);
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                placementGridSquareLM.GetComponent<Renderer>().material.color = Color.black;
                break;

                // can use blow torch
            case 15:
                placementGridSquareLM.GetComponent<Renderer>().material = darkTile;
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                GameManager.instance.uiManager.ShopButtonCantBuy(lawnMowerButton);

                GameManager.instance.uiManager.DefaultShopOutline(blowTorchButton);
                blowTorchButton.GetComponent<TutButtons>().enabled = true;
                placementGridSquareBT.GetComponent<Renderer>().material.color = Color.black;
                break;
                // starts dialouge after blowtorch has been activated  

                // can start wave
            case 17:
                GameManager.instance.uiManager.ShopButtonCantBuy(blowTorchButton);
                blowTorchButton.GetComponent<TutButtons>().enabled = false;
                placementGridSquareBT.GetComponent<Renderer>().material = lightTile;

                waveStartButton.GetComponent<Button>().enabled = true;
                break;

                // slows time and allows activation
            case 18:
                waveStartButton.GetComponent<Button>().enabled = false;
                // in blowtorch
                if (Time.timeScale < 0.1)
                {
                    tutCanUseBT = true;
                }
                break;
                // time is reset 
            case 19:
                timer = 0;
                Time.timeScale = 1;

                break;
                // dialouge 
            case 20:
                contineDialouge.GetComponent<Button>().enabled = true;

                break;
                // go into menu
            case 21:
                contineDialouge.GetComponent<Button>().enabled = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    NextStep();
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                break;

            case 23:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    NextStep();
                    FindObjectOfType<DialogueManager>().DisplayNextSentence();
                }
                break;
                // game done
            case 24:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;
                blowTorchButton.GetComponent<TutButtons>().enabled = false;

                lawnMowerButton.GetComponent<ButtonInputs>().enabled = true;
                blowTorchButton.GetComponent<ButtonInputs>().enabled = true;
                leafBlowerButton.GetComponent<ButtonInputs>().enabled = true;
                bugZapperButton.GetComponent<ButtonInputs>().enabled = true;
                rakeButton.GetComponent<ButtonInputs>().enabled = true;

                GameManager.instance.currencyManager.UIOutline();
                waveStartButton.GetComponent<Button>().enabled = true;
                tutEnd = true;
                Destroy(contineDialouge.transform.parent.gameObject);
                break;


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
        if (posCounter == 9 || posCounter == 10 || posCounter == 18)
        {
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1, 0, timer / 1f);
        }
    }
}
