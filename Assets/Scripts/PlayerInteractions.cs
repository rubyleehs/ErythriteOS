using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Transform mouseElementTransform;

    private int elementID;
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
    }
    
    private void PickInventoryElement()
    {
        InventoryTile it = InventoryOS.RequestUse(MainCamera.mousePos);
        if(it != null)
        {
            BoardOS.bvus.ForceComplete();
            elementID = it.ReadID();
            elementDeltaPos = (Vector2)it.transform.position - MainCamera.mousePos;
            mouseElementSpriteRenderer.sprite = GridElementManager.elements[elementID].sprite;
        }
        else mouseElementSpriteRenderer.sprite = null;
    }

    private void DropInventoryElement()
    {
        mouseElementSpriteRenderer.sprite = null;

        if (BoardOS.TryUpdateElement(mouseElementTransform.position, elementID, true)) HistoryManager.AddPresentToHistory(InventoryOS.ConfirmRequest());
        else InventoryOS.CancelRequest();
    }
}


