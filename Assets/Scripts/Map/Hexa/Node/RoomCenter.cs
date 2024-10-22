using System.Collections.Generic;

public class RoomCenter : RoomNode
{
    #region Attribute
    List<RoomNode> nodes;
    BaseBuilding building;
    Dictionary<BaseBuilding, int> buildings = new();
    int roomPhase;
    #endregion

    #region Getter & Setter
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
    public override void SetBuilding(BaseBuilding building)
    {
        this.building = building;
        SetBuildable(false);
        AddBuildings(building);
    }
    public void AddBuildings(BaseBuilding building)
    {
        if (buildings.ContainsKey(building))
        {
            buildings[building] += 1;
        }
        buildings.Add(building, 1);
    }
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