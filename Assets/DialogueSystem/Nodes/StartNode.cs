using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "StartNode", menuName = "Dialogue/StartNode")]
public class StartNode : Node
{
    //This node simply marks the beginning of the graph, it should be empty
    [Output] public DialogueNode nextNode;

    public DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}
