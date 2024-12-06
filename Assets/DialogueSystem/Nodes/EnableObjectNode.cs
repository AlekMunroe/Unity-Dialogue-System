using UnityEngine;

[CreateAssetMenu(fileName = "EnableObject", menuName = "Dialogue/EnableObject")]
public class EnableObjectNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public bool enableObject = true;
    
    public string objectName;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        GameObject obj = DialogueUtilities.FindObjectByName(objectName);

        obj.SetActive(enableObject);
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}