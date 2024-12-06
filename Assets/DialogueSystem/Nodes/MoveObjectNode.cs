using UnityEngine;

[CreateAssetMenu(fileName = "MoveObject", menuName = "Dialogue/MoveObject")]
public class MoveObjectNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public string objectName;

    //A fake way to create a Vector3
    public float posX = 0f;
    public float posY = 0f;
    public float posZ = 0f;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        GameObject obj = DialogueUtilities.FindObjectByName(objectName);

        Vector3 pos = new Vector3(posX, posY, posZ);

        obj.transform.localPosition = pos;
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}