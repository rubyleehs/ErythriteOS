using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTile : HexTile
{
    public InventoryTile(Transform tile, Vector2 worldPos, Vector2Int gridPos, int elementID)
    {
        this.tile = tile;
        this.worldPos = worldPos;
        this.gridPos = gridPos;
        this.elementID = elementID;
        this.spriteRenderer = tile.GetComponent<SpriteRenderer>();
        this.elementSpriteRenderer = tile.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
