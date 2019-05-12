using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardOS : MonoBehaviour //Used for interactions within the board.
{
    [Header("Refrences")]
    private static HexBoard hexBoard;
    public HexBoard I_hexBoard;

    public static BoardVisualUpdateSequencer bvus;
    public BoardVisualUpdateSequencer I_bvus;

    [Header("Board Gen")]
    public int boardRadius;
    public float tileXDelta;
    public Vector2 centerPos;

    public static HashSet<HexBoardTile> endTurnEffectsTiles;
    public static HashSet<HexBoardTile> endTurnEffectsTilesQueue;

    private void Awake()
    {
        hexBoard = I_hexBoard;
        bvus = I_bvus;
        endTurnEffectsTiles = new HashSet<HexBoardTile>();
        endTurnEffectsTilesQueue = new HashSet<HexBoardTile>();
        hexBoard.Initialize(boardRadius, tileXDelta, centerPos);
        hexBoard.CreateGrid();
    }

    public static bool TryUpdateElement(Vector2 worldPos, int id, bool runUpdated)
    {
        HexBoardTile ht = hexBoard.WorldPosToGrid(worldPos) as HexBoardTile;
        return TryUpdateElement(ht, id, runUpdated);
    }
    public static bool TryUpdateElement(HexBoardTile ht, int id, bool runUpdated)
    {
        if (ht == null || ht.ReadElementID() != -1) return false;
        else
        {
            ht.UpdateElement(id);
            if (runUpdated) Run(ht);
            return true;
        }
    }

    public static void ForceChange(Vector2Int index, int id, bool runUpdated)
    {
        HexBoardTile ht = hexBoard.grid[index.y][index.x];
        ht.UpdateElement(id);
        ht.UpdateVisuals();
        if (runUpdated) Run(ht);
    }

    public static void Run(HexBoardTile tile)
    {
        if (tile == null) return;
        if (tile.ReadElementID() >= 0)
        {
            int elementUsed = tile.ReadElementID();
            bvus.ForceComplete();
            tile.UpdateVisuals();
            GridElementManager.elements[elementUsed].Run(tile);
        }
        EndTurn();
    }

    public static void EndTurn()
    {
        for (int i = 0; i < endTurnEffectsTiles.Count; i++)
        {
            HexBoardTile ht = endTurnEffectsTiles.ElementAt(i);
            if (ht.ReadElementID() != -1) GridElementManager.elements[ht.ReadElementID()].OnTurnEnd(ht);
            else
            {
                Debug.Log("Invalid End Turn Effect!");
            }
        }

        for (int i = 0; i < endTurnEffectsTilesQueue.Count; i++)
        {
            endTurnEffectsTiles.Add(endTurnEffectsTilesQueue.ElementAt(i));
        }
        endTurnEffectsTilesQueue.Clear();
    }
}
