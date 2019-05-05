using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/RotateGE")]
public class RotateGE : GridElement
{
    public override void Run(HexTile tile)
    {
        List<HexTile> visualUpdateTiles = new List<HexTile>();
        int[] ids = new int[6];
        for (int i = 0; i < tile.adjTiles.Length; i++)
        {
            if(tile.adjTiles[i] != null)
            {
                HexTile ht = tile.adjTiles[(i + 1) % tile.adjTiles.Length];
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
        TileVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
    }
}
