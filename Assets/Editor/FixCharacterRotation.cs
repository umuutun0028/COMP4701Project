using UnityEditor;
using UnityEngine;

public class FixCharacterRotation
{
    public static void Execute()
    {
        GameObject main = GameObject.Find("Characters/Main");
        if (main != null)
        {
            Rigidbody rb = main.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                Debug.Log("Froze X and Z rotation on Main character's Rigidbody.");
            }
            else
            {
                Debug.Log("No Rigidbody found on Main character.");
            }
        }
        else
        {
            Debug.Log("Main character not found.");
        }
    }
}