using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridChange//have to be a struct to store by value;
{
    public Vector2Int gridPos;
    public int originalElementID;
    public int changedElementID;
}

public class GridHistory : MonoBehaviour
{
    private static List<List<GridChange>> history;
    private static List<GridChange> present;

    private static int currentPointOfTime = 0;

    private void Awake()
    {
        history = new List<List<GridChange>>();
        present = new List<GridChange>();
    }

    public static void AddToPresent(Vector2Int gridPos, int oriElementID, int changedElementID)
    {
        present.Add(new GridChange()
        {
            gridPos = gridPos,
            originalElementID = oriElementID,
            changedElementID = changedElementID,
        });
    }

    public static void AddPresentToHistory()
    {
        for (int i = currentPointOfTime + 1; i < history.Count;)
        {
            history.RemoveAt(i);
        }
        history.Add(new List<GridChange>(present));
        present.Clear();
        currentPointOfTime = history.Count - 1;
    }

    public static void Undo()
    {
        if (currentPointOfTime < 0) return;
        GameManager.tvus.ForceComplete();

        for (int i = 0; i < history[currentPointOfTime].Count; i++)
        {
            GridChange change = history[currentPointOfTime][i];
            HexTile tile = HexGrid.grid[change.gridPos.y][change.gridPos.x];

            tile.UpdateElement(change.originalElementID);
            tile.UpdateVisuals();
        }
        currentPointOfTime--;
        present.Clear();
    }

    public static void Redo()
    {
        if (currentPointOfTime >= history.Count - 1) return;
        GameManager.tvus.ForceComplete();

        for (int i = 0; i < history[currentPointOfTime + 1].Count; i++)
        {
            GridChange change = history[currentPointOfTime + 1][i];
            HexTile tile = HexGrid.grid[change.gridPos.y][change.gridPos.x];

            tile.UpdateElement(change.changedElementID);
            tile.UpdateVisuals();
        }
        currentPointOfTime++;
        present.Clear();
    }
}
