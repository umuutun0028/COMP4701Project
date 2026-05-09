using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool restartSceneOnGameOver = true;

    public bool IsGameOver { get; private set; }
    public int Score { get; private set; }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("Score: " + Score);
    }

    public void GameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;

        // Minimal behaviour: log + optionally restart.
        Debug.Log("Game Over");

        if (restartSceneOnGameOver)
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}