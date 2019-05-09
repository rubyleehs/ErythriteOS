using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/BombGE")]
public class BombGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        for (int i = 0; i < tile.adjTiles.Length; i++)
        {
            if (tile.adjTiles[i] != null)
            {
                tile.adjTiles[i].UpdateElement(-1);
                visualUpdateTiles.Add(tile.adjTiles[i]);
            }
        }
        BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
    }
}