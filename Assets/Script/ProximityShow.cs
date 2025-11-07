using UnityEngine;

public class ProximityShow : MonoBehaviour
{
    private Transform player; // Will be auto-filled
    public float triggerDistance = 40f;
    private MeshRenderer myMesh;

    // Timer variables for performance
    private float checkTimer;
    private float checkInterval = 0.25f; // Check 4 times per second (1 / 0.25 = 4)
    
    void Start()
    {
        // This automatically finds the object tagged "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Get this object's own Mesh Renderer
        myMesh = GetComponent<MeshRenderer>();
        
        // Make sure it's invisible at the start
        if (myMesh != null)
        {
            myMesh.enabled = false;
        }
    }

    void Update()
    {
        // --- Performance Timer ---
        // Count down the timer
        checkTimer -= Time.deltaTime;
        
        // If the timer is still greater than 0, do nothing.
        if (checkTimer > 0)
        {
            return; // Not time to check yet
        }
        
        // If the timer hits 0, reset it
        checkTimer = checkInterval; 
        // --- End of Timer ---

        
        // If the rock is already visible, or the player isn't set, do nothing.
        if (myMesh == null || myMesh.enabled || player == null)
        {
            return;
        }

        // Calculate the distance between this rock and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if the player is close enough
        if (distance < triggerDistance)
        {
            // Make the rock visible
            myMesh.enabled = true;
        }
    }
}