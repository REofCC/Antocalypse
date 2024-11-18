using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Object/Resource")]
public class ResourceData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    string resourceName;
    [SerializeField]
    Resourcetype type;
    [SerializeField]
    bool buildable;
    [SerializeField]
    int amount;
    [SerializeField]
    int maxWorker;
    #endregion

    #region Getter & Setter
    public string ResourceName { get { return resourceName; } }
    public Resourcetype ResourceType { get { return type; } }
    public bool Buildable { get { return buildable; } }
    public int Amount { get { return amount; } }
    public int MaxWorker { get { return maxWorker; } }
    #endregion
}
