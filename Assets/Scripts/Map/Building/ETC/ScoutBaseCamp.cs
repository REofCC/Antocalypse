using System.Collections.Generic;
using UnityEngine;

public class ScoutBaseCamp : BaseBuilding
{
    List<Vector3> route;
    public override void EventStart()
    {
        HexaMapNode end = MapManager.Map.UnderGrid.GetDoorPos();
        HexaMapNode start = GetBuildedPos();
        route = MapManager.Map.UnderPathFinder.PathFinding(start, end);
    }

    public override void EventStop()
    {
        return;
    }
}
