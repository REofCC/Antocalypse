using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchInformationUI : MonoBehaviour
{
    [SerializeField] ResearchTreeManager researchTreeManager;
    [SerializeField] Image researchIconImage;
    [SerializeField] TMP_Text researchDescriptionText;
    [SerializeField] TMP_Text requirementResourceText;
    [SerializeField] Button progressResearchButton;
    [SerializeField] Button cancleResearchButton;

    ResearchNode currentResearchNode;

    void Start()
    {
        progressResearchButton.onClick.AddListener(OnProgressResearch);
        cancleResearchButton.onClick.AddListener(OnCancleResearch);
        researchTreeManager.ResearchUpdate += UpdateButtonState;
    }

    public void DisplayNodeInformation(ResearchNode node)
    {
        currentResearchNode = node;
        researchIconImage.sprite = node.NodeIcon;
        researchDescriptionText.text = $"Research: {node.name}\n Description: {node.Description}";
        requirementResourceText.text = $"Cost: {node.Cost} / Time: {node.ProgressTime}";

        UpdateButtonState();
    }

    //[LSH:TODO]해당 함수는 ResearchTreeManager에 위치하는것이 패턴상 더 적절할것 같다
    public void UpdateButtonState()
    {
        if(currentResearchNode.NodeState == NodeState.UNLOCKED)
        {
            progressResearchButton.interactable = true;
            cancleResearchButton.interactable = false;
        }
        else if(currentResearchNode.NodeState == NodeState.IN_PROGRESS)
        {
            progressResearchButton.interactable = false;
            cancleResearchButton.interactable = true;
        }
        else
        {
            progressResearchButton.interactable = false;
            cancleResearchButton.interactable = false;
        }
    }

    void OnProgressResearch()
    {
        researchTreeManager.StartResearchNode(currentResearchNode);
    }

    void OnCancleResearch()
    {
        researchTreeManager.CancelResearchNode(currentResearchNode);
    }
}
