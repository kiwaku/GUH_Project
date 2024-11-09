using System;
using Unity.Mathematics;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform focusPoint;          // The point around which the camera rotates
    public float rotationSpeed = 5.0f;    // Speed of rotation
    public float zoomSpeed = 10.0f;       // Speed of zooming
    public float minVerticalAngle = -80f; // Minimum vertical angle (near the south pole)
    public float maxVerticalAngle = 80f;  // Maximum vertical angle (near the north pole)

    private float currentVerticalAngle = 0f;    // Track the current vertical angle
    private float currentDistance;              // Distance from focus point
    private Vector3 lastMousePosition;

    void Start()
    {
        // Initialize distance based on the starting position
        currentDistance = Vector3.Distance(transform.position, focusPoint.position);

        // Initialize the vertical angle based on the current rotation
        Vector3 directionToFocus = (focusPoint.position - transform.position).normalized;
        currentVerticalAngle = Vector3.SignedAngle(Vector3.ProjectOnPlane(directionToFocus, Vector3.up), directionToFocus, transform.right);
    }

    void Update()
    {
        HandleMouseDrag();
        HandleZoom();
        LookAtFocusPoint();  // Ensure the camera is always looking at the focus point
    }

    // Handle rotating the camera with mouse drag
    void HandleMouseDrag()
    {
        transform.position = new Vector3(transform.position[0], Mathf.Clamp(transform.position[1], -80f, 80f), transform.position[2]);
        if (Input.GetMouseButton(0))  // Check if the left mouse button is held down
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;  // Calculate mouse movement
            float horizontal = delta.x * rotationSpeed * Time.deltaTime;
            float vertical = -delta.y * rotationSpeed * Time.deltaTime;
            if (transform.position[1] > 80f || transform.position[1] < -80f)
            {
                vertical = 0;
            }
            // Horizontal rotation (around the Y-axis of the focus point)
            transform.RotateAround(focusPoint.position, Vector3.up, horizontal);

            transform.RotateAround(focusPoint.position, transform.right, vertical);  // Rotate around right vector
        }

        // Update the last mouse position
        lastMousePosition = Input.mousePosition;
    }

    // Handle zooming in and out using the mouse scroll wheel
    void HandleZoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance - zoom, 5f, 100f);  // Adjust distance with clamping

        // Update camera position based on the new distance from the focus point
        Vector3 direction = (transform.position - focusPoint.position).normalized;
        transform.position = focusPoint.position + direction * currentDistance;
    }

    // Ensure the camera always looks at the focus point
    void LookAtFocusPoint()
    {
        transform.LookAt(focusPoint);
    }
}
