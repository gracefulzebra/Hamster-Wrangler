using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolfLevel : MonoBehaviour
{

    private void Start()
    {
        GetComponent<TriggerDialogue>().DialogueTrigger();
    }

    private void Update()
    {
        if (Input.anyKey)
        { 
        if (!GetComponent<DialogueManager>().sentencePrinting)
            {
                Destroy(gameObject);
            }
        }
    }
}
