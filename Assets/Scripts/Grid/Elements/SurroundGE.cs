using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/SurroundGE")]
public class SurroundGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> runtiles = new List<HexBoardTile>();
        List<HexBoardTile> runtilesQueue = new List<HexBoardTile>();
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();

        runtiles.Add(tile);
        while (runtiles.Count > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                if (runtiles[0].adjTiles[i] != null && runtiles[0].adjTiles[i].ReadElementID() == -1)
                {
                    for (int a = 0; a < 6; a++)
                    {
                        if (runtiles[0].adjTiles[i].adjTiles[a] != null && runtiles[0].adjTiles[i].adjTiles[a].ReadElementID() >= 0 && runtiles[0].adjTiles[i].adjTiles[a].ReadElementID() != id)
                        {
                            runtiles[0].adjTiles[i].UpdateElement(id);
                            visualUpdateTiles.Add(runtiles[0].adjTiles[i]);
                            runtilesQueue.Add(runtiles[0].adjTiles[i]);
                            break;
                        }
                    }
                }
            }
            runtiles.RemoveAt(0);
            if(runtiles.Count == 0)
            {
                BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
                visualUpdateTiles.Clear();
                runtiles.AddRange(runtilesQueue);
                runtilesQueue.Clear();
            }
        }
    }
}
