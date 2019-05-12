using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/SnailGE")]
public class SnailGE : GridElement
{
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
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        HexBoardTile eastTile = tile.adjTiles[((int)Hexinal.E)];
        if (eastTile == null)
        {
            tile.UpdateElement(-1);
            visualUpdateTiles.Add(tile);
        }
        else if (eastTile.ReadElementID() == -1)
        {
            tile.UpdateElement(-1);
            eastTile.UpdateElement(id);
            visualUpdateTiles.Add(tile);
            visualUpdateTiles.Add(eastTile);
        }
        BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
    }

}
