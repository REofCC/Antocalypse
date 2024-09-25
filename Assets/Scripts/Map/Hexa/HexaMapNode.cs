using UnityEngine;

public class HexaMapNode
{
    #region Attribute
    Sprite sprites;

    HexaMapTileInfo tileInfo;

    bool buildable;

    Vector2 cellPos;

    int fcost;
    int hcost;
    #endregion

    #region Getter & Setter
    public void SetSprites(Sprite sprite)
    {
        this.sprites = sprite; 
    }
    public Sprite GetSprites()
    {
        return sprites;
    }
    public void SetTileInfo(HexaMapTileInfo tileInfo)
    {
        this.tileInfo = tileInfo;   
    }
    public bool GetTileInfo()
    {
        return tileInfo;
    }
    public void SetCellPos(Vector2 cellPos)
    {
        this.cellPos = cellPos;
    }
    public Vector2 GetCellPos()
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
    #endregion

    #region Fuction

    #endregion

    #region Unity Function

    #endregion
}