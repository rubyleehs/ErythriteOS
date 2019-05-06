using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/SpiralGE")]
public class SpiralGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        HexBoardTile runTile = tile;
        for (int i = 0; i < 6; i++)
        {
            while (runTile.adjTiles[i] != null && runTile.adjTiles[i].ReadElementID() == -1)
            {
                runTile = runTile.adjTiles[i];
                runTile.UpdateElement(id);
                BoardVisualUpdateSequencer.AddToQueue(runTile);
            }
        }
    }
}
