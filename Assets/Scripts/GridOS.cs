using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOS : MonoBehaviour
{
    [Header("Rerences")]
    public static HexGrid hexGrid;
    public HexGrid I_hexGrid;

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

        hexGrid = I_hexGrid;
        hexGrid.Initialize(gridRadius, tileXDelta, centerPos);
        hexGrid.CreateGrid();
    }

    public static void Run(HexTile tile)
    {
        if (tile.ReadElementID() >= 0)
        {
            Debug.Log(tile.ReadElementID());
            GameManager.tvus.ForceComplete();
            tile.UpdateVisuals();
            elements[tile.ReadElementID()].Run(tile);
            GridHistory.AddPresentToHistory();
        }
    }
}
