using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOS : MonoBehaviour
{
    [Header("Refrences")]
    public static InventoryGrid inventoryGrid;
    public InventoryGrid I_inventoryGrid;

    public static GridElement[] elements;
    public GridElement[] I_elements;

    [Header("Board Gen")]
    public int boardRadius;
    public float tileXDelta;
    public Vector2 centerPos;

    private void Awake()
    {
        elements = I_elements;

        inventoryGrid = I_inventoryGrid;
        inventoryGrid.Initialize(boardRadius, tileXDelta, centerPos);
        inventoryGrid.CreateGrid();
    }
}
