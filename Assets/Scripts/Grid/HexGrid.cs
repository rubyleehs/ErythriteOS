using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject tileGO;

    [HideInInspector]
    public new Transform transform;
    public Transform background;

    protected int gridRadius;
    protected Vector2 tileDelta;
    protected Vector2 centerPos;

    public HexTile[][] grid;

    public virtual void Initialize(int gridRadius, float tileXDelta, Vector2 centerPos)
    {
        transform = GetComponent<Transform>();
        this.gridRadius = gridRadius;
        this.tileDelta = new Vector2(tileXDelta, tileXDelta * Mathf.Cos(30 * Mathf.Deg2Rad));
        this.centerPos = centerPos;
    }

    public static Vector2[][] CalGridWorldPosPositions(int radius, Vector2 tileDelta, Vector2 centerPos)
    {
        Vector2[][] output = new Vector2[radius * 2 - 1][];
        for (int y = 0; y < radius * 2 - 1; y++)
        {
            int numOfx = (int)Mathf.PingPong(y, radius - 1) + radius;
            output[y] = new Vector2[numOfx];
            for (int x = 0; x < numOfx; x++)
            {
                output[y][x] = new Vector2(x * tileDelta.x - (numOfx - 1) * tileDelta.x * 0.5f + centerPos.x, (y - radius + 1) * tileDelta.y + centerPos.y);
            }
        }
        return output;
    }

    public virtual Vector2Int? ScreenPosToGridPos(Vector2 screenPos)
    {
        return WorldPosToGridPos(MainCamera.camera.ScreenToWorldPoint(screenPos));
    }

    public virtual Vector2Int? WorldPosToGridPos(Vector2 worldPos)
    {
        int y = Mathf.RoundToInt((worldPos.y - centerPos.y) / tileDelta.y) + gridRadius - 1;
        int x = Mathf.RoundToInt(((worldPos.x - centerPos.x) + 0.5f * tileDelta.x * (int)(Mathf.PingPong(y, gridRadius - 1) + gridRadius - 1))/tileDelta.x);
        if (y < 0 || x < 0 || y >= gridRadius * 2 - 1 || x >= Mathf.PingPong(y, gridRadius - 1) + gridRadius) return null;
        else return new Vector2Int(x,y);
    }
}
