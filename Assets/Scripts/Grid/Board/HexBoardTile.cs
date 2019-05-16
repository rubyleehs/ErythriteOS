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
        visualElementId = new List<int>();
    }

    public HexBoardTile[] adjTiles;
    private Color originalColor;
    private IEnumerator fade;
    public List<int> visualElementId;

    public bool UpdateElement(int id)
    {
        if (id < -1) id = -1;
        visualElementId.Add(id);
        if (elementID != id)
        {
            if (id == -1) GridElementManager.elements[elementID].Deathrattle(this);
            else GridElementManager.elements[id].BattleCry(this);

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
        GameManager.shotgunSurgery.ForceStartCoroutine(LightUp(GameManager.animationInfo.tileLightUpColor));

        if (visualElementId.Count <= 0) return;
        if (visualElementId[0] >= 0)
        {
            GameManager.shotgunSurgery.ForceStopCoroutine(fade);
            Color c = elementSpriteRenderer.color;
            c.a = 1;
            elementSpriteRenderer.color = c;
            elementSpriteRenderer.sprite = GridElementManager.elements[visualElementId[0]].sprite;
            elementSpriteRenderer.material = GridElementManager.elements[visualElementId[0]].material;
        }
        else
        {
            fade = FadeElement();
            GameManager.shotgunSurgery.ForceStartCoroutine(fade);
        }
        visualElementId.RemoveAt(0);
    }
   
    public IEnumerator LightUp(Color color)
    {
        Color endColor;
        if(visualElementId.Count <= 0)
        {
            if (elementID == -1) endColor = originalColor;
            else endColor = GameManager.animationInfo.occupiedTileColor;
        }
        else if (visualElementId[0] == -1) endColor = originalColor;
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
