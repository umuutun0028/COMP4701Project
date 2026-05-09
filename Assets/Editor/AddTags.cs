using UnityEditor;
using UnityEngine;

public class AddTags
{
    public static void Execute()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        
        string[] tagsToAdd = { "Player", "Enemy" };
        
        foreach (string tag in tagsToAdd)
        {
            bool found = false;
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
                if (t.stringValue.Equals(tag)) { found = true; break; }
            }
            
            if (!found)
            {
                tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
                SerializedProperty n = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
                n.stringValue = tag;
                Debug.Log($"Added '{tag}' tag.");
            }
        }
        tagManager.ApplyModifiedProperties();
    }
}