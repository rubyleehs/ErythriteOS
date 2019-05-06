using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AnimationInfo
{
    public float staggerAnimDuration;

    public float tileLightUpDuration;

    public Color tileLightUpColor;
    public Color occupiedTileColor;

}

public class GameManager : MonoBehaviour
{
    public static AnimationInfo animationInfo;
    public AnimationInfo I_animationInfo;

    public static BoardVisualUpdateSequencer bvus;
    public BoardVisualUpdateSequencer I_bvus;

    public static ShotgunSurgery shotgunSurgery;
    public ShotgunSurgery I_shotgunSurgery;

    private void Awake()
    {
        animationInfo = I_animationInfo;
        bvus = I_bvus;
        shotgunSurgery = I_shotgunSurgery;
    }

    private void Update()
    {
        HexBoardTile ht = GridOS.hexBoard.WorldPosToGrid(MainCamera.GetMouseWorld2DPoint()) as HexBoardTile;
        if (ht != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (ht.adjTiles[i] != null) Debug.DrawLine(ht.worldPos, ht.adjTiles[i].worldPos, Color.red, 1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ht.UpdateElement(0);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ht.UpdateElement(1);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ht.UpdateElement(2);
                GridOS.Run(ht);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ht.UpdateElement(3);
                GridOS.Run(ht);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log(ht.ReadElementID());
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) HistoryManager.Undo();
        else if (Input.GetKeyDown(KeyCode.Y)) HistoryManager.Redo();
    }

}
