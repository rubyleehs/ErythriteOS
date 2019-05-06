﻿using System.Collections;
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
    private static List<List<GridChange>> gridHistory;
    private static List<GridChange> gridPresent;

    private static List<int> elementUsed;

    private static int currentPointOfTime = 0;

    private void Awake()
    {
        gridHistory = new List<List<GridChange>>();
        gridPresent = new List<GridChange>();

        elementUsed = new List<int>();
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

    public static void AddPresentToHistory(int elementUsedID)
    {
        for (int i = currentPointOfTime + 1; i < gridHistory.Count;)
        {
            gridHistory.RemoveAt(i);
            elementUsed.RemoveAt(i);
        }
        elementUsed.Add(elementUsedID);
        gridHistory.Add(new List<GridChange>(gridPresent));
        gridPresent.Clear();
        currentPointOfTime = gridHistory.Count - 1;
    }

    public static void Undo()
    {
        if (currentPointOfTime < 0) return;
        BoardOS.bvus.ForceComplete();

        for (int i = 0; i < gridHistory[currentPointOfTime].Count; i++)
        {
            GridChange change = gridHistory[currentPointOfTime][i];
            HexBoardTile tile = HexBoard.grid[change.gridPos.y][change.gridPos.x];

            tile.UpdateElement(change.originalElementID);
            tile.UpdateVisuals();
        }
        currentPointOfTime--;
        gridPresent.Clear();
    }

    public static void Redo()
    {
        if (currentPointOfTime >= gridHistory.Count - 1) return;
        BoardOS.bvus.ForceComplete();

        for (int i = 0; i < gridHistory[currentPointOfTime + 1].Count; i++)
        {
            GridChange change = gridHistory[currentPointOfTime + 1][i];
            HexBoardTile tile = HexBoard.grid[change.gridPos.y][change.gridPos.x];

            tile.UpdateElement(change.changedElementID);
            tile.UpdateVisuals();
        }
        currentPointOfTime++;
        gridPresent.Clear();
    }
}