using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1CameraMovement : MonoBehaviour
{
    public float upwardMovement = 0.5f; // The amount by which the camera moves up after each successful jump
    public float smoothSpeed = 2f;      // Speed at which the camera moves to the new position

    private Transform cameraTransform;  // Reference to the camera's transform
    public Vector3 cameraTargetPosition;     // The target position the camera will move to
    private bool shouldMoveCamera = false; // Flag to control camera movement

    public Transform backgroundTransform;
    public Vector3 backgroundTargetPosition;

    
    public Vector3 initialCameraPosition;
    public Vector3 initialBackgroundTransformPosition;

    private void Start()
    {

        initialCameraPosition = transform.position;
        initialBackgroundTransformPosition = backgroundTransform.position;

        // Get the camera's transform
        cameraTransform = Camera.main.transform;
        // Initialize the target position to the current camera position
        cameraTargetPosition = cameraTransform.position;


        backgroundTargetPosition = backgroundTransform.position;

    }

    private void Update()
    {
        // If the flag is set, smoothly move the camera towards the target position
        if (shouldMoveCamera)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraTargetPosition, smoothSpeed * Time.deltaTime);

            // If the camera is close enough to the target position, stop moving
            if (Vector3.Distance(cameraTransform.position, cameraTargetPosition) < 0.01f)
            {
                cameraTransform.position = cameraTargetPosition;
                shouldMoveCamera = false;
            }

            backgroundTransform.position = Vector3.Lerp(backgroundTransform.position, backgroundTargetPosition, smoothSpeed * Time.deltaTime);
            if (Vector3.Distance(backgroundTransform.position, backgroundTargetPosition) < 0.01f)
            {
                backgroundTransform.position = backgroundTargetPosition;
                shouldMoveCamera = false;
            }
        }
    }

    // This method should be called when the player successfully lands a jump
    public void OnSuccessfulJump()
    {
        // Set the new target position upward
        cameraTargetPosition += new Vector3(0, upwardMovement, 0);

        backgroundTargetPosition += new Vector3(0, upwardMovement, 0);

        // Enable the camera movement
        shouldMoveCamera = true;
    }
}
