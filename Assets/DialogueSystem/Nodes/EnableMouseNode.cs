using UnityEngine;

[CreateAssetMenu(fileName = "EnableMouseNode", menuName = "Dialogue/EnableMouseNode")]
public class EnableMouseNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public bool enableMouse = true;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}