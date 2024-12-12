using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UIEnums;

public abstract class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum TooltipPositionMode { Auto, Manual }
    public enum TooltipDirection { TopLeft, TopRight, BottomLeft, BottomRight }

    [SerializeField] GameObject tooltipPrefab = null;
    [SerializeField] TooltipPositionMode positionMode = TooltipPositionMode.Auto;
    [SerializeField] TooltipDirection manualDirection = TooltipDirection.TopLeft;

    GameObject tooltip = null;

    public abstract void UpdateTooltip(GameObject tooltip);
    public abstract bool CanCreateTooltip();

    private void OnDestroy()
    {
        ClearTooltip();
    }

    private void OnDisable()
    {
        ClearTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Canvas parentCanvas = GetComponentInParent<Canvas>();

        if (tooltip && !CanCreateTooltip())
        {
            ClearTooltip();
        }

        if (!tooltip && CanCreateTooltip())
        {
            tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);
        }

        if (tooltip)
        {
            UpdateTooltip(tooltip);
            PositionTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearTooltip();
    }

    void PositionTooltip()
    {
        Canvas.ForceUpdateCanvases();

        Vector3[] tooltipCorners = new Vector3[4];
        tooltip.GetComponent<RectTransform>().GetWorldCorners(tooltipCorners);
        Vector3[] slotCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(slotCorners);

        int slotCorner, tooltipCorner;

        if (positionMode == TooltipPositionMode.Auto)
        {
            bool below = transform.position.y > Screen.height / 2;
            bool right = transform.position.x <= Screen.width / 2;

            slotCorner = GetCornerIndex(below, right);
            tooltipCorner = GetCornerIndex(!below, !right);
        }
        else
        {
            slotCorner = GetManualCornerIndex(manualDirection);
            tooltipCorner = GetManualCornerIndex(GetOppositeDirection(manualDirection));
        }

        tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + tooltip.transform.position;
    }

    int GetCornerIndex(bool below, bool right)
    {
        if (below && !right) return 0;
        else if (!below && !right) return 1;
        else if (!below && right) return 2;
        else return 3;
    }

    int GetManualCornerIndex(TooltipDirection direction)
    {
        switch (direction)
        {
            case TooltipDirection.TopLeft: return 1;
            case TooltipDirection.TopRight: return 2;
            case TooltipDirection.BottomLeft: return 0;
            case TooltipDirection.BottomRight: return 3;
            default: return 0;
        }
    }

    TooltipDirection GetOppositeDirection(TooltipDirection direction)
    {
        switch (direction)
        {
            case TooltipDirection.TopLeft: return TooltipDirection.BottomRight;
            case TooltipDirection.TopRight: return TooltipDirection.BottomLeft;
            case TooltipDirection.BottomLeft: return TooltipDirection.TopRight;
            case TooltipDirection.BottomRight: return TooltipDirection.TopLeft;
            default: return TooltipDirection.TopLeft;
        }
    }

    void ClearTooltip()
    {
        if (tooltip)
        {
            Destroy(tooltip.gameObject);
        }
    }
}
