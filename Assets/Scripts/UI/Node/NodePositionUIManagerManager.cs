using UnityEngine;
using UnityEngine.EventSystems;

public class NodePositionUIManager :  MonoBehaviour
{
    [SerializeField] NodeOutlineHighlighter nodeOutlineHighlighter;
    [SerializeField] NodeOrderPanelUI nodeOrderPanelUI;
    HexaMapNode node;
    BaseResource baseResource;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                node = ClickTile(Input.mousePosition);
                baseResource = node.GetResource();
                HighlightNode(node);
            }
        }
    }

    private HexaMapNode ClickTile(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));        

        Vector2Int gridPos = MapManager.Map.UnderPosCalc.CalcGridPos(mouseWorldPos);
        return MapManager.Map.UnderGrid.GetNode(mouseWorldPos);
    }

    bool NodeCheck(TileType nodeTileType)
    {
        if (nodeTileType == TileType.TravelNode || nodeTileType == TileType.ResourceNode)
        {
            return true;
        }
        return false;
    }

    public void HighlightNode(HexaMapNode node)
    {
        if(!NodeCheck(node.GetTileType()))
        {
            ClearHighlight();
            return;
        }

        nodeOutlineHighlighter.HighlightNode(node);
        Vector3 highlightedNodePosition = node.GetWorldPos();
        nodeOrderPanelUI.SetUIPosition(highlightedNodePosition, node, baseResource);
    }

    public void ClearHighlight()
    {
        nodeOutlineHighlighter.ClearHighlight();
        nodeOrderPanelUI.HideUI();
    }
}
