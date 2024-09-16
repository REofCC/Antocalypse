using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Scriptable Object/NodeData", order =int.MaxValue)]
public class NodeData : ScriptableObject
{
    public string nodeType;
    public int value;
    public int maxWorker;
}
