using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // This is the new variable.
    // We will drag our GameManager object here in the Inspector.
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instead of just logging, we now call the GameManager
            Debug.Log("Finish Line Reached!");
            gameManager.ShowGameWon();
        }
    }
}