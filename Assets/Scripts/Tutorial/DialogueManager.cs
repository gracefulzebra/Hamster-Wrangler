using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public bool sentencePrinting;

    private Queue<string> sentences;

    Coroutine typingSentence;

    string sentence;

    [SerializeField] float textDelay;


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
            sentence = sentences.Dequeue();
            
          //  if (SceneManager.GetActiveScene().name == "TutorialLevel")
          //  { 
          //          TutManager.tutInstance.NextStep();
          //  }
            typingSentence = StartCoroutine(TypeSentence(sentence));
            sentencePrinting = true;
        }
        else if (sentencePrinting)
        {
            StopCoroutine(typingSentence);
            dialogueText.text = "";
            dialogueText.text = sentence;
            sentencePrinting = false;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";   
        foreach (char letter in sentence.ToCharArray())
        {
             dialogueText.text += letter;
             yield return new WaitForSecondsRealtime(textDelay);
        }
        sentencePrinting = false;
    }

    void EndDialogue()
    {
        print("done");
    }
}
