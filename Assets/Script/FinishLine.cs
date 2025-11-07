using UnityEngine;
using UnityEngine.SceneManagement; // We need this to change scenes later

public class FinishLine : MonoBehaviour
{
    // This function runs when anything with a Rigidbody enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // We check if the object that entered is the "Player"
        if (other.CompareTag("Player"))
        {
            // If it is the player, they win!
            Debug.Log("YOU WIN!");
            
            // For now, we'll just reload the game
            // Later, we'll change this to load the "Game Over" screen
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}