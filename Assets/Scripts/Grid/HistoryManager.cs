using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridChange//have to be a struct to store by value;
{
    public Vector2Int gridPos;
    public int originalElementID;
    public int changedElementID;
}

public class HistoryManager : MonoBehaviour //Managers history of the board & element used
{
    private static List<InventoryTile> inventoryHistory;
    private static List<List<GridChange>> gridHistory;
    private static List<GridChange> gridPresent;

    public static int currentPointOfTime = -1;

    public static UnityEngine.UI.Button bUndo;
    public UnityEngine.UI.Button I_bUndo;
    public static UnityEngine.UI.Button bRedo;
    public UnityEngine.UI.Button I_bRedo;
    public static UnityEngine.UI.Button bClear;
    public UnityEngine.UI.Button I_bClear;
    public static UnityEngine.UI.Button bSubmit;
    public UnityEngine.UI.Button I_bSubmit;

    private void Awake()
    {
        gridHistory = new List<List<GridChange>>();
        gridPresent = new List<GridChange>();

        inventoryHistory = new List<InventoryTile>();
        bUndo = I_bUndo;
        bRedo = I_bRedo;
        bSubmit = I_bSubmit;
        bClear = I_bClear;
        bUndo.interactable = false;
        bRedo.interactable = false;
        bSubmit.interactable = false;
        bClear.interactable = false;
    }

    public static void AddToPresent(Vector2Int gridPos, int oriElementID, int changedElementID)
    {
        gridPresent.Add(new GridChange()
        {
            gridPos = gridPos,
            originalElementID = oriElementID,
            changedElementID = changedElementID,
        });
    }

    public static void AddPresentToHistory(InventoryTile inventoryElementUsed)
    {
        for (int i = currentPointOfTime + 1; i < gridHistory.Count;)
        {
            gridHistory.RemoveAt(i);
            inventoryHistory.RemoveAt(i);
        }
        inventoryHistory.Add(inventoryElementUsed);
        gridHistory.Add(new List<GridChange>(gridPresent));
        gridPresent.Clear();
        currentPointOfTime = gridHistory.Count - 1;

        bUndo.interactable = true;
        bRedo.interactable = false;
        bSubmit.interactable = PuzzleManager.CheckIfCurrentCanBeSubmitted();
        bClear.interactable = true;
    }

    public void Undo(bool lightsUp)
    {
        if (currentPointOfTime < 0) return;
        Debug.Log("Undo | " + currentPointOfTime);
        BoardOS.bvus.ForceComplete(lightsUp);

        if(inventoryHistory[currentPointOfTime] != null) inventoryHistory[currentPointOfTime].UpdateAvailability(true);
        for (int i = gridHistory[currentPointOfTime].Count -1; i >= 0; i--)
        {
            GridChange change = gridHistory[currentPointOfTime][i];
            BoardOS.ForceChange(change.gridPos,change.originalElementID, lightsUp, false);
        }
        currentPointOfTime--;
        gridPresent.Clear();

        bRedo.interactable = true;
        bUndo.interactable = (currentPointOfTime >= 0);
    }

    public void Redo()
    {
        if (currentPointOfTime >= gridHistory.Count - 1) return;
        Debug.Log("Redo | " + (currentPointOfTime + 1));
        BoardOS.bvus.ForceComplete(true);

        if (inventoryHistory[currentPointOfTime + 1] != null) inventoryHistory[currentPointOfTime + 1].UpdateAvailability(false);
        for (int i = 0; i < gridHistory[currentPointOfTime + 1].Count; i++)
        {
            GridChange change = gridHistory[currentPointOfTime + 1][i];
            BoardOS.ForceChange(change.gridPos, change.changedElementID, true, false);//
        }
        currentPointOfTime++;
        gridPresent.Clear();

        bRedo.interactable = (currentPointOfTime < gridHistory.Count - 1);
        bUndo.interactable = true;
    }

    public void Clear()
    {
        BoardOS.bvus.ForceComplete(true);
        while (currentPointOfTime >= 0)
        {
            if (inventoryHistory[currentPointOfTime] != null) inventoryHistory[currentPointOfTime].UpdateAvailability(true);
            for (int i = gridHistory[currentPointOfTime].Count - 1; i >= 0; i--)
            {
                GridChange change = gridHistory[currentPointOfTime][i];
                BoardOS.ForceChange(change.gridPos, -1, BoardOS.GetTileID(change.gridPos) != -1, false);
            }
            currentPointOfTime--;
        }
        gridHistory.Clear();
        inventoryHistory.Clear();
        bRedo.interactable = false;
        bUndo.interactable = false;
        bSubmit.interactable = false;
        bClear.interactable = false;
    }
}
