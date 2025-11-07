using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using TMPro; // For TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("End Screen UI")]
    public GameObject endScreenPanel;      // The panel itself
    public TextMeshProUGUI endScreenTitle; // The "Game Over" or "You Win" text
    public string menuSceneName = "StartMenu"; // The name of your menu scene

    // Call this from other scripts when the player loses
    public void ShowGameOver()
    {
        endScreenPanel.SetActive(true); // Show the panel
        endScreenTitle.text = "Game Over"; // Set the text
        Time.timeScale = 0f; // Freeze the game
    }

    // Call this from other scripts when the player wins
    public void ShowGameWon()
    {
        endScreenPanel.SetActive(true); // Show the panel
        endScreenTitle.text = "You Win!"; // Set the text
        Time.timeScale = 0f; // Freeze the game
    }

    // This will be called by the "Retry" button
    public void RetryGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This will be called by the "Quit" button
    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(menuSceneName); // Load the menu
    }
}