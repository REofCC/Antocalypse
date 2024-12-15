using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButttonData : MonoBehaviour
{
    [SerializeField] BuildingType buildingType;
    bool isInteractive = false;

    public BuildingType GetBuildingName()
    {
        return buildingType;
    }

    public void SetInteractive(bool _isInteractive)
    {
        isInteractive = _isInteractive;
        GetComponent<Button>().interactable = isInteractive;
    }

    public bool GetInteractive()
    {
        return isInteractive;
    }
}
