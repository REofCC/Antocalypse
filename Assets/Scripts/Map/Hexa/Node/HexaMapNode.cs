using UnityEngine;

public class HexaMapNode
{
    #region Attribute
    bool buildable;
    bool walkable;

    Vector2Int cellPos;

    int fcost;
    int hcost;
    #endregion

    #region Getter & Setter
    public void SetCellPos(Vector2Int cellPos)
    {
        this.cellPos = cellPos;
    }
    public Vector2Int GetCellPos()
    {
        return cellPos;
    }
    public void SetFcost(int fcost)
    {
        this.fcost = fcost;
    }
    public int GetFcost()
    {
        return fcost;
    }
    public void SetHcost(int hcost)
    {
        this.hcost = hcost;
    }
    public int Gethcost()
    {
        return hcost;
    }
    public void SetBuildable(bool buildable)
    {
        this.buildable = buildable;
    }
    public bool GetBuildable()
    {
        return buildable;
    }
    public void SetWalkable(bool walkable)
    {
        this.walkable = walkable;
    }
    public bool GetWalkable()
    {
        return walkable;
    }
    #endregion

    #region Fuction

    #endregion

    #region Unity Function

    #endregion
}