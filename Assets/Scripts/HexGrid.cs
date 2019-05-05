using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hexinal { E = 0,NE = 1,NW = 2,W = 3,SW =  4,SE= 5};

public class HexGrid : MonoBehaviour
{
    [HideInInspector]
    public new Transform transform;
    public Transform background;

    public static HexTile[][] grid;
    public GameObject tileGO;

    private int gridRadius;
    private Vector2 tileDelta;
    private Vector2 centerPos;

    public void Initialize(int gridRadius, float tileXDelta, Vector2 centerPos)
    {
        transform = GetComponent<Transform>();
        this.gridRadius = gridRadius;
        this.tileDelta = new Vector2(tileXDelta, tileXDelta * Mathf.Cos(30 * Mathf.Deg2Rad));
        this.centerPos = centerPos;
    }

    public void CreateGrid()
    {
        background.position = centerPos;
        background.localScale = Vector2.one * (gridRadius * 2 - 0.5f);
        grid = new HexTile[gridRadius * 2 - 1][];

        for (int y = 0; y < gridRadius * 2 - 1; y++)
        {
            int numOfx = (int)Mathf.PingPong(y, gridRadius - 1) + gridRadius; 
            grid[y] = new HexTile[numOfx];
            for (int x = 0; x < numOfx; x++)
            {
                Vector2 worldPos = new Vector2(x * tileDelta.x - (numOfx - 1)  * tileDelta.x * 0.5f + centerPos.x, (y - gridRadius + 1) * tileDelta.y + centerPos.y);
                HexTile hexTile = new HexTile(worldPos, new Vector2Int(x, y), Instantiate(tileGO, worldPos, Quaternion.identity, this.transform).transform);
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
            }
        }
    }

    
    public HexTile ScreenPosToGrid(Vector2 screenPos)
    {
        return WorldPosToGrid(MainCamera.camera.ScreenToWorldPoint(screenPos));
    }

    public HexTile WorldPosToGrid(Vector2 worldPos)
    {
        int y = Mathf.RoundToInt((worldPos.y - centerPos.y) / tileDelta.y) + gridRadius - 1;
        int x = Mathf.RoundToInt(((worldPos.x - centerPos.x) + 0.5f * tileDelta.x * (int)(Mathf.PingPong(y, gridRadius - 1) + gridRadius - 1))/tileDelta.x);
        if (y < 0 || x < 0 || y >= grid.Length || x >= grid[y].Length) return null;
        else return grid[y][x];
    }
    
}
