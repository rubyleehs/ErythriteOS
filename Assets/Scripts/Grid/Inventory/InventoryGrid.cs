using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : HexGrid
{
    public static InventoryTile[][] grid;

    public override void CreateGrid()
    {
        base.CreateGrid();
        grid = new InventoryTile[gridTransforms.Length][];

        for (int y = 0, i = 0; y < grid.Length; y++)
        {
            grid[y] = new InventoryTile[gridTransforms[y].Length];

            for (int x = 0; x < grid[y].Length; x++, i++)
            {
                int elementID = -1;
                if (i < InventoryOS.elements.Length && InventoryOS.elements[i] != null) elementID = InventoryOS.elements[i].id;
                grid[y][x] = new InventoryTile(gridTransforms[y][x], new Vector2Int(x, y), elementID);
            }
        }
    }
}
