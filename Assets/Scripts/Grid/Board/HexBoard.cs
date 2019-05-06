using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hexinal { E = 0,NE = 1,NW = 2,W = 3,SW =  4,SE= 5};

public class HexBoard : HexGrid
{
    public static HexBoardTile[][] grid;

    public override void CreateGrid()
    {
        base.CreateGrid();

        grid = new HexBoardTile[gridTransforms.Length][];

        for (int y = 0; y < grid.Length; y++)
        {
            grid[y] = new HexBoardTile[gridTransforms[y].Length];

            for (int x = 0; x < grid[y].Length; x++)
            {
                HexBoardTile hexTile = new HexBoardTile(gridTransforms[y][x], new Vector2Int(x, y));
                if(x > 0)
                {
                    hexTile.adjTiles[(int)Hexinal.W] = grid[y][x - 1];
                    grid[y][x - 1].adjTiles[(int)Hexinal.E] = hexTile;
                }
                if(y > 0)
                {
                    if (y > gridRadius - 1)
                    {
                        hexTile.adjTiles[(int)Hexinal.SE] = grid[y - 1][x + 1];
                        grid[y - 1][x + 1].adjTiles[(int)Hexinal.NW] = hexTile;

                        hexTile.adjTiles[(int)Hexinal.SW] = grid[y - 1][x];
                        grid[y - 1][x].adjTiles[(int)Hexinal.NE] = hexTile;

                    }
                    else
                    {
                        if (x < grid[y].Length - 1)
                        {
                            hexTile.adjTiles[(int)Hexinal.SE] = grid[y - 1][x];
                            grid[y - 1][x].adjTiles[(int)Hexinal.NW] = hexTile;
                        }
                        if(x > 0)
                        {
                            hexTile.adjTiles[(int)Hexinal.SW] = grid[y - 1][x - 1];
                            grid[y - 1][x - 1].adjTiles[(int)Hexinal.NE] = hexTile;
                        }
                    }
                }
                grid[y][x] = hexTile;
            }
        }
    }   

    public HexBoardTile WorldPosToGrid(Vector2 worldPos)
    {
        Vector2Int? gridPos = WorldPosToGridPos(worldPos);
        if (gridPos == null) return null;
        else return grid[gridPos.Value.y][gridPos.Value.x];
    }
}
