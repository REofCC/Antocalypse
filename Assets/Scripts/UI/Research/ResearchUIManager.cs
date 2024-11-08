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

    private void Start()
    {        
        researchTreeManager.ResearchUpdate += Redraw;
        Redraw();
    }

    void Redraw()
    {
        for (int i = 0; i < nodeButtonList.Count; i++)
        {
            Button button = nodeButtonList[i];
            ResearchNode node = researchTreeManager.GetNodeByIndex(i);            
            button.gameObject.transform.transform.GetChild(0).GetComponent<TMP_Text>().text = node.NodeName;

            if (node == null)
            {
                return;
            }

            switch(node.NodeState)
            {
                case NodeState.LOCKED:
                    button.interactable = false;
                    break;
                case NodeState.UNLOCKED:
                    button.interactable = true;
                    break;
                case NodeState.IN_PROGRESS:
                    button.interactable = false;
                    break;
                case NodeState.COMPLETED:
                    button.interactable = false;
                    break;
                case NodeState.CLOSED:
                    button.interactable = false;
                    break;
            }
        }
    }

    void StartResearch(ResearchNode node)
    {
        researchTreeManager.StartResearchNode(node);
    }
}