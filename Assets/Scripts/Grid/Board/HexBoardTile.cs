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
    private IEnumerator fade;

    public bool UpdateElement(int id)
    {
        if (elementID != id)
        {
            HistoryManager.AddToPresent(gridPos, elementID, id);
            elementID = id;
            return true;
        }
        return false;
    }

    public int ReadElementID()
    {
        return elementID;
    }

    public void UpdateVisuals()
    {
        if (elementID >= 0)
        {
            GameManager.shotgunSurgery.ForceStopCoroutine(fade);
            Color c = elementSpriteRenderer.color;
            c.a = 1;
            elementSpriteRenderer.color = c;
            elementSpriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
            elementSpriteRenderer.material = GridElementManager.elements[elementID].material;
        }
        else
        {
            fade = FadeElement();
            GameManager.shotgunSurgery.ForceStartCoroutine(fade);
        }

        GameManager.shotgunSurgery.ForceStartCoroutine(LightUp(GameManager.animationInfo.tileLightUpColor));

    }
   
    public IEnumerator LightUp(Color color)
    {
        Color endColor;
        if (elementID == -1) endColor = originalColor;
        else endColor = GameManager.animationInfo.occupiedTileColor;

        spriteRenderer.color = color;

        float dt = 0;
        float smoothProgress = 0;
        while(dt < GameManager.animationInfo.tileLightUpDuration)
        {
            dt += Time.deltaTime;
            smoothProgress = Mathf.SmoothStep(0, 1, dt / GameManager.animationInfo.tileLightUpDuration);
            spriteRenderer.color = Color.Lerp(color, endColor, smoothProgress);
            yield return new WaitForEndOfFrame();
        }

        spriteRenderer.color = endColor;
    }

    private IEnumerator FadeElement()
    {
        Color originalColor = elementSpriteRenderer.color;
        Color endColor = originalColor;
        endColor.a = 0;

        float dt = 0;
        float smoothProgress = 0;
        while (dt < GameManager.animationInfo.tileLightUpDuration)
        {
            dt += Time.deltaTime;
            smoothProgress = Mathf.SmoothStep(0, 1, dt / GameManager.animationInfo.tileElementFadeDuration);
            elementSpriteRenderer.color = Color.Lerp(originalColor, endColor, smoothProgress);
            yield return new WaitForEndOfFrame();
        }

        elementSpriteRenderer.sprite = null;
        elementSpriteRenderer.color = originalColor;

    }
}
