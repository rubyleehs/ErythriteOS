using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/LightningGE")]
public class LightningGE : GridElement
{
    public override void Run(HexTile tile)
    {
        HexTile runTile = tile;
        bool nextIsRight = false;
        bool hasHitSomething = false;

        while (!hasHitSomething)
        {

            if (nextIsRight) runTile = runTile.adjTiles[(int)Hexinal.SE];
            else runTile = runTile.adjTiles[(int)Hexinal.SW];

            if (runTile == null) hasHitSomething = true;
            else
            {
                hasHitSomething = (runTile.ReadElementID() != -1);
                runTile.UpdateElement(id);
            }

            nextIsRight = !nextIsRight;

            TileVisualUpdateSequencer.AddToQueue(runTile);
        }
    }
}
