using UnityEngine;
public abstract class BaseBuilding : MonoBehaviour
{
    #region Attribute
    BuildingType type;
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
    public abstract void EventStart();
    public abstract void EventStop();
    #endregion
}
