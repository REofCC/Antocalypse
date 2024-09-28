using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaPathFinder : MonoBehaviour
{
    #region 
    HexaMapNode start;
    HexaMapNode end;

    #endregion
    #region  Setter & Getter
    public void SetStartNode( HexaMapNode start)
    {
        this.start = start;
    }
    public HexaMapNode GetStartNode()
    {
        return start;
    }
    public void SetEndNode( HexaMapNode end)
    {
        this.end = end;
    }
    public HexaMapNode GetEndNode()
    {
        return end;
    }
    #endregion

    #region Function
    #endregion

    #region Unity Function
    #endregion
}
