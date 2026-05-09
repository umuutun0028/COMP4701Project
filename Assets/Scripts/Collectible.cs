using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
}
