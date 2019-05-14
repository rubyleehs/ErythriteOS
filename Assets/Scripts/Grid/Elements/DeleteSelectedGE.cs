using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/DeleteSelectedGE")]
public class DeleteSelectedGE : GridElement
{
    HexBoardTile[] selectedTile;
    int[] selectedId;

    List<List<HexBoardTile>> allSelected;
    List<HexBoardTile> visualUpdateTiles;

    public override void Run(HexBoardTile tile)
    {
        selectedTile = new HexBoardTile[6];
        selectedId = new int[6] { -1, -1, -1, -1, -1, -1 };

        allSelected = new List<List<HexBoardTile>>();
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        for (int i = 0; i < 6; i++)
        {
            allSelected.Add(new List<HexBoardTile>());
        }

        tile.adjTiles.CopyTo(selectedTile, 0);
        for (int i = 0; i < 6; i++)
        {
            if (selectedTile[i] == null) continue;
            if (selectedTile[i].ReadElementID() == id)
            {
                selectedId[i] = id;
                break;
            }
            else selectedId[i] = selectedTile[i].ReadElementID();
        }
        //======
        HexBoardTile pivot = tile;
        while (pivot.adjTiles[(int)Hexinal.W] != null) pivot = pivot.adjTiles[(int)Hexinal.W];

        HexBoardTile[] axialRunTiles = new HexBoardTile[2] { pivot.adjTiles[(int)Hexinal.NE], pivot};
        HexBoardTile[] runTiles = new HexBoardTile[4];

        while (true)
        {
            axialRunTiles.CopyTo(runTiles, 0);
            axialRunTiles.CopyTo(runTiles, 2);

            bool rowDone = false;
            while (!rowDone)
            {
                rowDone = true;
                for (int i = 0; i < 4; i++)
                {
                    if (runTiles[i] != null)
                    {
                        rowDone = false;
                        TryAddToAllSelected(runTiles[i]);
                        if(i < 2) runTiles[i] = runTiles[i].adjTiles[(int)Hexinal.E];
                        else runTiles[i] = runTiles[i].adjTiles[(int)Hexinal.W];
                    }
                }
            }


            if (axialRunTiles[0] != null) axialRunTiles[0] = axialRunTiles[0].adjTiles[(int)Hexinal.NE];
            if (axialRunTiles[1] != null) axialRunTiles[1] = axialRunTiles[1].adjTiles[(int)Hexinal.SE];
            if (axialRunTiles[0] == null && axialRunTiles[1] == null) break;
        }

        //======
        for (int i = 0; i < 6; i++)
        {
            if (selectedTile[i] != null) BoardVisualUpdateSequencer.AddToQueue(selectedTile[i]);
            if (selectedId[i] != -1 && selectedTile[i].ReadElementID() != -1)
            {
                for (int a = 0; a < allSelected[i].Count; a++)
                {
                    allSelected[i][a].UpdateElement(-1);
                }
                BoardVisualUpdateSequencer.AddToQueue(allSelected[i]);
            }
        }
    }

    private void TryAddToAllSelected(HexBoardTile tile)
    {
        for (int e = 0; e < 6; e++)
        {
            if (selectedId[e] == -1) continue;

            if (tile.ReadElementID() == selectedId[e]) allSelected[e].Add(tile);
        }
    }
    
}

