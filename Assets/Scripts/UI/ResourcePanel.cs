using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void UpdateResource(ResourceType type, int currentAmount, int maxAmount)
    {
        //[LSH:TODO] [USE OBSERVER PATTERN]
        string displayText = $"{currentAmount}/{maxAmount}";

        switch (type)
        {
            case ResourceType.LEAF:
                leafText.text = displayText;
                break;
            case ResourceType.WOOD:
                woodText.text = displayText;
                break;
            case ResourceType.LIQUID_FOOD:
                liquidFoodText.text = displayText;
                break;
            case ResourceType.SOLID_FOOD:
                solidFoodText.text = displayText;
                break;
            case ResourceType.GENETIC_MATERIAL:
                geneticMaterialText.text = displayText;
                break;
            case ResourceType.POPULATION:
                populationText.text = displayText;
                break;
            default:
                Debug.LogError("Invalid resource type");
                break;
        }
    }
}