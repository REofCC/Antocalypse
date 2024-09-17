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
    #endregion
    void Start()
    {
        xPos = this.gameObject.transform.position.x;
        yPos = this.gameObject.transform.position.y;
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

    public void SetXpos(int xPos)
    {
        this.xPos = xPos;
    }

    public void SetYpos(int yPos)
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
    #endregion

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
}
