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

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }
    
    void HandleMovement()
    {
        Vector3 move = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - screenEdgeBorderThickness)
        {
            move.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= screenEdgeBorderThickness)
        {
            move.y -= 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= screenEdgeBorderThickness)
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - screenEdgeBorderThickness)
        {
            move.x += 1;
        }
        
        move.Normalize();
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scroll * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
    }
}
