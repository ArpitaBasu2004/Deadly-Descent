using UnityEngine;
using UnityEngine.SceneManagement; // To reload the scene
using UnityEngine.UI; // Required for UI components like Sliders

public class VehicleHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthSlider; // This is the slot for our slider

    
    // This is called once at the start of the game
    void Start()
    {
        // Set the car's health to full when the game starts
        currentHealth = maxHealth;
        
        // This is our first compulsory debug message
        Debug.Log("Health: " + currentHealth);
        
        // Update the slider to be full at the start
        healthSlider.value = currentHealth;
    }

    // This function is called automatically by Unity's physics
    // whenever this object collides with another one.
    private void OnCollisionEnter(Collision collision)
    {
        // 1. Calculate how hard the crash was
        int damage = (int)collision.relativeVelocity.magnitude;

        // 2. Only apply damage if the crash was hard enough (speed > 5)
        if (damage > 5)
        {
            // 3. Subtract the damage from our health
            currentHealth -= damage;

            // 4. This is the COMPULSORY debug message from the assignment
            Debug.Log("Health: " + currentHealth);
            
            // 5. Update the slider's visual
            healthSlider.value = currentHealth;

            // 6. Play the collision sound
           
            // 7. Check if the car is destroyed
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // This is our function for what happens when we die
    void Die()
    {
        Debug.Log("GAME OVER");
        
        // For now, just reload the level.
        // Later, we will change this to load the "Game Over" screen.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}