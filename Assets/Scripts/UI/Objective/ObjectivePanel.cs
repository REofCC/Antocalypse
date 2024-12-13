using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] TMP_Text titleText;    
    [SerializeField] TMP_Text leafText;
    [SerializeField] TMP_Text woodText;
    [SerializeField] TMP_Text liquidText;

    private void OnEnable()
    {
        Managers.YearManager.OnWinterEvent += UpdateTitle;
        Managers.Resource.OnResourceChange += UpdateObjective;
    }

    private void OnDisable()
    {
        Managers.YearManager.OnWinterEvent -= UpdateTitle;
        Managers.Resource.OnResourceChange -= UpdateObjective;
    }

    private void UpdateTitle()
    {
        int year = Managers.YearManager.GetCurrentYear();
        titleText.text = $"{year}Years Required Resources ";
        UpdateObjective(ResourceType.LEAF);
        UpdateObjective(ResourceType.WOOD);
        UpdateObjective(ResourceType.LIQUID_FOOD);
    }

    private void UpdateObjective(ResourceType resourceType)
    {               
        switch (resourceType)
        {            
            case ResourceType.LEAF:
                liquidText.text = "Leaf: " + $"{Managers.Resource.GetLiquidFood()}/{Managers.YearManager.GetRequireResource(ResourceType.LEAF)}";
                break;
            case ResourceType.WOOD:
                woodText.text = "Wood: " + $"{Managers.Resource.GetWood()}/{Managers.YearManager.GetRequireResource(ResourceType.WOOD)}";
                break;
            case ResourceType.LIQUID_FOOD:
                leafText.text = "Liquid: " + $"{Managers.Resource.GetLeaf()} / {Managers.YearManager.GetRequireResource(ResourceType.LIQUID_FOOD)}";
                break;
            default:
                Debug.LogError("Invalid resource type");
                break;
        }
    }
}