using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridChange
{
    public Vector2Int gridPos;
    public int originalElementID;
    public int changedElementID;
}

public class GridHistory : MonoBehaviour
{
    public static List<List<GridChange>> history;
    private static List<GridChange> present;

    private void Awake()
    {
        history = new List<List<GridChange>>();
        present = new List<GridChange>();
    }

    public static void AddToPresent(Vector2Int gridPos, int oriElementID, int changedElementID)
    {
        present.Add(new GridChange()
        {
            gridPos = gridPos,
            originalElementID = oriElementID,
            changedElementID = changedElementID,
        });
    }

}
