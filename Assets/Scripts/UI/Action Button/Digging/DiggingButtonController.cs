using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiggingButtonController : ActionButtonController
{
    protected override void OnClickedButton(Button button)
    {
        ActiveManager.Active.BreakTile();
    }
}
