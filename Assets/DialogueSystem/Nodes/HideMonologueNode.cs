using UnityEngine;

[CreateAssetMenu(fileName = "HideMonologueNode", menuName = "Dialogue/HideMonologueNode")]
public class HideMonologueNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public bool enableUI = false;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}