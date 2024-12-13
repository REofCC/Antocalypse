using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ResourcePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI leafText;
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI liquidFoodText;
    [SerializeField] TextMeshProUGUI solidFoodText;    
    [SerializeField] TextMeshProUGUI populationText;

    private void Start()
    {
        Managers.Resource.OnResourceChange += UpdateResource;
        
        UpdateResource(ResourceType.LEAF);
        UpdateResource(ResourceType.WOOD);
        UpdateResource(ResourceType.LIQUID_FOOD);
        UpdateResource(ResourceType.SOLID_FOOD);
        UpdateResource(ResourceType.WOOD);
    }

    private void OnDestroy()
    {
        Managers.Resource.OnResourceChange -= UpdateResource;
    }

    void UpdateResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.LEAF:
                leafText.text = $"{Managers.Resource.GetLeaf()}/{Managers.Resource.GetMaxLeaf()}";
                break;
            case ResourceType.WOOD:
                woodText.text = $"{Managers.Resource.GetWood()}/{Managers.Resource.GetMaxWood()}";
                break;
            case ResourceType.LIQUID_FOOD:
                liquidFoodText.text = $"{Managers.Resource.GetLiquidFood()}/{Managers.Resource.GetMaxLiquidFood()}";
                break;
            case ResourceType.SOLID_FOOD:
                solidFoodText.text = $"{Managers.Resource.GetSolidFood()}/{Managers.Resource.GetMaxSolidFood()}";                            
                break;
            default:
                Debug.LogError("Invalid resource type");
                break;
        }
    }
}