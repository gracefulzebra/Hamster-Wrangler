
using UnityEngine;
using UnityEngine.UI;


public class TutManager : MonoBehaviour
{
    public int posCounter;
    float timer;
    public bool tutCanUse;
    public bool unfreezeTime;

    public static TutManager tutInstance;

    [SerializeField] Button contineDialouge;
    [SerializeField] Button waveStartButton;
    [SerializeField] GameObject lawnMowerButton;
    [SerializeField] GameObject blowTorchButton;

    [SerializeField] GameObject placementGridSquare;



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
                placementGridSquare.GetComponent<Renderer>().material.color = Color.white;
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;

                contineDialouge.GetComponent<Button>().enabled = false;
                break;
          /*  case 6:
                if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
                {
               //     NextStep();
                }
                break;*/
          // more dialogue after trap is placed
            case 7:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;

                contineDialouge.GetComponent<Button>().enabled = true;

                placementGridSquare.GetComponent<Renderer>().material.color = Color.green;
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
                    tutCanUse = true;              
                }
                break;
            // trap is activated
            case 11:

                Time.timeScale = 1;
                contineDialouge.GetComponent<Button>().enabled = true;
                break;

                // need to rework how to snap to grid and traps work. will need 2 probaly

                // allows lawnmoer and stuff to be palced 
            case 13:
                contineDialouge.GetComponent<Button>().enabled = false;
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;

                blowTorchButton.GetComponent<TutButtons>().enabled = true;
                break;


        }
    }

    public void LerpTimeDown()
    {
        if (posCounter == 10)
        {
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1, 0, timer / 1f);
        }
    }


}
