using UnityEngine;

[CreateAssetMenu(fileName = "CameraSwitchNode", menuName = "Dialogue/CameraSwitchNode")]
public class CameraSwitchNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    [Header("Do not add anything into the fields above.")]

    public bool disablePlayerCam = true;
    public string playerCam = "PlayerCamera_Cam01";
    
    public string disableCam;
    public string enableCam;

    // Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        GameObject playerCamObj = DialogueUtilities.FindObjectByName(playerCam);
        GameObject disableCamObj = DialogueUtilities.FindObjectByName(disableCam);
        GameObject enableCamObj = DialogueUtilities.FindObjectByName(enableCam);

        // Debug.Log("Player Cam: " + playerCamObj.name);
        // Debug.Log("Disable Cam: " + disableCamObj.name);
        // Debug.Log("Enable Cam: " + enableCamObj.name);
        
        if (playerCam != null)
        {
            if (playerCamObj != null)
            {
                if (disablePlayerCam)
                {
                    playerCamObj.SetActive(false);
                }
                else
                {
                    playerCamObj.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("No playerCam assigned to CameraSwitchNode");
        }
        
        disableCamObj.SetActive(false);

        enableCamObj.SetActive(true);
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}