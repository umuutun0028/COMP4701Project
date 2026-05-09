using UnityEditor;
using UnityEngine;

public class RemoveRigidbodyFromCamera
{
    public static void Execute()
    {
        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null)
        {
            Rigidbody2D rb = camera.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Object.DestroyImmediate(rb);
                Debug.Log("Removed Rigidbody2D from Main Camera.");
            }
            else
            {
                Debug.Log("No Rigidbody2D found on Main Camera.");
            }
        }
        else
        {
            Debug.Log("Main Camera not found.");
        }
    }
}