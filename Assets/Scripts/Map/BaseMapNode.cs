using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMapNode : MonoBehaviour
{
    [SerializeField]
    MapTileInfo tileinfo;
    private int xPos;
    private int yPos;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public int GetXpos() {
        return xPos;
    }

    public int GetYpos()
    {
        return yPos;
    }
    #endregion

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
}
