using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOS : MonoBehaviour
{
    [Header("Refrences")]
    public static HexBoard hexBoard;
    public HexBoard I_hexBoard;

    public static GridElement[] elements;
    public GridElement[] I_elements;

    [Header("Grid Gen")]
    public int gridRadius;
    public float tileXDelta;
    public Vector2 centerPos;

    private void Awake()
    {
        elements = I_elements;
        for (int i = 0; i < elements.Length; i++) elements[i].id = i;

        hexBoard = I_hexBoard;
        hexBoard.Initialize(gridRadius, tileXDelta, centerPos);
        hexBoard.CreateGrid();
    }

    public static void Run(HexBoardTile tile)
    {
        if (tile.ReadElementID() >= 0)
        {
            int elementUsed = tile.ReadElementID();
            GameManager.bvus.ForceComplete();
            tile.UpdateVisuals();
            elements[elementUsed].Run(tile);
            HistoryManager.AddPresentToHistory(elementUsed);
        }
    }
}
