using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    #region Attribute
    BuildingType type;
    int level;
    HexaMapNode buildedPos;
    [SerializeField]
    BuildResourceData buildData;
    #endregion

    #region Getter & Setter
    public BuildingType GetBuildingType()
    {
        return type;
    }
    public void SetBuildingType(BuildingType type)
    {
        this.type = type;
    }
    public int GetBuildingLevel()
    {
        return level;
    }
    public void SetBuildingLevel(int level)
    {
        this.level = level;
    }
    public HexaMapNode GetBuildedPos()
    {
        return buildedPos;
    }
    public void SetBuildedPos(HexaMapNode node)
    {
        buildedPos = node;
    }
    public BuildResourceData GetBuildResourceData()
    {
        return buildData;
    }
    #endregion

    #region Function
    public virtual bool UpgradeBuilding()
    {
        return true;
    }
    #endregion
}
