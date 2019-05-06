using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardOS : MonoBehaviour //Used for interactions within the board.
{
    [Header("Refrences")]
    public static HexBoard hexBoard;
    public HexBoard I_hexBoard;

    public static BoardVisualUpdateSequencer bvus;
    public BoardVisualUpdateSequencer I_bvus;

    public static GridElement[] elements;
    public GridElement[] I_elements;


    [Header("Board Gen")]
    public int boardRadius;
    public float tileXDelta;
    public Vector2 centerPos;

    private void Awake()
    {
        elements = I_elements;
        for (int i = 0; i < elements.Length; i++) elements[i].id = i;

        hexBoard = I_hexBoard;
        bvus = I_bvus;
        hexBoard.Initialize(boardRadius, tileXDelta, centerPos);
        hexBoard.CreateGrid();
    }

    public static void Run(HexBoardTile tile)
    {
        if (tile.ReadElementID() >= 0)
        {
            int elementUsed = tile.ReadElementID();
            bvus.ForceComplete();
            tile.UpdateVisuals();
            elements[elementUsed].Run(tile);
            HistoryManager.AddPresentToHistory(elementUsed);
        }
    }
}
