using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Settings")]
    public float speed = 5f;

    private Transform target;

    void Start()
    {
        // Start by moving towards Point A
        target = pointA;
    }

    void Update()
    {
        // Calculate the speed for this frame
        float step = speed * Time.deltaTime;

        // Move our object's position towards the target's position
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if we are very close to the target
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // If we reached Point A, our new target is Point B
            if (target == pointA)
            {
                target = pointB;
            }
            // If we reached Point B, our new target is Point A
            else
            {
                target = pointA;
            }
        }
    }
}