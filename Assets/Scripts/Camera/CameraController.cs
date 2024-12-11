using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float screenEdgeBorderThickness = 100f;
    public float zoomSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    float targetZoom;
    float zoomVelocity;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }
    
    void HandleMovement()
    {
        Vector3 move = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))// || Input.mousePosition.y >= Screen.height - screenEdgeBorderThickness)
        {
            move.y += 1;
        }
        if (Input.GetKey(KeyCode.S))// || Input.mousePosition.y <= screenEdgeBorderThickness)
        {
            move.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))// || Input.mousePosition.x <= screenEdgeBorderThickness)
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))// || Input.mousePosition.x >= Screen.width - screenEdgeBorderThickness)
        {
            move.x += 1;
        }
        
        move.Normalize();        
        transform.position += move * moveSpeed * Time.unscaledDeltaTime;
    }
    
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, 0.1f);
    }
}
