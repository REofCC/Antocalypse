using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUIManager : MonoBehaviour
{
    [SerializeField] ResearchTreeManager researchTreeManager;
    [SerializeField] List<Button> nodeButtonList = new List<Button>();
    [SerializeField] RectTransform queenAntResearchPanel;
    [SerializeField] RectTransform specializeResearchPanel;
    ResearchInformationUI researchInformationUI;

    private void Start()
    {
        researchInformationUI = GetComponent<ResearchInformationUI>();
        researchTreeManager.ResearchUpdate += Redraw;        
        InitializeNodeButton();
    }

    void Redraw()
    {
        for (int i = 0; i < nodeButtonList.Count; i++)
        {
            Button button = nodeButtonList[i];
            ResearchNode node = button.GetComponent<ResearchNodeButtonUI>().GetResearchNodeInButton();

            if (node == null)
            {
                return;
            }

            ChangeNodeState(node, button);
        }
    }
    
    void InitializeNodeButton()
    {
        for (int i = 0; i < nodeButtonList.Count; i++)
        {
            Button button = nodeButtonList[i];
            ResearchNode node = researchTreeManager.GetNodeByIndex(i);
            button.GetComponent<ResearchNodeButtonUI>().SetResearchUIManager(this);
            button.gameObject.transform.transform.GetChild(0).GetComponent<TMP_Text>().text = node.NodeName;
            button.GetComponent<ResearchNodeButtonUI>().SetResearchNodeInButton(node);

            if (node == null)
            {
                return;
            }

            ChangeNodeState(node, button);
        }
    }

    public void ChangeNodeState(ResearchNode node, Button button)
    {
        switch (node.NodeState)
        {
            case NodeState.LOCKED:
                button.GetComponent<Image>().color = Color.gray;
                break;
            case NodeState.UNLOCKED:
                button.GetComponent<Image>().color = Color.white;
                break;
            case NodeState.IN_PROGRESS:
                button.GetComponent<Image>().color = Color.gray;
                break;
            case NodeState.COMPLETED:
                button.GetComponent<Image>().color = Color.gray;
                break;
            case NodeState.CLOSED:
                button.GetComponent<Image>().color = Color.gray;
                break;
        }
    }
    void StartResearch(ResearchNode node)
    {
        researchTreeManager.StartResearchNode(node);
    }

    public void TossNodeInformation(ResearchNode node)
    {
        researchInformationUI.DisplayNodeInformation(node);
    }
}