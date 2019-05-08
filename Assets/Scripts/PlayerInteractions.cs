using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Transform mouseElementTransform;

    private int elementID;
    private Vector2 elementDeltaPos;
    private SpriteRenderer mouseElementSpriteRenderer;

    private InventoryTile it;

    private void Awake()
    {
        mouseElementSpriteRenderer = mouseElementTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) HistoryManager.Undo();
        else if (Input.GetKeyDown(KeyCode.Y)) HistoryManager.Redo();

        if (Input.GetKeyDown(KeyCode.Mouse0)) PickInventoryElement();
        else if (Input.GetKeyUp(KeyCode.Mouse0)) DropInventoryElement();

        mouseElementTransform.position = MainCamera.mousePos + elementDeltaPos;
    }
    
    private void PickInventoryElement()
    {
        it = InventoryOS.inventoryGrid.WorldPosToGrid(MainCamera.mousePos);
        if (it != null && it.ReadID() >= 0)
        {
            if (it.CheckAvailability())
            {
                BoardOS.bvus.ForceComplete();
                elementID = it.ReadID();
                it.UpdateAvailability(false);

                elementDeltaPos = (Vector2)it.transform.position - MainCamera.mousePos;
                mouseElementSpriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
            }
            else
            {
                mouseElementSpriteRenderer.sprite = null;
                return;
            }
        }
    }

    private void DropInventoryElement()
    {
        if (it == null) return;
        mouseElementSpriteRenderer.sprite = null;

        if (BoardOS.TryUpdateElement(mouseElementTransform.position, it.ReadID(), true)) return;
        else it.UpdateAvailability(true);

        /*
        HexBoardTile ht = BoardOS.hexBoard.WorldPosToGrid(mouseElementTransform.position) as HexBoardTile;
        mouseElementSpriteRenderer.sprite = null;
        if (ht == null)
        {
            it.UpdateAvailability(true);
            return;
        }
        else
        {
            ht.UpdateElement(it.ReadID());
            BoardOS.Run(ht);
        }
        */
    }
}


