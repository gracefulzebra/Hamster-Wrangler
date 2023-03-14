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

    [SerializeField] Button contineDialouge;
    [SerializeField] Button waveStartButton;
    [SerializeField] GameObject lawnMowerButton;
    [SerializeField] GameObject blowTorchButton;

    [SerializeField] GameObject placementGridSquareLM;
    [SerializeField] GameObject placementGridSquareBT;

    public bool tutCanUseLM;
    public bool tutCanUseBT;

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
            posCounter++;
    }

    private void Update()
    {    
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
                break;

            // more dialouge

            // player starts wave
            case 9:
                contineDialouge.GetComponent<Button>().enabled = false;

                waveStartButton.GetComponent<Button>().enabled = true;
                break;
            // go to tutlawnmower script for time slowing

                // when time is zero lawnmower is useable
            case 10:
                waveStartButton.GetComponent<Button>().enabled = false;

                // this has a fucntion that will need to be changed if poscounter changes
                if (Time.timeScale < 0.1)
                {
                    tutCanUseLM = true;              
                }
                break;
            // trap is activated
            case 11:
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
            case 19:
                timer = 0;

                Time.timeScale = 1;
                break;

        }
    }

    public void LerpTimeDown()
    {
        if (posCounter == 10 || posCounter == 18)
        {
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1, 0, timer / 1f);
        }
    }
}
