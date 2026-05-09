using UnityEngine;

/// <summary>
/// Simple generic Singleton base.
/// Inherit like: <c>public class GameManager : Instance&lt;GameManager&gt;</c>
/// and access via <c>GameManager.Instance</c>.
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindFirstObjectByType<T>();
            if (_instance != null) return _instance;

            // Fallback: create one if none exists in scene.
            var go = new GameObject(typeof(T).Name);
            _instance = go.AddComponent<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}