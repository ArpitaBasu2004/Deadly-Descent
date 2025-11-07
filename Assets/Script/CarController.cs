/*using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider wheelFL; // Front Left
    public WheelCollider wheelFR; // Front Right
    public WheelCollider wheelBL; // Back Left
    public WheelCollider wheelBR; // Back Right

    [Header("Car Physics")]
    public float motorForce = 1500f;
    public float brakeForce = 2000f;
    public float steerAngle = 30f;

    // FixedUpdate is used for all physics calculations
    void FixedUpdate()
    {
        // 1. Get input from the player (Arrow Keys or WASD)
        float vertical = Input.GetAxis("Vertical");     // Up/Down
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right

        // 2. Apply motor force to the rear wheels to move
        wheelBL.motorTorque = vertical * motorForce;
        wheelBR.motorTorque = vertical * motorForce;

        // 3. Apply steering angle to the front wheels to turn
        wheelFL.steerAngle = horizontal * steerAngle;
        wheelFR.steerAngle = horizontal * steerAngle;

        // 4. Apply brakes if the player presses the Space bar
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply brake force to all wheels
            wheelFL.brakeTorque = brakeForce;
            wheelFR.brakeTorque = brakeForce;
            wheelBL.brakeTorque = brakeForce;
            wheelBR.brakeTorque = brakeForce;
        }
        else
        {
            // Release the brakes (set force to 0)
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }
    }
}*/
/*
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider wheelFL; // Front Left
    public WheelCollider wheelFR; // Front Right
    public WheelCollider wheelBL; // Back Left
    public WheelCollider wheelBR; // Back Right

    // --- NEW STUFF (PART 1) ---
    [Header("Wheel Visuals")]
    public Transform wheelVisualFL; // Front Left Visual Wheel
    public Transform wheelVisualFR; // Front Right Visual Wheel
    public Transform wheelVisualBL; // Back Left Visual Wheel
    public Transform wheelVisualBR; // Back Right Visual Wheel
    // --- END OF NEW STUFF (PART 1) ---


    [Header("Car Physics")]
    public float motorForce = 1500f;
    public float brakeForce = 2000f;
    public float steerAngle = 30f;

    // FixedUpdate is used for all physics calculations
    void FixedUpdate()
    {
        // 1. Get input from the player (Arrow Keys or WASD)
        float vertical = Input.GetAxis("Vertical");     // Up/Down
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right

        // 2. Apply motor force to the rear wheels to move
        wheelBL.motorTorque = vertical * motorForce;
        wheelBR.motorTorque = vertical * motorForce;

        // 3. Apply steering angle to the front wheels to turn
        wheelFL.steerAngle = horizontal * steerAngle;
        wheelFR.steerAngle = horizontal * steerAngle;

        // 4. Apply brakes if the player presses the Space bar
        if (Input.GetKey(KeyCode.Space))
        {
            wheelFL.brakeTorque = brakeForce;
            wheelFR.brakeTorque = brakeForce;
            wheelBL.brakeTorque = brakeForce;
            wheelBR.brakeTorque = brakeForce;
        }
        else
        {
            // Release the brakes (set force to 0)
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }
    }

    
    // Update is called once per frame, good for visuals
    void Update()
    {
        UpdateWheelVisuals();
    }

    // This function updates all four wheels
    void UpdateWheelVisuals()
    {
        UpdateSingleWheel(wheelFL, wheelVisualFL);
        UpdateSingleWheel(wheelFR, wheelVisualFR);
        UpdateSingleWheel(wheelBL, wheelVisualBL);
        UpdateSingleWheel(wheelBR, wheelVisualBR);
    }

    // This function matches a single visual wheel to its collider
    void UpdateSingleWheel(WheelCollider collider, Transform visual)
    {
        Vector3 pos;
        Quaternion rot;
        
        // Get the collider's world position and rotation
        collider.GetWorldPose(out pos, out rot); 
        
        // Set the visual wheel's position and rotation
        visual.position = pos;
        visual.rotation = rot;
    }
   
}*/
using UnityEngine;
using TMPro; // Required for TextMeshPro UI

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider wheelFL; // Front Left
    public WheelCollider wheelFR; // Front Right
    public WheelCollider wheelBL; // Back Left
    public WheelCollider wheelBR; // Back Right

    [Header("Wheel Visuals")]
    public Transform wheelVisualFL; // Front Left Visual Wheel
    public Transform wheelVisualFR; // Front Right Visual Wheel
    public Transform wheelVisualBL; // Back Left Visual Wheel
    public Transform wheelVisualBR; // Back Right Visual Wheel

    [Header("Car Physics")]
    public float motorForce = 1500f;
    public float brakeForce = 2000f;
    public float steerAngle = 30f;

    [Header("Audio")]
    private Rigidbody rb;
    private AudioSource engineAudio;
    public float minPitch = 0.8f;
    public float maxPitch = 2.5f;
    public float maxSpeedForPitch = 50f;

    [Header("UI")]
    public TextMeshProUGUI distanceText; // Slot for our text
    private float totalDistance = 0f;    // Tracks the total distance

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody and AudioSource components
        rb = GetComponent<Rigidbody>();
        engineAudio = GetComponent<AudioSource>();
    }

    // FixedUpdate is used for all physics calculations
    void FixedUpdate()
    {
        // 1. Get input from the player (Arrow Keys or WASD)
        float vertical = Input.GetAxis("Vertical");     // Up/Down
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right

        // 2. Apply motor force to the rear wheels to move
        wheelBL.motorTorque = vertical * motorForce;
        wheelBR.motorTorque = vertical * motorForce;

        // 3. Apply steering angle to the front wheels to turn
        wheelFL.steerAngle = horizontal * steerAngle;
        wheelFR.steerAngle = horizontal * steerAngle;

        // 4. Apply brakes if the player presses the Space bar
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply brake force to all wheels
            wheelFL.brakeTorque = brakeForce;
            wheelFR.brakeTorque = brakeForce;
            wheelBL.brakeTorque = brakeForce;
            wheelBR.brakeTorque = brakeForce;
        }
        else
        {
            // Release the brakes (set force to 0)
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This will update your wheel visuals
        UpdateWheelVisuals();
        
        // This will update the engine sound
        UpdateEngineSound();

        // --- NEW ---
        // This will update the distance counter
        UpdateDistance();
    }
    
    void UpdateEngineSound()
    {
        // Get the car's current speed
        float speed = rb.linearVelocity.magnitude; // Use linearVelocity instead

        // Calculate a new pitch based on speed
        // The "Mathf.Lerp" smoothly moves between minPitch and maxPitch
        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeedForPitch);

        // Apply the new pitch to the audio source
        engineAudio.pitch = pitch;
    }

    // --- NEW FUNCTION ---
    void UpdateDistance()
    {
        // 1. Add the distance moved this frame to our total
        // (speed * time)
        totalDistance += rb.linearVelocity.magnitude * Time.deltaTime;

        // 2. Update the text
        // "F0" formats the number to have 0 decimal places
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + totalDistance.ToString("F0") + "m";
        }
    }

    // This function updates all four wheels
    void UpdateWheelVisuals()
    {
        UpdateSingleWheel(wheelFL, wheelVisualFL);
        UpdateSingleWheel(wheelFR, wheelVisualFR);
        UpdateSingleWheel(wheelBL, wheelVisualBL);
        UpdateSingleWheel(wheelBR, wheelVisualBR);
    }

    // This function matches a single visual wheel to its collider
    void UpdateSingleWheel(WheelCollider collider, Transform visual)
    {
        Vector3 pos;
        Quaternion rot;
        
        // Get the collider's world position and rotation
        collider.GetWorldPose(out pos, out rot); 
        
        // Set the visual wheel's position and rotation
        visual.position = pos;
        visual.rotation = rot;
    }
}