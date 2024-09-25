using UnityEngine;

public class BaseMapNode : MonoBehaviour
{
    #region attribute
    [SerializeField]
    private MapTileInfo tileinfo;
    [SerializeField]
    private string nextMapTile;
    private float xPos;
    private float yPos;

    // A grid attribute
    private int gCost;
    private int hCost;
    private BaseMapNode parent;
    #endregion
    void Start()
    {
        SetXpos(this.transform.position.x);
        SetYpos(this.transform.position.y);
        SethCost(1000000);
    }

    private void OnDestroy()
    {
        DropResource();
    }

    #region Getter & Setter
    public bool GetWalkable()
    {
        return tileinfo.Walkable;
    }

    public void SetXpos(float xPos)
    {
        this.xPos = xPos;
    }

    public void SetYpos(float yPos)
    {
        this.yPos = yPos;
    }

    public float GetXpos()
    {
        return xPos;
    }

    public float GetYpos()
    {
        return yPos;
    }

    public MapTileInfo GetTileInfo()
    {
        return this.tileinfo;
    }

    public BaseMapNode GetParentNode()
    {
        return parent;
    }

    public int GetgCost()
    {
        return gCost;
    }

    public int GethCost()
    {
        return hCost;
    }

    public void SetParentNode(BaseMapNode parent)
    {
        this.parent = parent;
    }

    public void SetgCost(int gcost)
    {
        gCost = gcost;
    }

    public void SethCost(int hcost)
    {
        hCost = hcost;
    }
    #endregion

    #region Function
    public void DropResource()
    {
        //TODO Drop When Tile Change
    }

    public GameObject InstantiateNode()
    {
        GameObject nextNode = null;
        // TODO  Make Next Tile GameObject
        return nextNode;
    }
    #endregion
}
