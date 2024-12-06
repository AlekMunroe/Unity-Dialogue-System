using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SwitchSceneNode", menuName = "Dialogue/SwitchSceneNode")]
public class SwitchSceneNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]
    public int sceneIndex = 0;

    // Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}