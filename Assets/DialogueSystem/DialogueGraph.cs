using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "DialogueGraph", menuName = "Dialogue/DialogueGraph")]
public class DialogueGraph : NodeGraph
{
    public StartNode startNode; //Reference to the StartNode

    //Find the start node
    public StartNode GetStartNode()
    {
        //If the startnode is already set, return it
        if (startNode != null)
        {
            return startNode;
        }

        //If the startnode isnt already set, search the graph for it
        foreach (var node in nodes)
        {
            if (node is StartNode start)
            {
                startNode = start; //Set the startnode for future use
                return start;
            }
        }

        //If it cannot find the startnode, show the error in the log
        Debug.LogWarning("DialogueGraph: There is no StartNode in the dialogue graph.");
        return null;
    }

    //Make sure only one startnode exists
    public void OnValidate()
    {
        int startNodeCount = 0;

        //Count how many startnodes exist
        foreach (var node in nodes)
        {
            if (node is StartNode)
            {
                startNodeCount++;
            }
        }

        //If there is more than one startnode, show the error int he log
        if (startNodeCount > 1)
        {
            Debug.LogError("DialogueGraph: There are multiple StartNodes in the graph. Make sure there is only one StartNode.");
        }
    }
}
