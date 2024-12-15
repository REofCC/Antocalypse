using System.Collections.Generic;
using UnityEngine;

public class TravelTrail
{
    LineRenderer renderer;

    public void DrawLine(List<Vector3> worldPoses)
    {
        for(int i = 0; i < worldPoses.Count; i++)
        {
            renderer.SetPosition(i, worldPoses[i]);
        }
    }


    public void OnAwake(LineRenderer renderer)
    {
        this.renderer = renderer;
    }
}
