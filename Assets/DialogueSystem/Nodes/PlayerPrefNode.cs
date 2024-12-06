using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "PlayerPrefNode", menuName = "Dialogue/PlayerPrefNode")]
public class PlayerPrefNode : DialogueNode
{
    [Header("Do not add anything into the fields above.")]
    public string warningText; //This is a placeholder just to show the header

    public enum PlayerPrefType { String, Int, Float }
    public PlayerPrefType prefType;

    public string playerPrefName;

    //The playerpref value inputs
    public string stringValue;
    public int intValue;
    public float floatValue;

    [Output] public DialogueNode nextNode; //The output node

    //Used to save the player pref
    private void SavePlayerPref()
    {
        switch (prefType)
        {
            case PlayerPrefType.String:
                PlayerPrefs.SetString(playerPrefName, stringValue);
                break;
            case PlayerPrefType.Int:
                PlayerPrefs.SetInt(playerPrefName, intValue);
                break;
            case PlayerPrefType.Float:
                PlayerPrefs.SetFloat(playerPrefName, floatValue);
                break;
        }
        PlayerPrefs.Save();
    }

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        SavePlayerPref();
        return GetOutputPort("nextNode").Connection?.node as DialogueNode;
    }
}
