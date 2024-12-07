using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButttonData : MonoBehaviour
{
    [SerializeField] BuildingType buildingType;

    public BuildingType GetBuildingName()
    {
        return buildingType;
    }
}
