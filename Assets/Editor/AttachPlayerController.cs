using UnityEditor;
using UnityEngine;

public class AttachPlayerController
{
    public static void Execute()
    {
        GameObject main = GameObject.Find("Characters/Main");
        if (main != null)
        {
            if (main.GetComponent<PlayerController>() == null)
            {
                main.AddComponent<PlayerController>();
                Debug.Log("Attached PlayerController to Main.");
            }
            else
            {
                Debug.Log("PlayerController already attached to Main.");
            }
        }
        else
        {
            Debug.Log("Main character not found.");
        }
    }
}