using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/ReflectGE")]
public class ReflectGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        HexBoardTile pivot = tile;
        while (true)
        {
            if (pivot.adjTiles[(int)Hexinal.SW] != null) pivot = pivot.adjTiles[(int)Hexinal.SW];
            else break;
        }

        HexBoardTile[] axialRunTiles = new HexBoardTile[2] { pivot.adjTiles[(int)Hexinal.NW], pivot.adjTiles[(int)Hexinal.E] };
        HexBoardTile[] runTiles = new HexBoardTile[2];
        int[] runTilesOriID = new int[2];

        while (true)
        { 
            visualUpdateTiles.Clear();
            axialRunTiles.CopyTo(runTiles, 0);

            //Copy NE to E row and vise-versa
            bool rowDone = false;
            while (!rowDone)
            {
                rowDone = true;
                for (int i = 0; i < 2; i++)
                {
                    if (runTiles[i] != null)
                    {
                        rowDone = false;
                        runTilesOriID[i] = runTiles[i].ReadElementID();
                    }
                    else runTilesOriID[i] = -1;
                }

                for (int i = 0; i < 2; i++)
                {
                    if (runTiles[i] != null)
                    {
                        rowDone = false;

                        runTiles[i].UpdateElement(runTilesOriID[1 - i]);
                        visualUpdateTiles.Add(runTiles[i]);
                        runTiles[i] = runTiles[i].adjTiles[(int)Hexinal.NE];
                    }
                }
            }

            //Set everything reflected from outside board to -1
            axialRunTiles.CopyTo(runTiles, 0);

            for (int i = 0; i < 2; i++)
            {
                if (runTiles[i] == null) continue;
                while (true)
                {
                    runTiles[i] = runTiles[i].adjTiles[(int)Hexinal.SW];
                    if (runTiles[i] != null)
                    {
                        runTiles[i].UpdateElement(-1);
                        visualUpdateTiles.Add(runTiles[i]);
                    }
                    else break;
                }
            }         
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);

            if (axialRunTiles[0] != null) axialRunTiles[0] = axialRunTiles[0].adjTiles[(int)Hexinal.NW];
            if (axialRunTiles[1] != null) axialRunTiles[1] = axialRunTiles[1].adjTiles[(int)Hexinal.E];
            if (axialRunTiles[0] == null && axialRunTiles[1] == null) break;
        }
    }
}
