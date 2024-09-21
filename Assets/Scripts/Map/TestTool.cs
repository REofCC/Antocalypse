using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTool : MonoBehaviour
{
    #region attribute
    UnderGroundGrid grid;
    TileSpawner tileSpawner;
    PathFinder pathFinder;

    public int mapXsize;
    public int mapYsize;
    public Vector2 startPoint;
    public Vector2 endPoint;

    public Button createMapButton;
    public Button makeGridButton;
    public Button findPathButton;
    #endregion

    private void Start()
    {
        GameObject go = GameObject.Find("Grid");
        grid = go.GetComponent<UnderGroundGrid>();
        tileSpawner= go.GetComponent<TileSpawner>();
        pathFinder = go.GetComponent<PathFinder>();

        createMapButton.onClick.AddListener(MakeMap);
        makeGridButton.onClick.AddListener(MakeGrid);
        findPathButton.onClick.AddListener(FindPath);

        startPoint.x = 0;
        startPoint.y = 0;
        endPoint.x = 1;
        endPoint.y = 1;
    }
    #region Button Function
    public void MakeMap()
    {
        
    }

    public void MakeGrid()
    {
        grid.MakeGrid();
    }

    public void FindPath()
    {
        int startX = grid.CalcXaxis(startPoint.x);
        int startY = grid.CalcXaxis(startPoint.y);
        int endX = grid.CalcXaxis(endPoint.x);
        int endY = grid.CalcXaxis(endPoint.y);

        BaseMapNode startNode = grid.GetNode(startX, startY);
        BaseMapNode endNode = grid.GetNode(endX, endY);
        List<BaseMapNode> route = pathFinder.FindingPath(startNode, endNode);
        foreach (BaseMapNode routeNode in route)
        {
            Debug.Log(routeNode.gameObject);
        }
    }
    #endregion
}
