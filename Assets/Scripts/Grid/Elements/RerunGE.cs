using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/RerunGE")]
public class RerunGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        HexBoardTile tile2run = tile.adjTiles[(int)Hexinal.W];
        if (tile2run != null)
        {
            BoardVisualUpdateSequencer.AddToQueue(tile2run);
            if (tile2run.ReadElementID() >= 0) GridElementManager.elements[tile2run.ReadElementID()].Run(tile2run);
        }
    }
}