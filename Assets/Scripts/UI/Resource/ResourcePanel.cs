using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum ResourceType
{
    LEAF,
    WOOD,
    LIQUID_FOOD,
    SOLID_FOOD,
    GENETIC_MATERIAL,
    POPULATION
}

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI leafText;
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI liquidFoodText;
    [SerializeField] TextMeshProUGUI solidFoodText;
    [SerializeField] TextMeshProUGUI geneticMaterialText;
    [SerializeField] TextMeshProUGUI populationText;

    private void Start()
    {
        Managers.Resource.OnResourceChange += UpdateResource;
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
                leafText.text = $"{Managers.Resource.GetLeaf()}";
                break;
            case ResourceType.WOOD:
                woodText.text = $"{Managers.Resource.GetWood()}";
                break;
            case ResourceType.LIQUID_FOOD:
                liquidFoodText.text = $"{Managers.Resource.GetLiquidFood()}/{Managers.Resource.GetMaxLiquidFood()}";
                break;
            case ResourceType.SOLID_FOOD:
                solidFoodText.text = $"{Managers.Resource.GetSolidFood()}/{Managers.Resource.GetMaxSolidFood()}";
                break;
            case ResourceType.GENETIC_MATERIAL:
                geneticMaterialText.text = $"{Managers.Resource.GetGenetic()}";
                break;
            default:
                Debug.LogError("Invalid resource type");
                break;
        }
    }
}