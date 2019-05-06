using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class HexTile 
{
    public Transform tile;

    public Vector2 worldPos;
    public Vector2Int gridPos;
    protected int elementID;

    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer elementSpriteRenderer;
}
