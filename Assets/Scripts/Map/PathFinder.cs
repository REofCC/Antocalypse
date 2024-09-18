using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    #region Attribute
    private BaseMapNode startNode, endNode;
    private UnderGroundGrid grid;
    

    private List<BaseMapNode> waitingNodes;
    private List<BaseMapNode> finishedNodes;
    private BaseMapNode currentNode;
    #endregion

    #region Getter & Setter
    public void SetStartNode(BaseMapNode startNode)
    {
        this.startNode = startNode;
        currentNode = startNode;
    }

    public void SetEndNode(BaseMapNode endNode)
    {
        this.endNode = endNode;
    }
    #endregion

    private void Start()
    {
        grid = GetComponent<UnderGroundGrid>();
        waitingNodes = new List<BaseMapNode>();
        finishedNodes = new List<BaseMapNode>();
    }

    #region Function
    private int SortHcost(BaseMapNode anode, BaseMapNode bnode)
    {
        return anode.GethCost().CompareTo(bnode.GethCost());
    }
    private void CalcCost(BaseMapNode node)
    {
        int cost;

        if (currentNode.GetXpos() != node.GetXpos() && currentNode.GetYpos() != node.GetYpos())
        {
            cost = 14;
        }
        else
        {
            cost = 10;
        }

        if (cost + currentNode.GethCost() < node.GethCost())
        {
            node.SetgCost(cost);
            node.SetParentNode(currentNode);
            node.SethCost(cost + currentNode.GethCost());
        }
        else
            return;
    }
    private void AddWaitingNodes()
    {
        List<BaseMapNode> candidateNodes = grid.GetNeighborForAgrid(currentNode.GetXpos(), currentNode.GetYpos());
        for (int i = 0; i < candidateNodes.Count; i++)
        {
            if (finishedNodes.Contains(candidateNodes[i]))
                continue;
            else
            {
                waitingNodes.Add(candidateNodes[i]);
                CalcCost(candidateNodes[i]);
            }
        }
    }
    private void SelectNextNode()
    {
        waitingNodes.Remove(currentNode);
        finishedNodes.Add(currentNode);
        waitingNodes.Sort(SortHcost);
        currentNode = waitingNodes[0];
    }
    private List<BaseMapNode> ReturnRoute()
    {
        List<BaseMapNode> route = new List<BaseMapNode>();
        BaseMapNode cur = endNode;
        route.Add(cur);
        while (cur != startNode)
        {
            route.Add(cur.GetParentNode());
            cur = cur.GetParentNode();
        }
        return route;
    }
    public List<BaseMapNode> FindingPath(BaseMapNode start, BaseMapNode end)
    {
        waitingNodes.Add(start);
        SetEndNode(end);
        SetStartNode(start);
        currentNode = waitingNodes[0];
        while(currentNode == endNode)
        {
            AddWaitingNodes();
            SelectNextNode();
        }

        return ReturnRoute();
    }
    #endregion
}
