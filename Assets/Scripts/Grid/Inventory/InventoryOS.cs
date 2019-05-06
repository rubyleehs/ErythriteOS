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

    public static Material usedMat;
    public Material I_usedMat;

    public static Material grayScaleMat;
    public Material I_grayScaleMat;

    [Header("Board Gen")]
    public int boardRadius;
    public float tileXDelta;
    public Vector2 centerPos;

    private void Awake()
    {
        elements = I_elements;
        inventoryGrid = I_inventoryGrid;
        usedMat = I_usedMat;
        grayScaleMat = I_grayScaleMat;

        inventoryGrid.Initialize(boardRadius, tileXDelta, centerPos);
        inventoryGrid.CreateGrid();
    }
}
