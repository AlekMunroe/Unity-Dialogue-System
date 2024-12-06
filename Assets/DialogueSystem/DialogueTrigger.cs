using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueGraph dialogueGraph;
    public DialogueManager dialogueManager;

    public bool destroyOnStart = true;

    public float startDelayTime = 1.5f;
    
    //Trigger options
    public enum TriggerOptions
    {
        StartOnAwake,
        StartOnTrigger,
        StartAfterTime
        
    }
    
    //Create a public variable for the enum
    public TriggerOptions triggerOptions;

    //Return the trigger options as a string
    public string GetTriggerOptionsAsString()
    {
        return triggerOptions.ToString();
    }

    void Start()
    {
        if (triggerOptions == TriggerOptions.StartOnAwake)
        {
            StartDialogue();
        }
        else if (triggerOptions == TriggerOptions.StartAfterTime)
        {
            StartCoroutine(startDelay());
        }
    }

    private IEnumerator startDelay()
    {
        yield return new WaitForSeconds(startDelayTime);

        StartDialogue();
    }

    private void StartDialogue()
    {
        if (dialogueManager != null && dialogueGraph != null)
        {
            EnableDialogueUi();
            
            //The GetStartNode method is used to get the StartNode from the graph
            StartNode startNode = dialogueGraph.GetStartNode();

            if (startNode != null)
            {
                dialogueManager.StartDialogue(startNode);

                if (destroyOnStart)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Debug.LogWarning("DialogueTrigger: There is no StartNode in the dialogue graph.");
            }
        }
        else
        {
            Debug.LogWarning("DialogueTrigger: DialogueManager or DialogueGraph does not exist.");
        }
    }

    private void EnableDialogueUi()
    {
        dialogueManager.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggerOptions == TriggerOptions.StartOnTrigger)
        {
            StartDialogue();
        }
    }
}
