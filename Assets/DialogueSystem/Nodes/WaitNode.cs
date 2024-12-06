using UnityEngine;

[CreateAssetMenu(fileName = "WaitNode", menuName = "Dialogue/WaitNode")]
public class WaitNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public float delayTime = 1.5f;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}