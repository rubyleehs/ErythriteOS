using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HexTile 
{
    public Transform transform;

    public Vector2Int gridPos;
    protected int elementID;

    protected SpriteRenderer spriteRenderer;
    protected SpriteRenderer elementSpriteRenderer;
}
