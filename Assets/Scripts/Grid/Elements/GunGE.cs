using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/GunGE")]
public class GunGE : GridElement
{
    public GridElement laserGE;
    public override void Run(HexBoardTile tile)
    {
        if (BoardOS.endTurnEffectsTiles.Contains(tile))
        {
            BoardOS.endTurnEffectsTilesQueue.Add(tile);
            BoardOS.endTurnEffectsTiles.Remove(tile);
        }
    }

    public override void BattleCry(HexBoardTile tile)
    {
        BoardOS.endTurnEffectsTiles.Add(tile);
    }

    public override void Deathrattle(HexBoardTile tile)
    {
        BoardOS.endTurnEffectsTiles.Remove(tile);
    }

    public override void OnTurnEnd(HexBoardTile tile)
    {
        HexBoardTile runTile = tile.adjTiles[((int)Hexinal.E)];

        int failsafe = 0;
        while (failsafe < 50)
        {
            failsafe++;
            if (runTile == null) return;
            else if (runTile.ReadElementID() == -1)
            {
                BoardOS.AddToEndTurnUpdateQueue(runTile,laserGE.id);
                return;
            }
            else if (runTile.ReadElementID() == laserGE.id) runTile = runTile.adjTiles[((int)Hexinal.E)];
            else return;
        }
        Debug.Log("failsafe triggered");
    }
}
