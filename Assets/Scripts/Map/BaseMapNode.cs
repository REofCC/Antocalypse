using UnityEngine;

public class BaseMapNode : MonoBehaviour
{
    #region attribute
    [SerializeField]
    MapTileInfo tileinfo;
    private float xPos;
    private float yPos;
    private SpriteRenderer spriteRenderer;
    #endregion
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        xPos = this.gameObject.transform.position.x;
        yPos = this.gameObject.transform.position.y;
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

    public float GetXpos() {
        return xPos;
    }

    public float GetYpos()
    {
        return yPos;
    }
    #endregion

    #region ChangeRenderer
    public void ChangeSprite()
    {
        if (tileinfo == null)
            return;
        spriteRenderer.sprite = tileinfo.Sprite;
    }

    public void ChangeLayer()
    {
        if (tileinfo == null)
            return;
        spriteRenderer.sortingOrder = tileinfo.Layer;
    }
    public void ChangeTile(MapTileInfo info)
    {
        if (info == null) return;

        tileinfo = info;
        ChangeSprite();
        ChangeLayer();
    }
    #endregion
}
