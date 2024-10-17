using System.Collections.Generic;

public class RoomCenter : HexaMapNode
{
    #region Attribute
    List<RoomNode> nodes;
    BaseBuilding building;
    int roomPhase;
    #endregion

    #region Getter & Setter
    public void SetBuilding(BaseBuilding building)
    {
        this.building = building;
        SetBuildable(false);
        SetWalkable(false);
    }
    public BaseBuilding GetBuilding()
    {
        return building;
    }
    public void SetRoomPhase(int phase)
    {
        roomPhase = phase;
    }
    public int GetRoomPhase()
    {
        return roomPhase;
    }
    #endregion

    #region Function
    public void AddRoomNode(List<RoomNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!this.nodes.Contains(nodes[i]))
            {
                this.nodes.Add(nodes[i]);
            }
        }
    }
    public void AddRoomNode(RoomNode node)
    {
        if (!this.nodes.Contains(node))
        {
            this.nodes.Add(node);
        }
    }
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(true);
        SetWalkable(true);
        SetTileType(TileType.RoomCenter);
        nodes = new List<RoomNode>();
    }
    #endregion
}