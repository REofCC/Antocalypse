using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NodeOutlineHighlighter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Color highlightColor = Color.yellow;
    [SerializeField] float lineWidth = 0.05f;
    [SerializeField] float hexRadius = 0.41f;

    private Vector3[] hexVertices = new Vector3[7];

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void HighlightNode(HexaMapNode node)
    {
        Vector3 center = node.GetWorldPos();

        for (int i = 0; i < 6; i++)
        {
            float angle = Mathf.Deg2Rad * (60 * i - 30);
            hexVertices[i] = new Vector3(
                center.x + hexRadius * Mathf.Cos(angle),
                center.y + hexRadius * Mathf.Sin(angle),
                center.z = -0.1f
            );
        }

        hexVertices[6] = hexVertices[0];

        lineRenderer.positionCount = hexVertices.Length;
        lineRenderer.SetPositions(hexVertices);
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = highlightColor;
        lineRenderer.endColor = highlightColor;
        lineRenderer.enabled = true;
    }

    public void ClearHighlight()
    {
        lineRenderer.enabled = false;
    }
}
