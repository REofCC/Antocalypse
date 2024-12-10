using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{    
    [SerializeField] Button addLeafButton;
    [SerializeField] Button removeLeafButton;
    [SerializeField] Button addWoodButton;
    [SerializeField] Button removeWoodButton;
    [SerializeField] Button addLiquidFoodButton;
    [SerializeField] Button removeLiquidFoodButton;
    [SerializeField] TMP_InputField amountInputField;

    private void Start()
    {
        addLeafButton.onClick.AddListener(() => ModifyResource(ResourceType.LEAF, true));
        removeLeafButton.onClick.AddListener(() => ModifyResource(ResourceType.LEAF, false));
        addWoodButton.onClick.AddListener(() => ModifyResource(ResourceType.WOOD, true));
        removeWoodButton.onClick.AddListener(() => ModifyResource(ResourceType.WOOD, false));
        addLiquidFoodButton.onClick.AddListener(() => ModifyResource(ResourceType.LIQUID_FOOD, true));
        removeLiquidFoodButton.onClick.AddListener(() => ModifyResource(ResourceType.LIQUID_FOOD, false));
    }

    void ModifyResource(ResourceType type, bool isAdding)
    {
        int amount;
        if (int.TryParse(amountInputField.text, out amount))
        {
            if (isAdding)
            {
                switch (type)
                {
                    case ResourceType.LEAF:
                        Managers.Resource.AddLeaf(amount);
                        break;
                    case ResourceType.WOOD:
                        Managers.Resource.AddWood(amount);
                        break;          
                    case ResourceType.LIQUID_FOOD:
                        Managers.Resource.AddLiquidFood(amount);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case ResourceType.LEAF:
                        Managers.Resource.MinusLeaf(amount);
                        break;
                    case ResourceType.WOOD:
                        Managers.Resource.MinusWood(amount);
                        break;
                    case ResourceType.LIQUID_FOOD:
                        Managers.Resource.MinusLiquidFood(amount);
                        break;
                }
            }
        }
        else
        {
            Debug.LogError("Invalid amount input");
        }
    }
}
