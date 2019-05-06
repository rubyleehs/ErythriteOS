using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/WebGE")]
public class WebGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        HexBoardTile[] runTiles = new HexBoardTile[6];
        tile.adjTiles.CopyTo(runTiles,0);
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
            for (int i = 0; i < 6; i++)
            {
                if (runTiles[i] == null) continue;
                else
                {
                    if (runTiles[i].ReadElementID() == -1)
                    {
                        runTiles[i].UpdateElement(id);
                        visualUpdateTiles.Add(runTiles[i]);
                        isDone = false;
                        runTiles[i] = runTiles[i].adjTiles[i];
                    }
                    else runTiles[i] = null;
                }
            }
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
        }
    }
}
