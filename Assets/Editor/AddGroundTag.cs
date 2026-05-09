using UnityEditor;
using UnityEngine;

public class AddGroundTag
{
    public static void Execute()
    {
        // Add "Ground" tag if it doesn't exist
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        bool found = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals("Ground")) { found = true; break; }
        }
        
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = "Ground";
            tagManager.ApplyModifiedProperties();
            Debug.Log("Added 'Ground' tag.");
        }
        else
        {
            Debug.Log("'Ground' tag already exists.");
        }

        // Assign "Ground" tag to Plane and Ramp planes
        string[] groundNames = { "Plane", "Ramp/Plane", "Ramp/Plane (1)", "Ramp/Plane (2)", "Ramp/Plane (3)", "Ramp/Platform" };
        foreach (string name in groundNames)
        {
            GameObject go = GameObject.Find(name);
            if (go != null)
            {
                go.tag = "Ground";
                Debug.Log($"Set tag of {name} to Ground.");
            }
        }
    }
}