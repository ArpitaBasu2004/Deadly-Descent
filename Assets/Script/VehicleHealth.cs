using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class VehicleHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthSlider;

    // This is the new variable.
    // We will drag our GameManager object here in the Inspector.
    [Header("Game Logic")]
    public GameManager gameManager;


    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Health: " + currentHealth);
        healthSlider.value = currentHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int damage = (int)collision.relativeVelocity.magnitude;

        if (damage > 5)
        {
            currentHealth -= damage;
            Debug.Log("Health: " + currentHealth);
            healthSlider.value = currentHealth;
            
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // This function is now updated
    void Die()
    {
        Debug.Log("GAME OVER");

        // Instead of reloading, we call the GameManager
        gameManager.ShowGameOver();
    }
}