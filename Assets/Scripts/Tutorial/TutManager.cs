
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
            case 4:          
                break;
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
            case 7:
                lawnMowerButton.GetComponent<TutButtons>().enabled = false;

                contineDialouge.GetComponent<Button>().enabled = true;
                placementGridSquare.GetComponent<Renderer>().material.color = Color.green;
                waveStartButton.GetComponent<Button>().enabled = true;
                break;
            case 8:
                waveStartButton.GetComponent<Button>().enabled = true;

                timer += Time.deltaTime;
                Time.timeScale = Mathf.Lerp(1, 0, timer / 1f);
                break;
            case 9:
                tutCanUse = true;

                timer += Time.deltaTime;
                Time.timeScale = Mathf.Lerp(0, 1, timer / 1f);
                break;


        }
    }

}
