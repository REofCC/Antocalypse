using UnityEngine.UI;

public class BuildingButtonController : ActionButtonController
{
    private void Start()
    {
        MapManager.Map.BuildingFactory.OnBuildBuilding += BuildingButtonUnlock;
        gameObject.GetComponentInParent<SlidePopup>().OnButtonCheck += CheckButtons;
        InitButtons();
    }

    protected override void OnClickedButton(Button button)
    {
        BuildingType buildingType = button.GetComponent<BuildingButttonData>().GetBuildingName();
        ActiveManager.Active.BuildBuilding(buildingType);
    }

    protected void BuildingButtonUnlock(BuildingType buildingType)
    {
        foreach (Button button in GetButtons())
        {
            BuildingButttonData buildingButttonData = button.GetComponent<BuildingButttonData>();
            CheckButton(button, buildingType, buildingButttonData);
        }
    }

    private void CheckButton(Button button, BuildingType buildingType, BuildingButttonData buildingButttonData)
    {
        if (buildingType == buildingButttonData.GetBuildingName())
        {
            bool isInteractive = MapManager.Map.BuildingFactory.GetBuildingConstraint(buildingType);
            button.GetComponent<BuildingButttonData>().SetInteractive(isInteractive);
        }
    }

    private void CheckButtons()
    {
        foreach (Button button in GetButtons())
        {
            bool isInteractive = button.GetComponent<BuildingButttonData>().GetInteractive();
            if (!isInteractive)
            {
                button.GetComponent<BuildingButttonData>().SetInteractive(isInteractive);
            }
        }
    }
}