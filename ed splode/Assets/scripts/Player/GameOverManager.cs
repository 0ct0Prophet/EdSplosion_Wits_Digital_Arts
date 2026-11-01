using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject[] gameplayHUD;

    public void GameOver()
{
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    // Hide all gameplay HUD elements
    foreach (GameObject hud in gameplayHUD)
    {
        if (hud != null)
            hud.SetActive(false);
    }

    gameOverPanel.SetActive(true);
    Time.timeScale = 0f;
}

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}