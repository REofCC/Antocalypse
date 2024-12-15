using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct RequireResource
{
    public int leaf;
    public int wood;
    public int liquidFood;    
}

[CreateAssetMenu(fileName = "ResearchNode", menuName = "Research/Node")]
public class ResearchNode : ScriptableObject
{
    [SerializeField] List<BuildingType> unlockBuildingTypes;
    [SerializeField] Sprite nodeIcon;
    [SerializeField] string nodeName;
    [SerializeField] [TextArea] string description;
    [SerializeField] float progressTime;
    [SerializeField] RequireResource requireResource;
    [SerializeField] NodeState nodeState;
    [SerializeField] List<ResearchNode> nextNodes;
    [SerializeField] List<ResearchNode> previousNodes;
    [SerializeField] List<ResearchNode> exclusiveNodes;


    public List<BuildingType> UnlockBuildingTypes => unlockBuildingTypes;
    public Sprite NodeIcon => nodeIcon;
    public string NodeName => nodeName;
    public string Description => description;
    public float ProgressTime => progressTime;
    public int RequireLeaf => requireResource.leaf;
    public int RequireWood => requireResource.wood;
    public int RequireLiquidFood => requireResource.liquidFood;
    public NodeState NodeState => nodeState;
    public List<ResearchNode> NextNodes => nextNodes;
    public List<ResearchNode> PreviousNodes => previousNodes;
    public List<ResearchNode> ExclusiveNodes => exclusiveNodes;

    public void SetNodeState(NodeState _nodeState)
    {
        nodeState = _nodeState;
    }
}
