using UnityEngine;

[CreateAssetMenu(fileName = "CameraSwitchNode", menuName = "Dialogue/CameraSwitchNode")]
public class CameraSwitchNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    public bool disablePlayerCam = true;
    public string playerCam = "PlayerCamera_Cam01";
    
    public string disableCam;
    public string enableCam;

    // Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        GameObject disableCamObj = DialogueUtilities.FindObjectByName(disableCam);
        GameObject enableCamObj = DialogueUtilities.FindObjectByName(enableCam);

        // Look for and update the player when a name is inputted
        if (!string.IsNullOrWhiteSpace(playerCam))
        {
            GameObject playerCamObj = DialogueUtilities.FindObjectByName(playerCam);

            if(playerCamObj != null)
            {
                playerCamObj.SetActive(!disablePlayerCam);
            }
        }
        
        disableCamObj.SetActive(false);

        enableCamObj.SetActive(true);
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}