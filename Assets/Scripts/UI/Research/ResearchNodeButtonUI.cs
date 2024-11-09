using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNodeButtonUI : MonoBehaviour
{
    [SerializeField] ResearchNode researchNode;
    ResearchUIManager researchUIManager;    
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickNodeButton);     
    }

    public void SetResearchUIManager(ResearchUIManager manager)
    {
        researchUIManager = manager;
    }

    public void SetResearchNodeInButton(ResearchNode node)
    {
        researchNode = node;
        SetNodeSprite(node);        
    }
    
    public ResearchNode GetResearchNodeInButton()
    {
        return researchNode;
    }

    void SetNodeSprite(ResearchNode node)
    {
        GetComponent<Image>().sprite = node.NodeIcon;
    }

    public void OnClickNodeButton()
    {
        researchUIManager.TossNodeInformation(researchNode);
    }
}
