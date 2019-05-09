using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/PullGE")]
public class PullGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        HexBoardTile[] runTiles = new HexBoardTile[6];
        List<List<HexBoardTile>> visualUpdateGroup = new List<List<HexBoardTile>>();
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        tile.adjTiles.CopyTo(runTiles, 0);
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            visualUpdateTiles.Clear();
            for (int i = 0; i < 6; i++)
            {
                if (runTiles[i] == null) continue;
                visualUpdateTiles.Add(runTiles[i]);
                if(runTiles[i].adjTiles[i] != null)
                {
                    runTiles[i].UpdateElement(runTiles[i].adjTiles[i].ReadElementID());
                    runTiles[i] = runTiles[i].adjTiles[i];
                    isDone = false;
                }
                else
                {
                    runTiles[i].UpdateElement(-1);
                    runTiles[i] = null;
                }
            }
            visualUpdateGroup.Add(new List<HexBoardTile>(visualUpdateTiles));
        }
        visualUpdateGroup.Reverse();
        BoardVisualUpdateSequencer.AddToQueue(visualUpdateGroup);
    }
}
