using UnityEngine;

[CreateAssetMenu(fileName = "ClearNode", menuName = "Dialogue/ClearNode")]
public class ClearNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}