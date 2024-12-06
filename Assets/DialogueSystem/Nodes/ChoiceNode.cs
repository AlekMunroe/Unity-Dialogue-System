using UnityEngine;
using XNode;
using System.Linq;

[CreateAssetMenu(fileName = "ChoiceNode", menuName = "Dialogue/ChoiceNode")]

public class ChoiceNode : DialogueNode
{
    public string objectName = "ChoiceAudioSource_Null";
    
    [System.Serializable]
    public class Choice
    {
        public string choiceText;
    }

    public Choice[] choices; //The array of choices, this will be used for the buttons

    public override bool IsChoiceNode => true;

    protected override void Init()
    {
        base.Init();
        UpdateChoicePorts();
    }

    //Dynamically create output ports for each choice, to give multiple choices
    public void UpdateChoicePorts()
    {
        //Make sure there is the correct amount of ports for the amount of choices
        int currentPortCount = Ports.Count(p => p.IsOutput); //Count the amount of ports there are
        int choiceCount = choices.Length; //Count the amount of choices there are

        //If the port doesnt exist for the choice, create it
        for (int i = currentPortCount; i < choiceCount; i++)
        {
            AddDynamicOutput(typeof(DialogueNode), ConnectionType.Override, TypeConstraint.None, "Choice " + i);
        }

        //If there are too many ports, remove it
        for (int i = currentPortCount - 1; i >= choiceCount; i--)
        {
            RemoveDynamicPort("Choice " + i);
        }

        //Update the names of the ports to make sure they are matching
        for (int i = 0; i < choices.Length; i++)
        {
            NodePort port = GetPort("Choice " + i);
            if (port == null || port.fieldName != "Choice " + i)
            {
                //Make the name of the port the same as the index in the choices array
                RemoveDynamicPort("Choice " + i);
                AddDynamicOutput(typeof(DialogueNode), ConnectionType.Override, TypeConstraint.None, "Choice " + i);
            }
        }
    }


    //Unity calls this automatically when the node is updated in the editor
    public void OnValidate()
    {
        UpdateChoicePorts(); //Used to make sure the ports are connected correctly
    }

    //Make sure the ports are created when there is a connection
    public override void OnCreateConnection(NodePort from, NodePort to)
    {
        base.OnCreateConnection(from, to);
        UpdateChoicePorts();
    }

    //This is used to get the next node for the choice index
    public DialogueNode GetNextNodeForChoice(int choiceIndex)
    {
        return GetOutputPort("Choice " + choiceIndex).Connection?.node as DialogueNode;
    }

    //Override to handle the xNode output
    public override object GetValue(NodePort port)
    {
        //Since we dont need a value for the flow, return null
        return null;
    }

    public override DialogueNode GetNextNode()
    {
        //This returns null since this node branches based on the choices
        return null;
    }
}
