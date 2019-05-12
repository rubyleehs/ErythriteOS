using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteractions : MonoBehaviour
{
    public Transform mouseElementTransform;

    private Vector2 elementDeltaPos;
    private SpriteRenderer mouseElementSpriteRenderer;

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

        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            HexBoardTile ht = BoardOS.hexBoard.WorldPosToGrid(MainCamera.mousePos);
            Debug.Log("id: " + ht.ReadElementID() + "vid:" + ht.visualElementId.Count);
        }
        */
    }
    
    private void PickInventoryElement()
    {
        if(InventoryOS.RequestUse(MainCamera.mousePos))
        {
            BoardOS.bvus.ForceComplete();
            elementDeltaPos = (Vector2)InventoryOS.request.transform.position - MainCamera.mousePos;
            mouseElementSpriteRenderer.sprite = GridElementManager.elements[InventoryOS.request.ReadID()].sprite;
        }
        else mouseElementSpriteRenderer.sprite = null;
    }

    private void DropInventoryElement()
    {
        mouseElementSpriteRenderer.sprite = null;
        if (InventoryOS.request == null) return;

        if (BoardOS.TryUpdateElement(mouseElementTransform.position, InventoryOS.request.ReadID(), true)) HistoryManager.AddPresentToHistory(InventoryOS.ConfirmRequest());
        else InventoryOS.CancelRequest();
    }
}


