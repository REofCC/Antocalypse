using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    #region Attribute
    BuildingType type;
    int level;
    HexaMapNode buildedPos;
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
    #endregion

    #region Function
    #endregion
}
