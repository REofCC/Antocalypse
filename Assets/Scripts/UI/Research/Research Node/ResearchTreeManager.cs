using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum NodeState
{
    LOCKED,
    UNLOCKED,
    IN_PROGRESS,
    COMPLETED,
    CLOSED,
}

public class ResearchTreeManager : MonoBehaviour
{
    [SerializeField] List<ResearchNode> initialNodes = new List<ResearchNode>();
    [SerializeField] List<ResearchNode> unlockedNodes = new List<ResearchNode>();
    [SerializeField] List<ResearchNode> researchNodes = new List<ResearchNode>();
    List<ResearchNode> completedNodes = new List<ResearchNode>();

    public event Action ResearchUpdate;

    private void Start()
    {
        foreach (ResearchNode node in researchNodes)
        {
            node.SetNodeState(NodeState.LOCKED);
        }

        foreach (ResearchNode node in initialNodes)
        {
            node.SetNodeState(NodeState.UNLOCKED);
            unlockedNodes.Add(node);
        }
    }

    bool CanProgressNode(ResearchNode node)
    {
        return CheckUnlockConditions(node);
    }

    bool CheckUnlockConditions(ResearchNode node)
    {
        foreach (ResearchNode requireNode in node.PreviousNodes)
        {
            if (requireNode.NodeState != NodeState.COMPLETED)
            {
                return false;
            }
        }
        return true;
    }

    public void StartResearchNode(ResearchNode node)
    {
        if (node.NodeState != NodeState.UNLOCKED || !CanAffordNode(node) || !CanProgressNode(node))
        {
            return;
        }

        DeductResource(node.Cost);        
        CloseExclusiveNode(node);
        node.SetNodeState(NodeState.IN_PROGRESS);
        StartCoroutine(ProgressResearch(node));
        UpdateNode();
    }

    IEnumerator ProgressResearch(ResearchNode node)
    {        
        yield return new WaitForSeconds(node.ProgressTime); //[LSH:TODO] 게임 내 연차로 연동해야함        
        CompletedResearch(node);
        UpdateNode();
    }

    void CompletedResearch(ResearchNode node)
    {
        node.SetNodeState(NodeState.COMPLETED);      
        MapManager.Map.BuildingFactory.SetBuildingConstaint(node.BuildingType);
        UnlockNode(node);
        completedNodes.Add(node);
    }
    
    void UnlockNode(ResearchNode node)
    {
        foreach (ResearchNode nextNode in node.NextNodes)
        {            
            bool allPreviousCompleted = true;
            foreach (ResearchNode previousNode in nextNode.PreviousNodes)
            {
                if (previousNode.NodeState != NodeState.COMPLETED)
                {
                    allPreviousCompleted = false;
                    break;
                }
            }
            
            if (allPreviousCompleted && nextNode.NodeState == NodeState.LOCKED)
            {
                nextNode.SetNodeState(NodeState.UNLOCKED);
                unlockedNodes.Add(nextNode);
            }
        }

        UpdateNode();
    }


    void CloseExclusiveNode(ResearchNode node)
    {
        foreach(ResearchNode exclusiveNode in node.ExclusiveNodes)
        {
            exclusiveNode.SetNodeState(NodeState.CLOSED);
            //UPDATE UI
        }
    }

    public void CancelResearchNode(ResearchNode node)
    {

    }

    void UpdateNode()
    {
        ResearchUpdate.Invoke();
    }

    bool CanAffordNode(ResearchNode node)
    {
        return true;
    }

    void DeductResource(int cost)
    {

    }

    public ResearchNode GetNodeByIndex(int index)
    {
        if(index >= 0 && index < researchNodes.Count)
        {
            return researchNodes[index];
        }

        return null;
    }
    
    public bool IsCompletedNode(ResearchNode node)
    {
        return completedNodes.Contains(node);
    }
}
