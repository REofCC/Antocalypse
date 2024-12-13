using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Object/Resource")]
public class ResourceData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    string resourceName;
    [SerializeField]
    ResourceType type;
    [SerializeField]
    bool buildable;
    [SerializeField]
    int amount;
    [SerializeField]
    int maxWorker;
    [SerializeField]
    ResourceSize size;
    #endregion

    #region Getter & Setter
    public string ResourceName { get { return resourceName; } }
    public ResourceType ResourceType { get { return type; } }
    public bool Buildable { get { return buildable; } }
    public int Amount { get { return amount; } }
    public int MaxWorker { get { return maxWorker; } }
    public ResourceSize Size { get { return size; } }
    #endregion
}
