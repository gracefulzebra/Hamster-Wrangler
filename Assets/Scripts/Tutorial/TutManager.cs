
using UnityEngine;
using UnityEngine.UI;


public class TutManager : MonoBehaviour
{
    public int posCounter;
    float timer;
    public bool tutCanUse;

    public static TutManager tutInstance;

    [SerializeField] Button contineDialouge;
    [SerializeField] Button waveStartButton;
    [SerializeField] GameObject lawnMowerButton;
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
            case 10:
                waveStartButton.GetComponent<Button>().enabled = true;

                break;
            // go to tutlawnmower script for time slowing

                // when time is zero lawnmower is useable
            case 11:
                if (Time.timeScale == 0)
                {
                    tutCanUse = true;
                }
                break;
            // after trap is used go to tut snap to grid and respeed up time.
            case 12:
                break;


        }
    }

}
