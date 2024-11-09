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
        StartCoroutine(ProgressResearch(node));

        CloseExclusiveNode(node);
    }

    IEnumerator ProgressResearch(ResearchNode node)
    {
        node.SetNodeState(NodeState.IN_PROGRESS);
        yield return new WaitForSeconds(node.ProgressTime); //[LSH:TODO] 게임 내 연차로 연동해야함        
        CompletedResearch(node);
        UpdateNode();
    }

    void CompletedResearch(ResearchNode node)
    {
        node.SetNodeState(NodeState.COMPLETED);        
        UnlockNode(node);
    }
    
    void UnlockNode(ResearchNode node)
    {
        foreach (ResearchNode nextNode in node.NextNodes)
        {
            // 다음 노드의 모든 PreviousNodes가 COMPLETED 상태인지 확인
            bool allPreviousCompleted = true;
            foreach (ResearchNode previousNode in nextNode.PreviousNodes)
            {
                if (previousNode.NodeState != NodeState.COMPLETED)
                {
                    allPreviousCompleted = false;
                    break;
                }
            }

            // 모든 PreviousNode가 COMPLETED일 때만 UNLOCK
            if (allPreviousCompleted && nextNode.NodeState == NodeState.LOCKED)
            {
                nextNode.SetNodeState(NodeState.UNLOCKED);
                unlockedNodes.Add(nextNode);
            }
        }

        UpdateNode(); // UI 업데이트를 위해 UpdateNode 호출
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
}
