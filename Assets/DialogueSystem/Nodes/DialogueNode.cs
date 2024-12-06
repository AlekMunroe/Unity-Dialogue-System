using UnityEngine;
using XNode;

public abstract class DialogueNode : Node
{
    [Input] public DialogueNode input;
    public string speakerName;
    public string dialogueText;
    public AudioClip audioClip;
    public AnimationClip animationClip;
    public string targetAnimationObjectName;

    //This will show if it is a choice node or not, by default it is not
    public virtual bool IsChoiceNode => false;

    //This output is abstract so it can be defined in the class
    public abstract DialogueNode GetNextNode();
}
