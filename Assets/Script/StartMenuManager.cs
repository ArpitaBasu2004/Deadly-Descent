using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class StartMenuManager : MonoBehaviour
{
    // This is the name of your main game scene.
    // Make sure this matches your scene file!
    // Yours is "SampleScene"
    public string gameSceneName = "SampleScene"; 

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        // This quits the application
        // (Note: This will not work in the Unity Editor,
        // but it WILL work on your Android phone)
        Application.Quit();
    }
}