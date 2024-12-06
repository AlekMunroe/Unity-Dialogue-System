using UnityEngine;

[CreateAssetMenu(fileName = "PlayAudioNode", menuName = "Dialogue/PlayAudioNode")]
public class PlayAudioNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]
    
    public AudioClip sfxAudioClip;
    public string objectName = "sfxAudioObject_Null";

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}