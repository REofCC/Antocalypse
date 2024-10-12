using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UIEnums;

public abstract class TooltipSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject tooltipPrefab = null;
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

        if(tooltip && !CanCreateTooltip())
        {
            ClearTooltip();            
        }

        if(!tooltip && CanCreateTooltip())
        {
            tooltip = Instantiate(tooltipPrefab, parentCanvas.transform);            
        }

        if(tooltip)
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

        bool below = transform.position.y > Screen.height / 2;
        bool right = transform.position.x < Screen.width / 2;

        int slotCorner = GetCornerIndex(below, right);
        int tooltipCorner = GetCornerIndex(!below, !right);

        tooltip.transform.position = slotCorners[slotCorner] - tooltipCorners[tooltipCorner] + tooltip.transform.position;
    }

    int GetCornerIndex(bool below, bool right)
    {
        if(below && right)
        {
            return 0;
        }
        else if (below && !right)
        {
            return 1;
        }
        else if (!below && !right)
        {
            return 2;
        }
        else
        {
            return 3;
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
