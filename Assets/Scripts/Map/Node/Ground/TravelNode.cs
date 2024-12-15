using UnityEngine;

public class TravelNode : HexaMapNode
{
    #region Attribute
    Event travelEvent;
    #endregion

    #region Getter & Setter
    public void SetEvent(Event travelEvent)
    {
        this.travelEvent = travelEvent;
    }
    public Event GetEvent() 
    { 
        return this.travelEvent; 
    }
    #endregion

    #region Function
    public void OnEventComplete()
    {
        travelEvent = null;
        Vector3Int pos = this.GetCellPos();
        MapManager.Map.UpGrid.SwapNode(pos.x, pos.y, "TraveledNode", true);
        MapManager.Map.UpBlackMask.EraseNeighborNode(pos.x, pos.y);
    }

    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(false);
        SetWalkable(true);
        SetTileType(TileType.TravelNode);
    }
    #endregion
}
