using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonController : ActionButtonController
{
    protected override void OnClickedButton(Button button)
    {
        BuildingType buildingType = button.GetComponent<BuildingButttonData>().GetBuildingName();
        ActiveManager.Active.BuildBuilding(buildingType);
    }
}
