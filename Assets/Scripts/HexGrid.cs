using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hexinal { E = 0,NE = 1,NW = 2,W = 3,SW =  4,SE= 5};

public class HexTile
{
    public HexTile(Vector2 worldPos, Vector2Int gridPos)
    {
        this.worldPos = worldPos;
        this.gridPos = gridPos;
        adjTiles = new HexTile[6];
    }

    public Vector2 worldPos;
    public Vector2Int gridPos;
    public HexTile[] adjTiles;
}

public class HexGrid : MonoBehaviour
{
    public static HexTile[][] grid;
    public GameObject tempGO;

    private void Start()
    {
        CreateGrid(6, 2, Vector2.zero);

        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if(grid[y][x].adjTiles[i]!= null) Debug.DrawLine(grid[y][x].worldPos, grid[y][x].adjTiles[i].worldPos,Color.red, 9999999999);
                }
            }
        }
    }

    public void CreateGrid(int gridRadius, float tileRadius, Vector2 centerPos)
    {
        grid = new HexTile[gridRadius * 2 - 1][];

        for (int y = 0; y < gridRadius * 2 - 1; y++)
        {
            int numOfx = (int)Mathf.PingPong(y, gridRadius - 1) + gridRadius; 
            grid[y] = new HexTile[numOfx];
            for (int x = 0; x < numOfx; x++)
            {
                HexTile hexTile = new HexTile(new Vector2(x * tileRadius - numOfx * tileRadius * 0.5f + centerPos.x, y * tileRadius + centerPos.y), new Vector2Int(x, y));
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
                        if (x < numOfx - 1)
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
                Instantiate(tempGO, grid[y][x].worldPos, Quaternion.identity, null);
            }
        }
    }
}
