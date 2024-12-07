using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] List<Button> buildingButtons = new List<Button>();

    private void Start()
    {
        foreach (Button button in buildingButtons)
        {
            button.onClick.AddListener(() => OnBuildingButtonClicked(button));
        }
    }

    void OnBuildingButtonClicked(Button button)
    {
        BuildingType buildingType = button.GetComponent<BuildingButttonData>().GetBuildingName();
        ActiveManager.Active.BuildBuilding(buildingType);
    }
}
