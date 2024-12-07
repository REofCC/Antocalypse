using UnityEngine;

public class NodePositionUIManager : MonoBehaviour
{
    [SerializeField] NodeOutlineHighlighter nodeOutlineHighlighter;
    [SerializeField] NodeExplorationUI nodeExplorationUI;

    public void HighlightNode(HexaMapNode node)
    {
        nodeOutlineHighlighter.HighlightNode(node);
        Vector3 highlightedNodePosition = node.GetWorldPos();
        nodeExplorationUI.SetUIPosition(highlightedNodePosition);
    }

    public void ClearHighlight()
    {
        nodeOutlineHighlighter.ClearHighlight();
        nodeExplorationUI.HideUI();
    }
}
