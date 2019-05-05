using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/WebGE")]
public class WebGE : GridElement
{
    public override void Run(HexTile tile)
    {
        HexTile[] runTiles = tile.adjTiles;
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            List<HexTile> visualUpdateTiles = new List<HexTile>();
            for (int i = 0; i < 6; i++)
            {
                if (runTiles[i] == null) continue;
                else
                {
                    visualUpdateTiles.Add(runTiles[i]);
                    if (runTiles[i].ReadElementID() == -1)
                    {
                        runTiles[i].UpdateElement(id);
                        isDone = false;
                        runTiles[i] = runTiles[i].adjTiles[i];
                    }
                    else runTiles[i] = null;
                }
            }
            TileVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
        }
    }
}
