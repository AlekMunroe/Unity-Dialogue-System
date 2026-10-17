using UnityEngine;

[CreateAssetMenu(fileName = "DestroyNode", menuName = "Dialogue/DestroyNode")]
public class DestroyNode : DialogueNode
{
    [Output] public DialogueNode nextNode;
    
    public string objectToDestroy;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        GameObject destroyObject = DialogueUtilities.FindObjectByName(objectToDestroy);

        Destroy(destroyObject);
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}