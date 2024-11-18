using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : MonoBehaviour
{
    HexaGrid grid;

    readonly int[] ringNodeNum = { 6, 12, 18 };
    readonly int[] roomNodeNum = { 6, 18 };
    private bool CheckRoomNode(List<HexaMapNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            TileType type = nodes[i].GetTileType();
            if (type == TileType.RoomCenter || type == TileType.RoomNode || (!nodes[i].GetBreakable() && type != TileType.Path))
                return false;
        }
        return true;
    }
    private bool CheckRoomWall(List<HexaMapNode> rings)
    {
        for (int i = 0; i < rings.Count; i++)
        {
            TileType type = rings[i].GetTileType();
            if (type == TileType.RoomCenter || type == TileType.RoomNode)
                return false;
        }
        return true;
    }

    #region Room
    public bool MakeRoom(HexaMapNode RoomCenter)
    {
        Vector3Int centerPos = RoomCenter.GetCellPos();
        List<HexaMapNode> nodes = grid.GetNeighborNode(centerPos.x, centerPos.y, 1);
        if (nodes.Count != roomNodeNum[0])
            return false;
        if (!CheckRoomNode(nodes))
            return false;

        List<HexaMapNode> rings = grid.GetNeighborRingNode(centerPos.x, centerPos.y, 1);
        if (rings.Count != ringNodeNum[1])
            return false;
        if (!CheckRoomWall(rings))
            return false;

        grid.SwapNode(centerPos.x, centerPos.y, "RoomCenter");
        RoomCenter center = (RoomCenter)grid.GetNode(centerPos.x, centerPos.y);
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int nodePos = nodes[i].GetGridPos();
            grid.SwapNode(nodePos.x, nodePos.y, "RoomNode");
            RoomNode node = (RoomNode)grid.GetNode(nodePos.x, nodePos.y);
            center.AddRoomNode(node);
            node.SetCenter(center);
        }
        return true;
    }
    public bool ExpandRoom(RoomCenter roomCenter, int phase = 2)
    {
        if (roomCenter.GetTileType() != TileType.RoomCenter)
            return false;
        Vector2Int centerPos = roomCenter.GetGridPos();
        List<HexaMapNode> nodes = grid.GetNeighborRingNode(centerPos.x, centerPos.y, phase - 1);
        if (nodes.Count != ringNodeNum[phase - 1])
            return false;
        if (CheckRoomNode(nodes))
            return false;

        List<HexaMapNode> rings = grid.GetNeighborRingNode(centerPos.x, centerPos.y, phase);
        if (rings.Count != ringNodeNum[phase])
            return false;
        if (!CheckRoomWall(rings))
            return false;
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int nodePos = nodes[i].GetGridPos();
            grid.SwapNode(nodePos.x, nodePos.y, "RoomNode");
            RoomNode node = (RoomNode)grid.GetNode(nodePos.x, nodePos.y);
            roomCenter.AddRoomNode(node);
            node.SetCenter(roomCenter);
        }
        return true;
    }
    #endregion

    public void OnAwake()
    {
        grid = gameObject.transform.parent.GetComponent<HexaGrid>();
    }
}
