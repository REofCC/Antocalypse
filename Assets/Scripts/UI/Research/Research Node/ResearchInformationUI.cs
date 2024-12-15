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
        researchDescriptionText.text = $"[����]: {node.NodeName}\n[����]: {node.Description}";
        requirementResourceText.text = $"��: {node.RequireLeaf}\n����: {node.RequireWood}\n��ü �ķ�: {node.RequireLiquidFood}\n�ð�: {node.ProgressTime}";

        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (currentResearchNode.NodeState == NodeState.UNLOCKED)
        {
            progressResearchButton.interactable = true;
            cancleResearchButton.interactable = false;
        }
        else if (currentResearchNode.NodeState == NodeState.IN_PROGRESS)
        {
            progressResearchButton.interactable = false;
            cancleResearchButton.interactable = true;
        }
        else
        {
            progressResearchButton.interactable = false;
            cancleResearchButton.interactable = false;
        }

        if (researchTreeManager.IsResearchInProgress())
        {
            progressResearchButton.interactable = false;
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
