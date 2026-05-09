using UnityEditor;
using UnityEngine;

public class CheckInputSystem
{
    public static void Execute()
    {
        var inputHandling = PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Standalone);
        // Actually, activeInputHandler is an internal property, but we can check it via PlayerSettings
        
        SerializedObject projectSettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/ProjectSettings.asset")[0]);
        SerializedProperty activeInputHandler = projectSettings.FindProperty("activeInputHandler");
        
        if (activeInputHandler != null)
        {
            Debug.Log("Active Input Handler: " + activeInputHandler.intValue);
            // 0 = Old, 1 = New, 2 = Both
        }
        else
        {
            Debug.Log("Could not find activeInputHandler property.");
        }
    }
}