using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchNode", menuName = "Research/Node")]
public class ResearchNode : ScriptableObject
{
    [SerializeField] BuildingType buildingType;
    [SerializeField] Sprite nodeIcon;
    [SerializeField] string nodeName;
    [SerializeField] string description;
    [SerializeField] float progressTime;
    [SerializeField] int cost;
    [SerializeField] NodeState nodeState;
    [SerializeField] List<ResearchNode> nextNodes;
    [SerializeField] List<ResearchNode> previousNodes;
    [SerializeField] List<ResearchNode> exclusiveNodes;


    public BuildingType BuildingType => buildingType;
    public Sprite NodeIcon => nodeIcon;
    public string NodeName => nodeName;
    public string Description => description;
    public float ProgressTime => progressTime;
    public int Cost => cost;
    public NodeState NodeState => nodeState;
    public List<ResearchNode> NextNodes => nextNodes;
    public List<ResearchNode> PreviousNodes => previousNodes;
    public List<ResearchNode> ExclusiveNodes => exclusiveNodes;

    public void SetNodeState(NodeState _nodeState)
    {
        nodeState = _nodeState;
    }
}
