using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutManager : MonoBehaviour
{
    public int posCounter;
    public static TutManager tutInstance;

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


}
