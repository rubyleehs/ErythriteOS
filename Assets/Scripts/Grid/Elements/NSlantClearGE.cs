using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/NSlantClearGE")]
public class NSlantClearGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        HexBoardTile[] runTiles = new HexBoardTile[2] { tile.adjTiles[(int)Hexinal.NW], tile.adjTiles[(int)Hexinal.SE] };
        bool isDone = false;
        while (!isDone)
        {
            isDone = true;
            visualUpdateTiles.Clear();
            if (runTiles[0] != null)
            {
                isDone = false;
                runTiles[0].UpdateElement(-1);
                visualUpdateTiles.Add(runTiles[0]);
                runTiles[0] = runTiles[0].adjTiles[(int)Hexinal.NW];
            }
            if (runTiles[1] != null)
            {
                isDone = false;
                runTiles[1].UpdateElement(-1);
                visualUpdateTiles.Add(runTiles[1]);
                runTiles[1] = runTiles[1].adjTiles[(int)Hexinal.SE];
            }
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
        }
    }
}
