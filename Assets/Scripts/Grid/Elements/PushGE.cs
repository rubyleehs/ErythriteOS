using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/PushGE")]
public class PushGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        HexBoardTile[] runTiles = new HexBoardTile[6];
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        tile.adjTiles.CopyTo(runTiles, 0);

        int[] prevID = new int[6] { -1, -1, -1, -1, -1, -1 };
        int temp = -1;
        bool isDone = false;
        while(!isDone)
        {
            isDone = true;
            visualUpdateTiles.Clear();
            for (int i = 0; i < 6; i++)
            {
                if (runTiles[i] != null)
                {
                    isDone = false;
                    temp = runTiles[i].ReadElementID();
                    runTiles[i].UpdateElement(prevID[i]);
                    visualUpdateTiles.Add(runTiles[i]);
                    prevID[i] = temp;
                    runTiles[i] = runTiles[i].adjTiles[i];
                }
            }
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
        } 
    }
}
