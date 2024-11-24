using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNode : HexaMapNode
{
    #region Attribitue
    bool is_connected = false;

    #endregion

    #region Getter & Setter
    public bool IsConnected()
    {
        return is_connected;
    }

    public void SetConnect(bool connected)
    {
        is_connected = connected;
    }
    #endregion

    #region Function
    #endregion
}
