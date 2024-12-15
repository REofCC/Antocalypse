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

    private void Start()
    {
        UpdateTitle();        
    }
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
        titleText.text = $"{year}년차 필요 자원";
        UpdateObjective(ResourceType.LEAF);
        UpdateObjective(ResourceType.WOOD);
        UpdateObjective(ResourceType.LIQUID_FOOD);
    }

    private void UpdateObjective(ResourceType resourceType)
    {               
        switch (resourceType)
        {            
            case ResourceType.LEAF:
                liquidText.text = "잎: " + $"{Managers.Resource.GetLiquidFood()}/{Managers.YearManager.GetRequireResource(ResourceType.LEAF)}";
                break;
            case ResourceType.WOOD:
                woodText.text = "목재: " + $"{Managers.Resource.GetWood()}/{Managers.YearManager.GetRequireResource(ResourceType.WOOD)}";
                break;
            case ResourceType.LIQUID_FOOD:
                leafText.text = "액체 식량: " + $"{Managers.Resource.GetLeaf()} / {Managers.YearManager.GetRequireResource(ResourceType.LIQUID_FOOD)}";
                break;
            default:
                Debug.LogError("Invalid resource type");
                break;
        }
    }
}