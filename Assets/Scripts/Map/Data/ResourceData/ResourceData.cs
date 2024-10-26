using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Object/Resource")]
public class ResourceData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    ResourceType type;
    [SerializeField]
    bool buildable;
    [SerializeField]
    int amount;
    #endregion

    #region Getter & Setter
    public ResourceType ResourceType { get { return type; } }
    public bool Buildable { get { return buildable; } }
    public int Amount { get { return amount; } }
    #endregion
}
