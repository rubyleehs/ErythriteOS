using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexBoardTile: HexTile
{
    public HexBoardTile(Transform transform, Vector2Int gridPos)
    {
        this.transform = transform;

        this.gridPos = gridPos;
        this.adjTiles = new HexBoardTile[6];

        this.elementID = -1;

        this.spriteRenderer = transform.GetComponent<SpriteRenderer>();
        this.elementSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        this.originalColor = spriteRenderer.color;
    }

    public HexBoardTile[] adjTiles;
    private Color originalColor;

    public void UpdateElement(int id)
    {
        if (elementID != id)
        {
            HistoryManager.AddToPresent(gridPos, elementID, id);
            elementID = id;
        }
    }

    public int ReadElementID()
    {
        return elementID;
    }

    public void UpdateVisuals()
    {
        if (elementID >= 0)
        {
            elementSpriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
            elementSpriteRenderer.material = GridElementManager.elements[elementID].material;
        }
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
