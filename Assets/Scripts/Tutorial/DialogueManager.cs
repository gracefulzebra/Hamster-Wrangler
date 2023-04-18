using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public bool sentencePrinting;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialouge dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (!sentencePrinting)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            TutManager.tutInstance.NextStep();
            StartCoroutine(TypeSentence(sentence));
            sentencePrinting = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";   
        foreach (char letter in sentence.ToCharArray())
        {
             dialogueText.text += letter;
             yield return new WaitForSecondsRealtime(TutManager.tutInstance.textDelay);
        }
        sentencePrinting = false;
    }

    void EndDialogue()
    {
        print("done");
    }
}
