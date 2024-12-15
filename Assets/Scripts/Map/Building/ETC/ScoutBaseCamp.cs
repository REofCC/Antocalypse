using System.Collections.Generic;
using UnityEngine;

public class ScoutBaseCamp : BaseBuilding
{
    List<Vector3> routeUnder;
    public List<Vector3> GetRouteUnder()
    {
        return routeUnder;
    }
    public List<Vector3> GetRouteUp(HexaMapNode travelNode)
    {
        return MapManager.Map.UnderPathFinder.PathFindTravelNode(travelNode);
    }
    public override void EventStart()
    {
        HexaMapNode end = MapManager.Map.UnderGrid.GetDoorPos();
        HexaMapNode start = GetBuildedPos();
        routeUnder = MapManager.Map.UnderPathFinder.PathFinding(start, end);
    }

    public override void EventStop()
    {
        return;
    }
    
}
