using TMPro;
using UnityEngine;
using UnityEngine.U2D;
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
    TileType tileType;
    int number = 0;
    bool isGround = false;
    BaseResource baseResource;
    HexaMapNode node;
    //TaskManager.RequestTask(HexaMapNode node, TaskType type)
    private void Start()
    {
        leftArrowButton.onClick.AddListener(OnLeftButtonClick);
        rightArrowButton.onClick.AddListener(OnRightButtonClick);
        middleButton.onClick.AddListener(OnMiddleButtonClick);
        UpdateNumberText();
    }
    //지하 자원노드
    //지상 미탐사, 자원, 전투
    public void SetOrderType(TileType _tileType, BaseResource _baseResource)
    {
        isGround = MapManager.Map.State.IsGround();
        tileType = _tileType;
        baseResource = _baseResource;
        if (isGround)
        {
            switch (tileType)
            {
                case TileType.TravelNode:
                    orderType = OrderType.EXPLORATION;
                    ButtonControl(false, true, false, true);
                    break;
                case TileType.ResourceNode:
                    orderType = OrderType.HARVEST;
                    ButtonControl(true, true, true, true);
                    break;
                case TileType.BattleNode:
                    orderType = OrderType.BATTLE;
                    ButtonControl(true, true, true, true);
                    break;
            }
        }
        else
        {
            switch (tileType)
            {
                case TileType.ResourceNode:
                    orderType = OrderType.HARVEST;
                    ButtonControl(true, true, true, false);
                    break;
            }
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
        if (baseResource.GetCurrentWorker() + number > 0)
        {
            number--;
            UpdateNumberText();

            if (!isGround)
            {
                if (tileType == TileType.ResourceNode)
                {
                    Managers.Task.DismissTask(TaskType.Gather, node);
                }
            }
        }
    }

    void OnRightButtonClick()
    {
        number++;
        UpdateNumberText();
        if (!isGround)
        {
            if (tileType == TileType.ResourceNode)
            {
                Managers.Task.RequestTask(node, TaskType.Gather);
            }
        }
    }

    void OnMiddleButtonClick()
    {

        if (tileType == TileType.ResourceNode)
        {
            Managers.Task.RequestTask(node, TaskType.Gather);
        }
        else if (tileType == TileType.TravelNode)
        {
            Managers.Task.RequestTask(node, TaskType.None);
        }
        else if (tileType == TileType.BattleNode)
        {
            Managers.Task.RequestTask(node, TaskType.None);
        }

    }

    void UpdateNumberText()
    {
        if (!MapManager.Map.State.IsGround() && tileType == TileType.ResourceNode)
        {
            middleText.text = (baseResource.GetCurrentWorker() + number).ToString();
        }
        else
        {
            middleText.text = number.ToString();
        }
    }

    void StartExploration()
    {
        {
            if (tileType == TileType.ResourceNode)
            {
                for (int i = 0; i < number; i++)
                {
                    Managers.Task.RequestTask(node, TaskType.Gather);
                }
            }
            else if (tileType == TileType.TravelNode)
            {
                Managers.Task.RequestTask(node, TaskType.None);
            }
            else if (tileType == TileType.BattleNode)
            {
                Managers.Task.RequestTask(node, TaskType.None);
            }
        }
    }

    public void SetUIPosition(Vector3 nodePosition, HexaMapNode _node, BaseResource _baseResource)
    {
        node = _node;
        baseResource = _baseResource;
        uiElement.gameObject.SetActive(true);
        uiElement.position = nodePosition - new Vector3(0, 0.75f, 0);
        number = 0;
        SetOrderType(node.GetTileType(), baseResource);
        UpdateNumberText();
    }

    public void HideUI()
    {
        uiElement.gameObject.SetActive(false);
    }
}
