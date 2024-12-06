#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerPrefNode))]
public class PlayerPrefNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerPrefNode node = (PlayerPrefNode)target;

        //Show the player pref name and the type dropdown
        node.playerPrefName = EditorGUILayout.TextField("Player Pref Name", node.playerPrefName);
        node.prefType = (PlayerPrefNode.PlayerPrefType)EditorGUILayout.EnumPopup("Pref Type", node.prefType);

        //Show the correct input for the dropdown choice
        switch (node.prefType)
        {
            case PlayerPrefNode.PlayerPrefType.String:
                node.stringValue = EditorGUILayout.TextField("String Value", node.stringValue);
                break;
            case PlayerPrefNode.PlayerPrefType.Int:
                node.intValue = EditorGUILayout.IntField("Int Value", node.intValue);
                break;
            case PlayerPrefNode.PlayerPrefType.Float:
                node.floatValue = EditorGUILayout.FloatField("Float Value", node.floatValue);
                break;
        }

        //Used to show that you should not edit the "SpeakerName, DialogueText, AudioClip and AnimationClip" - I cannot remove them because they are inherited
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Do not add anything into the fields above.", MessageType.Warning);

        //Draw the default inspector
        DrawDefaultInspector();

        //Apply
        serializedObject.ApplyModifiedProperties();
    }
}
#endif