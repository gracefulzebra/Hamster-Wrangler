using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{

    public Dialouge dialouge;
  
    public void DialogueTrigger()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialouge);
    }

    private void Awake()
    {
        
    }
}
