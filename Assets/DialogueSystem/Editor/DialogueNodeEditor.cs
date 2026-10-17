using XNodeEditor;
using System.Collections.Generic;
using UnityEditor;
using XNode;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor
{
    private static readonly HashSet<string> InternalFields = new HashSet<string>
    {
        "m_Script",
        "graph",
        "position",
        "ports"
    };

    private static readonly HashSet<string> DialogueFields = new HashSet<string>
    {
        nameof(DialogueNode.speakerName),
        nameof(DialogueNode.dialogueText),
        nameof(DialogueNode.audioClip),
        nameof(DialogueNode.animationClip),
        nameof(DialogueNode.targetAnimationObjectName)
    };

    public override void OnBodyGUI()
    {
        serializedObject.Update();

        DialogueNode node = (DialogueNode)target;
        SerializedProperty property = serializedObject.GetIterator();

        bool enterChildren = true;

        while (property.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (InternalFields.Contains(property.name))
            {
                continue;
            }

            if(!node.ShowDialogueFields && DialogueFields.Contains(property.name))
            {
                continue;
            }

            NodeEditorGUILayout.PropertyField(property, true);
        }

        foreach(NodePort port in target.DynamicPorts)
        {
            if (NodeEditorGUILayout.IsDynamicPortListPort(port))
            {
                continue;
            }

            NodeEditorGUILayout.PortField(port);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
