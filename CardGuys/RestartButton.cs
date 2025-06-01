using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    /// <summary>
    /// Call this from a UI Button’s OnClick to reload the current scene.
    /// </summary>
    public void RestartLevel()
    {
        // Unpause if you had paused time
        Time.timeScale = 1f;

        // Reload the active scene
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
