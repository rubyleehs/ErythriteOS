using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/FloodGE")]
public class FloodGE : GridElement
{
    public override void Run(HexBoardTile tile)
    {
        List<HexBoardTile> visualUpdateTiles = new List<HexBoardTile>();
        List<HexBoardTile> checkList = new List<HexBoardTile>(tile.adjTiles);
        List<HexBoardTile> checkListQueue = new List<HexBoardTile>();

        while(checkList.Count > 0 || checkListQueue.Count > 0)
        {
            visualUpdateTiles.Clear();
            while (checkList.Count > 0)
            {
                if (checkList[0] != null && checkList[0].ReadElementID() == -1)
                {
                    checkList[0].UpdateElement(id);
                    visualUpdateTiles.Add(checkList[0]);
                    checkListQueue.AddRange(checkList[0].adjTiles);
                }
                checkList.RemoveAt(0);
            }
            checkList.AddRange(new List<HexBoardTile>(checkListQueue));
            checkListQueue.Clear();
            BoardVisualUpdateSequencer.AddToQueue(visualUpdateTiles);
        }
    }
}