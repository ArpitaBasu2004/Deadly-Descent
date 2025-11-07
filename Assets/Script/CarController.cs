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

    public WheelCollider wheelFL;

    public WheelCollider wheelFR;

    public WheelCollider wheelBL;

    public WheelCollider wheelBR;



    [Header("Wheel Visuals")]

    public Transform wheelVisualFL;

    public Transform wheelVisualFR;

    public Transform wheelVisualBL;

    public Transform wheelVisualBR;



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

    public TextMeshProUGUI distanceText;

    private float totalDistance = 0f;



    // --- NEW STUCK TIMER VARIABLES ---

    [Header("Game Logic")]

    public GameManager gameManager; // Slot for the GameManager

    public float maxStuckTime = 15f; // Time in seconds

    public float stuckSpeedThreshold = 0.5f; // Speed (m/s) below which we are "stuck"

    private float stuckTimer = 0f; // Our countdown timer

   

    // --- (EXISTING) INPUT VARIABLES ---

    private float verticalInput = 0f;

    private float horizontalInput = 0f;

    private bool isBraking = false;



    void Start()

    {

        rb = GetComponent<Rigidbody>();

        engineAudio = GetComponent<AudioSource>();

    }



    void FixedUpdate()

    {

        // Apply motor force

        wheelBL.motorTorque = verticalInput * motorForce;

        wheelBR.motorTorque = verticalInput * motorForce;



        // Apply steering

        wheelFL.steerAngle = horizontalInput * steerAngle;

        wheelFR.steerAngle = horizontalInput * steerAngle;



        // Apply brakes

        if (isBraking)

        {

            wheelFL.brakeTorque = brakeForce;

            wheelFR.brakeTorque = brakeForce;

            wheelBL.brakeTorque = brakeForce;

            wheelBR.brakeTorque = brakeForce;

        }

        else

        {

            wheelFL.brakeTorque = 0;

            wheelFR.brakeTorque = 0;

            wheelBL.brakeTorque = 0;

            wheelBR.brakeTorque = 0;

        }

    }



    void Update()

    {

        UpdateWheelVisuals();

        UpdateEngineSound();

        UpdateDistance();



        // --- NEW FUNCTION CALL ---

        UpdateStuckTimer(); // Check if the car is stuck



        // (For testing in editor)

        // #if UNITY_EDITOR

        // KeyboardTestInput();

        // #endif

    }

   

    // --- NEW STUCK TIMER FUNCTION ---

    void UpdateStuckTimer()

    {

        // Get the car's current speed

        float speed = rb.linearVelocity.magnitude;



        // Check if the car is moving slower than our "stuck" speed

        if (speed < stuckSpeedThreshold)

        {

            // If we are slow, add time to the stuck timer

            stuckTimer += Time.deltaTime;

        }

        else

        {

            // If we are moving fast enough, reset the timer

            stuckTimer = 0f;

        }



        // Now, check if our timer has gone past the 15-second limit

        if (stuckTimer >= maxStuckTime)

        {

            // We've been stuck for too long!

            Debug.Log("GAME OVER - Car was stuck!");

            gameManager.ShowGameOver(); // Trigger the Game Over screen

        }

    }



    // (This function is just for keyboard testing)

    private void KeyboardTestInput()

    {

        float v = Input.GetAxis("Vertical");

        float h = Input.GetAxis("Horizontal");
       

        bool b = Input.GetKey(KeyCode.Space);

        verticalInput = v;

        horizontalInput = h;

        isBraking = b;

    }



    void UpdateEngineSound()

    {

        float speed = rb.linearVelocity.magnitude;

        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeedForPitch);

        engineAudio.pitch = pitch;

    }



    void UpdateDistance()

    {

        totalDistance += rb.linearVelocity.magnitude * Time.deltaTime;

        if (distanceText != null)

        {

            distanceText.text = "Distance: " + totalDistance.ToString("F0") + "m";

        }

    }



    void UpdateWheelVisuals()

    {

        UpdateSingleWheel(wheelFL, wheelVisualFL);

        UpdateSingleWheel(wheelFR, wheelVisualFR);

        UpdateSingleWheel(wheelBL, wheelVisualBL);

        UpdateSingleWheel(wheelBR, wheelVisualBR);

    }



    void UpdateSingleWheel(WheelCollider collider, Transform visual)

    {

        Vector3 pos;

        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);

        visual.position = pos;

        visual.rotation = rot;

    }



    // --- (EXISTING) PUBLIC FUNCTIONS FOR BUTTONS ---

    public void OnGasPressed() {
        
        verticalInput = 1f; }

    public void OnGasReleased() { 
         
        verticalInput = 0f; }

    public void OnBrakePressed() { verticalInput = -1f; isBraking = true; }

    public void OnBrakeReleased() { verticalInput = 0f; isBraking = false; }

    public void OnLeftPressed() { horizontalInput = -1f; }

    public void OnLeftReleased() { horizontalInput = 0f; }

    public void OnRightPressed() { horizontalInput = 1f; }

    public void OnRightReleased() { horizontalInput = 0f; }

}