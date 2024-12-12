using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public enum OrderType
{
    EXPLORATION,
    HARVEST,
    BATTLE,
}

public class NodeOrderPanelUI : MonoBehaviour
{
    [SerializeField] RectTransform uiElement;
    [SerializeField] Button leftArrowButton;
    [SerializeField] Button middleButton;
    [SerializeField] Button rightArrowButton;    
    [SerializeField] TMP_Text middleText;
    OrderType orderType;
    LayerType layerType;

    int number = 1;
    
    private void Start()
    {
        leftArrowButton.onClick.AddListener(OnLeftButtonClick);
        rightArrowButton.onClick.AddListener(OnRightButtonClick);
        middleButton.onClick.AddListener(OnMiddleButtonClick);
        UpdateNumberText();
    }

    public void SetOrderType(TileType tileType)
    {
        if(tileType == TileType.TravelNode)
        {
            ButtonControl(false, true, false, true);
            middleText.text = "Exploration";
        }
        else if(tileType == TileType.ResourceNode)
        {
            layerType = MapManager.Map.LayerType;
            if (layerType == LayerType.Ground)
            {
                ButtonControl(true, true, true, true);
                middleText.text = "0"; //현재 작업 중인 
            }
            else
            {
                ButtonControl(true, true, true, false);                
                middleText.text = "0";
            }            
        }
        else if(tileType == TileType.BattleNode)
        {
            ButtonControl(true, true, true, true);
            middleText.text = "0";
        }
    }
    
    void ButtonControl(bool left, bool middle, bool right, bool interactive)
    {
        leftArrowButton.gameObject.SetActive(left);
        middleButton.gameObject.SetActive(middle);
        rightArrowButton.gameObject.SetActive(right);
        middleButton.interactable = interactive;
    }

    void OnLeftButtonClick()
    {
        if (number > 1)
        {
            number--;
            UpdateNumberText();
        }
    }

    void OnRightButtonClick()
    {
        number++;
        UpdateNumberText();
    }

    void OnMiddleButtonClick()
    {
        StartExploration();
    }

    void UpdateNumberText()
    {
        middleText.text = number.ToString();
    }

    void StartExploration()
    {
        Debug.Log("탐험 시작: " + number);        
    }

    public void SetUIPosition(Vector3 nodePosition)
    {
        uiElement.gameObject.SetActive(true);
        uiElement.position = nodePosition - new Vector3(0, 0.75f, 0);
        number = 1;
        UpdateNumberText();
    }

    public void HideUI()
    {
        uiElement.gameObject.SetActive(false);
    }
}
