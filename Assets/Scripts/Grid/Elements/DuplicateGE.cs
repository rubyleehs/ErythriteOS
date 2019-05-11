using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/DuplicateGE")]
public class DuplicateGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        for (int i = 0; i < tile.adjTiles.Length; i++)
        {
            if (tile.adjTiles[i] != null)
            {
                visualUpdateTiles.Add(tile.adjTiles[i]);

                if (tile.adjTiles[i].ReadElementID() == -1 && tile.adjTiles[(i + 3) % 6] != null)
                {
                    tile.adjTiles[i].UpdateElement(tile.adjTiles[(i + 3) % 6].ReadElementID());
                }
            }
        }
        BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
    }
}
