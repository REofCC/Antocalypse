using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    #region Attribute
    private BaseMapNode startNode, endNode;
    private UnderGroundGrid grid;

    #endregion

    #region Getter & Setter
    public void SetStartNode(BaseMapNode startNode)
    {
        this.startNode = startNode;
    }

    public void SetEndNode(BaseMapNode endNode)
    {
        this.endNode = endNode;
    }
    #endregion

    private void Start()
    {
        grid = GetComponent<UnderGroundGrid>();
    }
}
