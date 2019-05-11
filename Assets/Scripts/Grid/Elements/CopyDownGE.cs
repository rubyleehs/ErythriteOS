using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/CopyDownGE")]
public class CopyDownGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        HexBoardTile pivot = tile;
        while (true)
        {
            if (pivot.adjTiles[(int)Hexinal.W] != null) pivot = pivot.adjTiles[(int)Hexinal.W];
            else break;
        }

        HexBoardTile[] axialRunTiles = new HexBoardTile[2] { pivot.adjTiles[(int)Hexinal.NE], pivot.adjTiles[(int)Hexinal.SE] };
        HexBoardTile[] runTiles = new HexBoardTile[2];
        int idToCopy = -1;

        while (true)
        {
            visualUpdateTiles.Clear();
            axialRunTiles.CopyTo(runTiles, 0);

            bool rowDone = false;
            while (!rowDone)
            {
                rowDone = true;
                if (runTiles[0] != null)
                {
                    rowDone = false;
                    idToCopy = runTiles[0].ReadElementID();
                    visualUpdateTiles.Add(runTiles[0]);
                    runTiles[0] = runTiles[0].adjTiles[(int)Hexinal.E];
                }
                else idToCopy = -1;

                if (runTiles[1] != null)
                {
                    rowDone = false;
                    runTiles[1].UpdateElement(idToCopy);
                    visualUpdateTiles.Add(runTiles[1]);
                    runTiles[1] = runTiles[1].adjTiles[(int)Hexinal.E];
                }
            }

            //Copy stuff outside board aka -1
            axialRunTiles.CopyTo(runTiles, 0);

            for (int i = 0; i < 2; i++)
            {
                if (runTiles[i] == null) continue;
                while (true)
                {
                    runTiles[i] = runTiles[i].adjTiles[(int)Hexinal.W];
                    if (runTiles[i] != null)
                    {
                        if(i ==1) runTiles[i].UpdateElement(-1);
                        visualUpdateTiles.Add(runTiles[i]);
                    }
                    else break;
                }
            }
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);

            if (axialRunTiles[0] != null) axialRunTiles[0] = axialRunTiles[0].adjTiles[(int)Hexinal.NE];
            if (axialRunTiles[1] != null) axialRunTiles[1] = axialRunTiles[1].adjTiles[(int)Hexinal.SE];
            if (axialRunTiles[0] == null && axialRunTiles[1] == null) break;
        }
    }
}
