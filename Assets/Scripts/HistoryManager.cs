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

    private static int currentPointOfTime = 0;

    private void Awake()
    {
        gridHistory = new List<List<GridChange>>();
        gridPresent = new List<GridChange>();

        inventoryHistory = new List<InventoryTile>();
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
    }

    public static void Undo()
    {
        if (currentPointOfTime < 0) return;
        Debug.Log("Undo");
        BoardOS.bvus.ForceComplete();

        inventoryHistory[currentPointOfTime].UpdateAvailability(true);
        for (int i = 0; i < gridHistory[currentPointOfTime].Count; i++)
        {
            GridChange change = gridHistory[currentPointOfTime][i];
            BoardOS.ForceChange(change.gridPos,change.originalElementID, false);
        }
        currentPointOfTime--;
        gridPresent.Clear();
    }

    public static void Redo()
    {
        if (currentPointOfTime >= gridHistory.Count - 1) return;
        Debug.Log("Redo");
        BoardOS.bvus.ForceComplete();

        inventoryHistory[currentPointOfTime + 1].UpdateAvailability(false);
        for (int i = 0; i < gridHistory[currentPointOfTime + 1].Count; i++)
        {
            GridChange change = gridHistory[currentPointOfTime + 1][i];
            BoardOS.ForceChange(change.gridPos, change.changedElementID, false);
        }
        currentPointOfTime++;
        gridPresent.Clear();
    }
}
