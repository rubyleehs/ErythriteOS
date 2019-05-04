using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GridElement/SpiralGE")]
public class SpiralGE : GridElement
{
    public override IEnumerator Run(HexTile tile)
    {
        HexTile runTile = tile;
        for (int i = 0; i < 6; i++)
        {
            while (runTile.adjTiles[i] != null && runTile.adjTiles[i].ReadID() == -1)
            {
                float dt = 0;
                while(dt < GameManager.boardUpdateAnimDur && GameManager.allowBoardUpdateAnim)
                {
                    dt += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                runTile = runTile.adjTiles[i];
                runTile.UpdateElement(id);
            }
        }
    }
}
