using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaPathFinder : MonoBehaviour
{
    #region Attribute
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
    private void ResetCost()
    {
        for(int i=0; i<finished.Count; i++)
        {
            finished[i].SetHcost(999999);
            finished[i].SetParent(null);
        }
        for (int i = 0; i < candidates.Count; i++)
        {
            candidates[i].SetHcost(999999);
            candidates[i].SetParent(null);
        }
    }
    private void ClearList()
    {
        candidates.Clear();
        finished.Clear();
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
        if (candidates.Count <= 0)
        {
            Debug.Log("Error : Cant Find Path");
            current = null;
            return;
        }
        current = candidates[0];
    }
    private void GetCandidateNode(HexaMapNode node)
    {
        List<HexaMapNode> nodes = grid.GetNeighborWalkableNode(node.GetCellPos().x, node.GetCellPos().y);
        for (int i = 0; i < nodes.Count; i++)
        {
            CalcCost(nodes[i]);
            if (!finished.Contains(nodes[i]) && !candidates.Contains(nodes[i]))
            {
                candidates.Add(nodes[i]);
            }
        }
    }
    private List<Vector3> GetRoute()
    {
        HexaMapNode idx = end;
        List<Vector3> routes = new();

        routes.Add(grid.GetCellPosCalc().CalcWorldPos(idx));
        while(idx != start)
        {
            Debug.Log(idx.GetCellPos());
            idx = idx.GetParent();
            routes.Add(grid.GetCellPosCalc().CalcWorldPos(idx));
        }
        return routes;
    }
    private bool CheckEnd()
    {
        Vector2Int pos = current.GetGridPos();
        List<HexaMapNode> nodes = grid.GetNeighborNode(pos.x, pos.y);
        for(int i =  0; i < nodes.Count; i++)
        {
            if (nodes[i] == end)
            {
                end = current;
                return true;
            }
                
        }
        return false;
    }
    public List<Vector3> PathFinding(HexaMapNode start, HexaMapNode end)
    {
        ClearList();
        
        SetStartNode(start);
        SetEndNode(end);
        current = start;
        while (current != end)
        {
            GetCandidateNode(current);
            SelectNextNode();
            if(current == null)
            {
                return null;
            }
        }
        
        List<Vector3> routes =  GetRoute();
        ResetCost();
        return routes;
    }

    public List<Vector3> ReachWallPathFinding(HexaMapNode start, HexaMapNode end)
    {
        ClearList();
        SetStartNode(start);
        SetEndNode(end);
        current = start;
        while (true)
        {
            if (CheckEnd())
            {
                List<Vector3> routes = GetRoute();
                ResetCost();
                return routes;
            }
            GetCandidateNode(current);
            SelectNextNode();
            
            if (current == null)
            {
                return null;
            }
        }
    }
    #endregion

    #region Unity Function
    public void OnAwake(HexaGrid grid)
    {
        this.grid = grid;
    }
    #endregion
}