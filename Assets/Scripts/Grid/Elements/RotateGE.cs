using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/RotateGE")]
public class RotateGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        int[] ids = new int[6];
        for (int i = 0; i < tile.adjTiles.Length; i++)
        {
            if(tile.adjTiles[i] != null)
            {
                HexBoardTile ht = tile.adjTiles[(i + 1) % tile.adjTiles.Length];
                if (ht != null) ids[i] = ht.ReadElementID();
                else ids[i] = -1;
            }
        }
        for (int i = 0; i < tile.adjTiles.Length; i++)
        {
            if (tile.adjTiles[i] != null)
            {
                tile.adjTiles[i].UpdateElement(ids[i]);
                visualUpdateTiles.Add(tile.adjTiles[i]);
            }
        }
        BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
    }
}
