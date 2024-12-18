using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIEnums;

public class PopupEventTriggerWorldSpace : MonoBehaviour
{
    [SerializeField] private MouseAction mouseAction;
    [SerializeField] private SlidePopup slidePopup;
    [SerializeField] private Camera mainCamera;
    LayerMask layer;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        layer = LayerMask.GetMask("CombineSaver");
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, layer);
            if (hit.collider == null)
            {
                return;
            }
            if (hit.collider.gameObject == gameObject)
            {
                if (mouseAction == MouseAction.CLICK || mouseAction == MouseAction.PRESS)
                {
                    slidePopup.TogglePopup();
                }
            }
            else
            {
                slidePopup.ClosePopup();
            }
        }

        if (mouseAction == MouseAction.HOVER)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                slidePopup.OpenPopup();
            }
            else
            {
                slidePopup.ClosePopup();
            }
        }
    }
}
