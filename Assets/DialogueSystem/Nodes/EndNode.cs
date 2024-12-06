using UnityEngine;

[CreateAssetMenu(fileName = "EndNode", menuName = "Dialogue/EndNode")]
public class EndNode : DialogueNode
{
    //This node simply marks the end of the graph, it should be empty
    public override DialogueNode GetNextNode()
    {
        return null;
    }
}
