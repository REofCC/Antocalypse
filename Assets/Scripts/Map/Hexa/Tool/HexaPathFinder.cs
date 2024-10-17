using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaPathFinder : MonoBehaviour
{
    #region 
    HexaMapNode start;
    HexaMapNode end;
    HexaMapNode current;
    HexaGrid grid;

    List<HexaMapNode> candidates = new();
    List<HexaMapNode> finished = new();
    #endregion
    #region  Setter & Getter
    public void SetStartNode( HexaMapNode start)
    {
        this.start = start;
    }
    public HexaMapNode GetStartNode()
    {
        return start;
    }
    public void SetEndNode( HexaMapNode end)
    {
        this.end = end;
    }
    public HexaMapNode GetEndNode()
    {
        return end;
    }
    #endregion

    #region Function
    private void ClearList()
    {
        candidates.Clear();
    }
    private int SortHcost(HexaMapNode anode, HexaMapNode bnode)
    {
        return anode.GetHcost().CompareTo(bnode.GetHcost());
    }
    private void CalcCost(HexaMapNode node)
    {
        int cost = 1;

        if (cost + current.GetHcost() < node.GetHcost() || node.GetParent() == null)
        {
            node.SetParent(current);
            node.SetHcost(cost + current.GetHcost());
        }
    }
    private void SelectNextNode()
    {
        candidates.Remove(current);
        finished.Add(current);
        candidates.Sort(SortHcost);
        current = candidates[0];
    }
    private void GetCandidateNode(HexaMapNode node)
    {
        List<HexaMapNode> nodes = grid.GetNeighborWalkableNode(node.GetCellPos().x, node.GetCellPos().y);
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!finished.Contains(nodes[i]))
            {
                candidates.Add(nodes[i]);
                CalcCost(nodes[i]);
            }
        }
    }
    public List<Vector3> GetRoute()
    {
        HexaMapNode idx = end;
        List<Vector3> routes = new();

        routes.Add(grid.GetCellPosCalc().CalcWorldPos(idx));
        while(idx != start)
        {
            idx = idx.GetParent();
            routes.Add(grid.GetCellPosCalc().CalcWorldPos(idx));
        }
        return routes;
    }
    public List<Vector3> StartFind(HexaMapNode start, HexaMapNode end)
    {
        ClearList();
        
        SetStartNode(start);
        SetEndNode(end);
        current = start;
        while (current != end)
        {
            GetCandidateNode(current);
            SelectNextNode();
            if(candidates.Count == 0)
            {
                return null;
            }
        }
        return GetRoute();
    }
    #endregion

    #region Unity Function
    private void Start()
    {
        grid = gameObject.GetComponent<HexaGrid>();
    }
    #endregion
}