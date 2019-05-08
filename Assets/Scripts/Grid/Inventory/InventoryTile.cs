using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTile : HexTile
{
    public InventoryTile(Transform transform, Vector2Int gridPos, int elementID)
    {
        this.transform = transform;
        this.gridPos = gridPos;
        this.elementID = elementID;
        this.spriteRenderer = transform.GetComponent<SpriteRenderer>();
        this.elementSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if(elementID >= 0 && elementID < GridElementManager.elements.Length) elementSpriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
        UpdateAvailability(true);
    }

    public bool isLocked = false;
    public bool isAvailable;

    public int ReadID()
    {
        return elementID;
    }

    public bool CheckAvailability()
    {
        return (!isLocked && isAvailable);     
    }

    public bool UpdateAvailability(bool setAvailable)
    {
        if (isLocked)
        {
            elementSpriteRenderer.material = InventoryOS.grayScaleMat;
            return false;
        }
        else if (this.isAvailable == setAvailable) return false;
        else if (elementID >= 0 && elementID < GridElementManager.elements.Length)
        {
            this.isAvailable = setAvailable;
            if (isAvailable) elementSpriteRenderer.material = GridElementManager.elements[elementID].material;
            else elementSpriteRenderer.material = InventoryOS.usedMat;

            return true;
        }
        else return false;
    }
}
