using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueUtilities
{
    public static GameObject FindObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        Debug.LogError(name + " not found");
        return null;
    }
}
