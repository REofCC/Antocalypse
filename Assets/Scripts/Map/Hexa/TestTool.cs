using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTool : MonoBehaviour
{
    public Test test;
    public HexaGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<Test>();
        grid = GameObject.Find("MapTool").GetComponent<HexaGrid>();
    }

    public void Break()
    {
        Vector2Int gridPos = test.current.GetGridPos();
        if(test.current.GetBreakable())
        {
            grid.SwapNode(gridPos.x, gridPos.y, "Path");
            
        }
    }
}
