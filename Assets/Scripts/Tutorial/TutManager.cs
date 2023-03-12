
using UnityEngine;
using UnityEngine.UI;


public class TutManager : MonoBehaviour
{
    public int posCounter;
    int tempCounter;

    public static TutManager tutInstance;

    [SerializeField] Button waveStartButton;
    [SerializeField] GameObject lawnMowerButton;



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

        switch(posCounter)
        {
            case 4:
                lawnMowerButton.GetComponent<TutButtons>().enabled = true;
                break;
            case 6:
                waveStartButton.GetComponent<Button>().enabled = true;
                break;
            case 7:
               

                break;


        }
    }

}
