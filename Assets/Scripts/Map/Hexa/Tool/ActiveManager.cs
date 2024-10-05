using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActiveManager : MonoBehaviour
{
    #region Attribute
    HexaGrid grid;
    
    #endregion
    #region Function
    public HexaMapNode ClickTile(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Vector2Int gridPos = grid.GetCellPosCalc().CalcGridPos(mouseWorldPos);
        return grid.GetNode(gridPos.x, gridPos.y);
    }
    public bool BreakTile(HexaMapNode node)
    {
        if(node == null || !node.GetBreakable()) return false;
        
        //Exchange Tile
        return true;
    }
    #endregion

    #region Unity Function
    private void Start()
    {
        grid = GetComponent<HexaGrid>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HexaMapNode node = ClickTile(Input.mousePosition);
        }
    }
    #endregion
}
