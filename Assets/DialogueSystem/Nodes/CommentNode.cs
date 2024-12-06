using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "CommentNode", menuName = "Dialogue/CommentNode")]
public class CommentNode : Node
{
    //This script is used for adding developer comments
    [TextArea(12, 12)] 
    public string Comments;
}