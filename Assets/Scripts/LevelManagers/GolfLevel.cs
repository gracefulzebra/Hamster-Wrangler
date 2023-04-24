using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfLevel : MonoBehaviour
{

    private void Start()
    {
        GetComponent<TriggerDialogue>().DialogueTrigger();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GetComponent<DialogueManager>().sentencePrinting)
            {
                Destroy(gameObject);
            }
        }
    }
}
