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
        this.elementSpriteRenderer = tile.GetChild(0).GetComponent<SpriteRenderer>();
        this.spriteRenderer = tile.GetComponent<SpriteRenderer>();
        this.originalColor = spriteRenderer.color;

    }

    public Transform tile;

    public Vector2 worldPos;
    public Vector2Int gridPos;
    public HexTile[] adjTiles;

    private int elementID;

    private SpriteRenderer elementSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public void UpdateElement(int id)
    {
        if (elementID != id)
        {
            GridHistory.AddToPresent(gridPos, elementID, id);
            elementID = id;
        }
    }

    public int ReadElementID()
    {
        return elementID;
    }

    public void UpdateVisuals()
    {
        if (elementID >= 0) elementSpriteRenderer.sprite = GridOS.elements[elementID].sprite;
        else elementSpriteRenderer.sprite = null;

        GameManager.shotgunSurgery.ForceStartCoroutine(LightUp(GameManager.animationInfo.tileLightUpColor));

    }
   
    public IEnumerator LightUp(Color color)
    {
        Color endColor;
        if (elementID == -1) endColor = originalColor;
        else endColor = GameManager.animationInfo.occupiedTileColor;

        spriteRenderer.color = color;

        float dt = 0;
        while(dt < GameManager.animationInfo.tileLightUpDuration)
        {
            dt += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(color, endColor, dt / GameManager.animationInfo.tileLightUpDuration);
            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.color = endColor;
    }
}
