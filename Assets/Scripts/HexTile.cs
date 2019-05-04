using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile
{
    public HexTile(Vector2 worldPos, Vector2Int gridPos, Transform tile)
    {
        this.tile = tile;

        this.worldPos = worldPos;
        this.gridPos = gridPos;
        this.adjTiles = new HexTile[6];

        this.elementID = -1;
        this.spriteRenderer = tile.GetChild(0).GetComponent<SpriteRenderer>();
    }
    public Transform tile;

    public Vector2 worldPos;
    public Vector2Int gridPos;
    public HexTile[] adjTiles;

    public int elementID;
    public SpriteRenderer spriteRenderer;

    public void UpdateElement(int id)
    {
        elementID = id;

        if (elementID >= 0)spriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
        else spriteRenderer.sprite = null;
    }
}
