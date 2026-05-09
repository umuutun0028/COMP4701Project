using UnityEditor;
using UnityEngine;

public class SetInputSystemToBoth
{
    public static void Execute()
    {
        SerializedObject projectSettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/ProjectSettings.asset")[0]);
        SerializedProperty activeInputHandler = projectSettings.FindProperty("activeInputHandler");
        
        if (activeInputHandler != null)
        {
            activeInputHandler.intValue = 2; // 2 means Both
            projectSettings.ApplyModifiedProperties();
            Debug.Log("Active Input Handler set to Both (2).");
        }
        else
        {
            Debug.Log("Could not find activeInputHandler property.");
        }
    }
}